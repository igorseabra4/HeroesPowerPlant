using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class SetObjectShadow : SetObject
    {
        public Vector3 Rotation;

        public SetObjectManagerShadow objectManager;

        public SetObjectShadow() : this(new ObjectEntry(), Vector3.Zero, Vector3.Zero, 0, 10) { }

        public SetObjectShadow(byte List, byte Type, ObjectEntry[] objectEntries, Vector3 Position, Vector3 Rotation, byte Link, byte Rend)
        {
            FindObjectEntry(List, Type, objectEntries);
            this.Position = Position;
            this.Rotation = Rotation;
            this.Link = Link;
            this.Rend = Rend;

            IsSelected = false;

            objectManager = FindObjectManager(objectEntry.List, objectEntry.Type);

            if (objectEntry.MiscSettingCount != -1)
                objectManager.MiscSettings = new byte[objectEntry.MiscSettingCount];
            else
                objectManager.MiscSettings = new byte[4];

            CreateTransformMatrix();
        }

        public SetObjectShadow(ObjectEntry thisObject, Vector3 Position, Vector3 Rotation, byte Link, byte Rend)
        {
            objectEntry = thisObject;
            this.Position = Position;
            this.Rotation = Rotation;
            this.Link = Link;
            this.Rend = Rend;

            IsSelected = false;

            objectManager = FindObjectManager(objectEntry.List, objectEntry.Type);
            objectManager.MiscSettings = new byte[objectEntry.MiscSettingCount];
            CreateTransformMatrix();
        }

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

            objectManager.Draw(objectEntry.ModelNames, IsSelected);
        }

        public override void FindNewObjectManager()
        {
            objectManager = FindObjectManager(objectEntry.List, objectEntry.Type);
            objectManager.MiscSettings = new byte[objectEntry.MiscSettingCount];
        }

        private SetObjectManagerShadow FindObjectManager(byte ObjectList, byte ObjectType)
        {
            return new Object_ShadowEmpty();
        }
    }
}
