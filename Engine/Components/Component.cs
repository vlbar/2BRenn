namespace TwoBRenn.Engine.Components
{
    abstract class Component
    {
        public RennObject rennObject { get; set; }
        public bool IsEnabled = true;
        private bool isFirstUpdate = true;

        public void UpdateComponent()
        {
            if (!IsEnabled) return;
            if (isFirstUpdate)
            {
                OnStart();
                isFirstUpdate = false;
            }

            OnUpdate();
        }

        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnUnload() { }
    }
}
