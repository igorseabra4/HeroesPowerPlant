namespace HeroesPowerPlant.LayoutEditor
{
    public class Object04_CraneWallLight : SetObjectHeroes
    {
        public short RotSpeed
        {
            get => ReadShort(4);
            set => Write(4, value);
        }
    }
}
