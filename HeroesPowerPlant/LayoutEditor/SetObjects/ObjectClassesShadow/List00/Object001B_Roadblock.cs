namespace HeroesPowerPlant.LayoutEditor {
    public class Object001B_Roadblock : SetObjectShadow {
        //BreakObj
        public RoadblockType RoadblockType {
            get => (RoadblockType)ReadInt(0);
            set => Write(0, (int)value);
        }
    }

    public enum RoadblockType {
        GUN,
        BlackArms
    }
}

