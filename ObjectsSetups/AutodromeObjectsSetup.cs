using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using TwoBRenn.Engine;
using TwoBRenn.Engine.Common.ObjectsPlacers;
using TwoBRenn.Engine.Common.Path;
using TwoBRenn.Engine.Components;
using TwoBRenn.Engine.Interfaces;
using TwoBRenn.Engine.Render.ShaderPrograms;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.Engine.Render.Utils;
using TwoBRenn.Engine.Utils;

namespace TwoBRenn.ObjectsSetups
{
    class AutodromeObjectsSetup : IObjectsSetup
    {
        private readonly Texture groundTexture = new Texture(@"Assets\Textures\ground.jpg");
        private readonly Texture roadTexture = new Texture(@"Assets\Textures\road.jpg");
        private readonly Texture curbTexture = new Texture(@"Assets\Textures\curb.png");
        private readonly Texture gravelTexture = new Texture(@"Assets\Textures\gravel.jpg");
        private readonly Texture plasticTexture = new Texture(@"Assets\Textures\plastic.jpg");
        private readonly Texture carTexture = new Texture(@"Assets\Textures\Car\car.jpg");
        private readonly Texture wheelTexture = new Texture(@"Assets\Textures\Car\wheel.jpg");
        private readonly Texture sponsorsTexture = new Texture(@"Assets\Textures\Environment\sponsors.jpg");
        private readonly Texture treeTexture = new Texture(@"Assets\Textures\Environment\spruce.jpg");
        private readonly Texture smokeTexture = new Texture(@"Assets\Textures\Particles\smoke-puff.png");
        private readonly SimpleShader simpleShader = new SimpleShader();
        private readonly SimpleShader groundShader = new SimpleShader();
        private readonly SimpleShader roadShader = new SimpleShader();
        private readonly SimpleShader curbShader = new SimpleShader();
        private readonly SimpleShader treeShader = new SimpleShader();
        private readonly SimpleShader concreteShader = new SimpleShader();
        private readonly ParticleShader particleShader = new ParticleShader();

        private readonly float[,] forestMap = ImageMap.GenerateMap(@"Assets\Textures\Maps\forest-map.png", 24);

        public AutodromeObjectsSetup()
        {
            groundShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(2, 2));
            roadShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(1, 30));
            curbShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(1, 60));
            treeShader.SetDefaultShaderAttribute(SimpleShader.TILING, ShaderAttribute.Value(5, 5));
            concreteShader.SetDefaultShaderAttribute(SimpleShader.BASE_COLOR, ShaderAttribute.Value(Color.DarkGray));
        }

        public HashSet<RennObject> GetObjects()
        {
            HashSet<RennObject> objects = new HashSet<RennObject>();

            AddGround(objects);
            AddRoad(objects);
            AddForest(objects);
            AddBarriers(objects);
            AddAd(objects);
            AddStands(objects);
            AddTrainCart(objects);

            return objects;
        }

        private void AddAd(HashSet<RennObject> objects)
        {
            for (int i = 0; i < 6; i++)
            {
                RennObject barrier = new RennObject();
                barrier.Transform.SetPosition(-40f + i * 10, 0.1f, 3f);
                barrier.Transform.SetRotation(0f, 90f, 0f);
                barrier.Transform.SetScale(1f);
                MeshRenderer barrierRenderer = barrier.AddComponent<MeshRenderer>();
                barrierRenderer.SetTriangleMesh(EnvironmentMeshFactory.CreateMesh(EnvironmentType.AdFlag));
                barrierRenderer.SetShaderProgram(simpleShader);
                barrierRenderer.SetTexture(sponsorsTexture);
                objects.Add(barrier);
            }

            for (int i = 0; i < 5; i++)
            {
                RennObject adFlag = new RennObject();
                adFlag.Transform.SetPosition(-55f + i * 10, 0.1f, 57f - i * 0.5f);
                adFlag.Transform.SetRotation(0f, 90f, 0f);
                adFlag.Transform.SetScale(1f);
                MeshRenderer adFlagRenderer = adFlag.AddComponent<MeshRenderer>();
                adFlagRenderer.SetTriangleMesh(EnvironmentMeshFactory.CreateMesh(EnvironmentType.AdFlag));
                adFlagRenderer.SetShaderProgram(simpleShader);
                adFlagRenderer.SetTexture(sponsorsTexture);
                objects.Add(adFlag);
            }

            for (int i = 0; i < 5; i++)
            {
                RennObject adStand = new RennObject();
                adStand.Transform.SetPosition(-85f + i * 10, 0.1f, 0f - i * 3.5f);
                adStand.Transform.SetRotation(0f, 0f, 0f);
                adStand.Transform.SetScale(3f);
                MeshRenderer adStandRenderer = adStand.AddComponent<MeshRenderer>();
                adStandRenderer.SetTriangleMesh(EnvironmentMeshFactory.CreateMesh(EnvironmentType.AdStand));
                adStandRenderer.SetShaderProgram(simpleShader);
                adStandRenderer.SetTexture(sponsorsTexture);
                objects.Add(adStand);
            }

            for (int i = 0; i < 5; i++)
            {
                RennObject adStand = new RennObject();
                adStand.Transform.SetPosition(-150f + i * 9, 0.1f, -30f + i * 10.5f);
                adStand.Transform.SetRotation(0f, 0f, 0f);
                adStand.Transform.SetScale(3f);
                MeshRenderer adStandRenderer = adStand.AddComponent<MeshRenderer>();
                adStandRenderer.SetTriangleMesh(EnvironmentMeshFactory.CreateMesh(EnvironmentType.AdStand));
                adStandRenderer.SetShaderProgram(simpleShader);
                adStandRenderer.SetTexture(sponsorsTexture);
                objects.Add(adStand);
            }

            for (int i = 0; i < 5; i++)
            {
                RennObject adPlane = new RennObject();
                adPlane.Transform.SetPosition(-35f + i * 10, 0.1f, -7.5f);
                adPlane.Transform.SetRotation(0f, 90f, 0f);
                adPlane.Transform.SetScale(2f);
                MeshRenderer adPlaneRenderer = adPlane.AddComponent<MeshRenderer>();
                adPlaneRenderer.SetTriangleMesh(EnvironmentMeshFactory.CreateMesh(EnvironmentType.AdPlane));
                adPlaneRenderer.SetShaderProgram(simpleShader);
                adPlaneRenderer.SetTexture(sponsorsTexture);
                objects.Add(adPlane);
            }

            for (int i = 0; i < 5; i++)
            {
                RennObject adPlane = new RennObject();
                adPlane.Transform.SetPosition(-120f + i * 10, 0.1f, 95f);
                adPlane.Transform.SetRotation(0f, -35f, 0f);
                adPlane.Transform.SetScale(2f);
                MeshRenderer adPlaneRenderer = adPlane.AddComponent<MeshRenderer>();
                adPlaneRenderer.SetTriangleMesh(EnvironmentMeshFactory.CreateMesh(EnvironmentType.AdPlane));
                adPlaneRenderer.SetShaderProgram(simpleShader);
                adPlaneRenderer.SetTexture(sponsorsTexture);
                objects.Add(adPlane);
            }
        }

        private void AddStands(HashSet<RennObject> objects)
        {
            List<Transform> transforms = new List<Transform>();
            Transform standTransform1 = new Transform();
            standTransform1.SetPosition(-100, 0, -30);
            standTransform1.SetRotation(0f, 45f, 0f);
            standTransform1.SetScale(10f, 3f, 25f);
            transforms.Add(standTransform1);

            Transform standTransform2 = new Transform();
            standTransform2.SetPosition(0, 0, -30);
            standTransform2.SetScale(40f, 10f, 15f);
            transforms.Add(standTransform2);

            Transform standTransform3 = new Transform();
            standTransform3.SetPosition(0, 0, 70);
            standTransform3.SetScale(40f, 3f, 15f);
            transforms.Add(standTransform3);

            Transform standTransform4 = new Transform();
            standTransform4.SetPosition(-150, 0, 60);
            standTransform4.SetScale(15f, 3f, 40f);
            transforms.Add(standTransform4);

            Transform standTransform5 = new Transform();
            standTransform5.SetPosition(-160, 0, -25);
            standTransform5.SetScale(4f, 8f, 4f);
            transforms.Add(standTransform5);

            Transform standTransform6 = new Transform();
            standTransform6.SetPosition(-50, 0, 30);
            standTransform6.SetScale(9f, 8f, 9f);
            transforms.Add(standTransform6);

            foreach (var transform in transforms)
            {
                RennObject stand = new RennObject();
                stand.Transform.SetPosition(transform.position);
                stand.Transform.SetRotation(transform.rotation);
                stand.Transform.SetScale(transform.scale);
                MeshRenderer standRenderer = stand.AddComponent<MeshRenderer>();
                standRenderer.SetTriangleMesh(PrimitiveMeshFactory.CreateCube());
                standRenderer.SetShaderProgram(concreteShader);
                standRenderer.SetTexture(plasticTexture);
                objects.Add(stand);
            }
        }

        private void AddBarriers(HashSet<RennObject> objects)
        {
            for (int i = 0; i < 40; i++)
            {
                RennObject barrier = new RennObject();
                barrier.Transform.SetPosition(-40f + i * 2, 0.1f, -8f);
                barrier.Transform.SetRotation(0f, 0f, 0f);
                barrier.Transform.SetScale(1f);
                MeshRenderer barrierRenderer = barrier.AddComponent<MeshRenderer>();
                barrierRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.CreateStructure(StructureType.Barrier));
                barrierRenderer.SetShaderProgram(simpleShader);
                if (i % 2 == 1) barrierRenderer.SetShaderAttribute(SimpleShader.BASE_COLOR, ShaderAttribute.Value(0.937F, 0.341F, 0.250F, 1f));
                barrierRenderer.SetTexture(plasticTexture);
                objects.Add(barrier);
            }

            for (int i = 0; i < 5; i++)
            {
                RennObject delinator = new RennObject();
                delinator.Transform.SetPosition(-101f + i * 2, 0.1f, 21f + i * 1);
                delinator.Transform.SetRotation(0f, 0f, 0f);
                delinator.Transform.SetScale(0.4f);
                MeshRenderer delinatorRenderer = delinator.AddComponent<MeshRenderer>();
                delinatorRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.CreateStructure(StructureType.Delineator));
                delinatorRenderer.SetShaderProgram(simpleShader);
                delinatorRenderer.SetShaderAttribute(SimpleShader.BASE_COLOR, ShaderAttribute.Value(Color.LightGray));
                delinatorRenderer.SetTexture(plasticTexture);
                objects.Add(delinator);
            }

            for (int i = 0; i < 7; i++)
            {
                RennObject delinator = new RennObject();
                delinator.Transform.SetPosition(-107f + i * 0.2f, 0.1f, 27f + i * 2);
                delinator.Transform.SetRotation(0f, 0f, 0f);
                delinator.Transform.SetScale(0.4f);
                MeshRenderer delinatorRenderer = delinator.AddComponent<MeshRenderer>();
                delinatorRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.CreateStructure(StructureType.Delineator));
                delinatorRenderer.SetShaderProgram(simpleShader);
                delinatorRenderer.SetShaderAttribute(SimpleShader.BASE_COLOR, ShaderAttribute.Value(Color.LightGray));
                delinatorRenderer.SetTexture(plasticTexture);
                objects.Add(delinator);
            }
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
                        treeRenderer.SetTexture(treeTexture);
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
            }

            roadPartsObjects[3].GetComponent<MeshRenderer>().SetTexture(gravelTexture);
            roadPartsObjects[6].GetComponent<MeshRenderer>().SetTexture(gravelTexture);

            RennObject curb = new RennObject();
            curb.SetParent(road);
            MeshRenderer curbRenderer = curb.AddComponent<MeshRenderer>();
            curbRenderer.SetShaderProgram(curbShader);
            curbRenderer.SetTriangleMesh(parts[0].Curb);
            curbRenderer.SetTexture(curbTexture);

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
        }

        private void AddTrainCart(HashSet<RennObject> objects)
        {
            Path path = new Path();
            path.AddManualSegment(Vector3.Zero, new Vector3(35f, 0f, 0.5f), new Vector3(55f, 0f, 0.5f));
            path.AddManualSegment(new Vector3(80f, 0f, 30f), new Vector3(80f, 0f, 40f), new Vector3(80f, 0f, 60f));
            path.AddManualSegment(new Vector3(50f, 0f, 60f), new Vector3(40f, 0f, 50f), new Vector3(30f, 0f, 40f));
            path.IsClosed = true;

            RennObject car = new RennObject();
            car.Transform.SetScale(0.4f);
            MeshRenderer carRenderer = car.AddComponent<MeshRenderer>();
            carRenderer.SetShaderProgram(simpleShader);
            carRenderer.SetTriangleMesh(CarsMeshFactory.CreateCar(CarType.SportCar));
            carRenderer.SetTexture(carTexture);
            PathFollow carPathFollow = car.AddComponent<PathFollow>();
            carPathFollow.Path = path;
            carPathFollow.MoveSpeed = 0.34f;
            objects.Add(car);

            List<Transform> wheelTransforms = new List<Transform>();
            Transform frontRight = new Transform();
            frontRight.SetPosition(3.8f, 1.3f, 2.5f);
            frontRight.SetRotation(0, 180, 0);
            wheelTransforms.Add(frontRight);

            Transform frontLeft = new Transform();
            frontLeft.SetPosition(3.8f, 1.3f, -2.5f);
            frontLeft.SetRotation(0, 0, 0);
            wheelTransforms.Add(frontLeft);

            Transform backRight = new Transform();
            backRight.SetPosition(-4.2f, 1.3f, 2.5f);
            backRight.SetRotation(0, 180, 0);
            wheelTransforms.Add(backRight);

            Transform backLeft = new Transform();
            backLeft.SetPosition(-4.2f, 1.3f, -2.5f);
            backLeft.SetRotation(0, 0, 0);
            wheelTransforms.Add(backLeft);

            foreach (var transform in wheelTransforms)
            {
                RennObject wheelHolder = new RennObject();
                wheelHolder.Transform.SetPosition(transform.position);
                wheelHolder.SetParent(car);
                wheelHolder.AddComponent<LoopRotation>().Speed = -1000;

                RennObject wheel = new RennObject();
                wheel.Transform.SetRotation(transform.rotation);
                wheel.SetParent(wheelHolder);
                MeshRenderer wheelRenderer = wheel.AddComponent<MeshRenderer>();
                wheelRenderer.SetShaderProgram(simpleShader);
                wheelRenderer.SetTriangleMesh(CarsMeshFactory.CreateCar(CarType.Wheel));
                wheelRenderer.SetTexture(wheelTexture);
            }

            RennObject carSmoke = new RennObject();
            carSmoke.Transform.SetPosition(-4, 0, 0f);
            carSmoke.SetParent(car);

            ParticleEmitter emitter = carSmoke.AddComponent<ParticleEmitter>();
            emitter.IsPlay = false;
            emitter.ShaderProgram = particleShader;
            emitter.Texture = smokeTexture;

            emitter.MaxParticles = 100;
            emitter.EmitRate = 20f;
            emitter.EmitRange = Vector3.UnitX * 1f + Vector3.UnitZ * 1f;
            emitter.StartSize = new Vector2(3.0f, 4.5f);
            emitter.SizeVelocity = new Vector2(3f, 4f);
            emitter.StartRotation = new Vector2(-180, 180);
            emitter.RotationVelocity = new Vector2(-30, 30);
            emitter.Velocity = Vector3.UnitY * 0.5f;
            emitter.VelocitySpread = new Vector3(0, 0.3f, 0);
            emitter.ColorVelocity = new Vector4(0, 0, 0, -150f);
            emitter.LifeTime = new Vector2(2f, 2f);

            carPathFollow.DriftParticle = emitter;
        }
    }
}