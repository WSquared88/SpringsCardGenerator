# SpringsCardGenerator
Unity generator for the graphic design of cards for the Springs board game. 

Imports a csv file into Unity, and creates a layout for a playtest card

Made in Unity version 2018.1.0f2



# What to do if the scene has missing scripts

Main Camera
	> Card Text Parser
		Set Card UI / Card Dropdown
Canvas > Card
	> Display Card, set cardframes.size to 3, set unit/spell/worker background
	> Set Name Text, etc
Canvas > Dropdown
	> Check that the dropdown script has "On Value Changed (Int32) set to Runtime only, CardTextParser.OnCardSelected, from the main camera.