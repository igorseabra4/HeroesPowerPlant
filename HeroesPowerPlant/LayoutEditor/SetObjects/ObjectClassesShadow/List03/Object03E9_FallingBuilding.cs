namespace HeroesPowerPlant.LayoutEditor
{
    public class Object03E9_FallingBuilding : SetObjectShadow
    {
        //FallingBuildingHolder obj

        public FallingBuildingType StructureType
        {
            get => (FallingBuildingType)ReadInt(0);
            set => Write(0, (int)value);
        }
    }

    public enum FallingBuildingType
    {
        Bridge15Angle = 0,
        Bridge45Angle = 1,
        Building = 2,
    }
}

