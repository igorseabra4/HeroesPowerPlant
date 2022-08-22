using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0005_Switch : SetObjectHeroes
    {
        public enum ESwitchType : byte
        {
            Alternate = 0,
            Touch = 1,
            Once = 2,
            Interlock = 3
        }

        public enum ESound : byte
        {
            Pi = 0,
            Pipori = 1
        }

        public ESwitchType SwitchType { get; set; }
        public bool Hidden { get; set; }
        public byte LinkIDforHidden { get; set; }
        public ESound Sound { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            SwitchType = (ESwitchType)reader.ReadByte();
            Hidden = reader.ReadByteBool();
            LinkIDforHidden = reader.ReadByte();
            Sound = (ESound)reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)SwitchType);
            writer.Write((byte)(Hidden ? 1 : 0));
            writer.Write(LinkIDforHidden);
            writer.Write((byte)Sound);
        }
    }
}