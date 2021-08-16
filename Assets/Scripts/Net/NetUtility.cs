using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Networking.Transport;

public enum OpCode
{
    KEEP_ALIVE = 1,
    WELCOME = 2,
    SETUP_PHASE = 3,
    START_GAME = 4,
    TAKE_TURN = 5,
    REMATCH = 6
    
}

public static class NetUtility
{
    public static void OnData(DataStreamReader stream, NetworkConnection cnn, Server server = null)
    {
        NetMessage msg = null;
        var opCode = (OpCode)stream.ReadByte();
        switch (opCode)
        {
            case OpCode.KEEP_ALIVE: msg = new NetKeepAlive(stream); break;
            case OpCode.WELCOME: msg = new NetWelcome(stream); break;
            case OpCode.SETUP_PHASE: msg = new NetSetupPhase(stream); break;
            case OpCode.START_GAME: msg = new NetStartGame(stream); break;
            //case OpCode.TAKE_TURN: msg = new NetTakeTurn(stream); break;
            //case OpCode.REMATCH: msg = new NetRematch(stream); break;
            default:
                Debug.LogError("Message received had no OpCode");
                break;
        }

        if(server != null)
        {
            msg.ReceivedOnServer(cnn);
        }
        else
        {
            msg.ReceivedOnClient();
        }
    }

    // All net messages:
    public static Action<NetMessage> C_KEEP_ALIVE;
    public static Action<NetMessage> C_WELCOME;
    public static Action<NetMessage> C_SETUP_PHASE;
    public static Action<NetMessage> C_START_GAME;
    public static Action<NetMessage> C_TAKE_TURN;
    public static Action<NetMessage> C_REMATCH;
    public static Action<NetMessage, NetworkConnection> S_KEEP_ALIVE;
    public static Action<NetMessage, NetworkConnection> S_WELCOME;
    public static Action<NetMessage, NetworkConnection> S_SETUP_PHASE;
    public static Action<NetMessage, NetworkConnection> S_START_GAME;
    public static Action<NetMessage, NetworkConnection> S_TAKE_TURN;
    public static Action<NetMessage, NetworkConnection> S_REMATCH;
}
