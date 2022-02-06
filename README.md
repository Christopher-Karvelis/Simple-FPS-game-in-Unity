# Simple FPS game in Unity 
This is a simple FPS game I developed during a university course using Unity engine.


## Start of the game
- The game starts with a starting menu which waits for the user to give a value n (greater than 1 for the game to make sense). When the user gives a value and presses the Enter button on the main keyboard and not the numpad (Input.GetKeyDown("return")) then a terain of  n*n cubes is created.

- Player begins with 100 points and 4 lives

## Basic Functions
The player can gather cubes and cylinders and start building in order to climb in to higher levels. 


## Scoring System
- Collecting a cube or a cylinder costs 5 points.
- Dropping at from level 3 and above results to loosing 10 points for each level.
- Climbing to a cube or a cylinder adds 10 points. 

When the player reaches level N then he gets 100 points and one life. In addition 
a red cube appears at level 2*N with the same properties as the magenta cube, 
the user has time depending on the size he gave to reach the red cube 
cube and climb on it. If the player succeeds he wins the game in every other 
in any other case the player loses the game.

## Cube Colors
- Green: 4 remaining 

## Controls
- \<p\> 
  - if the target cube is cyan color then you get a cylinder and the cube gets deleted
  - otherwise you get a cube and change color of target cube to indicate the remaining stock. 
- \<b\> a cube is created in front of the player
- \<q\> if there is a cube in front of the player then it gets deleted.
- \<x\> all the cubes that exist on a stack in front of the player get deleted.
- \<r\> if there are any cubes hovering above the ground they are dropped down.
- \<k\> kick any cubes that are infront of the player.
