namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0282_KameWave : SetObjectHeroes
    {
        public byte KameWaveType
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public float Speed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Scale
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
