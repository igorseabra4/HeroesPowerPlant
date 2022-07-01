namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1006_HealUnitServer : SetObjectShadow
    {
        //BombServer(Type)
        public BombServerServeType Serve
        {
            get => (BombServerServeType)ReadInt(0);
            set => Write(0, (int)value);
        }
    }

    public enum BombServerServeType
    {
        Bomb,
        HealUnit
    }
}
