using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { set; get; }

    [SerializeField] private Animator menuAnimator;

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
        menuAnimator.SetTrigger("HostMenu");
    }

    public void OnJoinGameButton()
    {
        Debug.Log("OnJoinGameButton");
        menuAnimator.SetTrigger("JoinMenu");
    }

    public void OnCreditsButton()
    {
        Debug.Log("OnCreditsButton");
    }

    public void JoinGameButton()
    {
        Debug.Log("JoinGameButton");
    }

    public void JoinMenuConnectButton()
    {
        Debug.Log("JoinMenuConnectButton");
    }

    public void BackToMainMenuButton()
    {
        Debug.Log("BackToMainMenuButton");
        menuAnimator.SetTrigger("StartMenu");
    }

    // Exit button (will be used throughout game for exiting to desktop/quiting player in Unity Editor)
    public void OnExitGameButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Game is exiting");
#endif
    }
}

