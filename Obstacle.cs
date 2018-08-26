using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
                new VertexPositionColor(new Vector3(100, 100, 0), Color.Green),
                new VertexPositionColor(new Vector3(150, 150, 0), Color.Red),
                new VertexPositionColor(new Vector3(150, 200, 0), Color.Green)
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

        public void Draw(SpriteBatch sb)
        {
            EffectTechnique effectTechnique = basicEffect.Techniques[0];
            EffectPassCollection effectPassCollection = effectTechnique.Passes;

            foreach (EffectPass pass in effectPassCollection)
            {
                pass.Apply();

                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList,
                    vertexPositionColors, 0, 1);
            }

            var pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.Red });
            sb.Draw(pixel, grossBox, Color.Red);
        }
    }
}
