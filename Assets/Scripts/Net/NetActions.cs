using UnityEngine;
using Unity.Networking.Transport;

public class NetActions : MonoBehaviour
{
    // Multiplayer Logic:
    public static int playerCount = -1;
    public static int currentTeam = -1;

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

        // Receive turn taken, broadcast back
        Server.Instance.Broadcast(msg);
        
    }

    private void OnShareShipsServer(NetMessage msg, NetworkConnection cnn)
    {
        // Receive message, broadcast back
        NetShareShips ss = msg as NetShareShips;

        Debug.Log($"Should be receiving coordinate ({ss.xcoord}, {ss.ycoord}), with ship number {ss.shipNum} and orientation number {ss.orientation}.");
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
    }

    private void OnSetupPhaseServer(NetMessage obj, NetworkConnection cnn)
    {
        // Activate boards for both players
        GameUI.Instance.ServerToSetupScreen();
    }

    private void OnTakeTurnClient(NetMessage msg)
    {
        NetTakeTurn tt = msg as NetTakeTurn;

        Debug.Log($"TT : {tt.teamID} : Attack at grid location ({tt.targetLocationY}, {tt.targetLocationX}) resulted in {tt.targetStatus}");

        if(tt.teamID != currentTeam)
        {
            hitherormiss.Instance.hitplr(tt.teamID, tt.targetLocationY, tt.targetLocationX);
        }
    }

    private void OnShareShipsClient(NetMessage msg)
    {
        // Receive message, broadcast back
        NetShareShips ss = msg as NetShareShips;

        Debug.Log($"Should be receiving coordinate ({ss.xcoord}, {ss.ycoord}), with ship number {ss.shipNum} and orientation number {ss.orientation}.");
    }
#endregion
}