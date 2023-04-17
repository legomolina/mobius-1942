using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Collisions
{
    public class CollisionsContainer : IUpdatable
    {
        private readonly List<ICollidable> collidables = new List<ICollidable>();

        public List<ICollidable> Collidables => collidables.ToList();

        public void Insert(ICollidable collidable)
        {
            Insert(new ICollidable[] { collidable });
        }

        public void Insert(ICollidable[] insertCollidables)
        {
            Insert(insertCollidables.ToList());
        }

        public void Insert(IEnumerable<ICollidable> insertCollidables)
        {
            collidables.AddRange(insertCollidables);
        }

        public void Update(GameTime gameTime)
        {
            IList<ICollidable> list = collidables.ToList();

            foreach (ICollidable collidable in list)
            {
                if (collidable.Active)
                {
                    foreach (ICollidable other in list)
                    {
                        if (collidable != other)
                        {
                            if (CheckForCollisions(collidable, other))
                            {
                                collidable.OnCollision(other);
                                other.OnCollision(collidable);
                            }
                        }
                    }
                }
            }
        }

        public void Remove(ICollidable collidable)
        {
            collidables.Remove(collidable);
        }

        private bool CheckForCollisions(ICollidable collidable, ICollidable other)
        {
            return collidable.Bounds.Intersects(other.Bounds);
        }
    }
}
