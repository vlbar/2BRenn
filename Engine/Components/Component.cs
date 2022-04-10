namespace TwoBRenn.Engine.Components
{
    abstract class Component
    {
        public RennObject rennObject { get; set; }

        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnUnload() { }
    }
}
