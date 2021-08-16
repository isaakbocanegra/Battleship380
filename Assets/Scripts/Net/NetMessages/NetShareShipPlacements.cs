using UnityEngine;
using Unity.Networking.Transport;

public class NetShareShipPlacements : NetMessage
{
    public int shipLocationX;
    public int shipLocationY;

    public NetShareShipPlacements()
    {
       Code = OpCode.SHARE_SHIPS;
    }

    public NetShareShipPlacements(DataStreamReader reader)
    {
        Code = OpCode.SHARE_SHIPS;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(shipLocationX);
        writer.WriteInt(shipLocationY);
    }
    

    public override void Deserialize(DataStreamReader reader)
    {
        // Already read the byte in NetUtility
        shipLocationX = reader.ReadInt();
        shipLocationY = reader.ReadInt();
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
