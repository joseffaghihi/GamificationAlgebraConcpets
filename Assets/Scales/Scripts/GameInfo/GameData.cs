using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameData
{
    //Save Function
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GameData.dat");

        GameInfo data = new GameInfo(); //For Data Input

        //Data to be saved

        //Stats
        Stats stats = new Stats();
        data.gamesWon = stats.GetWins();
        data.gamesLost = stats.GetLosses();
        data.lives = stats.GetLives();
        data.gold = stats.GetGold();
        data.trophy = stats.GetTrophyStatus();

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

            GameInfo data = (GameInfo)bf.Deserialize(file);

            file.Close(); //Close FileStream

            //Data to be loaded

            //Stats
            Stats stats = new Stats();
            stats.SetWins(data.gamesWon);
            stats.SetLosses(data.gamesLost);
            stats.SetLives(data.lives);
            stats.SetGold(data.gold);
            stats.SetTrophyStatus(data.trophy);
        }
    }
}


//Data to be saved/loaded
[Serializable]
class GameInfo
{
    public int gamesWon;
    public int gamesLost;
    public int lives;
    public int gold;
    public bool[] trophy = new bool[6]; //Stores the status of each trophy (number of trophies has to be adjusted if changed)
}