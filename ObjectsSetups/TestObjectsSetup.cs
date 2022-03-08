using System.Collections.Generic;
using TwoBRenn.Engine.Components;
using TwoBRenn.Engine.Core.Render;
using TwoBRenn.Engine.Core.Render.ShaderPrograms;
using TwoBRenn.Engine.Interfaces;
using TwoBRenn.ObjectsSetups;

namespace TwoBRenn.Engine.Core.Scene.Setups
{
    class TestObjectsSetup : IObjectsSetup
    {
        SimpleShader baseShader = new SimpleShader();
        SimpleShader groundShader = new SimpleShader();
        Texture containerTexture = new Texture(@"Textures\container.png");
        Texture groundTexture = new Texture(@"Textures\ground.jpg");

        public TestObjectsSetup()
        {
            groundShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(2, 2));
        }

        public HashSet<RennObject> GetObjects()
        {
            HashSet<RennObject> objects = new HashSet<RennObject>();

            RennObject plane = new RennObject();
            plane.Transform.SetPosition(0f, 0f, 0f);
            plane.Transform.SetRotation(0f, 0f, 0f);
            plane.Transform.SetScale(10f);
            MeshRenderer planeRenderer = plane.AddComponent<MeshRenderer>();
            planeRenderer.SetTriangleMesh(PrimitiveMeshFactory.CreatePlane());
            planeRenderer.SetShaderProgram(groundShader);
            planeRenderer.SetTexture(groundTexture);
            objects.Add(plane);

            RennObject cube = new RennObject();
            cube.Transform.SetPosition(2f, 0.5f, -1f);
            cube.Transform.SetRotation(0f, 0f, 0f);
            cube.Transform.SetScale(1f);
            MeshRenderer cubeRenderer = cube.AddComponent<MeshRenderer>();
            cubeRenderer.SetTriangleMesh(PrimitiveMeshFactory.CreateCube());
            cubeRenderer.SetShaderProgram(baseShader);
            cubeRenderer.SetTexture(containerTexture);
            objects.Add(cube);

            return objects;
        }
    }
}
