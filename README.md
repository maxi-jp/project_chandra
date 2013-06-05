project_chandra
===============

A 2D spacial shooter developed in the programming language C# in Visual Studio 2010 environment.

First of all i am going to explain the game controls:

	-Movement controls: 
		Left: A
		Right: D
		Up: W
		Down: S
		To turn the ship in gameA you can do it with the mouse.
		
	-To shoot: Left click mouse.
	
	-In main menu you can go to the test mode with "T" key, particles sistem test with "Y" key and play against the super final boss pressing "Q" key. 
	
	-In test mode you can see all the enemies with all the function keys: F1,F2,F3,F4,F5,F6,F7,F8,F9. And you can see the power ups with de F10 key.
	
	-When you are playing you can also switch to god mode pressing "G" key and activate and desactivate debug mode
	with "F" key. To stop the game you can do it pressing "P" key.
		
	-In debug mode you can get lives with the "L" key and loose lives with the "K" key.
	
	-In the particles sistem test you have these controls:
		Press FX to increase value, press FX+mouse.rightclick for decrease value 
        F1: Number of Particles = " + ship.particles.GetParticleCount() + "\n" +
		F2: PARTICLE_CREATION_INTERVAL = " + ship.particles.PARTICLE_CREATION_INTERVAL
		F3: INITIAL_DEAD_AGE = " + ship.particles.INITIAL_DEAD_AGE
		F4: FADEOUT_DECREMENT_INITIAL_TIME = " + ship.particles.FADEOUT_DECREMENT_INITIAL_TIME
		F5: FADEOUT_INCREMENT = " + ship.particles.FADEOUT_INCREMENT
		F6: FADEOUT_DECREMENT = " + ship.particles.FADEOUT_DECREMENT
		F7: INITIAL_GROWTH_INCREMENT = " + ship.particles.INITIAL_GROWTH_INCREMENT
		F8: MAX_DEFLECTION_GROWTH = " + ship.particles.MAX_DEFLECTION_GROWTH
		F9: MAX_ACELERATION_X = " + ship.particles.MAX_ACELERATION_X
		F10: MAX_ACELERATION_Y = " + ship.particles.MAX_ACELERATION_Y
		
	-To switch to full screen: ","

	
	
The root contains some documentation (in spanish) and the code of the project.
All the documentation is in Google Drive, now i am going to explain what kind of documentation
we have here:

In the architecture folder there are:
	-A document of control variables enemies that explains the operation of the most important enemies variables.
	-Secuence diagrams, that show the flow of events of a shot, an enemy and a power up.
	-UML diagrams for the most important classes of the project.
	
In the polls floder there are the resume of the march and june polls.

In the Sprints folder there are the sprints form december to may.

In the Story folder there are all the music and images used in the starting video and the story line. 
There are also images and a document explaining the final boss 1.

In the Use casese folder there are all the use case documents.
