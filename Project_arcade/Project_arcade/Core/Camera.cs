using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Project_arcade
{
    class Camera
    {
        float zoom;
        Matrix transform;
        Vector2 pos;
        public Vector2 origin;
        float rotation;
        float speed;

        public Camera(Vector2 pivot)
        {
            zoom = 1.0f;
            rotation = 0.0f;
            pos = Vector2.Zero;
            speed = 10.0f;
            transform = Matrix.Identity;

            // rotera och zooma från mitten
            origin = pivot;
        }

        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom < 0.1f) // En negativ zoom flippar bilden
                    zoom = 0.1f;
            }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public void Move(Vector2 displacement, bool respect_rotation = false)
        {
            if (respect_rotation)
            {
                displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(-rotation));
            }

            pos += displacement;
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public float Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                if (speed < 0.0f)
                    speed = 0.0f;
            }
        }

        public Matrix get_transformation()
        {
            transform =
                Matrix.CreateTranslation(new Vector3(-pos, 0)) *
                Matrix.CreateTranslation(new Vector3(-origin, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(origin, 0));

            return transform;
        }
    }
}

