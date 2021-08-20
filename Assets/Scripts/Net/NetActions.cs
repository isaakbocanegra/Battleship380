using UnityEngine;
using Unity.Networking.Transport;

public class NetActions : MonoBehaviour
{
    // Multiplayer Logic:
    public static int playerCount = -1;
    public static int currentTeam = -1;
    public static placeship sendShip = new placeship();

    private void Awake()
    {
        RegisterEvents();
    }

#region    
    private void RegisterEvents()
    {
        NetUtility.S_WELCOME += OnWelcomeServer;
        NetUtility.C_WELCOME += OnWelcomeClient;

        NetUtility.S_SETUP_PHASE += OnSetupPhaseServer;
        NetUtility.C_SETUP_PHASE += OnSetupPhaseClient;

        NetUtility.S_TAKE_TURN += OnTakeTurnServer;
        NetUtility.C_TAKE_TURN += OnTakeTurnClient;

        NetUtility.S_SHARE_SHIPS += OnShareShipsServer;
        NetUtility.C_SHARE_SHIPS += OnShareShipsClient;
    }

    private void UnRegisterEvents()
    {
        NetUtility.S_WELCOME -= OnWelcomeServer;
        NetUtility.C_WELCOME -= OnWelcomeClient;

        NetUtility.S_SETUP_PHASE -= OnSetupPhaseServer;
        NetUtility.C_SETUP_PHASE -= OnSetupPhaseClient;

        NetUtility.S_TAKE_TURN -= OnTakeTurnServer;
        NetUtility.C_TAKE_TURN -= OnTakeTurnClient;

        NetUtility.S_SHARE_SHIPS -= OnShareShipsServer;
        NetUtility.C_SHARE_SHIPS -= OnShareShipsClient;
    }

    // Server
    private void OnWelcomeServer(NetMessage msg, NetworkConnection cnn)
    {
        // Client has connected, assign a team (team 1) and return message back to client
        NetWelcome nw = msg as NetWelcome;

        // Assign the team
        nw.AssignedTeam = ++playerCount;

        // Return value back to client
        Server.Instance.SendToClient(cnn, nw);

        // If full, move to setup phase
        if(playerCount == 1)
        {
            Server.Instance.Broadcast(new NetSetupPhase());
            GameUI.Instance.BoardCameraChange();
        }
    }

    private void OnTakeTurnServer(NetMessage msg, NetworkConnection cnn)
    {
        // Receive message, broadcast back
        NetTakeTurn tt = msg as NetTakeTurn;

        Debug.Log($"TT : Receiving attack at grid location ({tt.targetLocationX}, {tt.targetLocationY}).");
    }

    private void OnShareShipsServer(NetMessage msg, NetworkConnection cnn)
    {
        // Receive message, broadcast back
        NetShareShips ss = msg as NetShareShips;
     
        Debug.Log($"Should be receiving coordinate ({ss.xcoord}, {ss.ycoord}), with ship number {ss.shipNum} and orientation number {ss.orientation}.");

        sendShip.receivedShipNowPlace(ss.xcoord, ss.ycoord, ss.shipNum, ss.orientation);
    }
    
    // Client
    private void OnWelcomeClient(NetMessage msg)
    {
        // Receive connection message
        NetWelcome nw = msg as NetWelcome;

        // Assign the team
        currentTeam = nw.AssignedTeam;

        Debug.Log($"My assigned team is {nw.AssignedTeam}");
    }

    private void OnSetupPhaseClient(NetMessage obj)
    {
        // Activate boards for both players
        GameUI.Instance.ActivateBoards();
        if(NetActions.currentTeam == 0)
        {
            GameUI.Instance.DestroyP2GridMouseActionsP1();
        }
        else if(NetActions.currentTeam == 1)
        {
            GameUI.Instance.DestroyP1GridMouseActionsP2();
        }
    }

    private void OnSetupPhaseServer(NetMessage obj, NetworkConnection cnn)
    {
        // Activate boards for both players
        GameUI.Instance.ServerToSetupScreen();
    }

    private void OnTakeTurnClient(NetMessage msg)
    {
        NetTakeTurn tt = msg as NetTakeTurn;

        Debug.Log($"TT : Receiving attack at grid location ({tt.targetLocationX}, {tt.targetLocationY}).");

        /*if(tt.teamID != currentTeam)
        {
            hitherormiss.Instance.hitplr(tt.teamID, tt.targetLocationY, tt.targetLocationX);
        }*/
    }

    private void OnShareShipsClient(NetMessage msg)
    {
        // Receive message, broadcast back
        NetShareShips ss = msg as NetShareShips;

        Debug.Log($"Should be receiving coordinate ({ss.xcoord}, {ss.ycoord}), with ship number {ss.shipNum} and orientation number {ss.orientation}.");

        sendShip.receivedShipNowPlace(ss.xcoord, ss.ycoord, ss.shipNum, ss.orientation);
    }
#endregion
}