using OpenTK;

namespace TwoBRenn.Engine.Core.Render
{
    public class Mesh
    {
        private float[] vertices;
        private float[] uvs;
        private float[] colors;
        private uint[] triangles;

        public Vector3[] Vertices
        {
            get
            {
                if (vertices == null) return null;
                Vector3[] vectoredVertices = new Vector3[vertices.Length / 3];
                for (int i = 0; i < vertices.Length / 3; i++)
                {
                    vectoredVertices[i] = new Vector3(vertices[i * 3 + 0], vertices[i * 3 + 1], vertices[i * 3 + 2]);
                }

                return vectoredVertices;
            }
            set
            {
                vertices = new float[value.Length * 3];
                for (int i = 0; i < value.Length; i++)
                {
                    vertices[i * 3 + 0] = value[i].X;
                    vertices[i * 3 + 1] = value[i].Y;
                    vertices[i * 3 + 2] = value[i].Z;
                }
            }
        }

        public Vector2[] UVs
        {
            get
            {
                if (uvs == null) return null;
                Vector2[] vectoredUVs = new Vector2[uvs.Length / 2];
                for (var i = 0; i < uvs.Length / 2; i++)
                {
                    vectoredUVs[i] = new Vector2(uvs[i * 2 + 0], uvs[i * 2 + 1]);
                }

                return vectoredUVs;
            }
            set
            {
                uvs = new float[value.Length * 2];
                for (var i = 0; i < value.Length; i++)
                {
                    uvs[i * 2 + 0] = value[i].X;
                    uvs[i * 2 + 1] = value[i].Y;
                }
            }
        }

        public Vector4[] Colors
        {
            get
            {
                if (colors == null) return null;
                Vector4[] vectoredColors = new Vector4[colors.Length / 4];
                for (int i = 0; i < vertices.Length / 4; i++)
                {
                    vectoredColors[i] = new Vector4(vertices[i * 4 + 0], vertices[i * 4 + 1], vertices[i * 4 + 2], vertices[i * 4 + 3]);
                }

                return vectoredColors;
            }
            set
            {
                colors = new float[value.Length * 4];
                for (var i = 0; i < value.Length; i++)
                {
                    colors[i * 4 + 0] = value[i].X;
                    colors[i * 4 + 1] = value[i].Y;
                    colors[i * 4 + 2] = value[i].Z;
                    colors[i * 4 + 3] = value[i].W;
                }
            }
        }

        public uint[] Triangles
        {
            get => triangles;
            set => triangles = value;
        }

        public float[] VerticesArray => vertices;
        public float[] UVsArray => uvs;
        public float[] ColorsArray => colors;

        public int DataArraySize => ((vertices?.Length ?? 0) + (uvs?.Length ?? 0) + (colors?.Length ?? 0)) * sizeof(float);
    }
}
