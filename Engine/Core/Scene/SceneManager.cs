using System.Collections.Generic;
using TwoBRenn.Engine.Core.Scene.Setups;
using TwoBRenn.Engine.Interfaces;

namespace TwoBRenn.Engine.Core
{
    class SceneManager
    {
        private HashSet<RennObject> sceneGraph = new HashSet<RennObject>();
        private HashSet<IObjectsSetup> objectsSetups = new HashSet<IObjectsSetup>();

        public SceneManager()
        {
            objectsSetups.Add(new TestObjectsSetup());

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
    }
}
