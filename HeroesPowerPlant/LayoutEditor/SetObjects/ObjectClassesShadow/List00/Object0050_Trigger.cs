using SharpDX;
using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object0050_Trigger : SetObjectShadow {

        //SetCollision

        public TriggerType TriggerType {
            get => (TriggerType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public TriggerShape Shape {
            get => (TriggerShape)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float Size_X {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Size_Y {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Size_Z {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("LinkIDTrigger's LinkID to Activate OR LinkID to watch in other types")]
        //Disappear
        public int Affect_LinkID { //5
            get => ReadInt(20);
            set => Write(20, value);
        }

        // 6 Does not appear on 2 objects throughout the game
        [Description("Set Disappear/Appear behavior when LinkID condition met.")]
        public TriggerLinkBehavior TriggerLinkBehavior { //6
            get {
                if (MiscSettings.Length > 24)
                    return (TriggerLinkBehavior)ReadInt(24);
                return (TriggerLinkBehavior)(-1);
            }
            set {
                if (MiscSettings.Length < 28)
                    return;
                Write(24, (int)value);
            }
        }

        public override void CreateTransformMatrix()
        {
            switch (Shape)
            {
                //case TriggerShape.Cone:
                //sphereBound = new BoundingSphere(Position, Radius);
                //transformMatrix = Matrix.Scaling(Radius * 2);
                //break;
                case TriggerShape.Cube:
                    transformMatrix = Matrix.Scaling(Size_X * 2, Size_Y * 2, Size_Z * 2);
                    break;
                case TriggerShape.Cylinder:
                    transformMatrix = Matrix.Scaling(Size_X, Size_Y * 2, Size_X);
                    //transformMatrix = Matrix.Scaling(Radius * 2, Height * 2, Radius * 2);
                    break;
                //default temp until cone determined
                default:
                    transformMatrix = Matrix.Scaling(Size_X * 2, Size_Y * 2, Size_Z * 2);
                    break;
            }

            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            List<Vector3> list = new List<Vector3>();

            switch (Shape)
            {
                //case TriggerShape.Cone:
                //list.AddRange(SharpRenderer.sphereVertices);
                //break;
                case TriggerShape.Cube:
                    list.AddRange(SharpRenderer.cubeVertices);
                    break;
                case TriggerShape.Cylinder:
                    list.AddRange(SharpRenderer.cylinderVertices);
                    break;
                default:
                    base.CreateBoundingBox();
                    return;
            }

            for (int i = 0; i < list.Count; i++)
                list[i] = (Vector3)Vector3.Transform(list[i], transformMatrix);

            boundingBox = BoundingBox.FromPoints(list.ToArray());
        }

        public override void Draw(SharpRenderer renderer)
        {
            //if (Shape == TriggerShape.Cone)
            //    renderer.DrawCone(transformMatrix, isSelected);
            //else if
            if (Shape == TriggerShape.Cylinder)
                renderer.DrawCylinderTrigger(transformMatrix, isSelected, new Color4(0f, 1f, 0f, 0.5f));
            else if (Shape == TriggerShape.Cube)
                renderer.DrawCubeTrigger(transformMatrix, isSelected, new Color4(0f, 1f, 0f, 0.5f));
            else
                DrawCube(renderer);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            switch (Shape)
            {
                //case TriggerShape.Cone:
                //    return r.Intersects(ref sphereBound, out distance);
                case TriggerShape.Cube:
                    return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
                case TriggerShape.Cylinder:
                    return TriangleIntersection(r, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices, initialDistance, out distance);
                default:
                    return base.TriangleIntersection(r, initialDistance, out distance);
            }
        }
    }

    public enum TriggerType {
        SolidCollision=0,
        LinkIDTrigger=2,
        HurtPlayer=3,
        KillPlayer=4,
        ChaosControlCancelOn=5,
        ChaosControlCancelOff=6,
        ChaosControlStop=7,
        MaintainBehaviorSkydive=9,
        LockControlsWhileInTrigger=10,
        CompleteMission=11
    }

    public enum TriggerShape {
        Cylinder,
        Cube,
        Cone
    }

    public enum TriggerLinkBehavior {
        NotValidInObject = -1,
        Disappear = 0,
        Appear = 1
    }
}
