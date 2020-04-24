namespace HeroesPowerPlant.LayoutEditor
{
    public class Object11_MansionWallThunder : SetObjectHeroes
    {
        public int ModelNumber
        {
            get => ReadInt(4);
            set => Write(4, value);
        }
    }
}
