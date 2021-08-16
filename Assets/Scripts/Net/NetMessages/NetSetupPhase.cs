using UnityEngine;
using Unity.Networking.Transport;

public class NetSetupPhase : NetMessage
{

    public NetSetupPhase()
    {
       Code = OpCode.SETUP_PHASE;
    }

    public NetSetupPhase(DataStreamReader reader)
    {
        Code = OpCode.SETUP_PHASE;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        // Already read the byte in NetUtility
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_SETUP_PHASE?.Invoke(this);
    }

    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_SETUP_PHASE?.Invoke(this, cnn);
    }
}
