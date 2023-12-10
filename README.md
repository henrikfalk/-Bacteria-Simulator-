# Bacteria Simulator

A bacteria simulation minigame - C# OOP in Unity

<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/SimulationScene.png" width="600">


This is my Unity project for the tutorial "Submission: Programming theory in action" from the mission "Apply object-oriented principles" from the "Junior Programmer " pathway at "Unity Learn".

The goal for this tutorial is to demonstrate knowledge of OOP (Object oriented programming) using abstraction, inheritance, polymorphism and encapsulation.

Link to tutorial: <a href="https://learn.unity.com/tutorial/submission-programming-theory-in-action" target="_blank">Submission: Programming theory in action</a>

Enjoy!

Regards Henrik

## The game
The game simulates the life of different bacteria types in a aquarium. Totally unrealistic and just for fun.

Maybe I should have chosen peaceful fish for the aquarium but I like the idea of something "out of control" is at bit more interesting for at simulation.

The game explores different Unity features and concepts.

## Unity concepts
- Mouse selection using RaycastHit and child gameobject
- Two cameras (One following GameObject and the other with zoom, rotate and pan function)
- "Glass" material
- GameObject highligt when mouseover
- Switching scenes with (memory) datapersistence (Singleton)
- Datapersistence between gamesessions (Laboratory settings)
- Use of StartCoroutine()
- TMPro package (TextMeshPro)
- Imported models and materials (UV mapped) from Blender
- FSM (Finite State Machine) for the simulation with UI interactions
- Particlesystems
- Custom HDRI Skybox
- Pause and unpause game (Scaled and unscaled time)

## Controls in the "SimulationScene"
Keys actions:

- 'n': New simulation
- Toggle 'l' for follow a selected bacteria or not (Switching camera)
- Toggle 'p' or 'spacebar' : Pause or resume running simulation
- 'q': Stop current simulation. If no simulation is in progress then the game will return to the MainSimulationScene
- 'Esc' : Quit Follow
- Toggle 'h': Help
- 'WASD' or arrow keys: Pan vertical and rotate horisontal

Mouse actions:

- Left Button : Select bacteria
- Right Button: Kill selected bateria
- Scroll Wheel: Zoom in and out
- Scroll Wheel Button: Pan vertical and rotate horisontal


## Development environment
- Operating system: Manjaro Linux
- Developer IDE: Unity Editor v2022.3.5f1
- Editor: Visual Studio Code
- Version control: <a href="https://github.com" target="_blank">GitHub</a> using <a href="https://www.gitkraken.com/" target="_blank">GitKraken</a>
- Documentation: <a href="https://obsidian.md/" target="_blank">Obsidian</a>

## Tools and resources
- Some ressorurces from the Unity <a href="https://assetstore.unity.com/packages/vfx/particles/particle-pack-127325" target="_blank">Particle Pack</a> from the Unity Asset Store
- For the aquarium and bacteria models: <a href="https://www.blender.org/" target="_blank">Blender 3.6.4</a>
- The background image for the panels: Image by <a href="https://pixabay.com/users/insaneartist-2792766/?utm_source=link-attribution&utm_medium=referral&utm_campaign=image&utm_content=1487216" target="_blank">InsaneArtist</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=image&utm_content=1487216" target="_blank">Pixabay</a>
- The HDRI Skybox: <a href="https://polyhaven.com/hdris" target="_blank">Poly Haven</a>

## Development status
Current version:
- Added "AddDetoxPopup" UI functionality
- Fixed rotated camera after quitting simulation
- Added water splash particle system when bacteria hits the water
- Added detox items (Including custom shader and particlesystem)
- Detox items removes toxicity (25 ppm for each "pill")
- Toxicity feature: If the toxicity level reaches 2000 ppm all bacteria dies
- Implemented detox UI feature
- Added purple bacteria to the laboratory scene
- Added various new toxicity settings to the laboratory scene
- You can now visit the laboratory from the simulation scene
- Finished the laboratory scene. You can load/save/rename/delete etc. different simulation configurations
- Added current simulation configuration name to the simulation scene
- Workaround for JSON float issue in svaed configurations. 4.5f is not 4.5 in JSON!!!

General:
- Each scene is runnable from the Unity Editor
- The bacteria are reproducing but the simulation is not balanced yet!
- Purple bacteria are killing the green and red bacteria
- The population limit is set to 500 bacteria
- One detox "pill" removes 25 ppm toxicity
- The bacteria have a approximate lifespan. When they die, they turn black and falls to the bottom of the aquarium. After a period they dissolves in the water

## In development
- The add food feature
- Cleanup old inactive configuration code
- Better light. Maybe user interactive.
- Solve the problem with the fertility percent
- Porting to Godot gameengine

## Roadmap
- More efficient way to track and handle bacteria in the simulation
- Custom event handling to decouple UI elements better
- View simulation report
- Move hardcoded values for balancing the simulation to one place
- Add watershader
- Add shader for food
- Internationalization
- Keyboard navigation in the dialogs
- Food (Doing something positive!)
- Gamification of simulation
- Core game settings like display resolution etc.
- Port to the gameengine Godot for comparison
- Create a beautiful UI

## Userguide

This userguide is work in progress. At the moment just screenshots.

### Main scene
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/MainMenuScene.png" width="600" >

### Laboratory
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/LaboratoryScene.png" width="600" >

### New simulation
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/NewSimulation.png" width="600" >

### Look at bacteria
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/LookAtBacteria.png" width="600" >


## Prototypes

### "SimulationScene"
This is my first prototype to test if I could make bacteria:

<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-1.png" width="600" >

My second prototype that controls the bacteria amount to add:
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-2.png" width="600" >

Prototype 3: Status UI element and an "Add Food" feature:
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-3.png" width="600" >

Prototype 4: Bacteria selection and info UI element:
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-4.png" width="600" >

### "MainMenuScene"
First UI mockup of the MainMenuScene, which also is the startscreen of the game:
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-5.png" width="600" >

### "LaboratoryScene"
First UI mockup of the LaboratoryScene in which you can modify the aquarium environment and the bateria:
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-6.png" width="600" >
