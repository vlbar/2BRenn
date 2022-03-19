using OpenTK;
using TwoBRenn.Engine.Core.Render;

namespace TwoBRenn.ObjectsSetups
{
    class PrimitiveMeshFactory
    {
        public static Mesh CreatePlane()
        {
            Mesh mesh = new Mesh
            {
                Vertices = new []
                {
                    new Vector3(-0.5f, 0, -0.5f),
                    new Vector3(0.5f, 0, -0.5f),
                    new Vector3(-0.5f, 0, 0.5f),
                    new Vector3(0.5f, 0, 0.5f),
                },
                UVs = new []
                {
                    new Vector2(0f, 0f),
                    new Vector2(1f, 0f),
                    new Vector2(0f, 1f),
                    new Vector2(1f, 1f),
                },
                Triangles = new uint[] {
                    0, 1, 3,
                    0, 3, 2,
                }
            };

            return mesh;
        }

        public static Mesh CreateCube()
        {
            Mesh mesh = new Mesh
            {
                Vertices = new Vector3[]
                {
                    new Vector3(0.5f, -0.5f, -0.5f),
                    new Vector3(0.5f, -0.5f, 0.5f),
                    new Vector3(0.5f, 0.5f, 0.5f),
                    new Vector3(0.5f, 0.5f, -0.5f),
                    new Vector3(-0.5f, 0.5f, -0.5f),
                    new Vector3(-0.5f, -0.5f, -0.5f),
                    new Vector3(-0.5f, 0.5f, 0.5f),
                    new Vector3(-0.5f, -0.5f, 0.5f),
                    new Vector3(-0.5f, 0.5f, -0.5f),
                    new Vector3(-0.5f, -0.5f, -0.5f),
                    new Vector3(-0.5f, -0.5f, -0.5f),
                    new Vector3(-0.5f, -0.5f, 0.5f),
                    new Vector3(-0.5f, 0.5f, -0.5f),
                    new Vector3(-0.5f, 0.5f, 0.5f),
                },
                UVs = new Vector2[]
                {
                    new Vector2(0f, 0f),
                    new Vector2(1f, 0f),
                    new Vector2(1f, 1f),
                    new Vector2(0f, 1f),
                    new Vector2(1f, 1f),
                    new Vector2(1f, 0f),
                    new Vector2(0f, 1f),
                    new Vector2(0f, 0f),
                    new Vector2(1f, 1f),
                    new Vector2(1f, 0f),
                    new Vector2(0f, 1f),
                    new Vector2(1f, 1f),
                    new Vector2(0f, 0f),
                    new Vector2(1f, 0f),
                },
                Triangles = new uint[] {
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
                }
            };

            return mesh;
        }
    }
}
