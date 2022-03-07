using TwoBRenn.Engine.Core.Render;

namespace TwoBRenn.ObjectsSetups
{
    class PrimitiveMeshFactory
    {
        public static Mesh CreatePlane()
        {
            float[] planeVertices = new float[] {
                // vertex          // tex coords
                -0.5f, 0, -0.5f,   0f, 0f,
                 0.5f, 0, -0.5f,   1f, 0f,
                -0.5f, 0,  0.5f,   0f, 1f,
                 0.5f, 0,  0.5f,   1f, 1f,
            };

            uint[] planeIndexes = new uint[] {
                0, 1, 3,
                0, 3, 2,
            };

            return new Mesh(planeVertices, planeIndexes);
        }

        public static Mesh CreateCube()
        {
            float[] cubeVertices = new float[] {
                // vertex             // tex coords
                 0.5f, -0.5f, -0.5f,  0f, 0f, // 0
                 0.5f, -0.5f,  0.5f,  1f, 0f, // 1
                 0.5f,  0.5f,  0.5f,  1f, 1f, // 2
                 0.5f,  0.5f, -0.5f,  0f, 1f, // 3
                -0.5f,  0.5f, -0.5f,  1f, 1f, // 4
                -0.5f, -0.5f, -0.5f,  1f, 0f, // 5
                -0.5f,  0.5f,  0.5f,  0f, 1f, // 6
                -0.5f, -0.5f,  0.5f,  0f, 0f, // 7
                -0.5f,  0.5f, -0.5f,  1f, 1f, // 8
                -0.5f, -0.5f, -0.5f,  1f, 0f, // 9
                -0.5f, -0.5f, -0.5f,  0f, 1f, // 10
                -0.5f, -0.5f,  0.5f,  1f, 1f, // 11
                -0.5f,  0.5f, -0.5f,  0f, 0f, // 12
                -0.5f,  0.5f,  0.5f,  1f, 0f, // 13
            };

            uint[] cubeIndexes = new uint[] {
                0, 1, 2,
                0, 2, 3,
                0, 3, 4,
                0, 4, 5,
                7, 2, 1,
                7, 6, 2,
                7, 9, 8,
                7, 8, 6,
                0, 10, 11,
                0, 11, 1,
                12, 3, 2,
                12, 2, 13,
            };

            return new Mesh(cubeVertices, cubeIndexes);
        }
    }
}
