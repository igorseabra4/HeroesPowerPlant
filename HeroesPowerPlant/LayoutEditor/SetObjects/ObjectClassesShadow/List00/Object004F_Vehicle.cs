namespace HeroesPowerPlant.LayoutEditor
{
    public class Object004F_Vehicle : SetObjectShadow
    {
        public enum EVehicle : int
        {
            None = 0x00,
            Jeep = 0x01,
            SportsCar = 0x02,
            ArmoredCar = 0x03,
            Motorcycle = 0x04,
            JumpMech = 0x05,
            ArmedMech = 0x06,
            AirSaucer = 0x07,
            BlackHawk = 0x08,
            BlackVolt = 0x09,
            GUNTurret = 0x0A,
            BATurret = 0x0B,
            GUNLiftFast = 0x0C,
            GUNLiftSlow = 0x0D
        }

        [MiscSetting]
        public EVehicle Vehicle { get; set; }
    }
}
