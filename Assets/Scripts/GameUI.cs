using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { set; get; }

    // Wakey-wakey, game starty
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    // Buttons
    public void OnHostGameButton()
    {
        Debug.Log("OnHostGameButton");
    }

    public void OnJoinGameButton()
    {
        Debug.Log("OnJoinGameButton");
    }

    public void OnCreditsButton()
    {
        Debug.Log("OnCreditsButton");
    }

    public void OnExitGameButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Game is exiting");
#endif
    }
}

