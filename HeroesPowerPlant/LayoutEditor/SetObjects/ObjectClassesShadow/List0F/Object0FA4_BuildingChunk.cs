namespace HeroesPowerPlant.LayoutEditor {
    public class Object0FA4_BuildingChunk : SetObjectShadow {
        //CityGadget(Type,ScaleX,ScaleY,ScaleZ)
        public int Model {
            get => ReadInt(0);
            set => Write(0, value);
        }
        public float ScaleX {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float ScaleY {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float ScaleZ {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
