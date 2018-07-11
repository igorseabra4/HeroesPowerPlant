namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0066_TriggerBobJump : SetObjectManagerHeroes
    {
        public float Width
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float Height
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float RunDistance
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float JumpDistance
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }
    }
}