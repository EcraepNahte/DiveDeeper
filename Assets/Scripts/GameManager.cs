using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int Score { get; set; }
    private int HighScore { get; set; }
    private int GoldForRun { get; set; }
    private int GoldInWallet { get; set; }
}
