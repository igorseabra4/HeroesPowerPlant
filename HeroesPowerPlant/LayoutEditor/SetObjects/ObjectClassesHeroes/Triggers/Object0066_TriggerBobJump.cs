namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0066_TriggerBobJump : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        [MiscSetting]
        public float Width { get; set; }
        [MiscSetting]
        public float Height { get; set; }
        [MiscSetting]
        public float RunDistance { get; set; }
        [MiscSetting]
        public float JumpDistance { get; set; }
    }
}