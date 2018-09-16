using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidMonoIntro
{
    class Obstacle
    {
        private VertexPositionColor[] vertexPositionColors;
        private BasicEffect basicEffect;
        private GraphicsDevice graphicsDevice;
        Rectangle grossBox;

        public Obstacle(GraphicsDevice gfx)
        {

            graphicsDevice = gfx;
            vertexPositionColors = new[]
            {
                new VertexPositionColor(new Vector3(100, 100, 0), Color.Yellow),
                new VertexPositionColor(new Vector3(200, 100, 0), Color.Red),
                new VertexPositionColor(new Vector3(200, 200, 0), Color.Green),
                new VertexPositionColor(new Vector3(100, 200, 0), Color.Blue),
                new VertexPositionColor(new Vector3(100, 100, 0), Color.Yellow),
            };
            basicEffect = new BasicEffect(gfx);
            basicEffect.World = Matrix.CreateOrthographicOffCenter(
                0, gfx.Viewport.Width, gfx.Viewport.Height, 0, 0, 1);

            {
                Point low = new Point(int.MaxValue);
                Point high = new Point(0);
                foreach (var vert in vertexPositionColors)
                {
                    if (vert.Position.X < low.X)
                    {
                        low.X = (int)vert.Position.X;
                    }
                    else if (vert.Position.X > high.X)
                    {
                        high.X = (int)vert.Position.X;
                    }


                    if (vert.Position.Y < low.Y)
                    {
                        low.Y = (int)vert.Position.Y;
                    }
                    else if (vert.Position.Y > high.Y)
                    {
                        high.Y = (int)vert.Position.Y;
                    }
                }
                grossBox = new Rectangle(low, high);
            }
        }

        public double min;
        public double max;
        public int minIndex;
        public int maxIndex;
        Vector2 left;
        Vector2 right;
        public bool IntersectsLine(Vector2 start, Vector2 end)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {

            }
            Vector2 direction = end - start;
            Vector2 normal = new Vector2(-direction.Y, direction.X);

            min = Vector2.Dot(vertexPositionColors[0].Position.ToVector2(), normal) / direction.Length();
            max = Vector2.Dot(vertexPositionColors[0].Position.ToVector2(), normal) / direction.Length();
            minIndex = 0;
            maxIndex = 0;
            for (int i = 1; i < vertexPositionColors.Length; i++)
            {
                double result = Vector2.Dot(vertexPositionColors[i].Position.ToVector2(), normal) / direction.Length();
                if (result < min)
                {
                    min = result;
                    minIndex = i;
                }
                if (result > max)
                {
                    max = result;
                    maxIndex = i;
                }
            }

            if (0 > min && 0 < max)
            {
                return true;
            }

            return false;
        }

        public void Draw(SpriteBatch sb)
        {
            EffectTechnique effectTechnique = basicEffect.Techniques[0];
            EffectPassCollection effectPassCollection = effectTechnique.Passes;

            foreach (EffectPass pass in effectPassCollection)
            {
                pass.Apply();
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip,
                    vertexPositionColors, 0, 3);
            }

            var pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            for (int i = 0; i < vertexPositionColors.Length; i++)
            {
                var vert = vertexPositionColors[i];
                var color = Color.Green;
                if (i == minIndex)
                {
                    color = Color.Blue;
                }
                else if (i == maxIndex)
                {
                    color = Color.Red;
                }
                sb.Draw(pixel, new Rectangle((int)vert.Position.X, (int)vert.Position.Y, 10, 10), color * 0.40f);
            }

            //var pixel = new Texture2D(graphicsDevice, 1, 1);
            //pixel.SetData(new Color[] { Color.Red });
            //sb.Draw(pixel, grossBox, Color.Red);
        }
    }
}
