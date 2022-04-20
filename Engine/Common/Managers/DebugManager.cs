using System.Collections.Generic;
using System.Text;
using OpenTK;
using TwoBRenn.Engine.Render.Camera;

namespace TwoBRenn.Engine.Common.Managers
{
    class DebugManager
    {
        private static DebugManager _instance;
        public static DebugManager Instance => _instance ?? (_instance = new DebugManager());

        private RennEngine rennEngine = RennEngine.Instance;

        private SortedDictionary<int, string> additionalInfo = new SortedDictionary<int, string>();

        public string GetDynamicDebugInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{rennEngine.Fps} fps");

            Quaternion q = Camera.GetViewMatrix().Inverted().ExtractRotation();
            Vector3 position = Camera.GetViewMatrix().Inverted().ExtractTranslation();
            stringBuilder.AppendLine($"XYZ: {position.X:0.00} / {position.Y:0.00} / {position.Z:0.00}");

            foreach (var info in additionalInfo.Values)
            {
                stringBuilder.AppendLine(info);
            }

            return stringBuilder.ToString();
        }

        public static void Debug(int order, string text)
        {
            if (Instance.additionalInfo.ContainsKey(order))
                Instance.additionalInfo[order] = text;
            else
                Instance.additionalInfo.Add(order, text);
        }
    }
}
