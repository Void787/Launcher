using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace SchoolEntry
{
    internal class singleLauncher
    {
        public void execute(string profileName) 
        {
            // Define path to the JSON configuration file
            string configPath = "config.json";

            // Read and parse the JSON configuration file
            string configContent = File.ReadAllText(configPath);
            JObject config = JObject.Parse(configContent);

            // Extract the list of applications
            var games = config[profileName];

            // ask what mode to execute
            Console.WriteLine("Choose a game.");

            int counter = 0;

            foreach (var game in games)
            {
                Console.WriteLine(counter + ": " + game["name"].ToString());
                counter++;
            }
            Console.WriteLine();
            string answer = Console.ReadLine();

            int gamenumber = Int32.Parse(answer);

            var choosenGame = games[gamenumber];

            if (choosenGame["issteamgame"].ToString() == "y")
            {
                int gameID = Int32.Parse(choosenGame["id"].ToString());
                OpenSteamGame(gameID);
            }
            else
            {
                string gameExePath = choosenGame["path"].ToString();
                StartLocalGame(gameExePath);
            }
        }

        public void OpenSteamGame(int appId)
        {
            // Controleer of de appId een geldig nummer is (niet negatief of nul)
            if (appId <= 0)
            {
                throw new ArgumentException("Ongeldig appId: appId moet groter zijn dan 0.");
            }

            // Bouw de Steam URI om de game te openen
            string steamUri = $"steam://run/{appId}";

            // Start het proces om de game via Steam te openen
            Process.Start(new ProcessStartInfo(steamUri) { UseShellExecute = true });
        }

        public void StartLocalGame(string gameExePath)
        {
            // Controleer of het opgegeven pad niet leeg of null is
            if (string.IsNullOrEmpty(gameExePath))
            {
                throw new ArgumentException("Het pad naar het game-bestand mag niet leeg zijn.");
            }

            // Controleer of het opgegeven bestand daadwerkelijk bestaat
            if (!System.IO.File.Exists(gameExePath))
            {
                throw new ArgumentException("Het opgegeven uitvoerbare bestand bestaat niet.");
            }

            try
            {
                // Start het proces om de game te openen via het .exe-bestand
                Process.Start(new ProcessStartInfo(gameExePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                // Foutmelding indien de game niet gestart kan worden
                Console.WriteLine($"Er is een fout opgetreden bij het starten van de game: {ex.Message}");
            }
        }
    }
}
