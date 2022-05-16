using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using TwoBRenn.Common;
using TwoBRenn.Engine;
using TwoBRenn.Engine.Common.ObjectsPlacers;
using TwoBRenn.Engine.Common.Path;
using TwoBRenn.Engine.Components.Common;
using TwoBRenn.Engine.Components.Light;
using TwoBRenn.Engine.Components.Physic;
using TwoBRenn.Engine.Components.Render;
using TwoBRenn.Engine.Interfaces;
using TwoBRenn.Engine.Render.Camera;
using TwoBRenn.Engine.Render.ShaderPrograms;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.Engine.Render.Utils;
using TwoBRenn.Engine.Utils;
using TwoBRenn.ObjectsSetups.MeshFactories;

namespace TwoBRenn.ObjectsSetups
{
    class AutodromeObjectsSetup : IObjectsSetup
    {
        public Transform[] CarCameraTargetTransforms = new Transform[2];
        public CarController[] CarControllers = new CarController[2];

        private Texture groundTexture;
        private Texture roadTexture;
        private Texture curbTexture;
        private Texture gravelTexture;
        private Texture sandTexture;
        private Texture plasticTexture;
        private Texture carTexture;
        private Texture gelikTexture;
        private Texture wheelTexture;
        private Texture sponsorsTexture;
        private Texture treeTexture;
        private Texture smokeTexture;
        private Texture animeTexture;
        private SimpleShader simpleShader;
        private SimpleShader groundShader;
        private SimpleShader roadShader;
        private SimpleShader curbShader;
        private SimpleShader treeShader;
        private SimpleShader concreteShader;
        private SimpleShader animeShader;
        private SimpleShader orangeShader;
        private ParticleShader particleShader;
        private InstanceShader instanceShader;

        private readonly float[,] forestMap = ImageMap.GenerateMap(@"Assets\Textures\Maps\forest-map.png", 24);
        
        private void SetupAssets()
        {
            groundTexture = new Texture(@"Assets\Textures\ground.jpg");
            roadTexture = new Texture(@"Assets\Textures\road.jpg");
            curbTexture = new Texture(@"Assets\Textures\curb.png");
            gravelTexture = new Texture(@"Assets\Textures\gravel.jpg");
            sandTexture = new Texture(@"Assets\Textures\sand.jpg");
            plasticTexture = new Texture(@"Assets\Textures\plastic.jpg");
            carTexture = new Texture(@"Assets\Textures\Car\car.jpg");
            gelikTexture = new Texture(@"Assets\Textures\Car\bronirovany.jpg");
            wheelTexture = new Texture(@"Assets\Textures\Car\wheel.jpg");
            sponsorsTexture = new Texture(@"Assets\Textures\Environment\sponsors.jpg");
            treeTexture = new Texture(@"Assets\Textures\Environment\spruce.jpg");
            smokeTexture = new Texture(@"Assets\Textures\Particles\smoke-puff.png");
            animeTexture = new Texture(@"Assets\Textures\Car\car-chill-animation.jpg");
            simpleShader = new SimpleShader();
            groundShader = new SimpleShader();
            roadShader = new SimpleShader();
            curbShader = new SimpleShader();
            treeShader = new SimpleShader();
            concreteShader = new SimpleShader();
            particleShader = new ParticleShader();
            instanceShader = new InstanceShader();
            animeShader = new SimpleShader();
            orangeShader = new SimpleShader();

            groundShader.SetDefaultShaderAttribute(SimpleShader.TilingUniform, ShaderAttribute.Value(50, 50));
            roadShader.SetDefaultShaderAttribute(SimpleShader.TilingUniform, ShaderAttribute.Value(1, 30));
            curbShader.SetDefaultShaderAttribute(SimpleShader.TilingUniform, ShaderAttribute.Value(1, 60));
            treeShader.SetDefaultShaderAttribute(SimpleShader.TilingUniform, ShaderAttribute.Value(5, 5));
            concreteShader.SetDefaultShaderAttribute(SimpleShader.BaseColorUniform, ShaderAttribute.Value(Color.DarkGray));
            orangeShader.SetDefaultShaderAttribute(SimpleShader.BaseColorUniform, ShaderAttribute.Value(0.937F, 0.341F, 0.250F, 1f));
        }

        public HashSet<RennObject> GetObjects()
        {
            SetupAssets();
            Camera.Instance.Controller.SetPosition(new Vector3(10, 0, 4));
            Camera.Instance.Controller.SetRotation(new Vector3(20, 40, 15));
            HashSet<RennObject> objects = new HashSet<RennObject>();

            AddGround(objects);
            AddRoad(objects);
            AddForest(objects);
            AddBarriers(objects); 
            AddAd(objects);
            AddStands(objects);
            AddTrainCart(objects);
            AddMovableCar(objects);
            AddMovableCar2(objects);

            return objects;
        }

        private void AddAd(HashSet<RennObject> objects)
        {
            for (int i = 0; i < 6; i++)
            {
                RennObject adFlag = new RennObject();
                adFlag.Transform.SetPosition(-40f + i * 10, 0.1f, 3f);
                adFlag.Transform.SetRotation(0f, 90f, 0f);
                adFlag.Transform.SetScale(1f);
                adFlag.AddComponent<BoxCollider>();
                adFlag.AddComponent<FallingPillar>();
                MeshRenderer barrierRenderer = adFlag.AddComponent<MeshRenderer>();
                barrierRenderer.SetTriangleMesh(EnvironmentMeshFactory.GetMesh(EnvironmentType.AdFlag));
                barrierRenderer.SetShaderProgram(simpleShader);
                barrierRenderer.SetTexture(sponsorsTexture);
                objects.Add(adFlag);
            }

            for (int i = 0; i < 5; i++)
            {
                RennObject adFlag = new RennObject();
                adFlag.Transform.SetPosition(-55f + i * 10, 0.1f, 57f - i * 0.5f);
                adFlag.Transform.SetRotation(0f, 90f, 0f);
                adFlag.Transform.SetScale(1f);
                adFlag.AddComponent<BoxCollider>();
                adFlag.AddComponent<FallingPillar>();
                MeshRenderer adFlagRenderer = adFlag.AddComponent<MeshRenderer>();
                adFlagRenderer.SetTriangleMesh(EnvironmentMeshFactory.GetMesh(EnvironmentType.AdFlag));
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
                adStand.AddComponent<Rigidbody>();
                adStand.AddComponent<BoxCollider>();
                MeshRenderer adStandRenderer = adStand.AddComponent<MeshRenderer>();
                adStandRenderer.SetTriangleMesh(EnvironmentMeshFactory.GetMesh(EnvironmentType.AdStand));
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
                adStand.AddComponent<Rigidbody>();
                adStand.AddComponent<BoxCollider>();
                MeshRenderer adStandRenderer = adStand.AddComponent<MeshRenderer>();
                adStandRenderer.SetTriangleMesh(EnvironmentMeshFactory.GetMesh(EnvironmentType.AdStand));
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
                adPlaneRenderer.SetTriangleMesh(EnvironmentMeshFactory.GetMesh(EnvironmentType.AdPlane));
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
                adPlane.AddComponent<BoxCollider>();
                FallingPillar fallingAd = adPlane.AddComponent<FallingPillar>();
                fallingAd.BlockAxis = Vector3.UnitX;
                fallingAd.FallenY = 0;
                MeshRenderer adPlaneRenderer = adPlane.AddComponent<MeshRenderer>();
                adPlaneRenderer.SetTriangleMesh(EnvironmentMeshFactory.GetMesh(EnvironmentType.AdPlane));
                adPlaneRenderer.SetShaderProgram(simpleShader);
                adPlaneRenderer.SetTexture(sponsorsTexture);
                objects.Add(adPlane);
            }

            RennObject adScreen = new RennObject();
            adScreen.Transform.SetPosition(-10, 6f, -22f);
            adScreen.Transform.SetRotation(0f, 0f, 0f);
            adScreen.Transform.SetScale(3.99f, 3.0f, 1f);
            adScreen.AddComponent<TextureAnimation>();
            MeshRenderer adScreenRenderer = adScreen.AddComponent<MeshRenderer>();
            adScreenRenderer.SetTriangleMesh(PrimitiveMeshFactory.CreateCube());
            adScreenRenderer.SetShaderProgram(animeShader);
            adScreenRenderer.SetTexture(animeTexture);
            objects.Add(adScreen);

            RennObject frame = new RennObject();
            frame.Transform.SetPosition(-10f, 5.95f, -22.1f);
            frame.Transform.SetRotation(0f, 0f, 0f);
            frame.Transform.SetScale(4.1f, 3.1f, 1f);
            MeshRenderer frameRenderer = frame.AddComponent<MeshRenderer>();
            frameRenderer.SetTriangleMesh(PrimitiveMeshFactory.CreateCube());
            frameRenderer.SetShaderProgram(simpleShader);
            frameRenderer.SetTexture(plasticTexture);
            objects.Add(frame);

            RennObject adScreen2 = new RennObject();
            adScreen2.Transform.SetPosition(-3, 6f, -22f);
            adScreen2.Transform.SetRotation(0f, 0f, 0f);
            adScreen2.Transform.SetScale(3.99f, 3.0f, 1f);
            adScreen2.AddComponent<TextureAnimation>();
            MeshRenderer adScreenRenderer2 = adScreen2.AddComponent<MeshRenderer>();
            adScreenRenderer2.SetTriangleMesh(PrimitiveMeshFactory.CreateCube());
            adScreenRenderer2.SetShaderProgram(animeShader);
            adScreenRenderer2.SetTexture(animeTexture);
            objects.Add(adScreen2);

            RennObject frame2 = new RennObject();
            frame2.Transform.SetPosition(-3f, 5.95f, -22.1f);
            frame2.Transform.SetRotation(0f, 0f, 0f);
            frame2.Transform.SetScale(4.1f, 3.1f, 1f);
            MeshRenderer frameRenderer2 = frame2.AddComponent<MeshRenderer>();
            frameRenderer2.SetTriangleMesh(PrimitiveMeshFactory.CreateCube());
            frameRenderer2.SetShaderProgram(simpleShader);
            frameRenderer2.SetTexture(plasticTexture);
            objects.Add(frame2);
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

            Mesh cube = PrimitiveMeshFactory.CreateCube();
            foreach (var transform in transforms)
            {
                RennObject stand = new RennObject();
                stand.Transform.SetPosition(transform.position);
                stand.Transform.SetRotation(transform.rotation);
                stand.Transform.SetScale(transform.scale);
                stand.AddComponent<BoxCollider>();
                MeshRenderer standRenderer = stand.AddComponent<MeshRenderer>();
                standRenderer.SetTriangleMesh(cube);
                standRenderer.SetShaderProgram(concreteShader);
                standRenderer.SetTexture(plasticTexture);
                objects.Add(stand);
            }

            // main building
            objects.Add(SimpleObject(new Vector3(0, 10, -30), Vector3.Zero, new Vector3(40f, 2f, 15f), concreteShader, plasticTexture, cube));
            objects.Add(SimpleObject(new Vector3(0, 6, -30), Vector3.Zero, new Vector3(38f, 8f, 14f), concreteShader, plasticTexture, cube));
            objects.Add(SimpleObject(new Vector3(-10, 14, -30), Vector3.Zero, new Vector3(14f, 8f, 14f), concreteShader, plasticTexture, cube));
            
            // pit line under
            objects.Add(SimpleObject(new Vector3(10, 0, -15), Vector3.Zero, new Vector3(60f, 0.3f, 14f), concreteShader, roadTexture, cube, false));

            // start cell
            objects.Add(SimpleObject(new Vector3(5, 2.5f, -10), Vector3.Zero, new Vector3(0.5f, 5f, 0.5f), concreteShader, roadTexture, cube));
            objects.Add(SimpleObject(new Vector3(5, 2.5f, 6), Vector3.Zero, new Vector3(0.5f, 5f, 0.5f), concreteShader, roadTexture, cube));
            objects.Add(SimpleObject(new Vector3(5, 5f, -2), Vector3.Zero, new Vector3(0.4f, 1f, 16f), concreteShader, plasticTexture, cube));

            // tower
            objects.Add(SimpleObject(new Vector3(-50, 5f, 30), Vector3.Zero, new Vector3(8f, 10f, 8f), concreteShader, plasticTexture, cube));
            objects.Add(SimpleObject(new Vector3(-50, 11f, 30), Vector3.Zero, new Vector3(9f, 2f, 9f), concreteShader, plasticTexture, cube));
            objects.Add(SimpleObject(new Vector3(-50, 12f, 30), Vector3.Zero, new Vector3(0.4f, 5f, 0.4f), concreteShader, roadTexture, cube));
            
            // tower place
            objects.Add(SimpleObject(new Vector3(-40, 0f, 25), Vector3.Zero, new Vector3(30f, 0.3f, 20f), concreteShader, roadTexture, cube, false));
        }

        private RennObject SimpleObject(Vector3 position, Vector3 rotation, Vector3 scale, BaseShaderProgram shader, Texture texture, Mesh mesh, bool rigid = true)
        {
            RennObject cube = new RennObject();
            cube.Transform.SetPosition(position);
            cube.Transform.SetRotation(rotation);
            cube.Transform.SetScale(scale);
            if(rigid) cube.AddComponent<BoxCollider>();
            MeshRenderer cubeRenderer = cube.AddComponent<MeshRenderer>();
            cubeRenderer.SetTriangleMesh(mesh);
            cubeRenderer.SetShaderProgram(shader);
            cubeRenderer.SetTexture(texture);
            return cube;
        }

        private void AddBarriers(HashSet<RennObject> objects)
        {
            for (int i = 0; i < 40; i++)
            {
                RennObject barrier = new RennObject();
                barrier.Transform.SetPosition(-40f + i * 2.1f, 0.1f, -8f);
                barrier.Transform.SetRotation(0f, 0f, 0f);
                barrier.Transform.SetScale(1f);
                barrier.AddComponent<BoxCollider>();
                MeshRenderer barrierRenderer = barrier.AddComponent<MeshRenderer>();
                barrierRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.Barrier));
                barrierRenderer.SetShaderProgram(simpleShader);
                if (i % 2 == 1) barrierRenderer.SetShaderProgram(orangeShader);
                barrierRenderer.SetTexture(plasticTexture);
                objects.Add(barrier);
            }

            for (int i = 0; i < 25; i++)
            {
                RennObject barrier = new RennObject();
                barrier.Transform.SetPosition(-40f + i * 2.1f, 0.1f, 5f);
                barrier.Transform.SetRotation(0f, 0f, 0f);
                barrier.Transform.SetScale(1f);
                barrier.AddComponent<BoxCollider>();
                MeshRenderer barrierRenderer = barrier.AddComponent<MeshRenderer>();
                barrierRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.Barrier));
                barrierRenderer.SetShaderProgram(simpleShader);
                if (i % 2 == 1) barrierRenderer.SetShaderProgram(orangeShader);
                barrierRenderer.SetTexture(plasticTexture);
                objects.Add(barrier);
            }

            for (int i = 0; i < 16; i++)
            {
                RennObject barrier = new RennObject();
                barrier.Transform.SetPosition(-60f + i * 2.1f, 0.1f, 43f - 0.3f * i);
                barrier.Transform.SetRotation(0f, 9f, 0f);
                barrier.Transform.SetScale(1f);
                barrier.AddComponent<BoxCollider>();
                MeshRenderer barrierRenderer = barrier.AddComponent<MeshRenderer>();
                barrierRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.Barrier));
                barrierRenderer.SetShaderProgram(simpleShader);
                if (i % 2 == 1) barrierRenderer.SetShaderProgram(orangeShader);
                barrierRenderer.SetTexture(plasticTexture);
                objects.Add(barrier);
            }

            for (int i = 0; i < 5; i++)
            {
                RennObject delinator = new RennObject();
                delinator.Transform.SetPosition(-101f + i * 2, 0.1f, 21f + i * 1);
                delinator.Transform.SetRotation(0f, 0f, 0f);
                delinator.Transform.SetScale(0.4f);
                delinator.AddComponent<BoxCollider>().IsTrigger = true;
                delinator.AddComponent<FallingPillar>();
                MeshRenderer delinatorRenderer = delinator.AddComponent<MeshRenderer>();
                delinatorRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.Delineator));
                delinatorRenderer.SetShaderProgram(simpleShader);
                delinatorRenderer.SetShaderAttribute(SimpleShader.BaseColorUniform, ShaderAttribute.Value(Color.LightGray));
                delinatorRenderer.SetTexture(plasticTexture);
                objects.Add(delinator);
            }

            for (int i = 0; i < 7; i++)
            {
                RennObject delinator = new RennObject();
                delinator.Transform.SetPosition(-107f + i * 0.2f, 0.1f, 27f + i * 2);
                delinator.Transform.SetRotation(0f, 0f, 0f);
                delinator.Transform.SetScale(0.4f);
                delinator.AddComponent<BoxCollider>().IsTrigger = true;
                delinator.AddComponent<FallingPillar>();
                MeshRenderer delinatorRenderer = delinator.AddComponent<MeshRenderer>();
                delinatorRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.Delineator));
                delinatorRenderer.SetShaderProgram(simpleShader);
                delinatorRenderer.SetShaderAttribute(SimpleShader.BaseColorUniform, ShaderAttribute.Value(Color.LightGray));
                delinatorRenderer.SetTexture(plasticTexture);
                objects.Add(delinator);
            }

            objects.Add(SimpleObject(new Vector3(-123.2f, 0f, 30.23f), Vector3.Zero, Vector3.One, 
                orangeShader, plasticTexture, SecurityStructuresMeshFactory.GetMesh(StructureType.Buffer)));
            objects.Add(SimpleObject(new Vector3(-120.7f, 0f, 32.24f), Vector3.Zero, Vector3.One,
                orangeShader, plasticTexture, SecurityStructuresMeshFactory.GetMesh(StructureType.Buffer)));
            objects.Add(SimpleObject(new Vector3(-119.9f, 0f, 34.88f), Vector3.Zero, Vector3.One,
                orangeShader, plasticTexture, SecurityStructuresMeshFactory.GetMesh(StructureType.Buffer)));
            objects.Add(SimpleObject(new Vector3(44.37f, 0f, -7.65f), Vector3.Zero, Vector3.One,
                orangeShader, plasticTexture, SecurityStructuresMeshFactory.GetMesh(StructureType.Buffer)));
            objects.Add(SimpleObject(new Vector3(13.99f, 0f, 5.08f), Vector3.Zero, Vector3.One,
                orangeShader, plasticTexture, SecurityStructuresMeshFactory.GetMesh(StructureType.Buffer)));
            objects.Add(SimpleObject(new Vector3(-107.83f, 0f, 23.81f), Vector3.Zero, Vector3.One,
                orangeShader, plasticTexture, SecurityStructuresMeshFactory.GetMesh(StructureType.Buffer)));
            objects.Add(SimpleObject(new Vector3(-103.35f, 0f, 20.86f), Vector3.Zero, Vector3.One,
                orangeShader, plasticTexture, SecurityStructuresMeshFactory.GetMesh(StructureType.Buffer)));
            objects.Add(SimpleObject(new Vector3(-87.3f, 0f, 7.5f), Vector3.Zero, Vector3.One,
                orangeShader, plasticTexture, SecurityStructuresMeshFactory.GetMesh(StructureType.Buffer)));

            objects.Add(SimpleObject(new Vector3(-90.5f, 0f, 9.39f), Vector3.UnitY * 28, Vector3.One,
                simpleShader, plasticTexture, SecurityStructuresMeshFactory.GetMesh(StructureType.Barrier)));
            objects.Add(SimpleObject(new Vector3(-93.1f, 0f, 10.22f), Vector3.Zero, Vector3.One,
                orangeShader, plasticTexture, SecurityStructuresMeshFactory.GetMesh(StructureType.Barrier)));
        }

        private void AddGround(HashSet<RennObject> objects)
        {
            RennObject plane = new RennObject();
            plane.Transform.SetPosition(-50, 0f, 20);
            plane.Transform.SetScale(380f);
            MeshRenderer planeRenderer = plane.AddComponent<MeshRenderer>();
            planeRenderer.SetTriangleMesh(PrimitiveMeshFactory.CreatePlane());
            planeRenderer.SetShaderProgram(groundShader);
            planeRenderer.SetTexture(groundTexture);
            objects.Add(plane);
        }

        private void AddForest(HashSet<RennObject> objects)
        {
            RennObject forest = new RennObject();
            forest.Transform.SetPosition(-240f, 0f, -170f);
            forest.Transform.SetRotation(0, 0, 0);
            forest.Transform.SetScale(9f);
            InstanceRenderer forestInstanceRenderer = forest.AddComponent<InstanceRenderer>();
            List<Transform> instanceTransforms = new List<Transform>();

            Random random = new Random();
            Mesh pineMesh = EnvironmentMeshFactory.GetMesh(EnvironmentType.Spruce);
            for (int i = 0; i < forestMap.GetLength(0); i++)
            {
                for (int j = 0; j < forestMap.GetLength(1); j++)
                {
                    float value = forestMap[i, j];
                    if (value < 0.04f)
                    {
                        RennObject tree = new RennObject();
                        tree.SetParent(forest);
                        tree.Transform.SetPosition(i + (float)random.NextDouble(), 0f, j + (float)random.NextDouble());
                        tree.Transform.SetRotation(0f, random.Next(180), 0f);
                        float scale = (0.08f + (0.04f - value) * 0.2f) * (1 + random.Next(60) * 0.01f - 0.3f);
                        tree.Transform.SetScale(scale);
                        instanceTransforms.Add(tree.Transform);
                    }
                }
            }

            forestInstanceRenderer.InstanceTransforms = instanceTransforms;
            forestInstanceRenderer.Mesh = pineMesh;
            forestInstanceRenderer.Shader = instanceShader;
            forestInstanceRenderer.Texture = treeTexture;

            objects.Add(forest);
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
            road.Transform.SetPosition(-180f, 0.1f, 85f);
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

            List<Material> materials = new List<Material>
            {
                new Material
                {
                    Name = "Асфальт",
                    Texture = roadTexture
                },
                new Material
                {
                    Name = "Гравий",
                    Texture = gravelTexture
                },
                new Material
                {
                    Name = "Песок",
                    Texture = sandTexture
                }
            };

            roadPartsObjects[3].GetComponent<MeshRenderer>().SetTexture(gravelTexture);
            roadPartsObjects[3].AddComponent<BoxCollider>().IsTrigger = true;
            Selectable roadPartSelectable3 = roadPartsObjects[3].AddComponent<Selectable>();
            roadPartSelectable3.Name = "Участок дороги №3";
            roadPartSelectable3.CanChangeTransform = false;
            roadPartSelectable3.Materials = materials;
            roadPartsObjects[6].GetComponent<MeshRenderer>().SetTexture(gravelTexture);
            roadPartsObjects[6].AddComponent<BoxCollider>().IsTrigger = true;
            Selectable roadPartSelectable6 = roadPartsObjects[6].AddComponent<Selectable>();
            roadPartSelectable6.Name = "Участок дороги №6";
            roadPartSelectable6.CanChangeTransform = false;
            roadPartSelectable6.Materials = materials;

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
            techRoad1.Transform.SetPosition(0, -0.01f, 0f);
            techRoad1.SetParent(road);
            MeshRenderer techRoadRenderer = techRoad1.AddComponent<MeshRenderer>();
            techRoadRenderer.SetShaderProgram(roadShader);
            techRoadRenderer.SetShaderAttribute(SimpleShader.TilingUniform, ShaderAttribute.Value(1, 2));
            techRoadRenderer.SetTriangleMesh(techRoadPart1.Road);
            techRoadRenderer.SetTexture(gravelTexture);

            Path techPath2 = new Path();
            techPath2.AddManualSegment(Vector3.Zero, new Vector3(31f, 0f, -12.5f), new Vector3(27f, 0f, -12.5f));
            techPath2.AddManualSegment(new Vector3(29.5f, 0f, -8.5f), new Vector3(30.5f, 0f, -7f), new Vector3(31.5f, 0f, -5.5f));
            RoadPart techRoadPart2 = RoadCreator.CreateMesh(techPath2.CalculateEvenlySpacedPoints(0.5f), techRoadCreatorSettings);
            RennObject techRoad2 = new RennObject();
            techRoad2.Transform.SetPosition(0, -0.01f, 0);
            techRoad2.SetParent(road);
            MeshRenderer techRoadRenderer2 = techRoad2.AddComponent<MeshRenderer>();
            techRoadRenderer2.SetShaderProgram(roadShader);
            techRoadRenderer2.SetShaderAttribute(SimpleShader.TilingUniform, ShaderAttribute.Value(1, 2));
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
            car.AddComponent<BoxCollider>();
            car.AddComponent<Rigidbody>().IsEnabled = false;
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

        private void AddMovableCar(HashSet<RennObject> objects)
        {
            RennObject car = new RennObject();
            car.Transform.SetPosition(0, -0.1f, 0);
            car.Transform.SetRotation(0, -90, 0);
            car.AddComponent<Rigidbody>();
            BoxCollider carCollider = car.AddComponent<BoxCollider>();
            carCollider.IsDynamic = true;
            carCollider.MaxBound = new Vector3(0.9f, 3, 4.5f);
            carCollider.MinBound = new Vector3(-0.9f, 0, -0.8f);
            CarController carController = car.AddComponent<CarController>();
            CarControllers[0] = carController;

            carController.IsInputReg = false;

            // cockpit
            RennObject cockpitBase= new RennObject();
            cockpitBase.Transform.SetPosition(0, 0, 2);
            cockpitBase.SetParent(car);
            carController.CockpitRotationCenter = cockpitBase;

            RennObject cockpit = new RennObject();
            cockpit.SetParent(cockpitBase);
            carController.Cockpit = cockpit;

            RennObject cockpitShell = new RennObject();
            cockpitShell.SetParent(cockpit);
            cockpitShell.Transform.SetRotation(0, -90, 0);
            cockpitShell.Transform.SetScale(0.4f);
            MeshRenderer cockpitRenderer = cockpitShell.AddComponent<MeshRenderer>();
            cockpitRenderer.SetShaderProgram(simpleShader);
            cockpitRenderer.SetTriangleMesh(CarsMeshFactory.CreateCar(CarType.SportCar));
            cockpitRenderer.SetTexture(carTexture);
            
            // wheels
            RennObject wheelsContainer = new RennObject();
            wheelsContainer.Transform.SetPosition(0, 0, 0);
            wheelsContainer.Transform.SetRotation(0, -90, 0);
            wheelsContainer.Transform.SetScale(0.4f);
            wheelsContainer.SetParent(cockpitBase);
            List<RennObject> wheels = new List<RennObject>();
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
                RennObject wheel = new RennObject();
                wheel.SetParent(wheelsContainer);
                wheel.Transform.SetPosition(transform.position.X, transform.position.Y, transform.position.Z);

                RennObject wheelRotate = new RennObject();
                wheelRotate.SetParent(wheel);

                RennObject wheelModel = new RennObject();
                wheelModel.SetParent(wheelRotate);
                wheelModel.Transform.SetRotation(0, transform.rotation.Y, 0);
                MeshRenderer wheelRenderer = wheelModel.AddComponent<MeshRenderer>();
                wheelRenderer.SetShaderProgram(simpleShader);
                wheelRenderer.SetTriangleMesh(CarsMeshFactory.CreateCar(CarType.Wheel));
                wheelRenderer.SetTexture(wheelTexture);
                wheels.Add(wheel);
            }
            carController.ForwardWheels = new[] { wheels[0], wheels[1] };
            carController.BackwardWheels = new[] { wheels[2], wheels[3] };

            // utils
            RennObject cameraTarget = new RennObject();
            cameraTarget.SetParent(cockpit);
            cameraTarget.Transform.SetPosition(0, 2f, 0);
            CarCameraTargetTransforms[0] = cameraTarget.Transform;

            RennObject rearLights = new RennObject();
            rearLights.SetParent(cockpit);
            rearLights.Transform.SetPosition(0, 0.5f, -3);
            PointLight carRearPointLight = rearLights.AddComponent<PointLight>();
            carRearPointLight.Color = Color.Red;
            carController.RearPointLight = carRearPointLight;

            RennObject carSmoke = new RennObject();
            carSmoke.Transform.SetPosition(0, 0, -2f);
            carSmoke.SetParent(cockpitBase);

            ParticleEmitter emitter = carSmoke.AddComponent<ParticleEmitter>();
            emitter.ShaderProgram = particleShader;
            emitter.Texture = smokeTexture;
            carController.SmokeParticle = emitter;

            emitter.MaxParticles = 100;
            emitter.EmitRate = 30f;
            emitter.EmitRange = Vector3.UnitX * 1f + Vector3.UnitZ * 1f;
            emitter.StartSize = new Vector2(3.0f, 4.5f);
            emitter.SizeVelocity = new Vector2(3f, 4f);
            emitter.StartRotation = new Vector2(-180, 180);
            emitter.RotationVelocity = new Vector2(-30, 30);
            emitter.Velocity = Vector3.UnitY * 0.5f;
            emitter.VelocitySpread = new Vector3(0, 0.3f, 0);
            emitter.ColorVelocity = new Vector4(0, 0, 0, -150f);
            emitter.LifeTime = new Vector2(2f, 2f);

            objects.Add(car);
        }

        private void AddMovableCar2(HashSet<RennObject> objects)
        {
            RennObject car = new RennObject();
            car.Transform.SetPosition(-97, -0.1f, 30);
            car.Transform.SetRotation(0, 140, 0);
            car.AddComponent<Rigidbody>();
            BoxCollider carCollider = car.AddComponent<BoxCollider>();
            carCollider.IsDynamic = true;
            carCollider.MaxBound = new Vector3(0.9f, 3, 4.5f);
            carCollider.MinBound = new Vector3(-0.9f, 0, -0.8f);
            CarController carController = car.AddComponent<CarController>();
            carController.IsInputReg = false;
            carController.ForwardSpeed = 7;
            carController.ReverseSpeed = 4;
            carController.BreakStrength = 10;
            carController.TurnSpeed = 1.5f;
            CarControllers[1] = carController;

            // cockpit
            RennObject cockpitBase = new RennObject();
            cockpitBase.Transform.SetPosition(0, 0, 2);
            cockpitBase.SetParent(car);
            carController.CockpitRotationCenter = cockpitBase;

            RennObject cockpit = new RennObject();
            cockpit.SetParent(cockpitBase);
            carController.Cockpit = cockpit;

            RennObject cockpitShell = new RennObject();
            cockpitShell.SetParent(cockpit);
            cockpitShell.Transform.SetRotation(0, 0, 0);
            cockpitShell.Transform.SetScale(1.8f);
            MeshRenderer cockpitRenderer = cockpitShell.AddComponent<MeshRenderer>();
            cockpitRenderer.SetShaderProgram(simpleShader);
            cockpitRenderer.SetTriangleMesh(CarsMeshFactory.CreateCar(CarType.Bronirovany));
            cockpitRenderer.SetTexture(gelikTexture);

            // wheels
            RennObject wheelsContainer = new RennObject();
            wheelsContainer.Transform.SetPosition(0, 0, 0);
            wheelsContainer.Transform.SetRotation(0, -90, 0);
            wheelsContainer.Transform.SetScale(0.4f);
            wheelsContainer.SetParent(cockpitBase);
            List<RennObject> wheels = new List<RennObject>();
            List<Transform> wheelTransforms = new List<Transform>();

            Transform frontRight = new Transform();
            frontRight.SetPosition(4.2f, 1.4f, 2.0f);
            frontRight.SetRotation(0, 180, 0);
            wheelTransforms.Add(frontRight);

            Transform frontLeft = new Transform();
            frontLeft.SetPosition(4.2f, 1.4f, -2.0f);
            frontLeft.SetRotation(0, 0, 0);
            wheelTransforms.Add(frontLeft);

            Transform backRight = new Transform();
            backRight.SetPosition(-4.0f, 1.4f, 2.0f);
            backRight.SetRotation(0, 180, 0);
            wheelTransforms.Add(backRight);

            Transform backLeft = new Transform();
            backLeft.SetPosition(-4.0f, 1.4f, -2.0f);
            backLeft.SetRotation(0, 0, 0);
            wheelTransforms.Add(backLeft);

            foreach (var transform in wheelTransforms)
            {
                RennObject wheel = new RennObject();
                wheel.SetParent(wheelsContainer);
                wheel.Transform.SetPosition(transform.position.X, transform.position.Y, transform.position.Z);

                RennObject wheelRotate = new RennObject();
                wheelRotate.SetParent(wheel);

                RennObject wheelModel = new RennObject();
                wheelModel.SetParent(wheelRotate);
                wheelModel.Transform.SetScale(1.1f);
                wheelModel.Transform.SetRotation(0, transform.rotation.Y, 0);
                MeshRenderer wheelRenderer = wheelModel.AddComponent<MeshRenderer>();
                wheelRenderer.SetShaderProgram(simpleShader);
                wheelRenderer.SetTriangleMesh(CarsMeshFactory.CreateCar(CarType.Wheel));
                wheelRenderer.SetTexture(wheelTexture);
                wheels.Add(wheel);
            }
            carController.ForwardWheels = new[] { wheels[0], wheels[1] };
            carController.BackwardWheels = new[] { wheels[2], wheels[3] };

            // utils
            RennObject cameraTarget = new RennObject();
            cameraTarget.SetParent(cockpit);
            cameraTarget.Transform.SetPosition(0, 2f, 0);
            CarCameraTargetTransforms[1] = cameraTarget.Transform;

            RennObject rearLights = new RennObject();
            rearLights.SetParent(cockpit);
            rearLights.Transform.SetPosition(0, 0.5f, -3);
            PointLight carRearPointLight = rearLights.AddComponent<PointLight>();
            carRearPointLight.Color = Color.Red;
            carController.RearPointLight = carRearPointLight;

            RennObject carSmoke = new RennObject();
            carSmoke.Transform.SetPosition(0, 0, -2f);
            carSmoke.SetParent(cockpitBase);

            ParticleEmitter emitter = carSmoke.AddComponent<ParticleEmitter>();
            emitter.ShaderProgram = particleShader;
            emitter.Texture = smokeTexture;
            carController.SmokeParticle = emitter;

            emitter.MaxParticles = 100;
            emitter.EmitRate = 30f;
            emitter.EmitRange = Vector3.UnitX * 1f + Vector3.UnitZ * 1f;
            emitter.StartSize = new Vector2(3.0f, 4.5f);
            emitter.SizeVelocity = new Vector2(3f, 4f);
            emitter.StartRotation = new Vector2(-180, 180);
            emitter.RotationVelocity = new Vector2(-30, 30);
            emitter.Velocity = Vector3.UnitY * 0.5f;
            emitter.VelocitySpread = new Vector3(0, 0.3f, 0);
            emitter.ColorVelocity = new Vector4(0, 0, 0, -150f);
            emitter.LifeTime = new Vector2(2f, 2f);

            objects.Add(car);
        }
    }
}