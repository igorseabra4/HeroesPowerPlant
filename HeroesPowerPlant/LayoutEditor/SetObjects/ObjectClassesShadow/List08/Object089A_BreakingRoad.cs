namespace HeroesPowerPlant.LayoutEditor {
    public class Object089A_BreakingRoad : SetObjectShadow {
        //BreakRoad(RoadType)
        public int RoadType {
            get => ReadInt(0);
            set => Write(0, value);
        }
    }
}
