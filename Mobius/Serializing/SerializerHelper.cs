using Engine.Core;
using org.matheval;

namespace Engine.Serializing
{
    public static class SerializerHelper
    {
        private static Dictionary<string, int> bindings = new Dictionary<string, int>()
        {
            { "window_width", GraphicsManager.Instance.WindowWidth },
            { "window_height", GraphicsManager.Instance.WindowHeight },
        };

        public static T Eval<T>(string value)
        {
            Expression expr = new Expression(value);
            
            foreach(KeyValuePair<string, int> binding in bindings)
            {
                expr.Bind(binding.Key, binding.Value);
            }

            return expr.Eval<T>();
        }
    }
}
