using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object189E_ARKDriftingPlat1 : SetObjectShadow
    {
        public enum EType
        {
            Burst,
            Hover
        }

        //FootingBreak(Type{Burst,Hover}, WaitSec, HoverLength point, HoverSec, MoveLengthX point, MoveLengthY point, MoveLengthZ point, MoveSec)
        public EType PlatformType { get; set; }
        public float ExplosionDelay { get; set; }
        public float HoverLength { get; set; }
        public float HoverSec { get; set; }
        public float TranslationX { get; set; }
        public float TranslationY { get; set; }
        public float TranslationZ { get; set; }
        public float TravelTime { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            PlatformType = (EType)reader.ReadInt32();
            ExplosionDelay = reader.ReadSingle();
            HoverLength = reader.ReadSingle();
            HoverSec = reader.ReadSingle();
            TranslationX = reader.ReadSingle();
            TranslationY = reader.ReadSingle();
            TranslationZ = reader.ReadSingle();
            TravelTime = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)PlatformType);
            writer.Write(ExplosionDelay);
            writer.Write(HoverLength);
            writer.Write(HoverSec);
            writer.Write(TranslationX);
            writer.Write(TranslationY);
            writer.Write(TranslationZ);
            writer.Write(TravelTime);
        }
    }
}
