using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1188_Curtain : SetObjectHeroes
    {
        public enum ECurtainType : byte
        {
            Light = 0,
            Dark = 1,
        }

        public ECurtainType CurtainType { get; set; }
        public byte Pole { get; set; }
        public bool IsUpsideDown { get; set; }
        public float Scale { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            CurtainType = (ECurtainType)reader.ReadByte();
            Pole = reader.ReadByte();
            IsUpsideDown = reader.ReadByteBool();
            reader.ReadByte();
            Scale = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)CurtainType);
            writer.Write(Pole);
            writer.Write((byte)(IsUpsideDown ? 1 : 0));
            writer.Write(Scale);
        }
    }
}
