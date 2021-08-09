using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour{
    public static GameOver Instance { set; get; }

    public void OnMainMenu(){
        SceneManager.LoadScene("TitleScene");
    }
    
    // Exit button (will be used throughout game for exiting to desktop/quiting player in Unity Editor)
    public void OnExitGameButton(){
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Game is exiting");
    #endif
    }
}
