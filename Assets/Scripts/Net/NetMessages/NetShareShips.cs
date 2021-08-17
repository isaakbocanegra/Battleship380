using UnityEngine;
using Unity.Networking.Transport;

public class NetShareShips : NetMessage
{
    public int shipLocationX;
    public int shipLocationY;
    public int teamID;
    public int shipsize;
    public string rowcolumn;

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
        writer.WriteInt(shipLocationX);
        writer.WriteInt(shipLocationY);
        writer.WriteInt(teamID);
        writer.WriteInt(shipsize);
        //writer.WriteSring(rowcolumn);
    }
    

    public override void Deserialize(DataStreamReader reader)
    {
        // Already read the byte in NetUtility
        shipLocationX = reader.ReadInt();
        shipLocationY = reader.ReadInt();
        teamID = reader.ReadInt();
        shipsize = reader.ReadInt();
        //rowcolumn = reader.ReadString();
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
