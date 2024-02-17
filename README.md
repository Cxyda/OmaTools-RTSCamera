# OmaTools-RTSCamera
A strategy like camera rig for Unity3d

> Summary: This is a simple and easy to use camera system for strategy-like games.

## Features:
- Camera is 'floating' above a layerMask and always keeps the same distance (handy for terrains with hills etc.)
- Camera translation can be controlled by the Keyboard and Mouse (new and old input system supported)
  - Moving the camera by moving the mouse to the screen edges is supported as well
- Camera Rotation can be controlled by the Keyboard and Mouse (new and old input system supported)
- Camera bounds can be specified where camera movement is allowed.
- Camera can follow a target (e.g. a player or any other gameObject)
- Easy to use and setup
- Lots of settings to customize the camera to your needs

## How to 'install'
*Using Unity's package manager (Unity3D 2021.3+)*
1) Open the package manager in Unity
2) Click the "+" Button at the upper left corner and select "Add package from git URL..."
3) Enter the URL: "https://github.com/Cxyda/OmaTools-RTSCamera.git"

OR

1) Open the **manifest.json** file in your ./packages/ folder in your unity project
2) add 
> **"de.oma.tools.rtscamera": "https://github.com/Cxyda/OmaTools-RTSCamera.git",** 

to your dependencies.
  > NOTE: don't forget to add the comma ',' at the end of the line OR at the line above the line you added
3) Go back to Unity and your Packages should be updated.


## How To Setup
1) In Unity Select *O.M.A.Tools > Cameras > Create RTSCamera* from Unity's tools menu
2) The *RTSCameraRig* GameObject will be spawned in the Scene.


## Known Issues
- No issues known

I hope you like this small camera system. Have fun with it.

## Donate
- If you feel like my tools are useful to you and / or you want to buy me a cup of coffee this is how you do it :)
[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=VXRUCCUSS8CSQ&source=url)
