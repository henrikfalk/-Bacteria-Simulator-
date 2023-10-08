# Bacteria Simulator

A bacteria simulation minigame - C# OOP in Unity

This is my Unity project for the tutorial "Submission: Programming theory in action" from the mission "Apply object-oriented principles" from the "Junior Programmer " pathway at "Unity Learn".

The goal for this tutorial is to demonstrate knowledge of OOP (Object oriented programming) using abstraction, inheritance, polymorphism and encapsulation.

Link to tutorial: https://learn.unity.com/tutorial/submission-programming-theory-in-action

Link to game on Unity play: Not yet present

Enjoy!

Regards Henrik

## Status
In development.

- There is a "MenuScene" that is runnable and can start the "FishtankScene" and the "LaboratoryScene"
- You can start a new simulation by pressing 'n' on keyboard
- You can select a bacteria using left-click
- If you press 'l' when a bacteria is selected you can follow the bacteria. Pressing 'l' is also a toogle between following a selected bacteria and not following it
- If you press WASD in main camera mode you can move the camera
- You can use the mouse scroll-wheel to zoom-in and out in main camera mode
- You can add food to the fishtank (but has no effect yet) by pressing 'f' on keyboard
- The bacteria have a approximate lifespan. Then they die, turn black and falls to the bottom of the fishtank. After a period they dissolves in the water creating a more toxic environment 
- If you right-click with the mouse on a bacteria it dies

## The game
The game simulates the life of different bacteria types in a fishtank.

Keys in the scene "FishtankScene":
- Press 'n' for new simulation
- Press 'f' for adding food
- Press 'l' for "look" and follow a selected bacteria
- Press 'Escape' or 'q' to stop current simulation. If no simulation is in progress then the game will return to the mainmenu.
- Press WASD to move camera or mouse scroll-wheel to zoom in and out in main camera mode

Mouse actions in the scene "FishtankScene":
- Left Button : Select bacteria
- Right Button: Kill a selected bateria
- Scroll Wheel: Zoom in and out

## Unity concepts
- Prefab variants
- "Glass" material
- Mouse selection using RaycastHit and child gameobject
- Two cameras
- GameObject highligt when mouseover
- Datapersistence between scenes
- Datapersistence between gamesessions (Game settings)
- Use of StartCoroutine()
- TMPro package (TextMeshPro)

## Development environment
Startet development using Unity Editor v2020.3.32f1 running on Manjaro Linux.
Updated the Unity Editor to v2022.3.5f1 at some point because v2020.3.32f1 didn't work anymore.

I used "GitKraken" as Git client.

## Prototype

### "FishtankScene"
This is my first prototype to test if I could make bacteria:

<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-1.png" width="600" >

My second prototype that controls the bacteria amount to add:
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-2.png" width="600" >

Prototype 3: Status UI element and an "Add Food" feature:
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-3.png" width="600" >

Prototype 4: Bacteria selection and info UI element:
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-4.png" width="600" >

### "MenuScene"
First UI mockup of the MenuScene, which also is the startscreen of the game:
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-5.png" width="600" >

### "LaboratoryScene"
First UI mockup of the LaboratoryScene in which you can modify the fishtank environment and the bateria:
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-6.png" width="600" >
