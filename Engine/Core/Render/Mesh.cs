﻿namespace TwoBRenn.Engine.Core.Render
{
    class Mesh
    {
        public float[] TriangleVertices { get; private set; }
        public uint[] VertexIndexes { get; private set; }

        public Mesh(float[] triangleVertices, uint[] vertexIndexes)
        {
            TriangleVertices = triangleVertices;
            VertexIndexes = vertexIndexes;
        }
    }
}
