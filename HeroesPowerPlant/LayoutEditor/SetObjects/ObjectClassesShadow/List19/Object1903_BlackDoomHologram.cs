using System.ComponentModel;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1903_BlackDoomHologram : SetObjectShadow
    {
        //BDHologram

        [Description("Distance (straight line) from player to object\nWhen met, the hologram disappears.")]
        public float DetectDistance { get; set; }
        public int VoiceID { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            DetectDistance = reader.ReadSingle();
            VoiceID = reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(DetectDistance);
            writer.Write(VoiceID);
        }
    }
}
