using System;
using System.Collections.Generic;
using System.Drawing;
using TwoBRenn.Engine;
using TwoBRenn.Engine.Components.Common;
using TwoBRenn.Engine.Components.Physic;
using TwoBRenn.Engine.Components.Render;
using TwoBRenn.Engine.Render.ShaderPrograms;
using TwoBRenn.Engine.Render.Textures;
using TwoBRenn.ObjectsSetups.MeshFactories;

namespace TwoBRenn.ObjectsSetups
{
    class SecurityStructPlacerSetup
    {
        private static readonly SimpleShader SimpleShader = new SimpleShader();
        private static readonly SimpleShader OrangePlasticShader = new SimpleShader();
        private static readonly SimpleShader BlackShader = new SimpleShader();
        private static readonly Texture PlasticTexture = new Texture(@"Assets\Textures\plastic.jpg");
        private static readonly Texture ConcreteTexture = new Texture(@"Assets\Textures\concrete.jpg");
        private static readonly Texture CurbTexture = new Texture(@"Assets\Textures\curb.png");

        public SecurityStructPlacerSetup()
        {
            OrangePlasticShader.SetDefaultShaderAttribute(SimpleShader.BaseColorUniform, ShaderAttribute.Value(Color.Coral));
            BlackShader.SetDefaultShaderAttribute(SimpleShader.BaseColorUniform, ShaderAttribute.Value(Color.Black));
        }

        public List<Func<RennObject>> GetObjectCreators()
        {
            List<Func<RennObject>> objectCreators = new List<Func<RennObject>>
            {
                BarrierPlacer,
                BufferPlacer,
                ConicalBufferPlacer,
                FencePlacer,
                CylinderPlacer
            };
            return objectCreators;
        }

        static RennObject BarrierPlacer()
        {
            List<Material> materials = new List<Material>()
            {
                new Material
                {
                    Name = "Белый пластик",
                    Color = Color.White,
                    Texture = PlasticTexture
                },
                new Material
                {
                    Name = "Красный пластик",
                    Color = Color.IndianRed,
                    Texture = PlasticTexture
                },
                new Material
                {
                    Name = "Оранжевый пластик",
                    Color = Color.Coral,
                    Texture = PlasticTexture
                },
                new Material
                {
                    Name = "Синий пластик",
                    Color = Color.DodgerBlue,
                    Texture = PlasticTexture
                },
                new Material
                {
                    Name = "Бетон",
                    Color = Color.White,
                    Texture = ConcreteTexture
                }
            };

            RennObject barrier = new RennObject();
            barrier.Transform.SetRotation(0f, 0f, 0f);
            barrier.Transform.SetScale(1f);
            Selectable selectable = barrier.AddComponent<Selectable>();
            selectable.Name = "Барьер";
            selectable.Materials = materials;
            barrier.AddComponent<BoxCollider>();
            MeshRenderer barrierRenderer = barrier.AddComponent<MeshRenderer>();
            barrierRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.Barrier));
            barrierRenderer.SetShaderProgram(SimpleShader);
            barrierRenderer.SetTexture(PlasticTexture);
            return barrier;
        }

        static RennObject BufferPlacer()
        {
            List<Material> materials = new List<Material>()
            {
                new Material
                {
                    Name = "Белый пластик",
                    Color = Color.White,
                    Texture = PlasticTexture
                },
                new Material
                {
                    Name = "Оранжевый пластик",
                    Color = Color.Coral,
                    Texture = PlasticTexture
                }
            };

            RennObject buffer = new RennObject();
            buffer.Transform.SetRotation(0f, 0f, 0f);
            buffer.Transform.SetScale(1f);
            Selectable selectable = buffer.AddComponent<Selectable>();
            selectable.Name = "Буфер";
            selectable.Materials = materials;
            buffer.AddComponent<BoxCollider>();
            MeshRenderer bufferRenderer = buffer.AddComponent<MeshRenderer>();
            bufferRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.Buffer));
            bufferRenderer.SetShaderProgram(OrangePlasticShader);
            bufferRenderer.SetTexture(PlasticTexture);
            return buffer;
        }

        static RennObject ConicalBufferPlacer()
        {
            List<Material> materials = new List<Material>()
            {
                new Material
                {
                    Name = "Белый пластик",
                    Color = Color.White,
                    Texture = PlasticTexture
                },
                new Material
                {
                    Name = "Оранжевый пластик",
                    Color = Color.Coral,
                    Texture = PlasticTexture
                },
                new Material
                {
                    Name = "Полосатый пластик",
                    Texture = CurbTexture
                }
            };

            RennObject buffer = new RennObject();
            buffer.Transform.SetRotation(0f, 0f, 0f);
            buffer.Transform.SetScale(0.5f);
            Selectable selectable = buffer.AddComponent<Selectable>();
            selectable.Name = "Конус";
            selectable.Materials = materials;
            buffer.AddComponent<BoxCollider>();
            MeshRenderer bufferRenderer = buffer.AddComponent<MeshRenderer>();
            bufferRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.ConicalBuffer));
            bufferRenderer.SetShaderProgram(OrangePlasticShader);
            bufferRenderer.SetTexture(PlasticTexture);
            return buffer;
        }

        static RennObject FencePlacer()
        {
            List<Material> materials = new List<Material>()
            {
                new Material
                {
                    Name = "Серый металл",
                    Color = Color.White,
                    Texture = PlasticTexture
                },
                new Material
                {
                    Name = "Красный металл",
                    Color = Color.IndianRed,
                    Texture = PlasticTexture
                }
            };

            RennObject fence = new RennObject();
            fence.Transform.SetRotation(0f, 0f, 0f);
            fence.Transform.SetScale(1.5f);
            Selectable selectable = fence.AddComponent<Selectable>();
            selectable.Name = "Ограждение";
            selectable.Materials = materials;
            fence.AddComponent<BoxCollider>();
            MeshRenderer fenceRenderer = fence.AddComponent<MeshRenderer>();
            fenceRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.Fencing));
            fenceRenderer.SetShaderProgram(SimpleShader);
            fenceRenderer.SetTexture(PlasticTexture);
            return fence;
        }

        static RennObject CylinderPlacer()
        {
            List<Material> materials = new List<Material>()
            {
                new Material
                {
                    Name = "Черные покрышки",
                    Color = Color.Black,
                    Texture = PlasticTexture
                },
                new Material
                {
                    Name = "Бетон",
                    Texture = ConcreteTexture
                }
            };

            RennObject cylinder = new RennObject();
            cylinder.Transform.SetRotation(0f, 0f, 0f);
            cylinder.Transform.SetScale(0.5f);
            Selectable selectable = cylinder.AddComponent<Selectable>();
            selectable.Name = "Стопка шин";
            selectable.Materials = materials;
            cylinder.AddComponent<BoxCollider>();
            MeshRenderer cylinderRenderer = cylinder.AddComponent<MeshRenderer>();
            cylinderRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.Cylinder));
            cylinderRenderer.SetShaderProgram(BlackShader);
            cylinderRenderer.SetTexture(PlasticTexture);
            return cylinder;
        }
    }
}
