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
            -0.7f, -0.5f, 0.0f,
            0.7f, -0.5f, 0.0f,
            -0.7f,  -1.0f, 0.0f
        };

        float[] plane2Vertices = new float[] {
            -0.8f,   0.6f, 0.0f,  
            -0.8f,   0.0f, 0.0f, 
            -0.2f,   0.0f, 0.0f,
            -0.8f,   0.6f, 0.0f,
            -0.2f,   0.6f, 0.0f,
            -0.2f,   0.0f, 0.0f,
        };

        public TestObjectsSetup()
        {
            baseShader = new SimpleShader();
            baseShader.SetDefaultShaderAttribute(SimpleShader.BASE_COLOR, ShaderAttribute.Value(Color.Aqua));
        }

        public HashSet<RennObject> GetObjects()
        {
            HashSet<RennObject> objects = new HashSet<RennObject>();

            RennObject cube = new RennObject();
            cube.Transform.SetPosition(1f, 0, 0);
            cube.Transform.SetRotation(90, 0, 0f);
            cube.Transform.SetScale(0.2f);
            cube.AddComponent<MeshRenderer>();
            MeshRenderer cubeMeshRenderer = cube.GetComponent<MeshRenderer>();
            cubeMeshRenderer.SetShaderProgram(baseShader);
            cubeMeshRenderer.SetTriangleMesh(planeVertices);
            cubeMeshRenderer.SetShaderAttribute(SimpleShader.BASE_COLOR, ShaderAttribute.Value(Color.Red));
            
            objects.Add(cube);

            RennObject cube2 = new RennObject();
            cube2.Transform.SetPosition(-1f, 0, 0);
            cube2.Transform.SetScale(0.5f);
            cube2.AddComponent<MeshRenderer>();
            MeshRenderer cube2MeshRenderer = cube2.GetComponent<MeshRenderer>();
            cube2MeshRenderer.SetShaderProgram(baseShader);
            cube2MeshRenderer.SetTriangleMesh(plane2Vertices);
            cube2MeshRenderer.SetShaderAttribute(SimpleShader.BASE_COLOR, ShaderAttribute.Value(Color.Blue));
            cube2.SetParent(cube);
            objects.Add(cube2);

            return objects;
        }
    }
}
