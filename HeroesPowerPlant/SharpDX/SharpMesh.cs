﻿using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using Buffer11 = SharpDX.Direct3D11.Buffer;

namespace HeroesPowerPlant
{
    /// <summary>
    /// To Render Static Object
    /// </summary>
    public class SharpMesh : IDisposable
    {
        /// <summary>
        /// Render Index
        /// </summary>
        public int RenderIndex { get; set; }
        
        /// <summary>
        /// Vertex Buffer
        /// </summary>
        public Buffer11 VertexBuffer { get; private set; }

        /// <summary>
        /// Index Buffer
        /// </summary>
        public Buffer11 IndexBuffer { get; private set; }

        /// <summary>
        /// Vertex Size
        /// </summary>
        public int VertexSize { get; private set; }

        /// <summary>
        /// Mesh Parts
        /// </summary>
        public List<SharpSubSet> SubSets { get; private set; }

        /// <summary>
        /// Draw Mesh
        /// </summary>
        public void Draw(SharpDevice Device)
        {
            Device.DeviceContext.InputAssembler.PrimitiveTopology = primitiveTopology;
            Device.DeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, VertexSize, 0));
            Device.DeviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
            Device.DeviceContext.DrawIndexed(SubSets[0].IndexCount, 0, 0);
        }

        private PrimitiveTopology primitiveTopology;

        /// <summary>
        /// Create From Vertices and Indices array
        /// </summary>
        /// <typeparam name="VType">Vertex Type</typeparam>
        /// <param name="device">Device</param>
        /// <param name="vertices">Vertices</param>
        /// <param name="indices">Indices</param>
        /// <returns>Mesh</returns>
        public static SharpMesh Create<VType>(SharpDevice device, VType[] vertices, int[] indices, List<SharpSubSet> SubSets,
            PrimitiveTopology topology = PrimitiveTopology.TriangleList) where VType : struct
        {
            return new SharpMesh()
            {
                VertexBuffer = Buffer11.Create<VType>(device.Device, BindFlags.VertexBuffer, vertices),
                IndexBuffer = Buffer11.Create(device.Device, BindFlags.IndexBuffer, indices),
                VertexSize = Utilities.SizeOf<VType>(),
                SubSets = SubSets,
                primitiveTopology = topology,
            };
        }

        /// <summary>
        /// Set all buffer and topology property to speed up rendering
        /// </summary>
        public void Begin(SharpDevice Device)
        {
            Device.DeviceContext.InputAssembler.PrimitiveTopology = primitiveTopology;
            Device.DeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, VertexSize, 0));
            Device.DeviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
        }

        /// <summary>
        /// Draw subset
        /// </summary>
        /// <param name="subset">Subsets</param>
        public void Draw(SharpDevice Device, int subset)
        {
            Device.DeviceContext.DrawIndexed(SubSets[subset].IndexCount, SubSets[subset].StartIndex, 0);
        }

        /// <summary>
        /// Draw all vertices as points
        /// </summary>
        /// <param name="count"></param>
        public void DrawPoints(SharpDevice Device, int count = int.MaxValue)
        {
            Device.DeviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.PointList;
            Device.DeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, VertexSize, 0));
            Device.DeviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
            Device.DeviceContext.DrawIndexed(Math.Min(count, SubSets[0].IndexCount), 0, 0);
        }

        /// <summary>
        /// Release resource
        /// </summary>
        public void Dispose()
        {
            VertexBuffer.Dispose();
            IndexBuffer.Dispose();
        }

        /// <summary>
        /// Release resource
        /// </summary>
        public void DisposeWithTextures()
        {
            VertexBuffer.Dispose();
            IndexBuffer.Dispose();

            foreach (var s in SubSets)
            {
                if (s.DiffuseMap != null)
                    s.DiffuseMap.Dispose();
            }
        }
    }
}
