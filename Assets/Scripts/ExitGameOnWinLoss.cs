using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameOnWinLoss : MonoBehaviour
{
    public void OnExitGameButton(){
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Game is exiting");
    #endif
    }
}
