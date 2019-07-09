using Newtonsoft.Json;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class SetObjectShadow : SetObject
    {
        [JsonConstructor]
        public SetObjectShadow()
        {
            UnkBytes = new byte[8];

            objectManager = FindObjectManager();
            objectManager.MiscSettings = new byte[MiscSettingCount];
        }

        public SetObjectShadow(byte List, byte Type, Vector3 Position, Vector3 Rotation, byte Link, byte Rend, int MiscSettingCount, byte[] UnkBytes = null)
        {
            this.List = List;
            this.Type = Type;
            FindObjectEntry(LayoutEditorSystem.GetActiveObjectEntries());
            this.Position = Position;
            this.Rotation = Rotation;
            this.Link = Link;
            this.Rend = Rend;

            this.UnkBytes = UnkBytes ?? new byte[8];
            this.MiscSettingCount = MiscSettingCount;

            isSelected = false;

            objectManager = FindObjectManager();
            objectManager.MiscSettings = new byte[MiscSettingCount];
        }

        [JsonIgnore]
        public SetObjectManagerShadow objectManager;

        public override void CreateTransformMatrix()
        {
            objectManager.CreateTransformMatrix(Position, Rotation);

            boundingBox = objectManager.CreateBoundingBox(ModelNames, ModelMiscSetting);
            boundingBox.Maximum = (Vector3)Vector3.Transform(boundingBox.Maximum, objectManager.transformMatrix);
            boundingBox.Minimum = (Vector3)Vector3.Transform(boundingBox.Minimum, objectManager.transformMatrix);
        }

        public override void Draw(SharpRenderer renderer, bool drawEveryObject)
        {
            if (drawEveryObject || !DontDraw(renderer.Camera.GetPosition()))
                objectManager.Draw(renderer, ModelNames, ModelMiscSetting, isSelected);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            return objectManager.TriangleIntersection(r, ModelNames, ModelMiscSetting, initialDistance, out distance);
        }
        
        [JsonIgnore]
        public override SetObjectManager ObjectManager => objectManager;

        public override byte[] MiscSettings
        {
            get => objectManager.MiscSettings;
            set => objectManager.MiscSettings = value;
        }

        public override void FindNewObjectManager(bool replaceMiscSettings = true)
        {
            byte[] oldMiscSettings = MiscSettings;

            objectManager = FindObjectManager();

            if (replaceMiscSettings)
                MiscSettings = new byte[MiscSettingCount == -1 ? 0 : MiscSettingCount];
            else
                MiscSettings = oldMiscSettings;
        }

        private SetObjectManagerShadow FindObjectManager()
        {
            switch (List)
            {
                case 0x00:
                    switch (Type)
                    {
                        case 0x01: case 0x02: case 0x03: case 0x06: return new Object00_SpringShadow();
                        case 0x04: return new Object0004_DashRamp();
                        case 0x07: return new Object0007_Case();
                        case 0x0E: return new Object000E_Rocket();
                        case 0x0F: return new Object000F_Platform();
                        case 0x10: return new Object0010_Ring();
                        case 0x12: return new Object0012_ItemCapsule();
                        case 0x14: return new Object0014_GoalRing();
                        case 0x1F: return new SetObjectManagerShadow(); // warp hole
                        case 0x20: return new Object0020_Weapon();
                        case 0x4F: return new Object004F_Vehicle();
                        default: return new Object_ShadowEmpty();
                    }
                case 0x01:
                    switch (Type)
                    {
                        case 0x90: return new Object0190_Partner();
                        default: return new Object_ShadowEmpty();
                    }
                case 0x0B:
                    switch (Type)
                    {
                        case 0xBE: return new Object0BBE_Chao();
                        default: return new Object_ShadowEmpty();
                    }
                case 0x18:
                    switch (Type)
                    {
                        case 0x9E: return new Object189E_ARKDriftingPlat1();
                        default: return new Object_ShadowEmpty();
                    }
                case 0x25:
                    switch (Type)
                    {
                        case 0x88: return new Object2588_Decoration1();
                        case 0x89: return new Object2589_Destructable1();
                        default: return new Object_ShadowEmpty();
                    }
                default: return new Object_ShadowEmpty();
            }
        }
    }
}
