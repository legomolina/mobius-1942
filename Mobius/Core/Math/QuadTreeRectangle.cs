namespace Engine.Core.Math
{
    public class QuadTreeRectangle : Rectangle
    {
        public bool IsOverlapped { get; set; } = false;

        public QuadTreeRectangle() : base() { }
        public QuadTreeRectangle(float x, float y, float width, float height) : base(x, y, width, height) { }
    }
}
