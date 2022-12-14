namespace Engine.Exceptions
{
    public class LoadAssetException : Exception
    {
        public LoadAssetException(string file) : base($"Can't load asset {file}") { }
    }
}
