using System;
using System.Collections.Generic;
using System.Linq;
using TwoBRenn.Engine.Components;
using TwoBRenn.Engine.Utils;

namespace TwoBRenn.Engine
{
    class RennObject
    {
        public Transform Transform { get; private set; } = new Transform();

        public HashSet<Component> Components { get; private set; } = new HashSet<Component>();

        public HashSet<RennObject> ChildObjects { get; private set; } = new HashSet<RennObject>();
        private RennObject parent;

        public RennObject()
        {

        }

        // COMPONENTS
        public T GetComponent<T>() where T : Component
        {
            return Components.OfType<T>().FirstOrDefault();
        }

        public T AddComponent<T>() where T : Component
        {
            T component = (T)Activator.CreateInstance(typeof(T));
            AddComponent(component);
            return component;
        }

        public Component AddComponent(Component component)
        {
            component.rennObject = this;
            Components.Add(component);
            return component;
        }

        public void RemoveComponent(Component component)
        {
            component.rennObject = null;
            Components.Remove(component);
        }

        public void RemoveComponent<T>() where T : Component
        {
            Components.Remove(GetComponent<T>());
        }

        // RELATION
        public RennObject GetParent()
        {
            return parent;
        }

        public void SetParent(RennObject newParent)
        {
            if (newParent != null)
            {
                if (parent != null)
                {
                    parent.RemoveChild(this);
                }

                parent = newParent;
                newParent.AddChild(this);

                Transform.SetParentTransform(newParent.Transform);
                foreach (var child in ChildObjects)
                {
                    child.Transform.UpdateGlobalModel();
                }
            }
        }

        public void AddChild(RennObject child)
        {
            ChildObjects.Add(child);
            if (child.parent != this) child.SetParent(this);
        }

        public void RemoveChild(RennObject child)
        {
            child.SetParent(null);
            ChildObjects.Remove(child);
        }

        // OTHER
        public void OnUpdate()
        {
            foreach (RennObject childObject in ChildObjects)
            {
                childObject.OnUpdate();
            }

            foreach (Component component in Components)
            {
                component.OnUpdate();
            }
        }
    }
}
