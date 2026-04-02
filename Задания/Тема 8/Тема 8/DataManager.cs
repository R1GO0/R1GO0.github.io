using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class DataManager
{
    private static readonly string FileName = "players.dat";

    public static Dictionary<string, Player> LoadPlayers()
    {
        if (!File.Exists(FileName))
            return new Dictionary<string, Player>();

        try
        {
            using (FileStream fs = new FileStream(FileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, Player>)formatter.Deserialize(fs);
            }
        }
        catch (Exception)
        {
            return new Dictionary<string, Player>();
        }
    }

    public static void SavePlayers(Dictionary<string, Player> players)
    {
        using (FileStream fs = new FileStream(FileName, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, players);
        }
    }
}