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
        RegisterEvents();
    }

    // Activate Boards for P1 and P2
    public void ActivateBoards()
    {
        Player1Board.p1BoardParent.SetActive(true);
        Player2Board.p2BoardParent.SetActive(true);
        if(NetActions.currentTeam == 1)
        {
            BoardCameraChange();
        }
    }

    public void ServerToSetupScreen()
    {
        BoardCameraChange();
    }

    // Buttons
    public void OnHostGameButton()
    {
        server.Init(8007);
        client.Init("127.0.0.1", 8007);
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

    public void BackToMainFromConnect()
    {
        server.Shutdown();
        client.Shutdown();
        Vector3 tempPos = Camera.main.transform.position;
        Debug.Log(Camera.main.transform.position.y);
        tempPos.y +=18f;
        Camera.main.transform.position = tempPos;
        Debug.Log(Camera.main.transform.position.y);
        Debug.Log("BackToMainMenuButton");
        menuAnimator.SetTrigger("StartMenu");
    }

    public void OnJoinConnect()
    {   
        client.Init(addressInput.text, 8007);
    }

    public void BoardCameraChange()
    {
        Vector3 tempPos = Camera.main.transform.position;
        Debug.Log(Camera.main.transform.position.y);
        tempPos.y -=18f;
        Camera.main.transform.position = tempPos;
        Debug.Log(Camera.main.transform.position.y);
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

#region    
    private void RegisterEvents()
    {
        NetUtility.C_SETUP_PHASE += OnSetupPhaseClient;
    }

    private void UnRegisterEvents()
    {
        NetUtility.C_SETUP_PHASE -= OnSetupPhaseClient;
    }

    private void OnSetupPhaseClient(NetMessage obj)
    {
        menuAnimator.SetTrigger("Connect");
    }
#endregion
}
/*
        Vector3 tempPos = Camera.main.transform.position;
        Debug.Log(Camera.main.transform.position.y);
        tempPos.y -=18f;
        Camera.main.transform.position = tempPos;
        Debug.Log(Camera.main.transform.position.y);*/