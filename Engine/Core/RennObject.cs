using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using TwoBRenn.Engine.Components;
using TwoBRenn.Engine.Interfaces;

namespace TwoBRenn.Engine.Core
{
    class RennObject
    {
        public Vector3 localPosition { get; private set; } = Vector3.Zero;
        public Vector3 localRotation { get; private set; } = Vector3.Zero;
        public Vector3 localScale { get; private set; } = new Vector3(1, 1, 1);

        public Vector3 globalPosition { get; private set; } = Vector3.Zero;
        public Vector3 globalRotation { get; private set; } = Vector3.Zero;
        public Vector3 globalScale { get; private set; } = new Vector3(1, 1, 1);

        public Matrix4 modelMatrix { get; private set; }

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
            }
            UpdateGlobalModel();
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

        // TRANSFORM

        private void UpdateGlobalModel()
        {
            if (parent != null)
            {
                float x = parent.globalPosition.X + localPosition.X;
                float y = parent.globalPosition.Y + localPosition.Y;
                float z = parent.globalPosition.Z + localPosition.Z;
                globalPosition = new Vector3(x, y, z);

                float xRot = parent.globalRotation.X + localRotation.X;
                float yRot = parent.globalRotation.Y + localRotation.Y;
                float zRot = parent.globalRotation.Z + localRotation.Z;
                globalRotation = new Vector3(xRot, yRot, zRot);

                float xScale = parent.globalScale.X * localScale.X;
                float yScale = parent.globalScale.Y * localScale.Y;
                float zScale = parent.globalScale.Z * localScale.Z;
                globalScale = new Vector3(xScale, yScale, zScale);
            }
            else
            {
                globalPosition = localPosition;
                globalRotation = localRotation;
                globalScale = localScale;
            }

            foreach (var child in ChildObjects)
            {
                child.UpdateGlobalModel();
            }
        }

        public void SetLocalPosition(Vector3 vector)
        {
            localPosition = vector;
            UpdateGlobalModel();
        }

        public void SetLocalRotation(Vector3 vector)
        {
            localRotation = vector;
            UpdateGlobalModel();
        }

        public void SetLocalRotation(float x, float y, float z)
        {
            SetLocalRotation(new Vector3(x, y, z));
        }

        public void SetLocalScale(Vector3 vector)
        {
            localScale = vector;
            UpdateGlobalModel();
        }

        public void SetLocalPosition(float x, float y, float z)
        {
            SetLocalPosition(new Vector3(x, y, z));
        }

        public void SetLocalScale(float x, float y, float z)
        {
            SetLocalScale(new Vector3(x, y, z));
        }

        public void SetLocalScale(float commonScale)
        {
            SetLocalScale(new Vector3(commonScale, commonScale, commonScale));
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
