namespace HeroesPowerPlant.LayoutEditor {
    public class Object07D7_DigitalBreakableTile : SetObjectShadow {
        //ElecCristalWall(type<Horizontal,Vertical>, AppearAngleX, AppearAngleY)

        public CommonDirectionType Type {
            get => (CommonDirectionType)ReadInt(0);
            set => Write(0, (int)value);
        }
        public float AppearAngleX {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float AppearAngleY {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
