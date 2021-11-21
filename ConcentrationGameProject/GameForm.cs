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
        List<Button> moves = new List<Button>();
        int matchedPictures = 0;

        public class Counter
        {
            public int threshold { get; set; }
            public int total = 0;

            public void Add()
            {
                ++total;
                if (total == threshold)
                {
                    ThresholdReached?.Invoke(this, EventArgs.Empty);
                }
            }

            public event EventHandler ThresholdReached;
        }

        static void countMoves_ThreasholdReached(object sender, EventArgs e)
        {
           
        }

        Counter countMoves = new Counter();

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            createPictureButtons(size, cardSize, rule);
            //countMoves.ThresholdReached += countMoves_ThresholdReached;
        }

        private void createPictureButtons(int size, int cardSize, int rule)
        {
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
                    pictureButtons[i + j * size].Tag = i;
                    pictureButtons[i + j * size].Location = new Point(cardSize * (i % size) + cardSize + i * 10, (cardSize * (i / size) + cardSize + 2 * j * cardSize) + 10);

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
            countMoves.Add();
            Console.WriteLine($"CountMoves: {countMoves}");
            ++totalMoves;
        }

        public void checkForMatch()
        {
            Console.WriteLine("Entered");

            if (2 == rule)
            {
                if (moves[0].Tag == moves[1].Tag)
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
                if (moves[0].Tag == moves[1].Tag && moves[0].Tag == moves[2].Tag)
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
            countMoves.total = 0;
            moves.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 5;
            cardSize = 60;
            mediumToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
            createPictureButtons(size, cardSize, rule);
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 11;
            cardSize = 80;
            smallToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
            createPictureButtons(size, cardSize, rule);
        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 17;
            cardSize = 100;
            smallToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = false;
            createPictureButtons(size, cardSize, rule);
        }

        private void match2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rule = 2;
            match3ToolStripMenuItem.Checked = false;
            createPictureButtons(size, cardSize, rule);
        }

        private void match3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rule = 3;
            match2ToolStripMenuItem.Checked = false;
            createPictureButtons(size, cardSize, rule);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }
    }
}
