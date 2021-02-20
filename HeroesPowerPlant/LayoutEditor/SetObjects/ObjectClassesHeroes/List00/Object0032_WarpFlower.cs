using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum FlowerType : byte
    {
        Item = 0,
        Scaffold = 1,
        Warp = 2
    }

    public class Object0032_WarpFlower : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();
            CreateBoundingBox();
        }
        
        public override void Draw(SharpRenderer renderer)
        {
            SetDFFModels();

            if (models != null)
            {
                for (int i = 0; i < models.Length; i++)
                {
                    if (renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[0][i]))
                    {
                        if (i == 2 | i == 3)
                        {
                            SetRendererStates(renderer);
                            renderData.worldViewProjection = Matrix.Translation(0f, 20f, 0f) * transformMatrix * renderer.viewProjection;

                            renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                            renderer.dffRenderer.DFFModels["OBJ_FLOWERC.DFF"].Render(renderer.Device);

                            if (FlowerType == FlowerType.Warp)
                            {
                                renderData.worldViewProjection = Matrix.Translation(-30f, 30f, 0f) * transformMatrix * renderer.viewProjection;
                                renderer.Device.SetBlendStateAdditive();
                                renderer.Device.SetCullModeNone();
                                renderer.Device.ApplyRasterState();
                                renderer.Device.UpdateAllStates();

                                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                                renderer.dffRenderer.DFFModels["EF_FLOWARP.DFF"].Render(renderer.Device);
                            }
                        }

                        else
                        {
                            SetRendererStates(renderer);
                            renderer.dffRenderer.DFFModels[ModelNames[0][i]].Render(renderer.Device);
                        }
                    }
                }
            }
        }

        public FlowerType FlowerType
        {
            get => (FlowerType)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public float Scale
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float RisingHeight
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
