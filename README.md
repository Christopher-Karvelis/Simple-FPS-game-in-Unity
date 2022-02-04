# Simple FPS game in Unity 
This is a simple FPS game I developed during a university course using Unity engine.


## Start of the game
The game starts with a starting menu which waits for the user to give a value (greater than 1 for the game to make sense). When the user gives a value and presses the Enter button on the main keyboard and not the numpad (Input.GetKeyDown("return"))  

## Basic Functions
If the player drops at least 2 levels he starts to lose points, also
If he climbs to a cube or a cylinder gets 10 points. 

When the player reaches level N then he gets 100 points and one life. In addition 
a red cube appears at level 2*N with the same properties as the magenta cube, 
the user has time depending on the size he gave to reach the red cube 
cube and climb on it. If the player succeeds he wins the game in every other 
in any other case the player loses the game.

Cube Colors

## Controls
- p change color on the cube indicate the remaining stock and update points
- b a cube is created in front of the player
- q if there is a cube in front of the player then it gets destroyed
- x all the cubes that exist on a stack in front of the player get deleted
- r if there are any cubes hovering above the ground they are dropped down.
- k player kicks any cubes that are infront of him
