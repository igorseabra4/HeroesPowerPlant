using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0023_Chao : SetObjectHeroes
    {
        public float Radius { get; set; }
        public float AngularSpeed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Radius = reader.ReadSingle();
            AngularSpeed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Radius);
            writer.Write(AngularSpeed);
        }
    }
}