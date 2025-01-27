// Include the namespaces (code libraries) you need below.
using System;
using System.Drawing;
using System.Linq;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        Vector2[] bugArray = [new Vector2(0,0), new Vector2(0, 200), new Vector2(0, 400)];
        int bugSpeed = 1;

        Vector2 platePosition = new Vector2(200, 200);
        int plateSize = 10;
        public void Setup()
        {
            Window.SetSize(400, 400); // Size is in pixels and ordered as (width, height)
            Window.TargetFPS = 60; // Aim to render 60 times a second
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite); // Reset the background

            for (int i = 0; i < bugArray.Length; i++)
            {
                Vector2 bugPosition = bugArray[i];
                Vector2 bugDirection = Vector2.Normalize(platePosition - bugPosition);
                drawBug(bugPosition, bugDirection);
                bugArray[i] = bugPosition + bugDirection * bugSpeed;
            }
        }

        public void drawBug(Vector2 position, Vector2 direction)
        {
            var bugScale = 5;
            
            Draw.FillColor = Color.Black;

            //Draw abdomen
            Draw.Ellipse(position + (Vector2.UnitX * 5 * bugScale), new Vector2(10, 6) * bugScale);

            //Draw thorax
            Draw.Ellipse(position + (Vector2.UnitX * 14 * bugScale), new Vector2(12, 2) * bugScale);

            //Draw head
            Draw.Circle(position + (Vector2.UnitX * 20 * bugScale), 3 * bugScale);

            //Draw antennae
            Draw.Line(position + (Vector2.UnitX * 20 * bugScale), position + new Vector2(28, 3) * bugScale);
            Draw.Line(position + (Vector2.UnitX * 20 * bugScale), position + new Vector2(28, -3) * bugScale);
            Draw.Line(position + new Vector2(28, 3) * bugScale, position + new Vector2(30, 3) * bugScale);
            Draw.Line(position + new Vector2(28, -3) * bugScale, position + new Vector2(30, -3) * bugScale);

            //Draw front legs
            Draw.Line(position + (Vector2.UnitX * 14 * bugScale), position + new Vector2(26, 7) * bugScale);
            Draw.Line(position + (Vector2.UnitX * 14 * bugScale), position + new Vector2(26, -7) * bugScale);

            //Draw middle legs
            Draw.Line(position + (Vector2.UnitX * 14 * bugScale), position + new Vector2(14, 3) * bugScale);
            Draw.Line(position + (Vector2.UnitX * 14 * bugScale), position + new Vector2(14, -3) * bugScale);
            Draw.Line(position + new Vector2(14, 3) * bugScale, position + new Vector2(16, 5) * bugScale);
            Draw.Line(position + new Vector2(14, -3) * bugScale, position + new Vector2(16, -5) * bugScale);
            Draw.Line(position + new Vector2(16, 5) * bugScale, position + new Vector2(12, 9) * bugScale);
            Draw.Line(position + new Vector2(16, -5) * bugScale, position + new Vector2(12, -9) * bugScale);

            //Draw back legs
            Draw.Line(position + (Vector2.UnitX * 10 * bugScale), position + new Vector2(9, 3) * bugScale);
            Draw.Line(position + (Vector2.UnitX * 10 * bugScale), position + new Vector2(9, -3) * bugScale);
            Draw.Line(position + new Vector2(9, 3) * bugScale, position + new Vector2(0, 7) * bugScale);
            Draw.Line(position + new Vector2(9, -3) * bugScale, position + new Vector2(0, -7) * bugScale);
        }

    }

}
