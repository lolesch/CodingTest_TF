https://lolesch.github.io/CodingTest_TF/
# CodingTest Tasks

> [!example]- 1 - BUTTON ACTIONS
> - [x] Create three buttons in the main view of the application. 
> 	- [x] The buttons should all have different graphics of your choosing. 
> 	- [x] Their initial position does not matter too much since they will move around. 
> ___
> - [x] Each button can be interacted with in the following ways: 
> 	- [x] When the user hovers over a button for more than 0.5 seconds a tooltip should appear over the button. 
> 		- [x] make the tooltip appear based on the current position
> 		- [x] Each button should have a different tooltip text. 
> 	- [x] When the user clicks on a button a closable popup should appear. 
> 		- [x] The popup has a text in it that is different for each button. 
> 	- [x] The user should be able to drag each button around the screen which will move it accordingly. 
> 	- [x] When the user right clicks a button it should cycle its color. 
> 		- [x] If you right click a red button it should turn green
> 		- [x] if you right click a green button it should turn blue 
> 		- [x] if you right click a blue button it should turn red

> [!example] 2 - RECORDING AND REPLAY 
> - [x] Create a recording system with which all user actions can be
> 	- [x] recorded, 
> 	- [x] saved and 
> 	- [x] replayed at a later time. 
> - [x] Put the controls for this feature (Start/Stop Recording button, Recording name input field and Load and play recording button) at the top of the screen. 
> - [x] To specify which recording should be started or loaded the user has to type in the name of the recording in an input field. 
> 	- [x] inputField OnSubmit -> 
> 	- [x] disable the startRecording button when no name is entered
> 		- [x] tooltip that a name is required for the recording to start
> - [x] After a recording has been started by clicking the start recording button, the start recording button should transform into a stop recording button. 
> - [x] When the recording is stopped via the stop recording button, the recording gets saved.
> - [x] If the user makes and saves a recording, they should be able to replay it even after restarting the application. 
> - [x] When the recording is replayed the recorded actions should be performed in the same sequence and with the same speed. 
> - [x] The recorded actions should include all button actions mentioned above. 
> - [x] When starting and stopping a recording with a previously used name, saving it should just overwrite the save for the old recording with the same name.


> [!example] 3 - BONUS TASKS
> - [ ] show all saved recordings in a dropdown
> - [ ] confirm popup if recording name will overwrite existing recording
> 	- [ ] rename option
> - [ ] replay time slider?

