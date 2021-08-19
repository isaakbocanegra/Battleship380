using UnityEngine;
using Unity.Networking.Transport;

public class NetShareShips : NetMessage
{
    public int xcoord;
    public int ycoord;
    public int teamID;
    public int shipNum;
    public int orientation;

    public NetShareShips()
    {
       Code = OpCode.SHARE_SHIPS;
    }

    public NetShareShips(DataStreamReader reader)
    {
        Code = OpCode.SHARE_SHIPS;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(xcoord);
        writer.WriteInt(ycoord);
        writer.WriteInt(teamID);
        writer.WriteInt(shipNum);
        writer.WriteInt(orientation);
    }
    

    public override void Deserialize(DataStreamReader reader)
    {
        // Already read the byte in NetUtility
        xcoord = reader.ReadInt();
        ycoord = reader.ReadInt();
        teamID = reader.ReadInt();
        shipNum = reader.ReadInt();
        orientation = reader.ReadInt();
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_SHARE_SHIPS?.Invoke(this);
    }

    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_SHARE_SHIPS?.Invoke(this, cnn);
    }
}
