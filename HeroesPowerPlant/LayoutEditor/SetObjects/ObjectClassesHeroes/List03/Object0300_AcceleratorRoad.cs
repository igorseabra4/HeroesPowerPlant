using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0300_AcceleratorRoad : SetObjectHeroes
    {
        public byte ObjectType { get; set; }
        public float Speed { get; set; }
        public float ColliZAdd { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ObjectType = reader.ReadByte();
            reader.BaseStream.Position += 3;
            Speed = reader.ReadSingle();
            ColliZAdd = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ObjectType);
            writer.Pad(3);
            writer.Write(Speed);
            writer.Write(ColliZAdd);
        }
    }
}