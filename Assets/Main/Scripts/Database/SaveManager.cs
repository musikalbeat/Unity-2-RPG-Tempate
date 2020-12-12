// HANDLE GETTING AND GIVING INFORMATION FROM AND TO THE SAVE FILE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    // PlayerPref dictionary to store the information that gets saved/loaded
    private class SaveObject
    {
        public Vector3 Position;
        public int Gold;
        public int Experience;
        public int Difficulty;
        public string Name;
        public List<int> ItemAmounts;
    }

    public PlayerManager pm;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SaveSystem.Init();
    }

    public void SaveGame()
    {
        // Locate the PlayerManager script
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        // Create a new save object to store information into a dictionary
        SaveObject saveObj = new SaveObject
        {
            Position = pm.transform.position,
            Gold = pm.gold,
            Experience = pm.experience,
            Difficulty = PlayerPrefs.GetInt(PrefNames.difficulty),
            Name = PlayerPrefs.GetString(PrefNames.playerName),
            ItemAmounts = new List<int>(pm.inventory.Count)
        };

        // Loop through the player inventory and assign the item amounts into the ItemAmounts list.
        foreach (inventorySlotProxy proxy in pm.inventory)
        {
            saveObj.ItemAmounts.Add(proxy.itemAmount);
        }

        // Converts dictionary into JSON file
        string json = JsonUtility.ToJson(saveObj);
        // Save JSON file into a file on your computer
        SaveSystem.Save(json);
    }

    public void LoadGame()
    {
        SaveObject loadedSave = JsonUtility.FromJson<SaveObject>(SaveSystem.Load());
        PlayerPrefs.SetInt(PrefNames.difficulty, loadedSave.Difficulty);
        PlayerPrefs.SetString(PrefNames.playerName, loadedSave.Name);
        Debug.Log(loadedSave.Experience);
        StartCoroutine(LoadingValues(loadedSave));
    }

    IEnumerator LoadingValues(SaveObject loadedSave)
    {
        while (SceneManager.GetActiveScene().buildIndex != 1)
        {
            Debug.Log("Wait");
            yield return new WaitForSecondsRealtime(0.5f);
        }
        Debug.Log("Load Value");
        
        // Locate the PlayerManager script
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        pm.gold = loadedSave.Gold;
        pm.experience = loadedSave.Experience;
        pm.gameObject.transform.position = loadedSave.Position;

        for (int i = 0; i < loadedSave.ItemAmounts.Count; i++) 
        {
            pm.inventory[i].itemAmount = loadedSave.ItemAmounts[i];
        }

        // Make sure that UpdateUI() is public in PlayerManager
        pm.UpdateUI();
    }
}
