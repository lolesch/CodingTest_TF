// DRAFT

/* RECORD:
 * drag and drop might contain the drag start time and drop end time 
 * and drop position to lerp from start to drop position
 * 
 * Starting and stopping the recording will add a time stamp
 * use the start timeStamp to calculate time delta for each data set 
 * so we dont need to calculate the delta when playing the recording?
 * 
 * REPLAY:
 * when loading a recording, it should automatically start playing
 * this means that ActionButton interaction should be disabled while playing
 * 
 * The recording should iterate over all timestamps and when the timestamp is reached, call its action.
 * This means, that the saved data has to be linked with the corresponding gameObject it was executed from
 * 
 * tooltip for disabled InputArea while playing a recording
 */

/* how to save the individual commands
 */

/* press recording executes a startRecordingCommand
 * should save the current state
 */
