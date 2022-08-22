using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0C80_BounceBall : SetObjectShadow
    {
        public enum EAppearType
        {
            Always,
            AfterLinkIDCleared
        }

        //CircusBall [Horror/Circus skins] (type, level, nocontrol time (sec), angle)
        public EAppearType AppearType { get; set; }
        public float Strength { get; set; }
        public float NoControlTime { get; set; }
        public float Angle { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            AppearType = (EAppearType)reader.ReadInt32();
            Strength = reader.ReadSingle();
            NoControlTime = reader.ReadSingle();
            Angle = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)AppearType);
            writer.Write(Strength);
            writer.Write(NoControlTime);
            writer.Write(Angle);
        }
    }
}
