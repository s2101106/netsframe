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
        SpriteRenderer spriteRenderer;
        double shootInterval = 0.9;
        double lastShootTime;

        public Enemy(Vector2 startPosition, Vector2 direction, float speed, int size, Texture image)
        {
            
            transform = new TransformComponent(startPosition, direction, speed);
            collision = new CollisionComponent(new Vector2(size, size));
            spriteRenderer = new SpriteRenderer(image, Raylib.RED, transform, collision);
            active = true;
            lastShootTime = -shootInterval;
        }
        internal void Draw()
        {
            if (active)
            {
                Raylib.DrawRectangleV(transform.position, collision.size, Raylib.DARKBROWN);
                spriteRenderer.Draw();
            }
        }

        public bool Update()
        {
            if (active)
            {

                float deltaTime = Raylib.GetFrameTime();
                transform.position += transform.direction * transform.speed * deltaTime;
                double timeNow = Raylib.GetTime();
                double timeSinceLastShot = timeNow - lastShootTime;
                if (timeSinceLastShot >= shootInterval)
                {
                    lastShootTime = timeNow;
                    return true;
                }
            }
            return false;
        }
    }
}
