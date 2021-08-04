using System;
using Unity.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

public class Server : MonoBehaviour
{
    #region Singleton implementation
    public static Server Instance { set; get; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public NetworkDriver driver;
    private NativeList<NetworkConnection> connections;

    private bool isActive = false;
    private const float keepAliveTickRate = 20.0f; //default 30 second w/out update causes timeout, disconnecting the client, this stops that from happening by sending a message every 20 seconds
    private float lastKeepAlive;

    public Action connectionDropped;

    /* Methods be here beyond this point, brace yourself for networking boogaloo */

    // Method for starting the server (clicking on Host Game)
    public void Init(ushort port)
    {
        driver = NetworkDriver.Create();
        NetworkEndPoint endpoint = NetworkEndPoint.AnyIpv4;
        endpoint.Port = port;

        // Checks if able to connect to port, returns debug error if doesn't work, else returns "binded to port"
        if(driver.Bind(endpoint) != 0)
        {
            Debug.Log("Unable to bind to the port " + endpoint.Port);
            return;
        }
        else
        {
            driver.Listen();
            Debug.Log("Binded to port " + endpoint.Port);
        }

        //Max number of players (host and client), is set to 2, because only two people will be playing at a time in a given match of Battleship
        connections = new NativeList<NetworkConnection>(2, Allocator.Persistent);
        isActive = true;
    }

    // Method for closing the server if the host exits / backs out of Host Menu
    public void Shutdown()
    {
        // Checks for if server is active
        if(isActive)
        {
            // Trashes driver and any connections, as well as setting the check for server to false, indicating server is down
            driver.Dispose();
            connections.Dispose();
            isActive = false;
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

        //KeepAlive();

        driver.ScheduleUpdate().Complete();

        CleanupConnections();
        AcceptNewConnections();
        UpdateMessagePump();
    }

    private void CleanupConnections()
    {
        // while i is less than current connections
        for(int i = 0; i < connections.Length; i++)
        {
            // if connections are dropped
            if(!connections[i].IsCreated)
            {
                //remove connections, minus i by 1 to fix the loop
                connections.RemoveAtSwapBack(i);
                --i;
            }
        }
    }

    private void AcceptNewConnections()
    {
        // Accepts new connections
        NetworkConnection c;
        // Checks that the connections is actually another person, and not the default
        while((c = driver.Accept()) != default(NetworkConnection))
        {
            connections.Add(c);
        }
    }

    private void UpdateMessagePump()
    {
        DataStreamReader stream;
        for(int i = 0; i < connections.Length; i++)
        {
            NetworkEvent.Type cmd;
            while((cmd = driver.PopEventForConnection(connections[i], out stream)) != NetworkEvent.Type.Empty)
            {
                if(cmd == NetworkEvent.Type.Data)
                {
                    // NetUtility.OnData(stream, connections[i], this);
                }
                else if(cmd == NetworkEvent.Type.Disconnect)
                {
                    Debug.Log("Player 2 disconnected from the server");
                    connections[i] = default(NetworkConnection);
                    connectionDropped?.Invoke();
                    Shutdown();
                }
            }
        }
    }

    // Server specific methods

    public void SendToClient(NetworkConnection connection, NetMessage msg)
    {
        DataStreamWriter writer;
        driver.BeginSend(connection, out writer);
        // msg.Serialize(ref writer);
        driver.EndSend(writer);
    }

    public void Broadcast(NetMessage msg)
    {
        for(int i = 0; i < connections.Length; i++)
        {
            if(connections[i].IsCreated)
            {
                // Debug.Log($"Sending {msg.Code} to : {connections[i].InternalId}");
                SendToClient(connections[i], msg);
            }
        }
    }
}
