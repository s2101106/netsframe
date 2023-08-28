using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace avaruus_invader
{
    internal class Bullet
    {
        public TransformComponent transform;
        public CollisionComponent collision;
        public bool active;
        SpriteRenderer spriteRenderer;

        public Bullet(Vector2 startPosition, Vector2 direction, float speed, int size, Texture image, Color color)
        {
            this.transform = new TransformComponent(startPosition, direction, speed);
            this.collision = new CollisionComponent(new Vector2(size, size));
            spriteRenderer=new SpriteRenderer(image, color,transform,collision);
            active= true;
        }

        public void Update()
        {
            if(active)
            {
                transform.position += transform.direction * transform.speed * Raylib.GetFrameTime();

            }
        }
        public void Draw()
        {
            if(active)
            {
                Raylib.DrawRectangleV(transform.position, collision.size, Raylib.WHITE);
                spriteRenderer.Draw();
            }
            
        }
    }
}
