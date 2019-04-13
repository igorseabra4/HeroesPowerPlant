namespace HeroesPowerPlant.LayoutEditor
{
    public class Object04_CraneWallLight : SetObjectManagerHeroes
    {
        public short RotSpeed
        {
            get => ReadShort(4);
            set => Write(4, value);
        }
    }
}
