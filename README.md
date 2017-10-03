# AR-Safari-Game
An augmented reality that places cute running animals all around your room. Gotta photograph 'em all!

This game can be run on any Android device and has been optimized for Google Glass. The game just has to be run and you have a timer than runs. Try to beat your highscore by photographing the most animals you can within that time. The game uses your camera and the animals merge into your actual room decor.

This game was designed for children to play and get acquainted with their physical environment.

## Video Demo
A demo of the running game can be seen [here](videoName.mp4). It may seem laggy as the Google Glass gets hot really fast and I had run the game about 10 times already until I found a robust solution to recording the screen on Glass. The project needs some performance optimization for smoother rendering (although it is really smooth in the first ~10 rounds, and the scope of the project was not performance).

## Run/Dev Instructions
Pull the repo and open the project folder on Unity3D
The game can be run without building on your PC although you will not get the spatial capabilities of the game
You can build it on any platform from Android to IOS but since the game uses some Native Android features, you will need to tweak some stuff to get it to run on other platforms.
(Google Glass, Android Phones and Tablets should work fine - Screen size needs to be altered before building for any of those but app is ready to go for Glass)

## File Hierarchy
* Assets folder contains the C# scripts and all the art assets used to draw the animals
* Project Settings contains the setting of the 3D environment that is dynamically created around you so that the animals are within your scope of vision

Note: The project needs to be open from the highest-level point that encompasses all the files in this repo
