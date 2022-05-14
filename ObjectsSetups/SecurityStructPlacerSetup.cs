using System;
using System.Collections.Generic;
using System.Drawing;
using TwoBRenn.Engine;
using TwoBRenn.Engine.Components;
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
        private static readonly Texture PlasticTexture = new Texture(@"Assets\Textures\plastic.jpg");

        public SecurityStructPlacerSetup()
        {
            OrangePlasticShader.SetDefaultShaderAttribute(SimpleShader.BaseColorUniform, ShaderAttribute.Value(Color.Coral));
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
            RennObject barrier = new RennObject();
            barrier.Transform.SetRotation(0f, 0f, 0f);
            barrier.Transform.SetScale(1f);
            barrier.AddComponent<Selectable>();
            barrier.AddComponent<BoxCollider>();
            MeshRenderer barrierRenderer = barrier.AddComponent<MeshRenderer>();
            barrierRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.Barrier));
            barrierRenderer.SetShaderProgram(SimpleShader);
            barrierRenderer.SetTexture(PlasticTexture);
            return barrier;
        }

        static RennObject BufferPlacer()
        {
            RennObject buffer = new RennObject();
            buffer.Transform.SetRotation(0f, 0f, 0f);
            buffer.Transform.SetScale(1f);
            buffer.AddComponent<Selectable>();
            buffer.AddComponent<BoxCollider>();
            buffer.AddComponent<Rigidbody>();
            MeshRenderer bufferRenderer = buffer.AddComponent<MeshRenderer>();
            bufferRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.Buffer));
            bufferRenderer.SetShaderProgram(OrangePlasticShader);
            bufferRenderer.SetTexture(PlasticTexture);
            return buffer;
        }

        static RennObject ConicalBufferPlacer()
        {
            RennObject buffer = new RennObject();
            buffer.Transform.SetRotation(0f, 0f, 0f);
            buffer.Transform.SetScale(0.4f);
            buffer.AddComponent<Selectable>();
            buffer.AddComponent<BoxCollider>();
            buffer.AddComponent<Rigidbody>();
            MeshRenderer bufferRenderer = buffer.AddComponent<MeshRenderer>();
            bufferRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.ConicalBuffer));
            bufferRenderer.SetShaderProgram(OrangePlasticShader);
            bufferRenderer.SetTexture(PlasticTexture);
            return buffer;
        }

        static RennObject FencePlacer()
        {
            RennObject fence = new RennObject();
            fence.Transform.SetRotation(0f, 0f, 0f);
            fence.Transform.SetScale(1.5f);
            fence.AddComponent<Selectable>();
            fence.AddComponent<BoxCollider>();
            MeshRenderer fenceRenderer = fence.AddComponent<MeshRenderer>();
            fenceRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.Fencing));
            fenceRenderer.SetShaderProgram(SimpleShader);
            fenceRenderer.SetTexture(PlasticTexture);
            return fence;
        }

        static RennObject CylinderPlacer()
        {
            RennObject cylinder = new RennObject();
            cylinder.Transform.SetRotation(0f, 0f, 0f);
            cylinder.Transform.SetScale(0.5f);
            cylinder.AddComponent<Selectable>();
            cylinder.AddComponent<BoxCollider>();
            MeshRenderer cylinderRenderer = cylinder.AddComponent<MeshRenderer>();
            cylinderRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.GetMesh(StructureType.Cylinder));
            cylinderRenderer.SetShaderProgram(SimpleShader);
            cylinderRenderer.SetTexture(PlasticTexture);
            return cylinder;
        }
    }
}
