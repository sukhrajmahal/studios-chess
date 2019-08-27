Requirements for Version 1
==========================

This documents the features that should be implemented in the first version of the project. They are by no means formal requirements and are more of a list of ideas, so that nothing gets forgotten. The 'requirements' also work as a limiting factor too keep work focused.

REQ-1: Chess Modes
------------------

The game should provide the ability to play the following chess modes:

* Standard
* Four Player (Custom Variation)
* Chess 960
* Chess 4.5

These should be implemented according to the rules outlined in the rules doc.

REQ-2: Save and Load
--------------------

The game should allow user to save and load any game mode. Whether the chess save is visible to the user outside of the game does not matter (i.e. saves are in the programs files or the user chooses where to save the files).

REQ-3: Scenario Maker
---------------------

The user should be able make (then resume/start) a custom scenario from the front screen. This would include, at a minimum, allowing the user to choose which pieces are active, the position of the pieces and whose turn it is.

The user should also be able to go into scenario mode at any time during a game to flesh out options.

REQ-4: Timer Options
--------------------

At the start of the game, the user should be allowed to specify if they would like a timer on the time a player can make a move.
The following options should be allowed (in minutes only)

1, 2, 5, 10 and custom. 

Custom must always be in minutes and must have an 100min upper bound.

Users should have the option to allow either of the following options if the timer expires

* Random move
* Lose match
* Opponent can pick one legal move (i.e., it doesn’t put original user in check etc).

REQ-5: List of Previous Moves
-----------------------------

The game shall maintain a list of moves. The user should be allowed to view the moves and revert to a previous move, should it be allowed at the start of the game.

REQ-6: Graphics Options
------------------------

The user should be allowed to play in either 2d or 3d. There should be the option to choose which 3d one they want. Further options may be added in the future. The user should be able to switch between 3d and 2d in game, without losing progress.

REQ-7: Tile Labels
------------------

The user should have the option to turn on or off title labels. Both at the start of the game and within the game.

Tile labels should be consistent with standard chess conventions.

REQ-8: Points
-------------

The user should be able to see how many points that they have gained and which pieces they have killed.

REQ-9: Rules
------------

The user should be able to view the rules of each type of game that has been supported without exiting the game.
