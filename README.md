# Endless Runner Game 

This project shows endless runner game built on foundation of [Create a 3D Endless Runner from Scratch in Unity](https://www.udemy.com/course/endlessrunner/) Udemy course.

I did this course recently. I am still learning about game development and Unity Engine. Lectures were quite good and teacher explained them really well. What I think is not quite good is style of coding that is used. Lots of code is cramed into Start() and Update() methods without refactoring.
I am also aware that game has some bugs in it and all changes are welcomed. Since I'm still learning Unity at this point I am not aware of Unity-specific best practices, I'm not sure if I'm was doing the right thing here when trying to do it I was just keeping myself on track. 

Implementation 

Basic ideas are: 

 - Platforms are spawning using "dummy" moving in front of player. 
 - World is scrolling towards player giving illusion that player is moving.
 - Platforms are spawning from object pool and they are recycled after using.

Project uses:
 
  - Object pooling 
  - Unity animation system(animation blending, triggers, etc.)
  - Particle systems(coin picking effect, magic spell effect, etc.)
  - Colliders(wall explosions, world generation, intersection turning, etc.)
  - Different platforms prefabs and prefab variations 
  - Unity Audiosource for playing music and sound effects
  - Unity UI system(menu system, score text, number of lives etc.)

Some gameplay footage:

![Gameplay](https://github.com/filipmihaljcic/endless-runner-unity/blob/main/images/AstroRunnerGameplay.gif)

Platform prefab showing triggers for world generation: 

![Prefab triggers](https://github.com/filipmihaljcic/endless-runner-unity/blob/main/images/AstroRunnerPlatformsTriggers.jpg)

Wall explosion when hit by magic spell:

![Wall explosion](https://github.com/filipmihaljcic/endless-runner-unity/blob/main/images/AstroRunnerWallDestruction.jpg) 

Coin picking:

![Coin picking](https://github.com/filipmihaljcic/endless-runner-unity/blob/main/images/AstroRunnerCoinPicking.jpg)

Main menu of the game:

![Main menu](https://github.com/filipmihaljcic/endless-runner-unity/blob/main/images/AstroRunnerMenu.jpg)
  
