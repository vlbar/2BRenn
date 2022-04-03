using System;
using System.Collections.Generic;
using OpenTK;
using TwoBRenn.Engine;
using TwoBRenn.Engine.Common.ObjectsPlacers;
using TwoBRenn.Engine.Common.Path;
using TwoBRenn.Engine.Components;
using TwoBRenn.Engine.Interfaces;
using TwoBRenn.Engine.Render.ShaderPrograms;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.ObjectsSetups
{
    class AutodromeObjectsSetup : IObjectsSetup
    {
        private readonly Texture groundTexture = new Texture(@"Assets\Textures\ground.jpg");
        private readonly Texture roadTexture = new Texture(@"Assets\Textures\road.jpg");
        private readonly Texture curbTexture = new Texture(@"Assets\Textures\curb.png");
        private readonly Texture gravelTexture = new Texture(@"Assets\Textures\gravel.jpg");
        private readonly Texture treeTextureAtlas = new Texture(@"Assets\Textures\Environment\spruce.jpg");
        private readonly SimpleShader groundShader = new SimpleShader();
        private readonly SimpleShader roadShader = new SimpleShader();
        private readonly SimpleShader curbShader = new SimpleShader();
        private readonly SimpleShader treeShader = new SimpleShader();

        private readonly float[,] forestMap = ImageMap.GenerateMap(@"Assets\Textures\Maps\forest-map.png", 24);

        public AutodromeObjectsSetup()
        {
            groundShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(2, 2));
            roadShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(1, 30));
            curbShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(1, 60));
            treeShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(5, 5));
        }

        public HashSet<RennObject> GetObjects()
        {
            HashSet<RennObject> objects = new HashSet<RennObject>();

            AddGround(objects);
            AddRoad(objects);
            AddForest(objects);

            return objects;
        }

        private void AddGround(HashSet<RennObject> objects)
        {
            RennObject ground = new RennObject();
            ground.Transform.SetPosition(-240f, 0f, -170f);
            objects.Add(ground);

            for (int i = 0; i < 38; i++)
            {
                for (int j = 0; j < 38; j++)
                {
                    RennObject plane = new RennObject();
                    plane.SetParent(ground);
                    plane.Transform.SetPosition(i * 10, 0f, j * 10);
                    plane.Transform.SetScale(10f);
                    MeshRenderer planeRenderer = plane.AddComponent<MeshRenderer>();
                    planeRenderer.SetTriangleMesh(PrimitiveMeshFactory.CreatePlane());
                    planeRenderer.SetShaderProgram(groundShader);
                    planeRenderer.SetTexture(groundTexture);
                    objects.Add(plane);
                }
            }
        }

        private void AddForest(HashSet<RennObject> objects)
        {
            RennObject forest = new RennObject();
            forest.Transform.SetPosition(-240f, 0f, -170f);
            forest.Transform.SetRotation(0, 0, 0);
            forest.Transform.SetScale(9f);
            objects.Add(forest);

            Random random = new Random();
            Mesh pineMesh = EnvironmentMeshFactory.CreateMesh(EnvironmentType.Spruce);
            for (int i = 0; i < forestMap.GetLength(0); i++)
            {
                for (int j = 0; j < forestMap.GetLength(1); j++)
                {
                    float value = forestMap[i, j];
                    if (value < 0.1f && random.NextDouble() > 0.9f)
                    {
                        RennObject tree = new RennObject();
                        tree.SetParent(forest);
                        tree.Transform.SetPosition(i + (float)random.NextDouble(), 0f, j + (float)random.NextDouble());
                        tree.Transform.SetRotation(0f, random.Next(180), 0f);
                        tree.Transform.SetScale(0.1f + (0.1f - value));
                        MeshRenderer treeRenderer = tree.AddComponent<MeshRenderer>();
                        treeRenderer.SetTriangleMesh(pineMesh);
                        treeRenderer.SetShaderProgram(treeShader);
                        treeRenderer.SetTexture(treeTextureAtlas);
                        objects.Add(tree);
                    }
                }
            }
        }

        private void AddRoad(HashSet<RennObject> objects)
        {
            // main road
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

            RennObject road = new RennObject();
            road.Transform.SetPosition(-180f, 0.2f, 85f);
            road.Transform.SetScale(7f);
            objects.Add(road);

            RoadCreatorSettings roadCreatorSettings = new RoadCreatorSettings();
            RoadPart[] parts = RoadCreator.CreateMeshes(path.CalculateEvenlySpacedPoints(0.1f), roadCreatorSettings, 10);
            RennObject[] roadPartsObjects = new RennObject[10];

            for (int i = 0; i < parts.Length; i++)
            {
                roadPartsObjects[i] = new RennObject();
                roadPartsObjects[i].SetParent(road);
                MeshRenderer roadPartRenderer = roadPartsObjects[i].AddComponent<MeshRenderer>();
                roadPartRenderer.SetShaderProgram(roadShader);
                roadPartRenderer.SetTriangleMesh(parts[i].Road);
                roadPartRenderer.SetTexture(roadTexture);
                objects.Add(roadPartsObjects[i]);
            }

            roadPartsObjects[3].GetComponent<MeshRenderer>().SetTexture(gravelTexture);
            roadPartsObjects[6].GetComponent<MeshRenderer>().SetTexture(gravelTexture);

            RennObject curb = new RennObject();
            curb.SetParent(road);
            MeshRenderer curbRenderer = curb.AddComponent<MeshRenderer>();
            curbRenderer.SetShaderProgram(curbShader);
            curbRenderer.SetTriangleMesh(parts[0].Curb);
            curbRenderer.SetTexture(curbTexture);
            objects.Add(curb);

            // tech roads
            RoadCreatorSettings techRoadCreatorSettings = new RoadCreatorSettings
            {
                IsClosed = false
            };

            Path techPath1 = new Path();
            techPath1.AddManualSegment(Vector3.Zero, new Vector3(9.2f, 0f, -5f), new Vector3(8.8f, 0f, -7f));
            techPath1.AddManualSegment(new Vector3(13.5f, 0f, -12f), new Vector3(13.5f, 0f, -10f), Vector3.Zero);
            RoadPart techRoadPart1 = RoadCreator.CreateMesh(techPath1.CalculateEvenlySpacedPoints(0.5f), techRoadCreatorSettings);
            RennObject techRoad1 = new RennObject();
            techRoad1.SetParent(road);
            MeshRenderer techRoadRenderer = techRoad1.AddComponent<MeshRenderer>();
            techRoadRenderer.SetShaderProgram(roadShader);
            techRoadRenderer.SetShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(1, 2));
            techRoadRenderer.SetTriangleMesh(techRoadPart1.Road);
            techRoadRenderer.SetTexture(gravelTexture);
            objects.Add(techRoad1);

            Path techPath2 = new Path();
            techPath2.AddManualSegment(Vector3.Zero, new Vector3(31f, 0f, -12.5f), new Vector3(27f, 0f, -12.5f));
            techPath2.AddManualSegment(new Vector3(29.5f, 0f, -8.5f), new Vector3(30.5f, 0f, -7f), new Vector3(31.5f, 0f, -5.5f));
            RoadPart techRoadPart2 = RoadCreator.CreateMesh(techPath2.CalculateEvenlySpacedPoints(0.5f), techRoadCreatorSettings);
            RennObject techRoad2 = new RennObject();
            techRoad2.SetParent(road);
            MeshRenderer techRoadRenderer2 = techRoad2.AddComponent<MeshRenderer>();
            techRoadRenderer2.SetShaderProgram(roadShader);
            techRoadRenderer2.SetShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(1, 2));
            techRoadRenderer2.SetTriangleMesh(techRoadPart2.Road);
            techRoadRenderer2.SetTexture(gravelTexture);
            objects.Add(techRoad2);
        }
    }
}