using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

public enum OpCode{
    KEEP_ALIVE = 1,
    WELCOME = 2,
    SETUP_SHIPS = 3,
    START_GAME = 4,
    TAKE_TURN = 5,
    REMATCH = 6
}

public class NetMessage 
{
    public OpCode Code { set; get; }

    public virtual void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }
    
    public virtual void Deserialize(DataStreamReader reader)
    {
        reader.ReadByte((byte)Code);
    }
    public virtual void ReceivedOnClient()
    {
        
    }
    public virtual void ReceivedOnServer(NetworkConnection cnn)
    {
        
    }
} 