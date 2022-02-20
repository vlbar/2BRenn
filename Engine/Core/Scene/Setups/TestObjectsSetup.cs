using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using TwoBRenn.Engine.Components;
using TwoBRenn.Engine.Interfaces;

namespace TwoBRenn.Engine.Core.Scene.Setups
{
    class TestObjectsSetup : IObjectsSetup
    {
        public HashSet<RennObject> GetObjects()
        {
            HashSet<RennObject> objects = new HashSet<RennObject>();

            RennObject cube = new RennObject();
            cube.Transform.SetPosition(1f, 0, 0);
            cube.Transform.SetRotation(90, 0, 0f);
            cube.Transform.SetScale(0.2f);
            cube.AddComponent<MeshRenderer>().SetRenderAction(DrawCube);
            objects.Add(cube);

            RennObject cube2 = new RennObject();
            cube2.Transform.SetPosition(-1f, 0, 0);
            cube2.Transform.SetScale(0.5f);
            cube2.AddComponent<MeshRenderer>().SetRenderAction(DrawCube);
            cube2.SetParent(cube);
            objects.Add(cube2);

            RennObject axis = new RennObject();
            axis.AddComponent<MeshRenderer>().SetRenderAction(DrawAxis);
            objects.Add(axis);

            return objects;
        }

        private void DrawAxis()
        {
            GL.Begin(BeginMode.Lines);
            GL.Color3(1f, 0f, 0f); GL.Vertex3(-1f, 0f, 0f); GL.Vertex3(1f, 0f, 0f);
            GL.Color3(0f, 1f, 0f); GL.Vertex3(0f, -1f, 0f); GL.Vertex3(0f, 1f, 0f);
            GL.Color3(0f, 0f, 1f); GL.Vertex3(0f, 0f, -1f); GL.Vertex3(0f, 0f, 1f);
            GL.End();
        }

        private void DrawCube()
        {
            GL.Begin(BeginMode.Quads);

            GL.Color3(Color.Silver);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.Honeydew);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.Moccasin);

            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.IndianRed);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }
    }
}
