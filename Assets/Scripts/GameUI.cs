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
    private void Awake(){
        Instance = this;
        RegisterEvents();
    }

    // Activate Boards for P1 and P2
    public void ActivateBoards(){
        Player1Board.p1BoardParent.SetActive(true);
        Player2Board.p2BoardParent.SetActive(true);
        if(NetActions.currentTeam == 1){
            BoardCameraChange();
        }
    }

    public void ServerToSetupScreen(){
        BoardCameraChange();
    }

    /*********** Buttons **********/
    public void OnHostGameButton(){
        server.Init(8007);
        client.Init("127.0.0.1", 8007);
        Debug.Log("OnHostGameButton");
        menuAnimator.SetTrigger("HostMenu");
    }

    // "join game" from main
    public void OnJoinGameButton(){
        Debug.Log("OnJoinGameButton");
        menuAnimator.SetTrigger("JoinMenu");
    }

    // "cred" from main
    public void OnCreditsButton(){
        Player1Board.p1BoardParent.SetActive(true);
        Player2Board.p2BoardParent.SetActive(true);
        Debug.Log("OnCreditsButton");
    }

    public void BackToMainFromConnect(){
        server.Shutdown();
        client.Shutdown();
        // if player=1
        if(NetActions.currentTeam == 0){
            Vector3 tempPos = Camera.main.transform.position;
            Debug.Log(Camera.main.transform.position.x);
            tempPos.x -=21f;
            Camera.main.transform.position = tempPos;
            Debug.Log(Camera.main.transform.position.x);
        }
        // if player=2
        else if(NetActions.currentTeam == 1){
            Vector3 tempPos = Camera.main.transform.position;
            Debug.Log(Camera.main.transform.position.x);
            tempPos.x -= 54.8f;
            Camera.main.transform.position = tempPos;
            Debug.Log(Camera.main.transform.position.x);
        }
        Debug.Log("BackToMainMenuButton");
        menuAnimator.SetTrigger("StartMenu");
    }

    public void BackToMainFromPlayScreen(){
        server.Shutdown();
        client.Shutdown();
        // if player=1
        Vector3 tempPos = Camera.main.transform.position;
        Debug.Log(Camera.main.transform.position.x);
        tempPos.x -= 32.9f;
        Camera.main.transform.position = tempPos;
        Debug.Log(Camera.main.transform.position.x);
        Debug.Log("BackToMainMenuButton");
        menuAnimator.SetTrigger("StartMenu");
    }


    public void SubmitShipPlacements(){

        GameObject temp;
        for (int i = 0; i < 8; i++){
            for (int j = 0; j < 8; j++){
                temp = GameObject.Find("Player2BoardParent/X:"+i+", Y"+j);
                temp.transform.position = new Vector2(temp.transform.position.x-15,temp.transform.position.y);
            }
        }
        if(NetActions.currentTeam == 0){
            Vector3 tempPos = Camera.main.transform.position;
            Debug.Log(Camera.main.transform.position.x);
            tempPos.x +=11.9f;
            Camera.main.transform.position = tempPos;
            Debug.Log(Camera.main.transform.position.x);
            DestroyGridMouseActions();
        }
        else if(NetActions.currentTeam == 1){
            Vector3 tempPos = Camera.main.transform.position;
            Debug.Log(Camera.main.transform.position.x);
            tempPos.x -=21.9f;
            Camera.main.transform.position = tempPos;
            Debug.Log(Camera.main.transform.position.x);
        }
        // switch video player background
        menuAnimator.SetTrigger("InGame");

    }



    //destroy gridmouse action then add phase2 gridmouse actions
    public void DestroyGridMouseActions(){
        GameObject temp, temp2;

        for (int i = 0; i < 8; i++){
            for (int j = 0; j < 8; j++){   
                // disables mouseactions for shipplacement then enables mouseactions for in-game
                temp = GameObject.Find("Player1BoardParent/X:"+i+", Y"+j);
                temp2 = GameObject.Find("Player2BoardParent/X:"+i+", Y"+j);
                Destroy(temp.GetComponent<GridMouseP1>());
                Destroy(temp2.GetComponent<GridMouseP2>());

                if(NetActions.currentTeam == 0){
                    //Destroy(temp.GetComponent<GridMouseP1>());
                    temp2.AddComponent<GridMousephase2>();
                    //temp.AddComponent<GridMouseP1>();
                }
                else if(NetActions.currentTeam == 1){
                    //Destroy(temp2.GetComponent<GridMouseP2>());
                    temp.AddComponent<GridMousephase2>();
                }

            }
        }
    }

    public void OnJoinConnect()
    {   
        client.Init(addressInput.text, 8007);
    }

    public void BoardCameraChange(){
        if(NetActions.currentTeam == 0){
            Vector3 tempPos = Camera.main.transform.position;
            Debug.Log(Camera.main.transform.position.x);
            tempPos.x +=21f;
            Camera.main.transform.position = tempPos;
            Debug.Log(Camera.main.transform.position.x);
        }
        else if(NetActions.currentTeam == 1){
            Vector3 tempPos = Camera.main.transform.position;
            Debug.Log(Camera.main.transform.position.x);
            tempPos.x +=54.8f;
            Camera.main.transform.position = tempPos;
            Debug.Log(Camera.main.transform.position.x);
        }
    }

    public void BackToMainMenuButton(){
        server.Shutdown();
        client.Shutdown();
        NetActions.playerCount -= 1;
        Debug.Log("BackToMainMenuButton");
        menuAnimator.SetTrigger("StartMenu");
    }

    // Exit button (will be used throughout game for exiting to desktop/quiting player in Unity Editor)
    public void OnExitGameButton(){
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Game is exiting");
    #endif
    }

#region    
    private void RegisterEvents(){
        NetUtility.C_SETUP_PHASE += OnSetupPhaseClient;
    }

    private void UnRegisterEvents(){
        NetUtility.C_SETUP_PHASE -= OnSetupPhaseClient;
    }

    private void OnSetupPhaseClient(NetMessage obj){
        menuAnimator.SetTrigger("Connect");
    }
#endregion
}
