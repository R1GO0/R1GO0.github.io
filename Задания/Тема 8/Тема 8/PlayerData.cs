using System;
using System.Collections.Generic;

[Serializable]
public class GameResult
{
    public DateTime Date { get; set; }
    public int Bet { get; set; }
    public int SelectedCockroach { get; set; }
    public int Winner { get; set; }
    public int Profit { get; set; } // Положительное - выигрыш, отрицательное - проигрыш
}

[Serializable]
public class Player
{
    public string Login { get; set; }
    public int Balance { get; set; }
    public List<GameResult> History { get; set; }

    public Player(string login)
    {
        Login = login;
        Balance = 1000; // Стартовый капитал
        History = new List<GameResult>();
    }
}