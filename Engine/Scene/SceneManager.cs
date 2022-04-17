using System.Collections.Generic;
using TwoBRenn.Engine.Interfaces;
using TwoBRenn.ObjectsSetups;

namespace TwoBRenn.Engine.Scene
{
    class SceneManager
    {
        private readonly HashSet<RennObject> sceneGraph = new HashSet<RennObject>();
        private readonly HashSet<IObjectsSetup> objectsSetups = new HashSet<IObjectsSetup>();

        public SceneManager()
        {
            objectsSetups.Add(new AutodromeObjectsSetup());

            foreach (IObjectsSetup objectsSetup in objectsSetups)
            {
                foreach (RennObject rennObject in objectsSetup.GetObjects())
                {
                    sceneGraph.Add(rennObject);
                }
            }
        }

        public void OnStart()
        {
            foreach (RennObject rennObject in sceneGraph)
            {
                rennObject.OnStart();
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
