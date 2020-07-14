using SharpDX;
using System.Collections.Generic;


namespace HeroesPowerPlant.ShadowCameraEditor {
 /*     // HEADER : SIZE=0x18
        0x0 = magic
        0x4 = magic
        0x8 = stageID int
        0xC =
        0x10 =
        0x14 = NumberOfCameras int

        //CAMERA : SIZE=0xDC
        0x0 - 0x18 = Unknown
        0x1C = CameraAffectSpeed int
        0x20 - 0x2B = TriggerPosition Vector3
        0x2C - 0x37 = Unknown
        0x38 - 0x43 = TriggerScale Vector3
 */
    public class ShadowCamera {
        public int field_00;
        public int field_04;
        public int field_08;
        public int field_0C;
        public int field_10;
        public int field_14;
        public int field_18;
        public int field_1C;
        public Vector3 TriggerPosition; //0x20 - 0x2C
        public Vector3 TriggerRotation;
        public Vector3 TriggerScale; //0x38 - 0x40
        public float field_44;
        public float field_48;
        public float field_4C;
        public float field_50;
        public float field_54;
        public float field_58;
        public byte[] UnknownSection3; //0x80 remaining length

        /*
        public int CameraType;
        public int CameraSpeed;
        public int Integer3;
        public int ActivationType;
        public int TriggerShape;
        public Vector3 TriggerPosition;
        public int TriggerRotX;
        public int TriggerRotY;
        public int TriggerRotZ;
        public Vector3 TriggerScale;
        public Vector3 CamPos;
        public int CamRotX;
        public int CamRotY;
        public int CamRotZ;
        public Vector3 PointA;
        public Vector3 PointB;
        public Vector3 PointC;
        public int Integer30;
        public int Integer31;
        public float FloatX32;
        public float FloatY33;
        public float FloatX34;
        public float FloatY35;
        public int Integer36;
        public int Integer37;
        public int Integer38;
        public int Integer39;*/

        public ShadowCamera(int i_00, int i_04, int i_08, int i_0C, int i_10, int i_14,
            int i_18, int i_1C, Vector3 triggerPos, Vector3 triggerRot, Vector3 triggerScale,
            float f_44, float f_48, float f_4C, float f_50, float f_54, float f_58,
            byte[] sec3) {
            field_00 = i_00;
            field_04 = i_04;
            field_08 = i_08;
            field_0C = i_0C;
            field_10 = i_10;
            field_14 = i_14;
            field_18 = i_18;
            field_1C = i_1C;
            TriggerPosition = triggerPos;
            TriggerRotation = triggerRot;
            TriggerScale = triggerScale;
            field_44 = f_44;
            field_48 = f_48;
            field_4C = f_4C;
            field_50 = f_50;
            field_54 = f_54;
            field_58 = f_58;
            UnknownSection3 = sec3;
        }

        public bool isSelected;

        private static DefaultRenderData renderData;

        private Matrix triggerPosWorld;
        private Matrix pointAWorld;
        private Matrix pointBWorld;
        private Matrix pointCWorld;
        private Matrix camPosWorld;

        public BoundingBox boundingBox;

        public override string ToString() {
            return $"Cam {field_00}, {field_04}, {field_08}, {field_0C}, {field_10}";
        }

        public void CreateBounding() {
            List<Vector3> vertices = new List<Vector3>();

            //if (TriggerShape == 1 || TriggerShape == 3) //plane, cube
            foreach (var v in SharpRenderer.cubeVertices)
               vertices.Add((Vector3)Vector3.Transform(v, triggerPosWorld));
           // else if (TriggerShape == 4) // cyl
             //   foreach (var v in SharpRenderer.cylinderVertices)
               //     vertices.Add((Vector3)Vector3.Transform(v, triggerPosWorld));
            //else
            //    foreach (var v in SharpRenderer.sphereVertices)
              //      vertices.Add((Vector3)Vector3.Transform(v, triggerPosWorld));

            boundingBox = BoundingBox.FromPoints(vertices.ToArray());
        }

        public void CreateTransformMatrix() {
            triggerPosWorld = Matrix.Scaling(TriggerScale * 2);


            triggerPosWorld *=
                Matrix.RotationYawPitchRoll(TriggerRotation.X, TriggerRotation.Y, TriggerRotation.Z);
                Matrix.Translation(TriggerPosition);

            //pointAWorld = Matrix.Scaling(5) * Matrix.Translation(PointA);
           // pointBWorld = Matrix.Scaling(5) * Matrix.Translation(PointB);
            //pointCWorld = Matrix.Scaling(5) * Matrix.Translation(PointC);
            //camPosWorld = Matrix.Scaling(5) * Matrix.Translation(CamPos);

            CreateBounding();
        }

        public void Draw(SharpRenderer renderer) {
            if (Vector3.Distance(renderer.Camera.GetPosition(), TriggerPosition) < 15000f)
                //if (TriggerShape == 1 || TriggerShape == 3) //plane, cube
                renderer.DrawCubeTrigger(triggerPosWorld, isSelected);
                //else if (TriggerShape == 4) // cyl
                  //  renderer.DrawCylinderTrigger(triggerPosWorld, isSelected);
                //else // sphere
                  //  renderer.DrawSphereTrigger(triggerPosWorld, isSelected);

            /*if (isSelected) {
                DrawCube(renderer, pointAWorld, Color.Red.ToVector4());
                DrawCube(renderer, pointBWorld, Color.Blue.ToVector4());
                DrawCube(renderer, pointCWorld, Color.Green.ToVector4());
                DrawCube(renderer, camPosWorld, Color.Pink.ToVector4());
            }*/
        }

        public void DrawCube(SharpRenderer renderer, Matrix transformMatrix, Vector4 color) {
            renderData.worldViewProjection = transformMatrix * renderer.viewProjection;
            renderData.Color = color;

            renderer.Device.SetFillModeDefault();
            renderer.Device.SetCullModeNone();
            renderer.Device.SetBlendStateAlphaBlend();
            renderer.Device.ApplyRasterState();
            renderer.Device.UpdateAllStates();

            renderer.Device.UpdateData(renderer.basicBuffer, renderData);
            renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
            renderer.basicShader.Apply();

            renderer.Cube.Draw(renderer.Device);
        }

        public float GetDistance() {
            return TriggerPosition.Length();
        }

        public float? IntersectsWith(Ray ray) {
            /*if (TriggerShape == 1 || TriggerShape == 3) //plane, cube
            {
                if (ray.Intersects(ref boundingBox))
                    return TriangleIntersection(ray, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices);
            } else if (TriggerShape == 4) // cyl
              {
                if (ray.Intersects(ref boundingBox))
                    return TriangleIntersection(ray, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices);
            } else {
                if (ray.Intersects(ref boundingBox))
                    return TriangleIntersection(ray, SharpRenderer.sphereTriangles, SharpRenderer.sphereVertices);
            }*/

            return null;
        }

        public float? TriangleIntersection(Ray r, List<LevelEditor.Triangle> triangles, List<Vector3> vertices) {
            bool hasIntersected = false;
            float smallestDistance = 10000f;

            foreach (var t in triangles) {
                Vector3 v1 = (Vector3)Vector3.Transform(vertices[t.vertex1], triggerPosWorld);
                Vector3 v2 = (Vector3)Vector3.Transform(vertices[t.vertex2], triggerPosWorld);
                Vector3 v3 = (Vector3)Vector3.Transform(vertices[t.vertex3], triggerPosWorld);

                if (r.Intersects(ref v1, ref v2, ref v3, out float distance)) {
                    hasIntersected = true;

                    if (distance < smallestDistance)
                        smallestDistance = distance;
                }
            }

            if (hasIntersected)
                return smallestDistance;
            else return null;
        }
    }
}