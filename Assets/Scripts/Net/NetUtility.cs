using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Networking.Transport;

public enum OpCode
{
    KEEP_ALIVE = 1,
    WELCOME = 2,
    SETUP_SHIPS = 3,
    START_GAME = 4,
    TAKE_TURN = 5,
    REMATCH = 6
}

public static class NetUtility
{
    // All net messages:
    public static Action<NetMessage> C_KEEP_ALIVE;
    public static Action<NetMessage> C_WELCOME;
    public static Action<NetMessage> C_SETUP_SHIPS;
    public static Action<NetMessage> C_START_GAME;
    public static Action<NetMessage> C_TAKE_TURN;
    public static Action<NetMessage> C_REMATCH;
    public static Action<NetMessage, NetworkConnection> S_KEEP_ALIVE;
    public static Action<NetMessage, NetworkConnection> S_WELCOME;
    public static Action<NetMessage, NetworkConnection> S_SETUP_SHIPS;
    public static Action<NetMessage, NetworkConnection> S_START_GAME;
    public static Action<NetMessage, NetworkConnection> S_TAKE_TURN;
    public static Action<NetMessage, NetworkConnection> S_REMATCH;
}
