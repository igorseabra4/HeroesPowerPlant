using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object05_Spring : SetObjectHeroes
    {
        public float Power { get; set; }
        public float RotSpeed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Power = reader.ReadSingle();
            RotSpeed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Power);
            writer.Write(RotSpeed);
        }
    }
}