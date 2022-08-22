using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0190_Partner : SetObjectShadow
    {
        public enum EPartner
        {
            None = 0x00,
            Sonic = 0x01,
            Tails = 0x02,
            Knuckles = 0x03,
            Amy = 0x04,
            Rouge = 0x05,
            Omega = 0x06,
            Vector = 0x07,
            Espio = 0x08,
            Maria = 0x09,
            Charmy = 0x0A,
            Eggman = 0x0B,
            DoomsEye = 0x0C
        }

        public EPartner Partner { get; set; }
        public float DetectRange { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            Partner = (EPartner)reader.ReadInt32();
            DetectRange = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)Partner);
            writer.Write(DetectRange);
        }
    }
}
