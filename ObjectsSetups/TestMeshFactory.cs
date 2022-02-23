using TwoBRenn.Engine.Core.Render;

namespace TwoBRenn.ObjectsSetups
{
    class TestMeshFactory
    {
        public static Mesh CreatePlane()
        {
            float[] planeVertices = new float[] {
                // vertex              // tex coords
                -0.5f, -0.5f, -0.5f,   0f, 0f,
                 0.5f, -0.5f, -0.5f,   1f, 0f,
                -0.5f,  0.5f, -0.5f,   0f, 1f,
                 0.5f,  0.5f, -0.5f,   1f, 1f,
            };

            uint[] planeIndexes = new uint[] {
                0, 1, 3,
                0, 2, 3,
            };

            return new Mesh(planeVertices, planeIndexes);
        }

        public static Mesh CreateCube()
        {
            float[] cubeVertices = new float[] {
                // vertex              // tex coords
                 0.5f,  0.5f, -0.5f,   1f, 0f,
                 0.5f, -0.5f, -0.5f,   0f, 0f,
                 0.5f,  0.5f,  0.5f,   1f, 1f,
                 0.5f, -0.5f,  0.5f,   0f, 1f,
                -0.5f,  0.5f, -0.5f,   1f, 1f,
                -0.5f, -0.5f, -0.5f,   0f, 1f,
                -0.5f,  0.5f,  0.5f,   1f, 0f,
                -0.5f, -0.5f,  0.5f,   0f, 0f,
            };

            uint[] cubeIndexes = new uint[] {
                0, 1, 2,
                1, 2, 3,
                4, 5, 6,
                5, 6, 7,
                0, 4, 6,
                2, 6, 0,
                0, 1, 4,
                1, 4, 5,
                1, 3, 5,
                3, 5, 7,
            };

            return new Mesh(cubeVertices, cubeIndexes);
        }
    }
}
