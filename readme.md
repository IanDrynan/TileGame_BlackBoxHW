#Tile Jumper

A simple platformer game made in 2 days to practice using a few design principles. State machines for player control and powerup management.

###Reflection

If I were to write this again I would use the template method to eliminate some of the redundancy in the code for the special tiles.
I also used an ugly way to move the character since I couldn't find a nice way of moving precisely 1 tile without messing up the character 
"hopping". In hindsight I should have checked the players position after each move and snapped the model to the center of the tile. Or some
other solution might have been better.

!(TileJumper.PNG)

###Objective:

Navigate the Sphere to the goal tiles at the end of the grid.

###Rules:

The color of the Sphere character must match the color of the tile he lands on. 
Does not include tiles with pictures on them as they are special tiles.

###Special Tiles:

Red Skull = Lose

Green Up Arrow = Jump 2 spaces when moving forward.

Blue Side Arrows = Jump 2 spaces when moving side to side.

White Question Mark = Inverses player controls.

###Controls:

WASD for movement.

'J' = Change color to Black

'K' = Change color to White
