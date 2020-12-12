// HANDLES READING AND WRITING FILES

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    // Creates a variable (SAVE_FOLDER) that stores the path of where the save folder (Save) is created.
    public static readonly string SAVE_FOLDER = Path.Combine(Application.dataPath, "Save");

    // Makes sures that the save folder exists on your system. If it doesn't then it will create one.
    public static void Init()
    {
        {
            if (!Directory.Exists(SAVE_FOLDER))
            {
                Directory.CreateDirectory(SAVE_FOLDER);
            }
        }
    }

    // Creates/Overwrites a file into the path where your save folder is and fills it will the information passed through the argument (saveString).
    public static void Save(string saveString)
    {
        File.WriteAllText(Path.Combine(SAVE_FOLDER, "save.json"), saveString);
    }

    // Return true/false if a save file exists
    public static bool SaveFound()
    {
        if (File.Exists(Path.Combine(SAVE_FOLDER, "save.json")))
        {
            return true;
        }
        return false;
    }

    public static string Load()
    {
        if ( SaveFound() )
        {
            string saveString = File.ReadAllText(Path.Combine(SAVE_FOLDER, "save.json"));
            return saveString;
        }
        else
        {
            return null;
        }

    }
}
