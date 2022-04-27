using System;
using System.Collections.Generic;
using System.Drawing;
using TwoBRenn.Engine;
using TwoBRenn.Engine.Components;
using TwoBRenn.Engine.Components.Common;
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
            OrangePlasticShader.SetDefaultShaderAttribute(SimpleShader.BASE_COLOR, ShaderAttribute.Value(Color.Coral));
        }

        public List<Func<RennObject>> GetObjectCreators()
        {
            List<Func<RennObject>> objectCreators = new List<Func<RennObject>>
            {
                BarrierPlacer,
                BufferPlacer
            };
            return objectCreators;
        }

        static RennObject BarrierPlacer()
        {
            RennObject barrier = new RennObject();
            barrier.Transform.SetRotation(0f, 0f, 0f);
            barrier.Transform.SetScale(1f);
            barrier.AddComponent<Selectable>();
            MeshRenderer barrierRenderer = barrier.AddComponent<MeshRenderer>();
            barrierRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.CreateStructure(StructureType.Barrier));
            barrierRenderer.SetShaderProgram(SimpleShader);
            barrierRenderer.SetTexture(PlasticTexture);
            return barrier;
        }

        static RennObject BufferPlacer()
        {
            RennObject barrier = new RennObject();
            barrier.Transform.SetRotation(0f, 0f, 0f);
            barrier.Transform.SetScale(1f);
            MeshRenderer barrierRenderer = barrier.AddComponent<MeshRenderer>();
            barrierRenderer.SetTriangleMesh(SecurityStructuresMeshFactory.CreateStructure(StructureType.Buffer));
            barrierRenderer.SetShaderProgram(OrangePlasticShader);
            barrierRenderer.SetTexture(PlasticTexture);
            return barrier;
        }
    }
}
