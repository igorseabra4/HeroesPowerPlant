using static HeroesPowerPlant.LayoutEditor.Object0014_GoalRing;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0015_BallSwitch : SetObjectShadow
    {
        public enum EActivateType
        {
            OnOff = 0,
            OnTouch = 1,
            OnAlways = 2,
            Decoration = 3
        }

        public EActivateType ActivateType { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            ActivateType = (EActivateType)reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)ActivateType);
        }
    }

}

