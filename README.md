# Simple FPS game in Unity 
This is a simple FPS game I developed during a university course using Unity engine and C#.


## Start of the game
- The game starts with a starting menu which waits for the user to give a value N (greater than 1 for the game to make sense). When the user gives a value and presses the Enter button on the main keyboard and not the numpad (Input.GetKeyDown("return")) then a terain of  N*N cubes is created.

- Player begins with 100 points and 4 lives

## Basic Functions
The player can gather cubes and cylinders and start building in order to climb in to higher levels. If player falls from level N or spend all his points he looses 1 life.


## Scoring System
- Collecting a cube or a cylinder costs 5 points.
- Dropping at from level 3 and above results to loosing 10 points for each level.
- Climbing to a cube or a cylinder adds 10 points. 

## Goal
When the player reaches level N instantly gets 100 points and one life. In addition a red cube appears at level 2*N with the same properties as the magenta cube and player has  a time limiy depending on N to reach the red cube cube and climb on it. If the player succeeds he wins the game otherwise it is game over.

## Cube Colors
The color of a cube indicates the amount of cubes that can be colected from that cube

- **Green:** 3 cubes
- **Red:** 2 cubes
- **Yellow:** 1 cubes
- **Blue:** none
- **Cyan:** 1 cylinder

## Controls
- \<p\> 
  - if the target cube is cyan color then you get a cylinder and the cube gets deleted
  - otherwise you get a cube and change color of target cube to indicate the remaining stock. 
- \<b\> a cube is created in front of the player
- \<q\> if there is a cube in front of the player then it gets deleted.
- \<x\> all the cubes that exist on a stack in front of the player get deleted.
- \<r\> if there are any cubes hovering above the ground they are dropped down.
- \<k\> kick any cubes that are infront of the player.
