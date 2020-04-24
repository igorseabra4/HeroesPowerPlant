using System.Linq;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0913_RainCollision : SetObjectHeroes
    {
        public float Scale
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public byte[] RainIDs
        {
            get => MiscSettings.Skip(8).Take(16).ToArray();
            set
            {
                for (int i = 0; i < 16; i++)
                    MiscSettings[i + 8] = value[i];
            }
        }
    }
}