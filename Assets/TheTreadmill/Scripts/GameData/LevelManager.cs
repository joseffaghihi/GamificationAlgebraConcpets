using UnityEngine;
using System.Collections;

public static class LevelManager
{
	private static string lastLevel; //Store name of the last level
	
    /** Stores the last scene
     * @param level - Name of the current Scene before loading the next one
     * @return void
     */
	public static void setLastLevel(string level)
	{
		lastLevel = level;
	}

    /** Gets the Name of the last scene
     * @return string lastLevel - Returns the name of the last scene
     */
	public static string getLastLevel()
	{
		return lastLevel;
	}

    /** Loads the last scene
     * @return void
     */
	public static void LoadPreviousLevel()
	{
		Application.LoadLevel(lastLevel);
	}
}