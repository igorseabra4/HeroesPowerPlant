namespace HeroesPowerPlant.LayoutEditor
{
    class Object1107_CrackedWall : SetObjectHeroes
    {
        public int ModelNumber
        {
            get => ReadInt(4);
            set => Write(value, 4);
        }
    }
}
