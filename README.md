# Launcher

**Version:** `net8.0`

## Setup

To get started, create a `config.json` file in the `bin/debug/net8.0/` directory. In this file, you can list all applications with their paths, and specify whether each one should ask for permission before launching.

![Example Config](https://github.com/user-attachments/assets/d04faf46-e981-4a04-beb2-28f7b3e4d7dd)

Download a sample config file here: [config.json](https://github.com/user-attachments/files/17676569/config.json)

## Profile Types

The launcher supports two types of profiles:

- **allLaunch**: Launches all applications listed in the config file. If `askpermission` is enabled for an application, the launcher will prompt you before starting it.
  
- **singleLaunch**: Displays a list of all applications/games specified in the profile. You can then select an application by entering its assigned number.
  - *Note:* Steam games can only be launched through the `singleLaunch` profile.

## Config Options

Each entry in the configuration file can include the following keys:

- **name**: Required for both profiles; specifies the application's name.
- **path**: Required for both profiles; specifies the path to the application.
- **askpermission**: Optional; used only in `allLaunch`. Determines if the user should be prompted before launching the application.
- **issteamgame**: Optional; used only in `singleLaunch`. Indicates that the application is a Steam game.
- **id**: Optional; used only in `singleLaunch`. Assigns an ID number to the application for selection purposes.

## Contributing

Want to edit the code? Create your own branch, and submit a pull request to the `development` branch when you're finished. Thank you!
