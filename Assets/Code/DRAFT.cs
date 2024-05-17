// DRAFT

/* #Recording:
 * the sequence contains data for each input
 * each input data has a time stamp and the performed action
 * 
 * drag and drop might contain the drag start time and drop end time and drop position
 * to lerp from current to drop position
 * 
 * Starting and stopping the recording will add a time stamp
 * use the start timeStamp to calculate time delta for each data set so we dont need to calculate the delta when playing the recording?
 * 
 * The start button should be disabled until a valid string is entered in the input field
 * make a tooltip to communicate this
 * 
 * The InputField should be disabled while recording
 * 
 * The load and Play button should be disabled while recording
 * 
 * make the load and play button a toggle to stop the playing recording
 * 
 * Add a pause button?
 * 
 * LOAD AND PLAY:
 * when loading a recording, it should automatically start playing
 * this means that ActionButton interaction should be disabled while playing
 * 
 * The recording should iterate over all timestamps and when the timestamp is reached, call its action.
 * This means, that the saved data has to be linked with the corresponding gameObject it was executed from
 * 
 */

/* input seite -> receiver 
 * static event auf button
 * tooltip provider der darauf reagiert und tooltip wird angezeigt/gestoppt
 * 
 * how to save the individual commands
 * 
 */

