using UnityEngine;
using Unity.Networking.Transport;

public class NetTakeTurn : NetMessage
{
    public int targetLocationX;
    public int targetLocationY;
    public int targetStatus;
    public int teamID;

    public NetTakeTurn() // Making the message
    {
        // OpCode 1
        Code = OpCode.TAKE_TURN;
    }

    public NetTakeTurn(DataStreamReader reader) // Receiving the message
    {
        // OpCode 1
        Code = OpCode.TAKE_TURN;
        // "Unpacks" the message (the OpCode)
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(targetLocationX);
        writer.WriteInt(targetLocationY);
        writer.WriteInt(targetStatus);
        writer.WriteInt(teamID);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        targetLocationX = reader.ReadInt();
        targetLocationY = reader.ReadInt();
        targetStatus = reader.ReadInt();
        teamID = reader.ReadInt();
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_TAKE_TURN?.Invoke(this);
    }

    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_TAKE_TURN?.Invoke(this, cnn);
    }
}