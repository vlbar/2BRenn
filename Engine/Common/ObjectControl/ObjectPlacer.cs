using System;
using System.Collections.Generic;
using OpenTK;
using TwoBRenn.Engine.Common.Managers;
using TwoBRenn.Engine.Common.RayCasting;
using TwoBRenn.Engine.Components;
using TwoBRenn.Engine.Render.Camera;
using TwoBRenn.Engine.Render.ShaderPrograms;
using TwoBRenn.Engine.Scene;

namespace TwoBRenn.Engine.Common.ObjectControl
{
    class ObjectPlacer
    {
        public List<Func<RennObject>> ObjectsCreators = new List<Func<RennObject>>();
        public SceneManager SceneManager;
        public RennObject ObjectToPlace;

        public void SelectObject(int index)
        {
            if (ObjectToPlace == null)
            {
                ObjectToPlace = ObjectsCreators[index]();
                ShaderAttribute_Vector4 baseColorAttribute =
                    (ShaderAttribute_Vector4)ObjectToPlace.GetComponent<MeshRenderer>().GetShaderAttributes()[
                        SimpleShader.BASE_COLOR];
                ObjectToPlace.GetComponent<MeshRenderer>().SetShaderAttribute(SimpleShader.BASE_COLOR,
                    ShaderAttribute.Value(baseColorAttribute.Vector.X, baseColorAttribute.Vector.Y,
                        baseColorAttribute.Vector.Z, 0.6f));
                
                SceneManager.AddObjectToScene(ObjectToPlace);
            }
        }

        public void OnUpdate()
        {
            if (ObjectToPlace != null)
            {
                if (InputManager.IsMouseButtonDown(MouseButton.Left))
                {
                    ShaderAttribute_Vector4 baseColorAttribute =
                        (ShaderAttribute_Vector4)ObjectToPlace.GetComponent<MeshRenderer>().GetShaderAttributes()[
                            SimpleShader.BASE_COLOR];
                    baseColorAttribute.Vector.W = 1f;

                    ObjectToPlace = null;
                    return;
                }

                Vector3 camPos = Camera.GetViewMatrix().Inverted().ExtractTranslation();
                Vector3 pos = InputManager.MousePositionToWorldDirection();
                Vector3? inter = Raycast.IntersectionWithPlane(camPos, pos, Vector4.UnitY);

                ObjectToPlace.Transform.SetPosition(inter ?? Vector3.Zero);
            }
        }
    }
}
