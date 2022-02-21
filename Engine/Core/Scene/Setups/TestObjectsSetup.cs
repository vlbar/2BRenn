using System.Collections.Generic;
using System.Drawing;
using TwoBRenn.Engine.Components;
using TwoBRenn.Engine.Core.Render;
using TwoBRenn.Engine.Core.Render.ShaderPrograms;
using TwoBRenn.Engine.Interfaces;

namespace TwoBRenn.Engine.Core.Scene.Setups
{
    class TestObjectsSetup : IObjectsSetup
    {
        BaseShaderProgram baseShader;

        float[] planeVertices = new float[] {
            -0.5f, -0.3f, -0.5f,
             0.5f, -0.3f, -0.5f,
            -0.5f,  0.7f, -0.5f,
             0.5f,  0.7f, -0.5f
        };

        uint[] planeIndexes = new uint[] {
            0, 1, 3,
            0, 2, 3,
        };

        float[] cubeVertices = new float[] {
            0.5f, 0.5f, -0.5f,
            0.5f, -0.5f, -0.5f,
            0.5f, 0.5f, 0.5f,
            0.5f, -0.5f, 0.5f,
            -0.5f, 0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, 0.5f, 0.5f,
            -0.5f, -0.5f, 0.5f,
        };

        uint[] cubeIndexes = new uint[] {
            0, 1, 2,
            1, 2, 3,
            4, 5, 6,
            5, 6, 7,
            0, 4, 6,
            2, 6, 0,
            0, 1, 4,
            1, 4, 5,
            1, 3, 5,
            3, 5, 7,
        };

        public TestObjectsSetup()
        {
            baseShader = new SimpleShader();
            baseShader.SetDefaultShaderAttribute(SimpleShader.BASE_COLOR, ShaderAttribute.Value(Color.Aqua));
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
            planeRenderer.SetTriangleMesh(planeVertices, planeIndexes);
            planeRenderer.SetShaderAttribute(SimpleShader.BASE_COLOR, ShaderAttribute.Value(Color.Red));
            objects.Add(plane);

            RennObject cube = new RennObject();
            cube.Transform.SetPosition(-1f, 0, 0);
            cube.Transform.SetScale(0.5f);
            cube.AddComponent<MeshRenderer>();
            MeshRenderer cubeRenderer = cube.GetComponent<MeshRenderer>();
            cubeRenderer.SetShaderProgram(baseShader);
            cubeRenderer.SetTriangleMesh(cubeVertices, cubeIndexes);
            cubeRenderer.SetShaderAttribute(SimpleShader.BASE_COLOR, ShaderAttribute.Value(Color.Blue));
            objects.Add(cube);

            return objects;
        }
    }
}
