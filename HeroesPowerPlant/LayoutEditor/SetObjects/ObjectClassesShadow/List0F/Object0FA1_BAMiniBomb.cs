using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0FA1_BAMiniBomb : SetObjectShadow
    {
        //CityBombSmall(Range point)
        public float DetectRange { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            DetectRange = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(DetectRange);
        }
    }
}
