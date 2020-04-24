namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0066_TriggerBobJump : SetObjectHeroes
    {
        public float Width
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float Height
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float RunDistance
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float JumpDistance
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }
    }
}