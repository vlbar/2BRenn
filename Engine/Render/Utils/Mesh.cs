using OpenTK;

namespace TwoBRenn.Engine.Render.Utils
{
    public class Mesh
    {
        private float[] vertices;
        private float[] uvs;
        private uint[] triangles;
        private float[] normals;

        private Vector3[] verticesVectors;
        private Vector2[] uvsVectors;
        private Vector3[] normalsVectors;

        public Vector3[] Vertices
        {
            get => GetVectoredDataOrDecompose(vertices, ref verticesVectors);
            set
            {
                verticesVectors = value;
                vertices = DecomposeVectors(value);
            }
        }

        public Vector2[] UVs
        {
            get => GetVectoredDataOrDecompose(uvs, ref uvsVectors);
            set
            {
                uvsVectors = value;
                uvs = DecomposeVectors(value);
            }
        }

        public Vector3[] Normals
        {
            get
            {
                if (vertices == null && triangles == null) return null;
                if (normals == null)
                {
                    normals = CalcNormals();
                }
                return GetVectoredDataOrDecompose(normals, ref normalsVectors);
            }
            set
            {
                normalsVectors = value;
                normals = DecomposeVectors(value);
            }
        }

        public uint[] Triangles
        {
            get => triangles;
            set => triangles = value;
        }

        public float[] VerticesArray => vertices;
        public float[] UVsArray => uvs;

        public float[] NormalsArray
        {
            get
            {
                if (vertices == null && triangles == null) return null;
                if (normals != null) return normals;
                float[] calculatedNormals = CalcNormals();
                normals = calculatedNormals;
                return calculatedNormals;
            }
        }

        public float[] CalcNormals()
        {
            Vector3[] verticesPos = Vertices;
            Vector3[] calculatedNormals = new Vector3[verticesPos.Length];

            for (int i = 0; i < triangles.Length; i += 3)
            {
                uint index0 = triangles[i + 0];
                uint index1 = triangles[i + 1];
                uint index2 = triangles[i + 2];

                Vector3 vector1 = verticesPos[index1] - verticesPos[index0];
                Vector3 vector2 = verticesPos[index2] - verticesPos[index0];
                Vector3 normal = Vector3.Cross(vector1, vector2);
                normal.Normalize();

                calculatedNormals[index0] += normal;
                calculatedNormals[index1] += normal;
                calculatedNormals[index2] += normal;
            }

            for (int i = 0; i < verticesPos.Length; i++)
            {
                calculatedNormals[i] = -calculatedNormals[i].Normalized();
            }

            return DecomposeVectors(calculatedNormals);
        }

        public static int GetMeshDataSize(float[] values, int location)
        {
            if (location == -1) return 0;
            return values.Length * sizeof(float);
        }


        // == PART OF VECTORS CRINGE ==
        // data getters
        private static Vector3[] GetVectoredDataOrDecompose(float[] values, ref Vector3[] vectors)
        {
            if (values == null) return null;
            if (vectors != null) return vectors;
            vectors = GenerateVectors3(values);
            return vectors;
        }

        private static Vector2[] GetVectoredDataOrDecompose(float[] values, ref Vector2[] vectors)
        {
            if (values == null) return null;
            if (vectors != null) return vectors;
            vectors = GenerateVectors2(values);
            return vectors;
        }

        // decompose
        private static float[] DecomposeVectors(Vector2[] vectors)
        {
            float[] floatArray = new float[vectors.Length * 2];
            for (int i = 0; i < vectors.Length; i++)
            {
                floatArray[i * 2 + 0] = vectors[i].X;
                floatArray[i * 2 + 1] = vectors[i].Y;
            }

            return floatArray;
        }

        private static float[] DecomposeVectors(Vector3[] vectors)
        {
            float[] floatArray = new float[vectors.Length * 3];
            for (int i = 0; i < vectors.Length; i++)
            {
                floatArray[i * 3 + 0] = vectors[i].X;
                floatArray[i * 3 + 1] = vectors[i].Y;
                floatArray[i * 3 + 2] = vectors[i].Z;
            }

            return floatArray;
        }

        // generate vector
        private static Vector3[] GenerateVectors3(float[] values)
        {
            Vector3[] vectors = new Vector3[values.Length / 3];
            for (int i = 0; i < values.Length / 3; i++)
            {
                vectors[i] = new Vector3(values[i * 3 + 0], values[i * 3 + 1], values[i * 3 + 2]);
            }

            return vectors;
        }

        private static Vector2[] GenerateVectors2(float[] values)
        {
            Vector2[] vectors = new Vector2[values.Length / 2];
            for (int i = 0; i < values.Length / 2; i++)
            {
                vectors[i] = new Vector2(values[i * 2 + 0], values[i * 2 + 1]);
            }

            return vectors;
        }
    }
}
