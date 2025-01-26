// Include the namespaces (code libraries) you need below.
using System;
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
        Vector2[] bugArray = [new Vector2(0,0)];
        int bugSize = 3;
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
                Vector2 bugVector = Vector2.Normalize(platePosition - bugPosition) * bugSpeed;
                Draw.FillColor = Color.Black;
                Draw.Circle(bugPosition, bugSize);
                bugArray[i] = bugPosition + bugVector;
            }
        }

        public void spawnBug()
        {

        }

    }

}
