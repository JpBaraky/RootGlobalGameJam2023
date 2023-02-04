using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState{
        PAUSE,
        GAMEPLAY,
        GAMESTOP,

    }
    public GameState currentState = GameState.GAMEPLAY;
    void Start()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRate;
    }

 
}
