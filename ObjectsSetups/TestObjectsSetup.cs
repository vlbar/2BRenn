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
        SimpleShader curbShader = new SimpleShader();
        Texture containerTexture = new Texture(@"Textures\container.png");
        Texture groundTexture = new Texture(@"Textures\ground.jpg");
        Texture roadTexture = new Texture(@"Textures\road.jpg");
        Texture curbTexture = new Texture(@"Textures\curb.png");
        Texture gravelTexture = new Texture(@"Textures\gravel.jpg");

        public TestObjectsSetup()
        {
            groundShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(2, 2));
            roadShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(1, 30));
            curbShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(1, 60));
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

            Path path = new Path();
            path.AddManualSegment(new Vector3(18f, 0f, -12.5f), new Vector3(20f, 0f, -12.5f), new Vector3(25f, 0f, -12.5f));
            path.AddManualSegment(new Vector3(28f, 0f, -12.5f), new Vector3(31f, 0f, -12.5f), new Vector3(34f, 0f, -12.5f));
            path.AddManualSegment(new Vector3(38.5f, 0f, -6f), new Vector3(36.5f, 0f, -4.5f), new Vector3(33f, 0f, -2f));
            path.AddManualSegment(new Vector3(31.5f, 0f, -5.5f), new Vector3(30.5f, 0f, -7f), new Vector3(29.5f, 0f, -8.5f));
            path.AddManualSegment(new Vector3(28f, 0f, -10f), new Vector3(26f, 0f, -9f), new Vector3(24f, 0f, -8f));
            path.AddManualSegment(new Vector3(25.5f, 0f, -6f), new Vector3(22f, 0f, -5.5f), new Vector3(18.5f, 0f, -5f));
            path.AddManualSegment(new Vector3(17f, 0f, -5.5f), new Vector3(14.5f, 0f, -1.5f), new Vector3(13.5f, 0f, -0f));
            path.AddManualSegment(new Vector3(8f, 0f, 0.5f), new Vector3(9f, 0f, -4f), new Vector3(10f, 0f, -7f));
            path.AddManualSegment(new Vector3(10f, 0f, -8.5f), new Vector3(8f, 0f, -9f), new Vector3(4f, 0f, -10f));
            path.AddManualSegment(new Vector3(-2f, 0f, -14f), new Vector3(1.5f, 0f, -17.5f), new Vector3(6f, 0f, -21.5f));
            path.AddManualSegment(new Vector3(9.5f, 0f, -8.5f), new Vector3(11.5f, 0f, -10f), new Vector3(13.5f, 0f, -8.5f));
            path.IsClosed = true;

            RoadCreatorSettings roadCreatorSettings = new RoadCreatorSettings();
            RoadPart[] parts = RoadCreator.CreateMeshes(path.CalculateEvenlySpacedPoints(0.1f), roadCreatorSettings, 10);
            RennObject[] roadPartsObjects = new RennObject[10];

            for (int i = 0; i < parts.Length; i++)
            {
                roadPartsObjects[i] = new RennObject();
                MeshRenderer roadPartRenderer = roadPartsObjects[i].AddComponent<MeshRenderer>();
                roadPartRenderer.SetShaderProgram(roadShader);
                roadPartRenderer.SetTriangleMesh(parts[i].Road);
                roadPartRenderer.SetTexture(roadTexture);
                if (i == 3) roadPartRenderer.SetTexture(gravelTexture);
                objects.Add(roadPartsObjects[i]);
            }

            roadPartsObjects[3].GetComponent<MeshRenderer>().SetTexture(gravelTexture);
            roadPartsObjects[6].GetComponent<MeshRenderer>().SetTexture(gravelTexture);

            RennObject curb = new RennObject();
            MeshRenderer curbRenderer = curb.AddComponent<MeshRenderer>();
            curbRenderer.SetShaderProgram(curbShader);
            curbRenderer.SetTriangleMesh(parts[0].Curb);
            curbRenderer.SetTexture(curbTexture);
            objects.Add(curb);

            return objects;
        }
    }
}
