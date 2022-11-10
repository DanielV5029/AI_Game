
/**********************\
VIDEO IS ON YOUTUBE
https://youtu.be/K17zsjNZ0Tw
/**********************\

Assets>Project>Level1

Game AI 

Knight’s Crusade 


This game is a Top down 2D game of a knight that the player controls. 

The objective of this game is for the player to find 3 keys that is scattered across the map that is randomly generated each time the game is reset and protected by enemies of different type of AI implementation. 

Once all 3 keys are found, the door is registered as usable, and the player must navigate through the enemies once again back to their spawn to progress onto the next level. 

 

Enemy 1: 

Patrols 

 

These enemies use waypoint system to travel across the map. They do not react to the player’s presence however if you come in close contact with them they will take a considerable amount of health off you. 

There are two battalions of these patrol units each consisting of 4 soldiers. They are only seen patrolling on the paths of the map and can be avoided by running onto the grass. 

 

Enemy 2: 

Followers 

 

These enemies use line in-sight chase to follow the player. 

They can be found scattered throughout the map, they have a range of 10 and will follow the player until the player runs away or until they have killed him. If one of these enemies touches you, you will lose health. 

They can be defeated using bullets that the player character is equipped with. These bullets can only shoot once per second to make the game a little more challenging and fun and takes health off the enemy over time. 

Enemy 3: 

Aware Enemies 

 

These enemies use finite states. I made them use 3 main ones, Patrol, Chase and Evade. 

Here is a diagram of how it works. 

 

They’re found patrolling around the keys that help you progress onto the next level. 

As you come into the range of 5 they will change their state to chase and will follow the player until the player runs away or is dead. 

If you come into contact with one your health will be reduced. 

If a power up is consumed while they chase you, they will change their state to evade and runaway for 5 seconds before going back to chasing you or back to their pattern movement depending on your distance from them. 
