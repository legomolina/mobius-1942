namespace Engine.Core.Math
{
    public class QuadTree<T>
    {
        private readonly HashSet<T> objects = new();
        private readonly IQuadTreeObject<T> objectBounds;

        private bool hasChildren;
        private QuadTree<T> quadTopLeft;
        private QuadTree<T> quadTopRight;
        private QuadTree<T> quadBottomLeft;
        private QuadTree<T> quadBottomRight;

        public QuadTreeRectangle Area { get; set; }
        public int CurrentLevel { get; }
        public int MaxLevel { get; }
        public int MaxObjects { get; }

        public QuadTree(float x, float y, float width, float height, IQuadTreeObject<T> objectBounds, int maxObjects = 10, int maxLevel = 5, int currentLevel = 0)
        {
            Area = new QuadTreeRectangle(x, y, width, height);
            this.objectBounds = objectBounds;

            CurrentLevel = currentLevel;
            MaxLevel = maxLevel;
            MaxObjects = maxObjects;

            hasChildren = false;
        }

        public QuadTree(Vector2 size, IQuadTreeObject<T> objectBounds, int maxObjects = 10, int maxLevel = 5, int currentLevel = 0)
            : this(0.0f, 0.0f, size.X, size.Y, objectBounds, maxObjects, maxLevel, currentLevel) { }

        public QuadTree(Vector2 position, Vector2 size, IQuadTreeObject<T> objectBounds, int maxObjects = 10, int maxLevel = 5, int currentLevel = 0)
            : this(position.X, position.Y, size.X, size.Y, objectBounds, maxObjects, maxLevel, currentLevel) { }

        private bool IsObjectInside(T obj)
        {
            if (objectBounds.GetTop(obj) > Area.Y + Area.Height)
            {
                return false;
            }

            if (objectBounds.GetBottom(obj) < Area.Y)
            {
                return false;
            }

            if (objectBounds.GetLeft(obj) > Area.X + Area.Width)
            {
                return false;
            }

            if (objectBounds.GetRight(obj) < Area.X)
            {
                return false;
            }

            return true;
        }

        private bool IsOverlapping(Rectangle rect)
        {
            if ((rect.X + rect.Width) < Area.X || rect.X > (Area.X + rect.Width))
            {
                return false;
            }

            if (rect.Y > (Area.Y + Area.Height) || (rect.Y + rect.Height) < Area.Y)
            {
                return false;
            }

            Area.IsOverlapped = true;
            return true;
        }

        private void Quarter()
        {
            if (CurrentLevel > MaxLevel)
            {
                return;
            }

            int nextLevel = CurrentLevel + 1;
            hasChildren = true;
            quadTopLeft = new QuadTree<T>(Area.X, Area.Y, Area.Width / 2, Area.Height / 2, objectBounds, MaxObjects, MaxLevel, nextLevel);
            quadTopRight = new QuadTree<T>((Area.X + Area.Width) / 2, Area.Y, Area.Width / 2, Area.Height / 2, objectBounds, MaxObjects, MaxLevel, nextLevel);
            quadBottomLeft = new QuadTree<T>(Area.X, (Area.Y + Area.Height) / 2, Area.Width / 2, Area.Height / 2, objectBounds, MaxObjects, MaxLevel, nextLevel);
            quadBottomRight = new QuadTree<T>((Area.X + Area.Width) / 2, (Area.Y + Area.Height) / 2, Area.Width / 2, Area.Height / 2, objectBounds, MaxObjects, MaxLevel, nextLevel);

            foreach (T obj in objects)
            {
                Insert(obj);
            }

            objects.Clear();
        }

        public void Clear()
        {
            if (hasChildren)
            {
                quadTopLeft.Clear();
                quadTopLeft = null;
                quadTopRight.Clear();
                quadTopRight = null;
                quadBottomLeft.Clear();
                quadBottomLeft = null;
                quadBottomRight.Clear();
            }

            objects.Clear();
            hasChildren = false;
            Area.IsOverlapped = false;
        }

        public bool Insert(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (!IsObjectInside(obj))
            {
                return false;
            }

            if (hasChildren)
            {
                if (quadTopLeft.Insert(obj))
                {
                    return true;
                }

                if (quadTopRight.Insert(obj))
                {
                    return true;
                }

                if (quadBottomLeft.Insert(obj))
                {
                    return true;
                }

                if (quadBottomRight.Insert(obj))
                {
                    return true;
                }
            }
            else
            {
                objects.Add(obj);
                if (objects.Count() > MaxObjects)
                {
                    Quarter();
                }
            }

            return true;
        }

        public void InsertRange(IEnumerable<T> objects)
        {
            foreach (T obj in objects)
            {
                Insert(obj);
            }
        }

        public int Count()
        {
            int count = 0;

            if (hasChildren)
            {
                count += quadTopLeft.Count();
                count += quadTopRight.Count();
                count += quadBottomLeft.Count();
                count += quadBottomRight.Count();
            }
            else
            {
                count = objects.Count;
            }

            return count;
        }

        public QuadTreeRectangle[] GetGrid()
        {
            List<QuadTreeRectangle> grid = new() { Area };

            if (hasChildren)
            {
                grid.AddRange(quadTopLeft.GetGrid());
                grid.AddRange(quadTopRight.GetGrid());
                grid.AddRange(quadBottomLeft.GetGrid());
                grid.AddRange(quadBottomRight.GetGrid());
            }

            return grid.ToArray();
        }

        public T[] FindObjects(QuadTreeRectangle rect)
        {
            List<T> foundObjects = new();

            if (hasChildren)
            {
                foundObjects.AddRange(quadTopLeft.FindObjects(rect));
                foundObjects.AddRange(quadTopRight.FindObjects(rect));
                foundObjects.AddRange(quadBottomLeft.FindObjects(rect));
                foundObjects.AddRange(quadBottomRight.FindObjects(rect));
            }
            else
            {
                if (IsOverlapping(rect))
                {
                    foundObjects.AddRange(objects);
                }
            }

            HashSet<T> result = new();
            result.UnionWith(foundObjects);

            return result.ToArray();
        }

        public T[] FindObjects(T bounds)
        {
            return FindObjects(new QuadTreeRectangle(objectBounds.GetLeft(bounds), objectBounds.GetTop(bounds), objectBounds.GetRight(bounds), objectBounds.GetBottom(bounds)));
        }
    }
}
