March 1, 2024

    I have a template of the project that I wanted to work on already so Im using that and expanded more for the final game requiremnets
so I had already done the game objects, setting, and the movement of the capsule player. The new thing I had added to the project is working on
the security camera object and the way it would move when serveying the level. I also did reworked the players movement because
it was a bit clunky and onky used mouse clicks for movement so I changed it so that you can move with the keys instead, giving the player
a bit more control. Next I want to work on getting a type of cone shaoe to display the player where the camera is surveying so they can avoid
being spotted.

March 11, 2024

    Today I have worked on creating a type of indicator of where the camera is looking out and is able to detect the player. At first when the camera was
looking around it would also spot other object that werent the player like a pillar wall. To get around this I made a layer for the player, and set it to the
player capsule so that it would ignore the default layer objects, and would only spot the player object. One problem Im still having is getting the
raycast to work accuratley. For now the cone sight, or ray of light from the caamera only detects the player if the player is within the bottom face of the
cone meaning if the players head is touching the middle part of the cone the camera wont detect the collsion. Another problem I have right now is that the
cone is only displayed within the scene editor and not when the game is actually played. So when the game starts you can see the camera move, but the
user wont be able to look where the camera cone is looking since the cone is invisible in the game mode. The next thing I want to work on is display a timer
on the screen to show how much time is left to collect the orbs on the ground. If the timer runs out the game over screen appears, or if the player
is spotted by a camera that also triggers the game over screen. Then program the finish point once the player has collect all 5 orbs and then display the
win screen with asking the player if they want to retry.

March 26, 2024

Today I worked on getting the menus such as the Main, Game over menu to give the player to choose if they want to play the game again, or quit the game
completely. Unitys canvas sucks so I used 3D objects to act as buttons, but had a bit a trouble because when dealing with 3D objects you need setup a raycast
to be able to click on them. I have officially have everything from a start menu to the gameplay of the game and an end point which when collected it loads up
a Retry menu. The only thing Im missing is still the display or vision for the player of where the camera is looking. Another thing is fixing the movement
because it messes up when the player collides with the walls collider. 

