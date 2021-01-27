using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ModerrNetworking
{
    public class ModerrPacket
    {
        // Variables
        [JsonProperty("sender")]
        private PacketSender sender;
        [JsonProperty("id")]
        private int id;
        [JsonProperty("value")]
        private object value;

        // Constructor
        [JsonConstructor]
        public ModerrPacket(PacketSender sender, int id, object value)
        {
            this.sender = sender;
            this.id = id;
            this.value = value;
        }

        // Getters
        public PacketSender getPacketSender()
        {
            return sender;
        }
        public int getId()
        {
            return id;
        }
        public object getValue()
        {
            return value;
        }

        // Static serialize, deserialize
        public static string Serialize(ModerrPacket packet)
        {
            return JsonConvert.SerializeObject(packet);
        }
        public static ModerrPacket Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<ModerrPacket>(json);
            }
            catch
            {
                throw new Exception("ModerrPacketJson has been corrupted!");
            }
        }
        public byte[] SerializeToByte()
        {
            string s = Serialize(this);
            return Encoding.ASCII.GetBytes(s);
        }

    }

    public enum PacketSender
    {
        SERVER,CLIENT
    }
    public enum PacketId
    {
        UNKNOWN = 0,
        RESPONSE = 1
    }

}
