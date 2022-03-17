using System;
using System.Collections.Generic;
using OpenTK;
using TwoBRenn.Engine.Core.Render;

namespace TwoBRenn.Engine.Common.Path
{
    public class RoadCreator
    {
        public static Mesh CreateMesh(Vector3[] points, bool isClosed, float roadWidth)
        {
            Vector3[] verts = new Vector3[points.Length * 2];
            Vector2[] uvs = new Vector2[points.Length * 2];
            int trisCount = (points.Length - 1) * 2 + (isClosed ? 2 : 0);
            uint[] tris = new uint[trisCount * 3];
            uint vertIndex = 0;
            uint trisIndex = 0;

            for (int i = 0; i < points.Length; i++)
            {
                Vector3 forward = Vector3.Zero;
                if (i < points.Length - 1 || isClosed)
                {
                    forward += points[(i + 1) % points.Length] - points[i];
                }
                if (i > 0 || isClosed)
                {
                    forward += points[i] - points[(i - 1 + points.Length) % points.Length];
                }

                forward.Normalize();
                Vector3 left = Vector3.Normalize(Vector3.Cross(forward, Vector3.UnitY));

                verts[vertIndex] = points[i] + left * roadWidth * .5f;
                verts[vertIndex + 1] = points[i] - left * roadWidth * .5f;

                float completionPercent = i / (float)(points.Length - 1);
                float v = 1 - Math.Abs(2 * completionPercent - 1);
                uvs[vertIndex] = new Vector2(0, v);
                uvs[vertIndex + 1] = new Vector2(1, v);

                if (i < points.Length - 1 || isClosed)
                {
                    tris[trisIndex + 0] = vertIndex + 0;
                    tris[trisIndex + 1] = vertIndex + 1;
                    tris[trisIndex + 2] = (uint)((vertIndex + 2) % verts.Length);

                    tris[trisIndex + 3] = vertIndex + 1;
                    tris[trisIndex + 4] = (uint)((vertIndex + 3) % verts.Length);
                    tris[trisIndex + 5] = (uint)((vertIndex + 2) % verts.Length);
                }

                vertIndex += 2;
                trisIndex += 6;
            }

            List<float> vertsArray = new List<float>();
            for (int i = 0; i < verts.Length; i++)
            {
                vertsArray.Add(verts[i].X);
                vertsArray.Add(verts[i].Y);
                vertsArray.Add(verts[i].Z);
                vertsArray.Add(uvs[i].X);
                vertsArray.Add(uvs[i].Y);
            }

            return new Mesh(vertsArray.ToArray(), tris);
        }
    }
}