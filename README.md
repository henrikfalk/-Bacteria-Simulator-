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

- There is a "FishtankScene" that is runnable
- You can start a new simulation by pressing 'n' on keyboard
- You can select a bacteria using left-click
- If you press 'l' when a bacteria is selected you can follow the bacteria. Pressing 'l' is also a toogle between following a selected bacteria and not following it
- If you press WASD in main camera mode you can move the camera
- You can use the mouse scroll-wheel to zoom-in and out in main camera mode
- You can add food to the fishtank (but has no effect yet) by pressing 'f' on keyboard
- The bacteria have a approximate lifespan. Then they die, turn black and falls to the bottom of the fishtank. After a period they dissolves in the water
- If you right-click with the mouse on a bacteria it dies
- The status UI element is just a UI mockup. Needs refactoring
- There is a "Menu" scene that is runnable and can start the "FishtankScene" but have no other functionality yet


## The game
The game simulates the life of different bacteria types in a fishtank.

Keys in the game:
Press 'n' for new simulation
Press 'f' for adding food
Press 'l' for "look" at selected bacteria
Press WASD to move camera or mouse scroll-wheel to zoom in and out in main camera mode

## Unity concepts
- Prefab variants
- "Glass" material
- Mouse selection using RaycastHit and child gameobject
- GameObject highligt when mouseover
- Datapersistence between scenes
- Use of StartCoroutine()
- TMPro package (TextMeshPro)

## Development environment
Unity Editor v2020.3.32f1 running on Manjaro Linux.

I used "GitKraken" as Git client.

## Prototype

This is my first prototype to test if I could make bacteria:

<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-1.png" width="600" >

My second prototype that controls the bacteria amount to add:
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-2.png" width="600" >

Prototype 3: Status UI element and an "Add Food" feature:
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-3.png" width="600" >

Prototype 4: Bacteria selection and info UI element:
<img src="https://raw.githubusercontent.com/henrikfalk/Bacteria-Simulator/main/Images/Bacteria-Simulator-Prototype-4.png" width="600" >
