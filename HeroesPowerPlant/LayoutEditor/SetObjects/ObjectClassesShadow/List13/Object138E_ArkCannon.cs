namespace HeroesPowerPlant.LayoutEditor
{
    public class Object138E_ArkCannon : SetObjectShadow
    {
        public ArkCannonType Model
        {
            get => (ArkCannonType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public float DetectRange
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }

    public enum ArkCannonType
    {
        Ground, //GRAND
        Air // AIR
    }
}

