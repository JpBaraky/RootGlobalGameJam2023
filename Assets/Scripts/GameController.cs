using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    void Start()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRate;
    }

 
}
