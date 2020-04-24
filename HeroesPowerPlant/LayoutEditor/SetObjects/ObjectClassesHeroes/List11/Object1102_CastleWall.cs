namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1102_CastleWall : SetObjectHeroes
    {
        public int ModelNumber
        {
            get => ReadInt(4);
            set => Write(4, value);
        }

        public bool IsUpsideDown
        {
            get => ReadInt(8) != 0;
            set => Write(8, value ? 1 : 0);
        }
    }
}
