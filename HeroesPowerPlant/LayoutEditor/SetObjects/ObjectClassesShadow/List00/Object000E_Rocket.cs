using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000E_Rocket : SetObjectShadow
    {
        public float TravelAngle { get; set; }
        public float TravelDistance { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            TravelAngle = reader.ReadSingle();
            TravelDistance = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(TravelAngle);
            writer.Write(TravelDistance);
        }
    }
}
