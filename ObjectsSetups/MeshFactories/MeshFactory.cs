using System;
using System.Collections.Generic;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.ObjectsSetups.MeshFactories
{
    abstract class MeshFactory<TC, T> where T : Enum where TC : MeshFactory<TC, T>, new()
    {
        private static TC _instance;
        private static TC Instance => _instance ?? (_instance = new TC());

        private readonly Dictionary<T, Mesh> meshes = new Dictionary<T, Mesh>();

        public static Mesh GetMesh(T type)
        {
            return Instance.GetMeshOrCreate(type);
        }

        private Mesh GetMeshOrCreate(T type)
        {
            if (meshes.ContainsKey(type))
            {
                return meshes[type];
            }

            Mesh mesh = CreateMesh(type);
            meshes.Add(type, mesh);
            return mesh;
        }

        protected abstract Mesh CreateMesh(T type);
    }
}
