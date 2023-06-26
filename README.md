# **Orbital 2023 Milestone 2**

## *Note: Please press Q to use the ability! See the details under Special Abilities.  

# Team Details


## Team Name 

Tank You Very Much 


## Team ID

5893


## Team Members

Tan Zhuo En

Ronn Ng Kheng Keat


## Proposed Level of Achievement

Apollo 11


# Project Details


## Code Base

Link to repository:

[https://github.com/willowisp01/TankYouVeryMuch](https://github.com/willowisp01/TankYouVeryMuch)


## Build Download

[Milestone 2 Final Build](https://drive.google.com/drive/folders/1nvKWqYngYaNDj9_ZiBfzoPsUC-Kbnf1s?usp=sharing)


## Project Pitch

Poster:

[Orbital Poster Milestone 2](https://drive.google.com/file/d/1I-O8HEkhNohGS9AhP40atUbyQ0NpzMCz/view)

Video:

[Orbital Video Milestone 2](https://drive.google.com/file/d/1wc6PIzbOJg6Rq5YYGGVicl7PGMQtHrBL/view?usp=sharing)


## Project Log

[Project Log](https://docs.google.com/spreadsheets/d/1quSz3zc2BvAIu-BQ3Xgk_EfCRI6jFS2fdS3GZb3G5JI/edit#gid=0)
## 


## Project Motivation 

Tanks is a classic game in which players control a tank, and aim to destroy other tanks while avoiding damage. It is typically played against CPU tanks. However, in its original form, the novelty of the game wears off quickly. We intend to make the game diverse and engaging through the introduction of various tank abilities, dynamic terrain, and interactive map elements. Additionally, we intend to create a single-player campaign, local multiplayer and online multiplayer. Our reward system includes new tanks, cosmetics, and upgradable abilities.


## Aim 

We aim to create an exciting and engaging gameplay experience with distinctive visuals to generate maximum entertainment for players. 


## Tech Stack

Unity


## User Stories


<table>
  <tr>
   <td><strong>As a…</strong>
   </td>
   <td><strong>I want to…</strong>
   </td>
   <td><strong>So that I can…</strong>
   </td>
  </tr>
  <tr>
   <td>TYVM Player
   </td>
   <td>explore the level and map
   </td>
   <td>appreciate the free Unity assets
   </td>
  </tr>
  <tr>
   <td>TYVM Player
   </td>
   <td>shoot projectiles 
   </td>
   <td>destroy enemies
   </td>
  </tr>
  <tr>
   <td>TYVM Player
   </td>
   <td>destroy the environment
   </td>
   <td>navigate the level better
   </td>
  </tr>
  <tr>
   <td>TYVM Player
   </td>
   <td>have different projectile types
   </td>
   <td>adapt to the enemy type
   </td>
  </tr>
  <tr>
   <td>TYVM Player
   </td>
   <td>have consumables 
   </td>
   <td>increase my power level temporarily
   </td>
  </tr>
  <tr>
   <td>TYVM Player
   </td>
   <td>have different types of enemies
   </td>
   <td>strategize according to the enemy type
   </td>
  </tr>
  <tr>
   <td>TYVM Player
   </td>
   <td>have rewards
   </td>
   <td>have a sense of achievement
   </td>
  </tr>
  <tr>
   <td>TYVM Player
   </td>
   <td>gacha
   </td>
   <td>gamble
   </td>
  </tr>
</table>



# Timeline


<table>
  <tr>
   <td><strong>Milestone</strong>
   </td>
   <td><strong>Features implemented</strong>
   </td>
  </tr>
  <tr>
   <td>Milestone 1
   </td>
   <td>
<ul>

<li>The core of the game, the tanks 
<ul>
 
<li>Health
 
<li>Shooting and projectiles
 
<li>Movement
</li> 
</ul>

<li>Maps 
<ul>
 
<li>Create a basic layout of a map
</li> 
</ul>

<li>Design a basic start screen and menu UI
</li>
</ul>
   </td>
  </tr>
  <tr>
   <td>Milestone 2
   </td>
   <td>
<ul>

<li>Player 
<ul>
 
<li>Special abilities
</li> 
</ul>

<li>Enemies 
<ul>
 
<li>Pathfinding
 
<li>Aiming AI
</li> 
</ul>

<li>Game mechanics 
<ul>
 
<li>Projectile reflection
 
<li>Victory and defeat conditions
 
<li>A new stage
</li> 
</ul>

<li>UI 
<ul>
 
<li>Pause menu
 
<li>End of stage screen
</li> 
</ul>
</li> 
</ul>
   </td>
  </tr>
  <tr>
   <td>Milestone 3
   </td>
   <td>To be implemented:
<ul>

<li>Tanks 
<ul>
 
<li>More projectile types
 
<li>More special abilities
 
<li>Visual effects for firing and abilities
 
<li>Cosmetic changes
 
<li>Improve on aiming AI and pathfinding for enemies
</li> 
</ul>

<li>Maps 
<ul>
 
<li>Consumables around the map which can be picked up
 
<li>Create interactive objects in the environment
</li> 
</ul>

<li>Game mechanics 
<ul>
 
<li>Tutorial stage
 
<li>Rewards system
 
<li>Lootbox system
</li> 
</ul>

<li>UI 
<ul>
 
<li>Stage select screen
 
<li>Settings menu
</li> 
</ul>
</li> 
</ul>
   </td>
  </tr>
</table>



# Implemented Features (add more pictures and diagrams)


## Shooting

The cornerstone of the game, shooting is perhaps the most important feature. In the game, players are able to shoot different types of projectiles, all of which have different properties and interactions, resulting in a fresh and engaging gameplay experience.

By default, shooting is bound to the left mouse button.


### Player Shooting

Player shooting is controlled by the Shooting script. It checks for the fire button input, upon which a projectile will be fired using the Shoot() method. This is done by instantiating a new projectile prefab and using AddForce() to apply a force to it.

There are a number of fields which the script gets from a ProjectileData ScriptableObject, such as launchForce, which determines the force at which the projectile is fired and cooldown, which prevents players from firing repeatedly. We will further elaborate on this decision under the Projectiles section.

A trajectory line is provided to assist with aiming, which is helpful as some projectiles are able to reflect off walls. This is controlled by the Trajectory script, which uses raycasts and a LineRenderer to draw a line simulating the path the fired projectile will take.

![TrajectoryLine](https://github.com/willowisp01/TankYouVeryMuch/assets/132592621/096b5b72-7343-4e0d-9541-78efeb9643b1)

We will talk about enemy shooting logic under the Enemies section.


### Design Decisions

Currently, we use Instantiate() to create new projectiles from a prefab which are then fired off. This is the simplest implementation possible and since there will not be too many projectiles at any point in time with the cooldown mechanic, the computational costs will not be too great.

However, if we find that a new feature (e.g. more enemies in a stage, projectile type which fires multiple objects) results in a noticeable cost, we will instead use an ObjectPool to store projectile instances instead of 

creating and destroying new projectiles constantly.


## Projectiles

Projectiles are another focus of the game as without them, shooting will not accomplish much. As mentioned earlier, there will be different projectile types available for players to use, each with their own unique mechanics. 

We use a projectile prefab to represent a projectile type. There are 2 main components attached to the prefab, a ProjectileBehaviour script and a ProjectileData ScriptableObject.


### ProjectileBehaviour

We wrote a ProjectileBehaviour abstract class, which every new ProjectileTypeBehaviour script inherits from. It includes fields and methods which every type of projectile will have, such as the damage it deals, the duration for which it will be active and the DestroyProjectile() method.

The DestroyProjectile() method is more than just the base Destroy() method provided by Unity. We had a bug with the audio where the sound played upon firing a projectile would be cut off when the projectile was destroyed as the AudioSource was attached to the projectile itself. To fix this, when DestroyProjectile() is called, the sprite, physics and collider are disabled so the projectile will no longer interact with the environment. Then, when the audio is done playing, Destroy() is called.

Then, we will write the unique mechanics of each projectile type in its own ProjectileTypeBehaviour script.


### ProjectileData

ProjectileData is a ScriptableObject which contains all the data the specific projectile has. It will be read by both ProjectileBehaviour and Shooting scripts for all the information mentioned above.

As of Milestone 2, 1 projectile type has been implemented:


### Light Shell

The basic projectile type which introduces the player to the game. It is very simple to use, as you just point your mouse at an enemy and fire. The projectile can also be reflected off obstacles, which introduces an additional element of skill as with precise aiming, the player can even hit enemies which are behind cover.

On collision with a tank, it deals damage to it using the TakeDamage() method in the Health script and DestroyProjectile() is called.

When implementing reflection in the LightShellBehaviour script, we could not figure out why the reflection would not work despite using Unity’s Vector2.Reflect() method. Eventually, after much struggle, we realised that when we passed the velocity of the projectile into Reflect() in OnCollisionEnter2D(), the velocity had _already changed_ from the collision. Now, we keep track of the projectile’s velocity every frame in Update() and pass in oldVelocity instead.


### Design Decisions

We separate projectile types into their own prefabs and further, we have a prefab for players and a prefab for enemies. This allows us to easily assign different data to different projectile types. For example, we can use a different sprite for enemy projectiles despite both the player and enemies using the same type of projectile to better differentiate them in-game.

By using an abstract class for ProjectileBehaviour, the Shooting script can simply call GetComponent&lt;ProjectileBehaviour>() to get any ProjectileTypeBehaviour script. So, we can just have 1 general Shooting script for all different types of projectiles. 

Since each projectile type has its own specific set of data, we use a ScriptableObject to hold the fields all projectiles have in common. This allows for highly modular, customisable code and it will make it easier for us to add new projectile types in the future.


## Movement

Player movement is controlled using the WASD keys and the PlayerMovement script. The script also controls turret rotation and hull rotation, which are independent of each other. The turret faces the direction of the mouse pointer, while the hull rotates in the direction of movement.

An interesting problem we faced was how to make the tank tower move independently of the tank body. Normally, we would have made the tank tower a child object of the tank itself (which makes logical sense). Unfortunately, this tied the transform of the child object, i.e. the tank tower, to its parent, i.e. the main body. Our workaround was to make the tank body and tower separate child objects of a placeholder GameObject.

Again, we will talk about enemy movement under Enemies.


## Special Abilities

In order to spice up the game, the player will have access to special abilities which can turn the tide of the game, especially in stages with multiple enemies.

The SkillSelect script is attached to the Player GameObject and it contains the Skill ScriptableObject, which itself contains the data for the current ability. This includes information like cooldown time and number of uses.

By default, the Q key is used to activate the current ability.

As of Milestone 2, 1 ability has been implemented:


<table>
  <tr>
   <td><strong>Skill</strong>
   </td>
   <td><strong>Effect</strong>
   </td>
  </tr>
  <tr>
   <td>Erase
   </td>
   <td>This skill removes all enemy projectiles currently on screen.
   </td>
  </tr>
</table>



## Enemies

Another core feature of the game, the enemies are the key to completing or failing a given stage. In each stage, there will be a certain number of enemies, all of which have to be defeated in order to unlock and move on to the next stage.


### Enemy Shooting

Enemy shooting logic is controlled by the EnemyShooting script. The logic is as described by the diagram below: 



1. Shooting script starts a countdown.
2. Meanwhile, a radar attached to the enemy tank sweeps the stage continuously.
3. The radar emits a reflecting raycast (similar to our player’s own trajectory prediction feature) to search for a player.
4. If an (indirect) path to the player is found, the appropriate angle to shoot at is cached.
5. At t = 0.3 seconds left, the algorithm either shoots straight at the player if no obstacles block the line of fire, else it uses the cached angle.

![EnemyLogic](https://github.com/willowisp01/TankYouVeryMuch/assets/132592621/4edbb8c3-2753-4885-b4c4-a36081f04e19)

Diagram showing enemy aiming logic

One area for improvement is that the tank tower instantly snaps into place to aim for a shot, which looks unnatural. We could remedy this by using the remaining 0.3 seconds prior to shooting to shift the tower into place gradually.


### Enemy Pathfinding

With the maze-like designs of the stages, enemy pathfinding logic is a must. With the help of the A* Pathfinding Project package, we have managed to implement a pathfinding system using grid graphs.

![Pathfinding](https://github.com/willowisp01/TankYouVeryMuch/assets/132592621/efb5205e-f1c1-42f8-a52d-b9351f5daa5e)

Screenshot of the grid graph

The obstacles are scanned and the red squares are areas where the enemy will not move into. The enemy will then calculate the shortest path to the player within the blue squares using the A* path search algorithm.

While we have tried to write our own AIPath script, the default one that comes with the package is already full of features. Therefore, we have decided to use the default script for the time being while we work on a custom script which will be more suitable for our purposes.


## Game Logic

tl;dr Kill all enemies => Win Die => Lose

![GameLogic](https://github.com/willowisp01/TankYouVeryMuch/assets/132592621/192c5fb5-6c10-4b9d-94ef-26ededb91466)

### Design Decisions

Currently, much of our code is based on OOP, where a script calls another script’s method directly to make changes to the game state. For example, upon victory, instead of having, say, an OnVictory event with subscribers, we directly call the DisablePlayer() method and the Result() method, which activates the summary screen.

For Milestone 3, we intend to rewrite the logic to be more event-driven using UnityEvents and delegates.


## UI

Currently only a barebones main menu and pause menu. We will add a stage select screen and settings page.


# Software Engineering Practices


## Event-driven Programming


## Observer Design Pattern


## Singleton Design Pattern

Not implemented yet; we intend to use this for objects which will only have 1 instance in the game, such as the player itself, player save data (e.g. coins).


## Github Version Control and Branching

For this project, we use Github to store our code and for version control and collaboration purposes. We have 1 master branch, which we generally do not edit directly unless we are only making minor changes. Outlined below is our basic workflow:



1. Create a new branch when one of us wants to add a new feature or fix a bug.
2. Make regular commits to the branch, especially when significant changes have been made. Commit summaries are written to inform the other party of changes.
3. When the feature has been added or the bug has been fixed, create a pull request which the other party will review and merge to master.

Since we usually do not work on the project simultaneously and therefore do not have multiple branches existing at the same time, we have only had 1 merge conflict so far.

We also use the Issues section extensively. When we think of a new feature to be added or experience a bug that needs to be fixed, we will add it to the section with the appropriate tag. Unfortunately, the list grows faster than we can close issues.


# Testing


## System Testing

To evaluate the robustness of our system, we devised several test cases based on the information gathered from player testing and feedback. These cases are designed to simulate the actions an actual player in the game would take, and are categorised for neatness. 


### Menu Navigation


<table>
  <tr>
   <td><strong>Test</strong>
   </td>
   <td><strong>Expected Result</strong>
   </td>
   <td><strong>Passed?</strong>
   </td>
   <td><strong>Remarks</strong>
   </td>
  </tr>
  <tr>
   <td>Click on start
   </td>
   <td>Navigate to first stage
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Player tank defeated
   </td>
   <td>Navigate to you lose
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>All enemy tanks defeated
   </td>
   <td>Navigate to you win
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>From you lose screen click restart
   </td>
   <td>Restart current stage
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>From you lose screen click main menu
   </td>
   <td>Navigate to main menu
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>From you win screen click next
   </td>
   <td>Navigate to next stage
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>From you win screen click main menu
   </td>
   <td>Navigate to main menu
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
</table>



### Player Controls


<table>
  <tr>
   <td><strong>Test</strong>
   </td>
   <td><strong>Expected Result</strong>
   </td>
   <td><strong>Passed?</strong>
   </td>
   <td><strong>Remarks</strong>
   </td>
  </tr>
  <tr>
   <td>Move using WASD
   </td>
   <td>Player tank moves according to input
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Move using arrow keys
   </td>
   <td>Player tank moves according to input
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Move mouse
   </td>
   <td>Player tank tower follows mouse
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Press Q
   </td>
   <td>Ability activates
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
</table>



### Player Abilities


<table>
  <tr>
   <td><strong>Test</strong>
   </td>
   <td><strong>Expected Result</strong>
   </td>
   <td><strong>Passed?</strong>
   </td>
   <td><strong>Remarks</strong>
   </td>
  </tr>
  <tr>
   <td>Player fires projectile
   </td>
   <td>Player projectile follows projectile trajectory
   </td>
   <td>Y*
   </td>
   <td>At sharp angles there is a slight deviation from the trajectory. This is possibly because the bullet is not a point object.
   </td>
  </tr>
  <tr>
   <td>Player uses ability “Erase”
   </td>
   <td>Enemy projectiles are destroyed
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Player uses “Erase” continuously
   </td>
   <td>Ability not activated unless the cooldown is complete
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Player uses “Erase” with no uses left
   </td>
   <td>Ability not activated
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
</table>



### Enemy Behaviour


<table>
  <tr>
   <td><strong>Test</strong>
   </td>
   <td><strong>Expected Result</strong>
   </td>
   <td><strong>Passed?</strong>
   </td>
   <td><strong>Remarks</strong>
   </td>
  </tr>
  <tr>
   <td>Enemy has clear line of sight to player
   </td>
   <td>Enemy takes a direct shot
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Player breaks line of sight by hiding behind wall
   </td>
   <td>Enemy ricochets projectile to hit player
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Player shoots enemy tank 3 times
   </td>
   <td>Enemy is destroyed
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
</table>



### Environment


<table>
  <tr>
   <td><strong>Test</strong>
   </td>
   <td><strong>Expected Result</strong>
   </td>
   <td><strong>Passed?</strong>
   </td>
   <td><strong>Remarks</strong>
   </td>
  </tr>
  <tr>
   <td>Player runs into wall
   </td>
   <td>Wall obstructs player
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Player shoots projectile at wall
   </td>
   <td>Projectile reflects off wall
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Player pushes enemy into wall
   </td>
   <td>Wall obstructs enemy
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Enemy shoots projectile at wall
   </td>
   <td>Projectile reflects off wall
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Player fires with muzzle in wall
   </td>
   <td>Projectile stuck in wall
   </td>
   <td>Y
   </td>
   <td>-
   </td>
  </tr>
</table>



## User Testing

User testing is necessary for us to gain new perspectives on our game that we ourselves would not have considered. The methodology we employed is as follows:


### Testing Methodology



* Players were asked to read the questions prior to playing the game.
* Players were then allowed to play the game without any prompts.
* Players were then asked to try and break the game to the best of their ability.
* Afterwards, if players did not discover certain features/functionalities on their own, they were prompted on how to use them.


### Questions



1. Is it easy to navigate through the game menu?
2. Are the game controls easy and intuitive?
3. What did you think of the enemy AI? Was it too easy or difficult?
4. Is the game balanced? Why or why not?
5. Could you identify any bugs or unintended side-effects?
6. Did you enjoy playing the game?
7. How do you think the game can be improved? What was done well?

### 
    Responses



#### Player K1


<table>
  <tr>
   <td><strong>Question</strong>
   </td>
   <td><strong>Player Response</strong>
   </td>
  </tr>
  <tr>
   <td>1
   </td>
   <td>Yes, however some introduction and basic tutorial would be good. There was no clear indication of how to use the ability, or its cooldown.
   </td>
  </tr>
  <tr>
   <td>2
   </td>
   <td>Yes. Classic PC setup.
   </td>
  </tr>
  <tr>
   <td>3
   </td>
   <td>AI was suited for tutorial level.
   </td>
  </tr>
  <tr>
   <td>4
   </td>
   <td>Yes.
   </td>
  </tr>
  <tr>
   <td>5
   </td>
   <td>Enemy could not aim properly when pinned into the wall.
   </td>
  </tr>
  <tr>
   <td>6
   </td>
   <td>Yes. Classic y8 game.
   </td>
  </tr>
  <tr>
   <td>7
   </td>
   <td>Quite addictive. However, a greater range of special effects or skins could be implemented.
   </td>
  </tr>
</table>



#### Player X


<table>
  <tr>
   <td><strong>Question</strong>
   </td>
   <td><strong>Player Response</strong>
   </td>
  </tr>
  <tr>
   <td>1
   </td>
   <td>Yes, there were not many pages to navigate to.
   </td>
  </tr>
  <tr>
   <td>2
   </td>
   <td>The movement and shooting were intuitive. It was not obvious to use Q to activate the ability. It was also not clear what the ability was.
   </td>
  </tr>
  <tr>
   <td>3
   </td>
   <td>It was easy. (However, it was a tutorial stage)
   </td>
  </tr>
  <tr>
   <td>4
   </td>
   <td>I would make the player cooldown shorter. It’s more entertaining to shoot faster.
   </td>
  </tr>
  <tr>
   <td>5
   </td>
   <td>Sometimes the enemy bullet is stuck in the wall. This occurs when the enemy fires directly into a wall.
   </td>
  </tr>
  <tr>
   <td>6
   </td>
   <td>The game is too simple. I would appreciate harder levels.
   </td>
  </tr>
  <tr>
   <td>7
   </td>
   <td>Add more complexity. Add more enemies, special abilities. The game runs smoothly without lag.
   </td>
  </tr>
</table>



#### Player K2


<table>
  <tr>
   <td><strong>Question</strong>
   </td>
   <td><strong>Player Response</strong>
   </td>
  </tr>
  <tr>
   <td>1
   </td>
   <td>Yes. The game is simple to navigate through.
   </td>
  </tr>
  <tr>
   <td>2
   </td>
   <td>It’s good that there was both WASD and arrow keys control. The special abilities controls could be included in an instruction popup.
   </td>
  </tr>
  <tr>
   <td>3
   </td>
   <td>It’s good that the enemy can use ricochet to shoot the player.
   </td>
  </tr>
  <tr>
   <td>4
   </td>
   <td>The balance is ok. For tank games it’s good that the player doesn’t die in one hit.
   </td>
  </tr>
  <tr>
   <td>5
   </td>
   <td>When the player tank is pressed directly onto an enemy, the enemy may not shoot straight.
<p>
When angled incorrectly, the player tank cannot pass through a narrow gap.
   </td>
  </tr>
  <tr>
   <td>6
   </td>
   <td>Yeah.
   </td>
  </tr>
  <tr>
   <td>7
   </td>
   <td>It would be good to have a guide on the mechanics of the game (for example, ricochet and abilities.)
<p>
I liked the bullet trajectory feature.
   </td>
  </tr>
</table>



### Our Findings


<table>
  <tr>
   <td><strong>Observation</strong>
   </td>
   <td><strong>Proposed Changes</strong>
   </td>
   <td><strong>Rationale</strong>
   </td>
  </tr>
  <tr>
   <td>It was not obvious how to use the ability, or that it had a cooldown.
   </td>
   <td>Include a tutorial on how to use the ability, as well as visual cues when the ability is used. (For example, enemy bullets glow and disintegrate.)
   </td>
   <td>This makes it obvious when the ability was used and informs players how to use the ability and what the ability does.
   </td>
  </tr>
  <tr>
   <td>When angled incorrectly, the player tank could not pass through a narrow gap.
   </td>
   <td>As a game design principle, do not make stages that require specific/niche setups to navigate.
   </td>
   <td>This would avoid making the game gimmicky and make it more intuitive to play the game.
   </td>
  </tr>
  <tr>
   <td>More skins, enemy tanks, and abilities could be added to the game.
   </td>
   <td>Add more skins, enemy tanks and abilities to the game.
   </td>
   <td>An increased amount of player content and features makes the game more engaging and interesting for long. We intend to enhance the current features by Milestone 3.
   </td>
  </tr>
  <tr>
   <td>Sometimes, enemy bullets are stuck in the wall when the enemy fires directly into the wall. 
   </td>
   <td>Shift the fire point closer into the centre of the tank.
   </td>
   <td>Sometimes, it is necessary to sacrifice “realism” to make the game function better. While firing a bullet into a wall in real life would probably cause it to be stuck, it would be weird in the context of this game.
   </td>
  </tr>
</table>



# Credits and Acknowledgements


## Assets


## Packages

[A* Pathfinding Project](https://arongranberg.com/astar/) 
