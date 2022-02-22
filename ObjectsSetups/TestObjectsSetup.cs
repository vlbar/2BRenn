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
            plane.Transform.SetPosition(1f, 0, 0);
            plane.Transform.SetRotation(90, 0, 0f);
            plane.Transform.SetScale(0.2f);
            plane.AddComponent<MeshRenderer>();
            MeshRenderer planeRenderer = plane.GetComponent<MeshRenderer>();
            planeRenderer.SetShaderProgram(baseShader);
            planeRenderer.SetTriangleMesh(TestMeshFactory.CreatePlane());
            planeRenderer.SetTexture(containerTexture);
            objects.Add(plane);

            RennObject cube = new RennObject();
            cube.Transform.SetPosition(-1f, 0, 0);
            cube.Transform.SetScale(0.5f);
            cube.AddComponent<MeshRenderer>();
            MeshRenderer cubeRenderer = cube.GetComponent<MeshRenderer>();
            cubeRenderer.SetShaderProgram(baseShader);
            cubeRenderer.SetTriangleMesh(TestMeshFactory.CreateCube());
            cubeRenderer.SetShaderAttribute(SimpleShader.BASE_COLOR, ShaderAttribute.Value(Color.Blue));
            objects.Add(cube);

            return objects;
        }
    }
}
