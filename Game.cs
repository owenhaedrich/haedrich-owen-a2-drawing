// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    // Your game code goes inside this class!
    public class Game
    {

        Color dirtBrown = new Color("8B4513");
        Color grassGreen = new Color("008013");
        Color pancakeBrown = new Color("ffff88");
        Color pancakeEdgeBrown = new Color("775511");

        Vector2[] grassArray = [new Vector2(30, 50), new Vector2(310, 310), new Vector2(270, 90), new Vector2(100, 200), new Vector2(200, 300)]; //Grass is stored as the location of the roots
        Vector2[] bugArray = [];
        float bugScale = 0.7f;
        int bugSpeed = 1;

        Vector2 platePosition = new Vector2(200, 200);
        Vector2 plateSize = new Vector2(100, 50);
        public void Setup()
        {
            Window.SetSize(400, 400); // Size is in pixels and ordered as (width, height)
            Window.TargetFPS = 60; // Aim to render 60 times a second
        }

        // Update runs every frame.
        public void Update()
        {
            Window.ClearBackground(dirtBrown); //Reset the background

            // Chance to spawn new bug
            if (Random.Integer(100) > 95)
            {
                Vector2 newBug = new Vector2(0);

                // Choose a random side of the screen to spawn the bug
                var bugSpawnDirection = Random.Integer(4);
                if (bugSpawnDirection == 0) newBug = new Vector2(-100, Random.Integer(Window.Height));
                if (bugSpawnDirection == 1) newBug = new Vector2(Window.Width + 100, Random.Integer(Window.Height));
                if (bugSpawnDirection == 2) newBug = new Vector2(Random.Integer(Window.Width), -100);
                if (bugSpawnDirection == 3) newBug = new Vector2(Random.Integer(Window.Width), Window.Height + 100);

                // Make the array bigger and add the new bug
                Array.Resize(ref bugArray, bugArray.Length + 1);
                bugArray[bugArray.Length - 1] = newBug;
            }

            // Draw thick lines for plate, pancake and grass
            Draw.LineSize = 2;

            // Draw plate
            Draw.FillColor = Color.LightGray;
            Draw.LineColor = Color.Gray;
            Draw.Ellipse(platePosition, plateSize);
            Draw.FillColor = Color.Gray;
            Draw.Ellipse(platePosition, plateSize * 0.8f);

            // Draw pancakes
            Draw.FillColor = pancakeBrown;
            Draw.LineColor = pancakeEdgeBrown;
            Draw.LineSize = 2;
            Draw.Ellipse(platePosition - Vector2.UnitX * 5, plateSize * 0.5f);
            Draw.Ellipse(platePosition + new Vector2(7, -1), plateSize * 0.5f);
            Draw.Ellipse(platePosition - Vector2.UnitY * 8, plateSize * 0.5f);
            Draw.LineSize = 1;

            // Draw grass at all root positions
            foreach (Vector2 grass in grassArray)
            {
                DrawGrass(grass);
            }

            // Draw thin lines for bugs
            Draw.LineSize = 1;

            // Update bugs
            for (int i = 0; i < bugArray.Length; i++)
            {
                // Get the bug's position and try to move it towards the plate
                // Bugs at negative infinity are dead and don't move
                Vector2 bugPosition = bugArray[i];
                if (bugPosition != Vector2.NegativeInfinity)
                {
                    Vector2 bugDirection = Vector2.Normalize(platePosition - bugPosition);
                    DrawBug(bugPosition, bugDirection);
                    bugArray[i] = bugPosition + bugDirection * bugSpeed;

                    // If the bug is close to the plate or the mouse, send it to negative infinity
                    bool onPlate = Vector2.Distance(bugArray[i], new Vector2(200, 200)) < 30;
                    bool onMouse = Vector2.Distance(bugArray[i], Input.GetMousePosition()) < 30;
                    if (onPlate || onMouse)
                    {
                        bugArray[i] = Vector2.NegativeInfinity;
                    }
                }
            }

        }

        // Draw a bug at the position, traveling in the direction
        public void DrawBug(Vector2 position, Vector2 direction)
        {
            var angle = Math.Atan2(direction.Y, direction.X);

            Draw.FillColor = Color.Black;
            Draw.LineColor = Color.Black;

            //Draw abdomen
            Draw.Circle(position + direction * 2 * bugScale, 2 * bugScale);
            Draw.Circle(position + direction * 4 * bugScale, 3 * bugScale);
            Draw.Circle(position + direction * 5 * bugScale, 3 * bugScale);
            Draw.Circle(position + direction * 6 * bugScale, 3 * bugScale);
            Draw.Circle(position + direction * 8 * bugScale, 2 * bugScale);

            //Draw thorax
            Draw.Capsule(position + direction * 7 * bugScale, position + direction * 17 * bugScale, bugScale);

            //Draw head
            Draw.Circle(position + (direction * 20 * bugScale), 3 * bugScale);

            //Draw antennae
            Vector2[] a1 = [(Vector2.UnitX * 20 * bugScale), new Vector2(28, 3) * bugScale, new Vector2(30, 3) * bugScale];
            Vector2[] a2 = [(Vector2.UnitX * 20 * bugScale), new Vector2(28, -3) * bugScale, new Vector2(30, -3) * bugScale];
            for (int i = 0; i < 3; i++)
            {
                a1[i] = position + RotateVector(a1[i], angle);
                a2[i] = position + RotateVector(a2[i], angle);
            }
            Draw.PolyLine(a1);
            Draw.PolyLine(a2);

            //Draw front legs
            Draw.Line(position + (direction * 14 * bugScale), position + RotateVector(new Vector2(26, 7) * bugScale, angle));
            Draw.Line(position + (direction * 14 * bugScale), position + RotateVector(new Vector2(26, -7) * bugScale, angle));

            //Draw middle legs
            Vector2[] ml1 = [(Vector2.UnitX * 14 * bugScale), new Vector2(14, 3) * bugScale, new Vector2(16, 5) * bugScale, new Vector2(12, 9) * bugScale];
            Vector2[] ml2 = [(Vector2.UnitX * 14 * bugScale), new Vector2(14, -3) * bugScale, new Vector2(16, -5) * bugScale, new Vector2(12, -9) * bugScale];
            for (int i = 0; i < 4; i++)
            {
                ml1[i] = position + RotateVector(ml1[i], angle);
                ml2[i] = position + RotateVector(ml2[i], angle);
            }
            Draw.PolyLine(ml1);
            Draw.PolyLine(ml2);

            //Draw back legs
            Vector2[] b1 = [(Vector2.UnitX * 10 * bugScale), new Vector2(9, 3) * bugScale, new Vector2(0, 7) * bugScale];
            Vector2[] b2 = [(Vector2.UnitX * 10 * bugScale), new Vector2(9, -3) * bugScale, new Vector2(0, -7) * bugScale];
            for (int i = 0; i < 3; i++)
            {
                b1[i] = position + RotateVector(b1[i], angle);
                b2[i] = position + RotateVector(b2[i], angle);
            }
            Draw.PolyLine(b1);
            Draw.PolyLine(b2);
        }


        //Drawa clump of grass at the position
        public void DrawGrass(Vector2 position)
        {
            Draw.LineColor = grassGreen;
            Draw.LineSize = 2;
            Draw.Line(position, position + new Vector2(0, -50));
            Draw.Line(position, position + new Vector2(10, -50));
            Draw.Line(position, position + new Vector2(-13, -40));
            Draw.Line(position, position + new Vector2(13, -40));
            Draw.Line(position, position + new Vector2(-10, -50));
            Draw.Line(position + new Vector2(-3, 0), position + new Vector2(-17, -50));
            Draw.Line(position + new Vector2(1, 0), position + new Vector2(30, -30));
            Draw.LineSize = 1;
        }

        // Rotate a vector by an angle from the x-axis
        public Vector2 RotateVector(Vector2 vector, double angle)
        {
            float x = (float)(vector.X * Math.Cos(angle) - vector.Y * Math.Sin(angle));
            float y = (float)(vector.X * Math.Sin(angle) + vector.Y * Math.Cos(angle));
            return new Vector2(x, y);
        }
    }

}
