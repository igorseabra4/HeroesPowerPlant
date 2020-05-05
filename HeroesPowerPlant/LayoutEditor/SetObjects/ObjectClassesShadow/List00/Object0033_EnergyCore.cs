namespace HeroesPowerPlant.LayoutEditor {
    public class Object0033_EnergyCore : SetObjectShadow {
        public EnergyCoreType CoreType {
            get => (EnergyCoreType)ReadInt(0);
            set => Write(0, (int)value);
        }
    }
}

