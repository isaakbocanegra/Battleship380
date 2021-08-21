using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { set; get; }

    public Server server;
    public Client client;
    public Button submitButton;
    public bool P1AllShipsPlaced = false;
    public bool P2AllShipsPlaced = false;
    public int hitOrMissSceneChangeLooper = 0;
    public int looperInsideHitOrMissLooper = 0;
    
    [SerializeField] private Animator menuAnimator;
    [SerializeField] private InputField addressInput;

    // Wakey-wakey, game starty
    private void Awake(){
        Instance = this;
        RegisterEvents();
    }

    public void Update()
    {
        if(placeship.localShipCount == 5)
        {
            P1AllShipsPlaced = true;
        }
        if(placeship.ship1P2 && placeship.ship2P2 && placeship.ship3P2 && placeship.ship4P2 && placeship.ship5P2)
        {
            P2AllShipsPlaced = true;
        }
        if(P1AllShipsPlaced && P2AllShipsPlaced)
        {
            HitOrMissStart();
        }
    }

    // Activate Boards for P1 and P2
    public void ActivateBoards(){
        submitButton = GameObject.Find("SubmitShipPlacements").GetComponent<Button>();
        Player1Board.p1BoardParent.SetActive(true);
        Player2Board.p2BoardParent.SetActive(true);
        
        submitButton.interactable = false;

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

    public void HitOrMissStart()
    {
        print("should only print once");
        while(hitOrMissSceneChangeLooper < 1)
        {
            GameObject temp;
            // moves P2s board to main cam
            for (int j = 0; j < 8; j++){
                for (int k = 0; k < 8; k++){
                    temp = GameObject.Find("Player2BoardParent/X:"+j+", Y"+k);
                    temp.transform.position = new Vector2(temp.transform.position.x-15.48f,temp.transform.position.y);
                }
            }
            
            // moves each players main cam to in-game view
            if(NetActions.currentTeam == 0){
                Vector3 tempPos = Camera.main.transform.position;
                Debug.Log(Camera.main.transform.position.x);
                tempPos.x +=10.69f;
                tempPos.y -=1.21f;
                Camera.main.transform.position = tempPos;
                DestroyGridMouseActionsP1();
            }
            else if(NetActions.currentTeam == 1){
                print("Client should now move to gameplay scene");
                Vector3 tempPos = Camera.main.transform.position;
                Debug.Log(Camera.main.transform.position.x);
                tempPos.x -=23.14f;
                tempPos.y -=1.22f;
                Camera.main.transform.position = tempPos;
                
                while(looperInsideHitOrMissLooper < 1){
                    P2AfterSubmitShips();
                    moveP1AircraftOutTheWay();
                    looperInsideHitOrMissLooper++;
                }

                DestroyGridMouseActionsP2();
            }
            // moves P2s ships with P2 board to in-game view
            //P2AfterSubmitShips();
            // switch video player background
            
            
            menuAnimator.SetTrigger("InGame");
            // resizing main cam
            Camera.main.orthographicSize = 7.128989f;

            hitOrMissSceneChangeLooper++;
            placeship.localShipCount++;
            P1AllShipsPlaced = false;
        }
    }

    public void moveP1AircraftOutTheWay(){
        GameObject temp;
        temp = GameObject.Find("AllShips");
        temp.transform.position = new Vector2(temp.transform.position.x-5f, temp.transform.position.y);
    }


    public void DestroyP2GridMouseActionsP1()
    {
        GameObject temp2;

        for (int i = 0; i < 8; i++){
            for (int j = 0; j < 8; j++){
                temp2 = GameObject.Find("Player2BoardParent/X:"+i+", Y"+j);
                Destroy(temp2.GetComponent<GridMouseP2>());
            }
        }
    }

    public void DestroyP1GridMouseActionsP2()
    {
        GameObject temp;

        for (int i = 0; i < 8; i++){
            for (int j = 0; j < 8; j++){
                temp = GameObject.Find("Player1BoardParent/X:"+i+", Y"+j);
                Destroy(temp.GetComponent<GridMouseP1>());
            }
        }
    }

    //destroy gridmouse action then add phase2 gridmouse actions
    public void DestroyGridMouseActionsP1(){
        GameObject temp, temp2;

        for (int i = 0; i < 8; i++){
            for (int j = 0; j < 8; j++){
                temp = GameObject.Find("Player2BoardParent/X:"+i+", Y"+j);
                temp2 = GameObject.Find("Player1BoardParent/X:"+i+", Y"+j);
                Destroy(temp.GetComponent<GridMouseP2>());
                Destroy(temp2.GetComponent<GridMouseP1>());

                if(NetActions.currentTeam == 0){
                    temp.AddComponent<GridMousephase2>();
                }
            }
        }
    }

    public void DestroyGridMouseActionsP2(){
        GameObject temp, temp2;

        for (int i = 0; i < 8; i++){
            for (int j = 0; j < 8; j++){
                temp = GameObject.Find("Player1BoardParent/X:"+i+", Y"+j);
                temp2 = GameObject.Find("Player2BoardParent/X:"+i+", Y"+j);
                Destroy(temp.GetComponent<GridMouseP1>());
                Destroy(temp2.GetComponent<GridMouseP2>());

                if(NetActions.currentTeam == 1){
                    temp.AddComponent<GridMouse2phase2>();
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

    // moving p2s shipPlacements to in-game view
    public void P2AfterSubmitShips(){
        GameObject temp;
        temp = GameObject.Find("P2_Ships");
        temp.transform.position = new Vector2(temp.transform.position.x-15.48f,temp.transform.position.y);
    }

    public void BackToMainMenuButton(){
        server.Shutdown();
        client.Shutdown();
        if(NetActions.playerCount > 0)
        {
            NetActions.playerCount -= 1;
        }
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
