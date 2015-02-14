using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Stats
{
    public static Stats gameControl;

    //Game Data
    private static int gamesWon = 0;
    private static int gamesLost = 0;
    private static int lifes = 5;
    private static int gold = 0;
    private static bool[] trophy = new bool[6]; //Stores the status of each trophy (number of trophies has to be adjusted if changed)

    //Keep Track of current panel
    private static int currentLocation = 0;

    //***Accessor Functions***
    public int GetWins()
    {
        return gamesWon;
    }
    public int GetLosses()
    {
        return gamesLost;
    }

    public int GetLifes()
    {
        return lifes;
    }

    public int GetGold()
    {
        return gold;
    }

    public bool GetTrophyStatus(int trophyID)
    {
        return trophy[trophyID];
    }

    public int GetCurrentPanel()
    {
        return currentLocation;
    }

    //***Mutator Functions***
    public void AddWin()
    {
        gamesWon++;
    }

    public void AddLoss()
    {
        gamesLost++;
    }

    public void LostLife()
    {
        if (lifes > 0)
            lifes--;
        else
            Debug.Log("You have no more lifes.");
    }

    public void AddGold(int value)
    {
        gold += value;
    }

    public void SubtractGold(int value)
    {
        gold -= value;
    }

    public void SetTrophyStatus(int trophyID, bool trophyStatus)
    {
        trophy[trophyID] = trophyStatus;
    }

    public void SetCurrentPanel(int newLocation)
    {
        currentLocation = newLocation;
    }

    //Check for any trophy changes (if requirements are met) ***Needs to be changed for new Trophies or Rewards***
    public void CheckTrophyUnlock()
    {
        //Trophy #1 Conditions
        if (gamesWon > 0 && trophy[0] == false)
        {
            SetTrophyStatus(0, true); //trophy completed
            AddGold(50); //reward added
        }
    }

    //Save Function
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GameData.dat");

        GameData data = new GameData(); //For Data Input

        //Data to be saved
        data.gamesWon = gamesWon;
        data.gamesLost = gamesLost;
        data.lifes = lifes;
        data.gold = gold;
        data.trophy = trophy;

        bf.Serialize(file, data); //Save Data
        file.Close(); //Close FileStream
    }

    //Load Function
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/GameData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);

            GameData data = (GameData)bf.Deserialize(file);

            file.Close(); //Close FileStream

            //Data to be loaded
            gamesWon = data.gamesWon;
            gamesLost = data.gamesLost;
            lifes = data.lifes;
            gold = data.gold;
            trophy = data.trophy;
        }
    }
}

//Data to be saved/loaded
[Serializable]
class GameData
{
    public int gamesWon;
    public int gamesLost;
    public int lifes;
    public int gold;
    public bool[] trophy = new bool[6]; //Stores the status of each trophy (number of trophies has to be adjusted if changed)
}