using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Common;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    [System.Serializable]
    public class GameData
    {
        public int Rows;
        public int Columns;
        public List<Card.CardData> LastGameCards;
        public int Score;
        public int Moves;
        public int Combo;
    }

    private string _savePath;

    new void Awake()
    {
        base.Awake();
        _savePath = Path.Combine(Application.persistentDataPath, "saveData.dat");
    }

    public void SaveGame(GameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(_savePath, FileMode.Create))
        {
            formatter.Serialize(stream, gameData);
        }
    }
    
    public GameData LoadGame()
    {
        if (File.Exists(_savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(_savePath, FileMode.Open))
            {
                return formatter.Deserialize(stream) as GameData;
            }
        }
        else return null;
        
    }
}
