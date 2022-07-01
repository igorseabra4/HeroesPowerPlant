namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1132_ElevatorPlatformColumn : SetObjectShadow
    {
        //Elevator(model, distance, speed, range)
        public int Model
        {
            get => ReadInt(0);
            set => Write(0, value);
        }
        public float TravelDistance
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float Speed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float DetectRange
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
