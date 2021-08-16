using UnityEngine;
using Unity.Networking.Transport;

public enum CameraAngles
{
    menu = 0,
    setup = 1,
    gameplay = 2,
}

public class NetActions : MonoBehaviour
{
    // Multiplayer Logic:
    public static int playerCount = -1;
    private static int currentTeam = -1;

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
    }

    private void UnRegisterEvents()
    {

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
#endregion
}