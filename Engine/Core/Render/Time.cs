namespace TwoBRenn.Engine.Core.Render
{
    class Time
    {
        private static Time instance;
        public static float deltaTime { get; private set; }

        public static Time GetInstance()
        {
            if (instance == null)
                instance = new Time();
            return instance;
        }

        public void CaptureFrametime(float frameTime)
        {
            deltaTime = frameTime / 1000;
        }
    }
}
