using System;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;  // Add the Newtonsoft.Json library for JSON parsing

namespace SchoolEntry
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define path to the JSON configuration file
            string configPath = "config.json";

            if (!File.Exists(configPath))
            {
                Console.WriteLine($"Error: Configuration file not found at {configPath}");
                return;
            }

            // Read and parse the JSON configuration file
            string configContent = File.ReadAllText(configPath);
            JObject config = JObject.Parse(configContent);

            // get different profiles and select one for execution
            var profiles = config["profiles"];

            Console.WriteLine("Select profile.");

            int counter = 0;

            foreach (var profile in profiles)
            {
                Console.WriteLine(counter + ": " + profile["name"].ToString());
                counter++;
            }
            Console.WriteLine();
            string ?profileAnswer = Console.ReadLine();

            int profilenumber = Int32.Parse(profileAnswer);

            var choosenProfile = profiles[profilenumber];

            string profileSetting = choosenProfile["type"].ToString();
            string profileName = choosenProfile["name"].ToString();

            if (profileSetting == "singleLaunch")
            {
                // execute singleLaunch
                singleLauncher singleLauncher = new singleLauncher();
                singleLauncher.execute(profileName);
                Environment.Exit(0);
            }

            // Extract the list of applications
            var applications = config[profileName];

            // ask what mode to execute
            Console.WriteLine("start or close?");
            string answer = Console.ReadLine();

            // starting apps or closing apps dependant on answer
            if (answer == "s" || answer == "start")
            {
                // Launch each application from the configuration
                Console.WriteLine("Launching applications...");
                foreach (var app in applications)
                {
                    string appName = app["name"].ToString();
                    string appPath = app["path"].ToString();
                    string appArgs = app["arguments"] != null ? app["arguments"].ToString() : "";
                    string askPermission = app["askpermission"].ToString();

                    if (askPermission == "Y")
                    {
                        if (appName == "Opera")
                        {
                            Console.WriteLine("start Music as well? y - n");
                        } else
                        {
                            Console.WriteLine("launch " + appName + " as well?");
                        }
                        
                        string MusicChoice = Console.ReadLine();

                        if (!(MusicChoice == "y" || MusicChoice == "yes"))
                        {
                            continue;
                        }
                    }

                    // Launch the application
                    LaunchApp(appName, appPath, appArgs);
                }

                Console.WriteLine("All applications launched. Press Enter to exit.");
                Console.ReadLine();
            }

            if (answer == "c" || answer == "close")
            {
                // Close each application from the configuration
                Console.WriteLine("Closing applications...");
                foreach (var app in applications)
                {
                    string appPath = app["path"].ToString();
                    string askPermission = app["askpermission"].ToString();

                    // Close the application
                    if (askPermission == "Y")
                    {
                        string appName = app["name"].ToString();
                        Console.WriteLine("Close " + appName + " as well?");
                        string CloseChoice = Console.ReadLine();

                        if (!(CloseChoice == "y" || CloseChoice == "yes"))
                        {
                            continue;
                        }
                    }

                    CloseAppByPath(appPath);
                }

                Console.WriteLine("All applications closed. Press Enter to exit.");
                Console.ReadLine();
            }
        }

        static void LaunchApp(string appName, string appPath, string args = "")
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = appPath;
                startInfo.Arguments = args;
                Process.Start(startInfo);
                Console.WriteLine($"Launched: {appName} ({appPath})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error launching {appName}: {ex.Message}");
            }
        }

        static void CloseApp(string appName)
        {
            try
            {
                // Find all processes with the given name
                Process[] processes = Process.GetProcessesByName(appName);

                if (processes.Length == 0)
                {
                    Console.WriteLine($"No running processes found for: {appName}");
                }
                else
                {
                    foreach (var process in processes)
                    {
                        // Attempt to close the process gracefully
                        process.CloseMainWindow();
                        process.WaitForExit(5000); // Wait up to 5 seconds for the process to exit

                        if (!process.HasExited)
                        {
                            // Forcefully kill the process if it hasn't exited
                            process.Kill();
                        }

                        Console.WriteLine($"Closed: {appName} (PID: {process.Id})");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing {appName}: {ex.Message}");
            }
        }

        static void CloseAppByPath(string appPath)
        {
            try
            {
                // Extract the application name (without the extension) from the full path
                string appName = Path.GetFileNameWithoutExtension(appPath);

                // Call the original CloseApp method using the extracted app name
                CloseApp(appName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing app by path: {ex.Message}");
            }
        }
    }
}
