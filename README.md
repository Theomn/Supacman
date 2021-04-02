# Supacman

The game is feature complete according to the test specifications, excluding 2 bonus objectives.
 
## Level Generation
> Class of interest: LevelController.cs

Levels are generated using txt files using this syntax:
* __X__ (Capitalized) - Wall
* __\*__ - Token
* __e__ - End Objective
* __p__ - Player
* __1__ - Horizontal Vilain
* __2__ - Diagonal Vilain
* __3__ - Random Vilain
* __Anything else__ - Leaves that tile empty

The level file can then be dragged in the "Level Text" field of the "Level" game object inspector. A few example files can be found in Assets/Levels.

Levels can be made rectangular. While it is also possible to have uneven line lenghts, the behaviour of the game in such a case is undefined.

## Agents
> Classes of interest: ControllableAgent.cs, AStarAlgorithm.cs, IControllerStrategy.cs, all strategies in Controller Strategy folder

All moving objects (Player and vilains) are the same class of ControllableAgent. Using the strategy pattern, it is effortless to change the behaviour of one of these agents. As such, it is possible to attach a manual strategy to an AI, and create a new AI controlled strategy to attach to the player. I designed it this way to make it easier to reach bonus objective 3.

All movement is constricted to the level grid, it is not possible to stand in between tiles.

The player can be moved using arrow keys or WASD. Vilains of type 1 and 2 use the A* algorithm to find their way to the player, and Vilain 3 just moves around randomly at each step.

## Metagame

End conditions have been kept simple. When the player loses a life, the level reloads itself. When the player loses all lives or reaches the end block, the scene is reloaded.

## Personnal impressions

I had a lot of fun working on this project, it took me about 12 hours of work. I am overall satisfied with the outcome, with all core elements made clean and scalable, and I did not add much comment since I feel most of the code is self-explanatory.

Some end points are kind of redundant (the two strategies using A*, and the level generation) but they would be easy to come back to and improve on without breaking the rest of the codebase. I would also have liked to make the metagame a bit more exciting.

While I did do some testing, I lacked the time to do extensive testing of edge cases and thus, make the code more robust and foolproof. It works well in standard conditions, though it should be easy to break.
