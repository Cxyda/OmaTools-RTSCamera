# OmaTools-FloatingCamera
A strategy like camera rig for Unity3d

> Summary: This is a simple and easy to use camera system for strategy-like games.

## Features:
- Camera is 'floating' above a layerMask and always keeps the same distance (handy for terrains with hills etc.)
- Camera translation can be controlled by the Keyboard
- Camera Rotation can be controled by the Mouse
- Camera bounds can be specified where camera movement is allowed.
- Easy to use and setup

## How To Setup
1) Clone / copy the *O.M.A.Games* folder to your */Assets/Plugins/* folder.
2) Select *O.M.A.Tools > Cameras > Create FloatingCamera* from Unity's tools menu
3) The *FloatingCameraRig* GameObject will be spawned in the Inspector.

## How To Customize
1) The settings file is located at */Assets/Plugins/O.M.A.Games/FloatingCamera/Settings*
2) Enable the features you like
3) Hit the Play! button
4) Tweak the settings of the FloatingCameraSettings ScriptableObject

## Trouble Shooting
- Make sure the *FloatingCameraRig* is **ABOVE** you terrain / object you want to float on. Otherwise the height recognizion will not work

## Known Issues
- The height-recognition currently also captures trees etc. on terrains. This causes jumping of the camera.

I hope you like this small camera system. Have fun with it.

## Donate
- If you feel like my tools are useful to you and / or you want to buy me a cup of coffee this is how you do it :)
[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=VXRUCCUSS8CSQ&source=url)
