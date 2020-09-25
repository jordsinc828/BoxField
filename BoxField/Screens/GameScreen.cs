using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown;

        //used to draw boxes on screen


        SolidBrush boxBrush = new SolidBrush(Color.Red);
        SolidBrush heroBrush = new SolidBrush(Color.White);

        // create a list to hold a column of boxes        

        List<Box> boxes = new List<Box>();
        int leftX = 200;
        int gap = 200;
        Boolean moveRight = true;
        string patternDirection = "right";
        int patternLength = 10;
        int patternSpeed = 7;

        Box hero;
        int heroSpeed = 10;
        int heroSize = 30;

        int gameScore = 0;
    


        Random randNum = new Random();
        Random randSpeed = new Random();
        Color c = Color.Red;

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }
        public void MakeBox()
        {
            int rand = randNum.Next(1, 4);

            //gets color for boxes

            if (rand == 1)
            {
                c = Color.Red;
            }
            else if (rand == 2)
            {
                c = Color.Yellow;
            }
            else if (rand == 3)
            {
                c = Color.Orange;
            }

            if (boxes[boxes.Count - 1].y > 21)
            {
                patternLength--;

                if (patternLength == 0)
                {
                    moveRight = !moveRight;
                    patternLength = randNum.Next(1, 20);
                }

                patternSpeed = randSpeed.Next(7, 35);

                if (moveRight == true)
                {
                    leftX += patternSpeed;
                }
                else
                {
                    leftX -= patternSpeed;
                }
                Box newBox1 = new Box(leftX, 0, 20, c);
                boxes.Add(newBox1);

                Box newBox2 = new Box(leftX + gap, 0, 20, c);
                boxes.Add(newBox2);
            }
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            // set game start values

            Box newBox1 = new Box(this.Width / 2 - 300 - 30, 0, 20, c);
            boxes.Add(newBox1);
            Box newBox2 = new Box(this.Width / 2 + 300 - 20, 0, 20, c);
            boxes.Add(newBox2);

            MakeBox();

            hero = new Box(this.Width / 2 - heroSize / 2, 370, heroSize, c);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;           
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            gameScore++;
            scoreLabel.Text = "" + gameScore;

            // check for collisions

            Rectangle heroRec = new Rectangle(hero.x, hero.y, hero.size, hero.size);

            if (boxes.Count >= 6)
            {

                // 0-3
                for (int i = 0; i < 6; i++)
                {
                    Rectangle boxRec = new Rectangle(boxes[i].x, boxes[i].y, boxes[i].size, boxes[i].size);

                    if (boxRec.IntersectsWith(heroRec))
                    {
                        gameLoop.Enabled = false;
                    }
                }
            }
            // move player

            if (leftArrowDown == true)
            {
                hero.Move(heroSpeed, false);
            }
            else if (rightArrowDown == true)
            {
                hero.Move(heroSpeed, true);
            }

            // update location of all boxes (drop down screen)

            foreach (Box b in boxes)
            {
                b.Move(5);
            }

            // remove box if it has gone of screen

            if (boxes[0].y > 400)
            {
                boxes.RemoveAt(0);
            }

            // add new box if it is time

            MakeBox();
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // draw boxes to screen

            foreach  (Box b in boxes)
            {
                boxBrush.Color = b.color;
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
                e.Graphics.FillRectangle(heroBrush, 0, 400, this.Width, 2);

                // draw hero
                e.Graphics.FillRectangle(heroBrush, hero.x, hero.y, hero.size, hero.size);
            }

        }
    }
}
