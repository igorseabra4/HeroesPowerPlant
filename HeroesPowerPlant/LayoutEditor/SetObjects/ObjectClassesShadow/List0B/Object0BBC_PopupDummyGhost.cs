namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0BBC_PopupDummyGhost : SetObjectShadow
    {
        //ThreatObjHolder(Model, Range_Radius m)
        public int Model
        {
            get => ReadInt(0);
            set => Write(0, value);
        }

        public float DetectRadius
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }
}
