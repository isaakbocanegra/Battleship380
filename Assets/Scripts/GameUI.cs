using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { set; get; }

    public Server server;
    public Client client;

    [SerializeField] private Animator menuAnimator;
    [SerializeField] private InputField addressInput;

    // Wakey-wakey, game starty
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    // Buttons
    public void OnHostGameButton()
    {
        server.Init(8007);
        // client.Init("127.0.0.1", 8007);
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

    public void JoinMenuConnectButton()
    {
        client.Init(addressInput.text, 8007);
    }

    public void BackToMainMenuButton()
    {
        server.Shutdown();
        client.Shutdown();
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

