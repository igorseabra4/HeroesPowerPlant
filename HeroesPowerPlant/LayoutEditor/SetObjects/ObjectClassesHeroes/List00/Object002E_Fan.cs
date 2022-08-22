using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object002E_Fan : SetObjectHeroes
    {
        public enum EFanMode : byte
        {
            Normal = 0,
            Switchable = 1,
            Normal2 = 2,
            Switchable2 = 3
        }

        public float Scale { get; set; }
        public float HeightTriangleDive { get; set; }
        public float HeightDefault { get; set; }
        public float Power { get; set; }
        public EFanMode Mode { get; set; }
        public byte LinkID { get; set; }
        public float WindScale { get; set; }
        public bool IsInvisible { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Scale = reader.ReadSingle();
            HeightTriangleDive = reader.ReadSingle();
            HeightDefault = reader.ReadSingle();
            Power = reader.ReadSingle();
            Mode = (EFanMode)reader.ReadByte();
            LinkID = reader.ReadByte();
            reader.BaseStream.Position += 2;
            WindScale = reader.ReadSingle();
            IsInvisible = reader.ReadByteBool();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Scale);
            writer.Write(HeightTriangleDive);
            writer.Write(HeightDefault);
            writer.Write(Power);
            writer.Write((byte)Mode);
            writer.Write(LinkID);
            writer.Pad(2);
            writer.Write(WindScale);
            writer.Write((byte)(IsInvisible ? 1 : 0));
        }
    }
}