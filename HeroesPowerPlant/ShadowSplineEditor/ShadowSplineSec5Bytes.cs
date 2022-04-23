namespace HeroesPowerPlant.ShadowSplineEditor {
    public class ShadowSplineSec5Bytes {
        public byte slot1 { get; set; }
        public byte slot2 { get; set; }
        public bool noSlot2 { get; set; }

        public override string ToString()
        {
            return $"S1: {slot1} | S2: {slot2} | noS2: {noSlot2}";
        }
    }
}