# Elegant Asteroids

> According Elegant Object Principals

The classic retro game

You play as spaceship. On board you have 2 guns: laser and cannon

Your goal is survive as long as possible evading UFOs and asteroids

Player dies on first touch

## Features

Project

- Object Composition Hierarchy
- Lightweight physic simulation from scratch with acceleration
- Each module is placed to own Assembly (.asmdef)

Gameplay

- Simple input
- 2 guns on board: Laser and Cannon
- Asteroids shattering on destroy
- UFOs follows player

## Structure

Project is divided into 3 parts: Core, View and Game

Whole code are placed at 
Assets/Scripts



### Core

Contains fundamental components (Physical, Cooldown & e.t.c.) and gameplay implementation (SpaceShip, Enemies, Guns & e.t.c) 

### View

Represents game state in Unity 

Implements IView interfaces from Core

### Game

Build point of app

Connects core, view and input to run game session

For input is used NewInputSystem by Unity 

## Configs

You can adjust configs at Assets/Configs

To modify configs scripts go to Assets/Scripts/

## Graphics

Project is not focused on graphical design. There are used primitive sprites to demonstrate functionaliy

If you want you can replace sprites on yours for prefabs that are located at Assets/Prefabs 

## License

MIT
