using System.Collections.Generic;
using System.Drawing;
using TwoBRenn.Engine.Components;
using TwoBRenn.Engine.Core.Render;
using TwoBRenn.Engine.Core.Render.ShaderPrograms;
using TwoBRenn.Engine.Interfaces;
using TwoBRenn.ObjectsSetups;

namespace TwoBRenn.Engine.Core.Scene.Setups
{
    class TestObjectsSetup : IObjectsSetup
    {
        SimpleShader baseShader;
        Texture containerTexture;

        public TestObjectsSetup()
        {
            baseShader = new SimpleShader();
            baseShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(2, 2));
            containerTexture = new Texture(@"Textures/container.png");
        }

        public HashSet<RennObject> GetObjects()
        {
            HashSet<RennObject> objects = new HashSet<RennObject>();

            RennObject plane = new RennObject();
            plane.Transform.SetPosition(0f, 0f, 0f);
            plane.Transform.SetRotation(0f, 0f, 0f);
            plane.Transform.SetScale(2f);
            MeshRenderer planeRenderer = plane.AddComponent<MeshRenderer>();
            planeRenderer.SetShaderProgram(baseShader);
            planeRenderer.SetTriangleMesh(TestMeshFactory.CreatePlane());
            planeRenderer.SetTexture(containerTexture);
            objects.Add(plane);

            RennObject cube = new RennObject();
            cube.Transform.SetPosition(2f, 1f, -1f);
            cube.Transform.SetRotation(0f, 0f, 0f);
            cube.Transform.SetScale(1f);
            MeshRenderer cubeRenderer = cube.AddComponent<MeshRenderer>();
            cubeRenderer.SetShaderProgram(baseShader);
            cubeRenderer.SetTriangleMesh(TestMeshFactory.CreateCube());
            objects.Add(cube);

            return objects;
        }
    }
}
