namespace TwoBRenn.Engine.Render.Utils
{
    class Time
    {
        private static Time _instance;
        public static float DeltaTime { get; private set; }

        public static Time GetInstance()
        {
            return _instance ?? (_instance = new Time());
        }

        public void CaptureFrametime(float frameTime)
        {
            DeltaTime = frameTime / 1000;
        }
    }
}
