using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown;

        //used to draw boxes on screen


        SolidBrush boxBrush = new SolidBrush(Color.White);

        // create a list to hold a column of boxes        

        List<Box> boxes = new List<Box>();

        Box hero;
        int heroSpeed = 10;
        int heroSize = 30;

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            //TODO - set game start values

            Box newBox = new Box(this.Width/2 - 300 - 30, 0, 20);
            boxes.Add(newBox);

            hero = new Box(this.Width / 2 - heroSize / 2, 370, heroSize);
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
            //TODO - update location of all boxes (drop down screen)

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

            if (boxes[boxes.Count - 1].y > 21)
            {
                Box newBox1 = new Box(this.Width/2 - 300 - 30, 0, 20);
                boxes.Add(newBox1);
                Box newBox2 = new Box(this.Width / 2 + 300 - 20, 0, 20);
                boxes.Add(newBox2);
            }
           

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // draw boxes to screen

            foreach  (Box b in boxes)
            {
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
                e.Graphics.FillRectangle(boxBrush, 0, 400, this.Width, 2);

                // draw hero
                e.Graphics.FillRectangle(boxBrush, hero.x, hero.y, hero.size, hero.size);
            }

        }
    }
}
