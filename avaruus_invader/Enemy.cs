using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_CsLo;
namespace avaruus_invader
{
    internal class Enemy
    {
        public TransformComponent transform;
        public CollisionComponent collision;
        public bool active;

        public Enemy(Vector2 startPosition, Vector2 direction, float speed, int size)
        {
            transform = new TransformComponent(startPosition, direction, speed);
            collision = new CollisionComponent(new Vector2(size, size));
            active = true;
        }
        internal void Draw()
        {
            if (active)
            {
                Raylib.DrawRectangleV(transform.position, collision.size, Raylib.DARKBROWN);
            }
        }

        internal void Update()
        {
            if (active)
            {
                float deltaTime = Raylib.GetFrameTime();
                transform.position += transform.direction * transform.speed * deltaTime;
            }
        }
    }
}
