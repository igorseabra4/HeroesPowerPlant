namespace HeroesPowerPlant.LayoutEditor
{
    public class Object189E_ARKDriftingPlat1 : SetObjectManagerShadow
    {
        public int PlatformType
        {
            get { return ReadInt(0x0); }
            set { Write(0x0, value); }
        }

        public float ExplosionDelay
        {
            get { return ReadFloat(0x4); }
            set { Write(0x4, value); }
        }

        public float Bobbing1
        {
            get { return ReadFloat(0x8); }
            set { Write(0x8, value); }
        }

        public float Bobbing2
        {
            get { return ReadFloat(0xC); }
            set { Write(0xC, value); }
        }

        public float TranslationX
        {
            get { return ReadFloat(0x10); }
            set { Write(0x10, value); }
        }

        public float TranslationY
        {
            get { return ReadFloat(0x14); }
            set { Write(0x14, value); }
        }

        public float TranslationZ
        {
            get { return ReadFloat(0x18); }
            set { Write(0x18, value); }
        }
        public float TravelTime
        {
            get { return ReadFloat(0x1C); }
            set { Write(0x1C, value); }
        }
    }
}
