namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0065_GUNBeetle : Object_ShadowEmpty
    {
        public string Note => "Not all misc. settings are in list yet.";

        public int Attack
        {
            get => ReadInt(32);
            set => Write(32, value);
        }

        public float PatrolWidth
        {
            get => ReadInt(40);
            set => Write(40, value);
        }

        public int Type
        {
            get => ReadInt(56);
            set => Write(56, value);
        }

        public int Weapon
        {
            get => ReadInt(60);
            set => Write(60, value);
        }
    }
}

