using System.Collections.Generic;
using TwoBRenn.Engine.Interfaces;

namespace TwoBRenn.Engine.Scene
{
    class SceneManager : IUpdatableEnginePart
    {
        private readonly HashSet<RennObject> sceneGraph = new HashSet<RennObject>();

        public SceneManager(HashSet<IObjectsSetup> objectsSetups)
        {
            foreach (IObjectsSetup objectsSetup in objectsSetups)
            {
                foreach (RennObject rennObject in objectsSetup.GetObjects())
                {
                    sceneGraph.Add(rennObject);
                }
            }
        }

        public void OnUpdate()
        {
            foreach (RennObject rennObject in sceneGraph)
            {
                rennObject.OnUpdate();
            }
        }

        public void OnLateUpdate()
        {
            foreach (RennObject rennObject in sceneGraph)
            {
                rennObject.OnLateUpdate();
            }
        }

        public void AddObjectToScene(RennObject rennObject)
        {
            sceneGraph.Add(rennObject);
        }
    }
}
