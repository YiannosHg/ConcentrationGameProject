using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcentrationGameProject
{
    public partial class GameForm : Form
    {
        private Button[] pictureButtons;

        // Default game variables
        int size = 5, rule = 2, cardSize = 60;
        int totalMoves = 0;
        int matchedPictures = 0;
        int countMoves = 0;

        // List of the moves played
        List<Button> moves = new List<Button>();
        
        
        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            createPictureButtons(size, cardSize, rule);
        }

        private void createPictureButtons(int size, int cardSize, int rule)
        {
            // Create array with random numbers that represent picture for buttons
            Random random = new Random();
            int[] array = Enumerable.Range(0, rule * size).OrderBy(c => random.Next()).ToArray();
            
            pictureButtons = new Button[size * rule]; // Create array of buttons

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < rule; ++j)
                {
                    // Properties
                    pictureButtons[i + j * size] = new Button(); // Add button in array
                    pictureButtons[i + j * size].Width = cardSize;
                    pictureButtons[i + j * size].Height = cardSize;
                    pictureButtons[i + j * size].BackgroundImage = Properties.Resources.qMark;
                    pictureButtons[i + j * size].BackgroundImageLayout = ImageLayout.Zoom;
                    //pictureButtons[i + j * size].Tag = i; // all buttons in the row have the same picture
                    pictureButtons[i + j * size].Tag = array[i + j * size]/rule; // random picture for each button
                    pictureButtons[i + j * size].Location = new Point(cardSize * (i % size) + 20 + i * 10, (cardSize * (i / size) + cardSize + 2 * j * cardSize) + 10);

                    // Events
                    pictureButtons[i + j * size].Click += new EventHandler(pictureButtons_ClickHandler);
                }
                this.Controls.AddRange(pictureButtons);
            }
        }

        private void pictureButtons_ClickHandler(object sender, System.EventArgs e)
        {
            string imageName = "image" + ((Button)sender).Tag;
            ((Button)sender).BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(imageName);
            moves.Add((Button)sender);
            ++countMoves;
            ++totalMoves;
            if (countMoves == rule)
                checkForMatch();
                //changePicturesTimer.Start();
            checkForEndOfGame();
        }

        private void resetGame()
        {
            totalMoves = 0;
            matchedPictures = 0;
            this.Controls.Clear();
            this.InitializeComponent();
        }

        public void checkForMatch()
        {
            if (2 == rule)
            {
                if ((int)moves[0].Tag == (int)moves[1].Tag)
                {
                    moves[0].Enabled = false;
                    moves[1].Enabled = false;
                    ++matchedPictures;
                }
                else
                {
                    moves[0].BackgroundImage = Properties.Resources.qMark;
                    moves[1].BackgroundImage = Properties.Resources.qMark;
                }
            }
            else // 3 = rule
            {
                if ((int)moves[0].Tag == (int)moves[1].Tag && (int)moves[0].Tag == (int)moves[2].Tag)
                {
                    moves[0].Enabled = false;
                    moves[1].Enabled = false;
                    moves[2].Enabled = false;
                    ++matchedPictures;
                }
                else 
                {
                    moves[0].BackgroundImage = Properties.Resources.qMark;
                    moves[1].BackgroundImage = Properties.Resources.qMark;
                    moves[2].BackgroundImage = Properties.Resources.qMark;
                }
            }
            countMoves = 0;
            moves.Clear();
        }

        private void checkForEndOfGame()
        {
            if (size == matchedPictures)
            {
                DialogResult dialogResult = MessageBox.Show($"The game was completed in {totalMoves} moves. \nDo you want to play again?", "Game completed", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    resetGame();
                    createPictureButtons(size, cardSize, rule);
                }
                else
                    this.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetGame();
            createPictureButtons(size, cardSize,rule);
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 5;
            cardSize = 60;
            mediumToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
            resetGame();
            createPictureButtons(size, cardSize, rule);
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 11;
            cardSize = 80;
            smallToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
            resetGame();
            createPictureButtons(size, cardSize, rule);
        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 17;
            cardSize = 100;
            smallToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = false;
            resetGame();
            createPictureButtons(size, cardSize, rule);
        }

        private void match2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rule = 2;
            match3ToolStripMenuItem.Checked = false;
            match3ToolStripMenuItem.CheckState = CheckState.Unchecked;
            match2ToolStripMenuItem.Checked = true;
            match2ToolStripMenuItem.CheckState = CheckState.Checked;
            resetGame();
            createPictureButtons(size, cardSize, rule);
        }

        private void match3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rule = 3;
            match2ToolStripMenuItem.Checked = false;
            match2ToolStripMenuItem.CheckState = CheckState.Unchecked;
            match3ToolStripMenuItem.Checked = true;
            match3ToolStripMenuItem.CheckState = CheckState.Checked;
            resetGame();
            createPictureButtons(size, cardSize, rule);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }

        private void changePicturesTimer_Tick(object sender, EventArgs e)
        {
            //checkForMatch();
        }
    }
}
