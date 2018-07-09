using SharpDX;
using System.IO;
using System.Windows.Forms;

namespace HeroesPowerPlant.LayoutEditor
{
    public class SetObjectShadow : SetObject
    {
        public int MiscSettingCount;

        public SetObjectShadow() : this(new ObjectEntry(), Vector3.Zero, Vector3.Zero, 0, 10, 0) { }

        public SetObjectShadow(byte List, byte Type, ObjectEntry[] objectEntries, Vector3 Position, Vector3 Rotation, byte Link, byte Rend, int MiscSettingCount)
        {
            FindObjectEntry(List, Type, objectEntries);
            this.Position = Position;
            this.Rotation = Rotation;
            this.Link = Link;
            this.Rend = Rend;
            this.MiscSettingCount = MiscSettingCount;

            isSelected = false;

            objectManager = FindObjectManager(objectEntry.List, objectEntry.Type);
            objectManager.MiscSettings = new byte[MiscSettingCount];
        }

        public SetObjectShadow(ObjectEntry thisObject, Vector3 Position, Vector3 Rotation, byte Link, byte Rend, int MiscSettingCount)
        {
            objectEntry = thisObject;
            this.Position = Position;
            this.Rotation = Rotation;
            this.Link = Link;
            this.Rend = Rend;
            this.MiscSettingCount = MiscSettingCount;

            isSelected = false;

            objectManager = FindObjectManager(objectEntry.List, objectEntry.Type);
            objectManager.MiscSettings = new byte[MiscSettingCount];

            CreateTransformMatrix();
        }

        public SetObjectManagerShadow objectManager;

        public override void CreateTransformMatrix()
        {
            objectManager.CreateTransformMatrix(Position, Rotation);

            boundingBox = objectManager.CreateBoundingBox(objectEntry.ModelNames);
            boundingBox.Maximum = (Vector3)Vector3.Transform(boundingBox.Maximum, objectManager.transformMatrix);
            boundingBox.Minimum = (Vector3)Vector3.Transform(boundingBox.Minimum, objectManager.transformMatrix);
        }

        public override void Draw(bool drawEveryObject)
        {
            if (!drawEveryObject & Vector3.Distance(SharpRenderer.Camera.GetPosition(), Position) > Rend * SharpRenderer.far / 5000f)
                return;
            
            objectManager.Draw(objectEntry.ModelNames, isSelected);
        }

        public override void FindNewObjectManager()
        {
            objectManager = FindObjectManager(objectEntry.List, objectEntry.Type);
            objectManager.MiscSettings = new byte[MiscSettingCount];
        }

        private SetObjectManagerShadow FindObjectManager(byte ObjectList, byte ObjectType)
        {
            switch (ObjectList)
            {
                case 0x00:
                    switch (ObjectType)
                    {
                        case 0x10: return new Object0010_Ring();
                        default: return new Object_ShadowEmpty();
                    }
                default: return new Object_ShadowEmpty();
            }
        }
    }
}
