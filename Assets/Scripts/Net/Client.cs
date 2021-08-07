using UnityEngine.UI;
using Unity.Networking.Transport;
using System;
using Unity.Collections;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Client : MonoBehaviour
{
    #region Singleton implementation
    public static Client Instance { set; get; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public NetworkDriver driver;
    private NetworkConnection connection;

    private bool isActive = false;

    public Action connectionDropped;
    public Text EnterIPText;

    // Method for starting the client (clicking on Join Game)
    public void Init(string ipAddress, ushort port)
    {
        driver = NetworkDriver.Create();
        NetworkEndPoint endpoint = NetworkEndPoint.Parse(ipAddress, port);
        endpoint.Port = port;
        connection = driver.Connect(endpoint);
        Debug.Log("Attempting to connect to host with IP: " + endpoint.Address);
        isActive = true;

        RegisterToEvent();
    }

    // Method for closing the server if the host exits / backs out of Host Menu
    public void Shutdown()
    {
        // Checks for if server is active
        if(isActive)
        {
            UnregisterToEvent();
            // Trashes driver and any connections, as well as setting the check for server to false, indicating server is down
            driver.Dispose();
            isActive = false;
            connection = default(NetworkConnection);
        }
    }

    // Force shuts down connections and whatnot on game close
    public void OnDestroy()
    {
        Shutdown();
    }

    public void Update()
    {
        if(!isActive)
        {
            return;
        }

        driver.ScheduleUpdate().Complete();
        CheckAlive();
        UpdateMessagePump();
    }

    private void CheckAlive()
    {
        if(!connection.IsCreated && isActive)
        {
            Debug.Log("Lost connection to server...");
            connectionDropped?.Invoke();
            Shutdown();
        }
    }

    private void UpdateMessagePump()
    {
        DataStreamReader stream;
        NetworkEvent.Type cmd;
        while((cmd = connection.PopEvent(driver, out stream)) != NetworkEvent.Type.Empty)
        {
            if(cmd == NetworkEvent.Type.Connect)
            {
                // SendToServer(new NetWelcome());
                Debug.Log("We're connected! Sweet.");
                EnterIPText.text = "You are connected to";
            }
            else if(cmd == NetworkEvent.Type.Data)
            {
                NetUtility.OnData(stream, default(NetworkConnection));
            }
            else if(cmd == NetworkEvent.Type.Disconnect)
            {
                Debug.Log("Client was disconnected from server...");
                EnterIPText.text = "You have disconnected from";
                connection = default(NetworkConnection);
                connectionDropped?.Invoke();
                Shutdown();
            }
        }
    }

    public void SendToServer(NetMessage msg)
    {
        DataStreamWriter writer;
        driver.BeginSend(connection, out writer);
        msg.Serialize(ref writer);
        driver.EndSend(writer);
    }

    // Event paring function
    private void RegisterToEvent()
    {
        NetUtility.C_KEEP_ALIVE += OnKeepAlive;
    }

    private void UnregisterToEvent()
    {
        NetUtility.C_KEEP_ALIVE -= OnKeepAlive;
    }

    private void OnKeepAlive(NetMessage nm)
    {
        // Send it back to keep both sides alive and from disconnecting due to timeout
        SendToServer(nm);
    }
}

/*public class CLient : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 4096;

    public string ip = "127.0.0.1";
    public int port = 8007;
}*/