using System;
using System.Collections.Generic;
using OpenTK;
using TwoBRenn.Engine.Common.Path;
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
        SimpleShader roadShader = new SimpleShader();
        Texture containerTexture = new Texture(@"Textures\container.png");
        Texture groundTexture = new Texture(@"Textures\ground.jpg");
        Texture roadTexture = new Texture(@"Textures\road.jpg");

        public TestObjectsSetup()
        {
            groundShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(2, 2));
            roadShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(1, 10));
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

            Path path = new Path(new List<Vector3>()
            {
                new Vector3(-8, 0, -8),
                new Vector3(-10, 0, 2),
                new Vector3(-9, 0, 7),
                new Vector3(-2, 0, 5),
                new Vector3(1, 0, 10),
                new Vector3(10, 0, 4),
                new Vector3(9, 0, -5)
            });
            path.IsClosed = true;

            RennObject road = new RennObject();
            MeshRenderer roadRenderer = cube.AddComponent<MeshRenderer>();
            roadRenderer.SetTriangleMesh(RoadCreator.CreateMesh(path.CalculateEvenlySpacedPoints(0.1f), path.IsClosed, 1.2f));
            roadRenderer.SetShaderProgram(roadShader);
            roadRenderer.SetTexture(roadTexture);
            objects.Add(road);

            return objects;
        }
    }
}
