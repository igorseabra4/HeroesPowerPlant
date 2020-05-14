namespace HeroesPowerPlant.LayoutEditor {
    public class Object13F1_HeavyBaseDoor : SetObjectShadow {
        //SetBaseDoor(type: normal/key, model, width, height, depth)
        public int int0 {
            get => ReadInt(0);
            set => Write(0, value);
        }
        public int int1 {
            get => ReadInt(4);
            set => Write(4, value);
        }
        public float float2 {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float float3 {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float float4 {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float float_unk5 {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float float6 {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public float float7 {
            get => ReadFloat(28);
            set => Write(28, value);
        }
    }
}
