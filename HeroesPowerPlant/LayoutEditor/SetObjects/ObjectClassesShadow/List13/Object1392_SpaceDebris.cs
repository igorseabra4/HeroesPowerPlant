namespace HeroesPowerPlant.LayoutEditor {
    public class Object1392_SpaceDebris : SetObjectShadow {
        //SpaceGadget(RotDegSpdX,RotDegSpdY,RotDegSpdZ)
        public SpaceDebrisModel ModelType {
            get => (SpaceDebrisModel)ReadInt(0);
            set => Write(0, (int)value);
        }
        public float RotDegSpdX {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float RotDegSpdY {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float RotDegSpdZ {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }

    public enum SpaceDebrisModel {
        Model0,
        Model1,
        Model2,
        Model3,
        Model4,
        Model5
    }
}
