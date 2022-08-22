using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0013_Balloon : SetObjectShadow
    {
        public enum EBalloonType
        {
            TranslationOnceAndDisappear,
            TranslationLoop,
            Orbit
        }

        public EBalloonType BalloonType { get; set; }
        public EShadowItem ItemType { get; set; }
        public float SpeedDampAmount { get; set; }
        public float OrbitDistance { get; set; }
        public float TranslationX { get; set; }
        public float TranslationY { get; set; }
        public float TranslationZ { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            BalloonType = (EBalloonType)reader.ReadInt32();
            ItemType = (EShadowItem)reader.ReadInt32();
            SpeedDampAmount = reader.ReadSingle();
            OrbitDistance = reader.ReadSingle();
            TranslationX = reader.ReadSingle();
            TranslationY = reader.ReadSingle();
            TranslationZ = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)BalloonType);
            writer.Write((int)ItemType);
            writer.Write(SpeedDampAmount);
            writer.Write(OrbitDistance);
            writer.Write(TranslationX);
            writer.Write(TranslationY);
            writer.Write(TranslationZ);
        }
    }
}
