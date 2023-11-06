# Bacteria Simulator

A bacteria simulation minigame - C# OOP in Unity

<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/SimulationScene.png" width="600" >


This is my Unity project for the tutorial "Submission: Programming theory in action" from the mission "Apply object-oriented principles" from the "Junior Programmer " pathway at "Unity Learn".

The goal for this tutorial is to demonstrate knowledge of OOP (Object oriented programming) using abstraction, inheritance, polymorphism and encapsulation.

Link to tutorial: https://learn.unity.com/tutorial/submission-programming-theory-in-action

Enjoy!

Regards Henrik

## The game
The game simulates the life of different bacteria types in a aquarium. Totally unrealistic and just for fun.

Maybe I should have chosen peaceful fish for the aquarium but I like the idea of something "out of control" is at bit more interesting for at simulation.

The game explores different Unity features and concepts.

## Unity concepts
- Mouse selection using RaycastHit and child gameobject
- Two cameras (One following GameObject and the other with zoom, rotate and pan function=
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

## Controls in the "SimulationScene"
Keys actions:

- Press 'n' for new simulation
- Toggle 'l' for follow a selected bacteria or not (Switching camera)
- Press 'q' to stop current simulation. If no simulation is in progress then the game will return to the MainSimulationScene
- Press 'Escape' to reset cameras
- Press 'h' for help
- Press WASD to move camera

Mouse actions:

- Left Button : Select bacteria
- Right Button: Kill a selected bateria
- Scroll Wheel: Zoom in and out
- Scroll Wheel Button: Pan vertical and rotate horisontal


## Development environment
- Operating system: Manjaro Linux
- Developer IDE: Unity Editor v2022.3.5f1
- Editor: Visual Studio Code
- Version control: <a href="https://github.com">GitHub</a> using <a href="https://www.gitkraken.com/">GitKraken</a>
- Documentation: <a href="https://obsidian.md/">Obsidian</a>

## Tools and resources
- For the aquarium and bacteria models: <a href="https://www.blender.org/">Blender 3.6.4</a>
- The background image for the panels: Image by <a href="https://pixabay.com/users/insaneartist-2792766/?utm_source=link-attribution&utm_medium=referral&utm_campaign=image&utm_content=1487216">InsaneArtist</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=image&utm_content=1487216">Pixabay</a>
- The HDRI Skybox: <a href="https://polyhaven.com/hdris">Poly Haven</a>

## Development status
Current version:
- Added a finite state machine to the bacteria
- Added particle system to dying bacteria
- Added elapsed simulationtime to "SimulationEndedPopup"
- Added total number of bacteria to the statuspanel
- Added toxicity to environment when a bacteria dies (no effect yet)
- Removed some obsolete C# classes and Prefabs
- Added toolbar panel and interactions with simulation statemachine
- Added custom HDRI skybox
- Help panel can now be toggled from the toolbar and is initially invisible
- Added "Exit" game button to toolbar
- Added "Pause", "Laboratory" and "Settings" buttons til toolbar but have no implementations yet
- Rewrote "Main Camera" controller for better zoom, drag and pan functions

General:
- Each scene is runnable from the Unity Editor
- The bacteria are reproducing but the simulation is not balanced yet!
- Purple bacteria are killing the green and red bacteria
- The population limit is set to 500 bacteria
- The bacteria have a approximate lifespan. When they die, they turn black and falls to the bottom of the fishtank. After a period they dissolves in the water

## In development
- Implementation of the toxicity feature
- Pause the simulation
- Better light. Maybe user interactive.
- Update the help panel
- Update the LaboratoryScene

## Roadmap
- Adding multiple simulation configurations which can be saved and retrieved
- Add watershader
- Add shader for food and medicine
- Internationalization
- Keyboard navigation the the dialogs
- Food (Doing something positive!)
- Medicine (Removing toxic water!)
- Gamification of simulation
- Core game settings like display resolution etc.

## Userguide

This userguide is work in progress. At the moment just screenshots.

### Laboratory
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/BacteriaLaboratory.png" width="600" >

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
