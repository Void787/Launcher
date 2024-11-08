# Launcher
version: net8.0

create a config.json file in bin/debug/net8.0/
there you can list all aplications and their paths and if it should be asked permission to launch before doing so.

example:
[config.json](https://github.com/user-attachments/files/17676519/config.json){
  "exclude": [
    "**/bin",
    "**/bower_components",
    "**/jspm_packages",
    "**/node_modules",
    "**/obj",
    "**/platforms"
  ],
  "profiles": [
    {
      "name": "put the name of the profile here.",
      "type": "the type of profile it should do."
    }
  ],
  "applications": [
    {
      "name": "put the name of the application here. (this doesn't matter for the application)",
      "path": "put the full path to the .exe here.",
      "askpermission": "put a n or y here too indicate if it should ask for permission before launching." 
    }
  ],
  "games": [
    {
      "name": "put the name of the game here.",
      "id": "put the id of the steam game here.",
      "issteamgame": "put here n or y to indicate if it's a steam game or not."
    },
    {
      "name": "put the name of the game here.",
      "path": "if it's not a steam game put the path to the exe file here.",
      "issteamgame": "put here n or y to indicate if it's a steam game or not."
    }
  ]
}



types of profiles: allLaunch: launches all applications in the list and asks permission if thats stated with askpermission, 
singleLaunch: shows a list of all applications/games that are listed in the profile. then you choose the number assighend to that applications/games.
! remember that steam games can only be launched via the singleLaunch profile.
