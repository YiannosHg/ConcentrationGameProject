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

        // Initialize game variables with default settings
        int size = 5;
        int rule = 2;
        int cardSize = 60;
        int countMoves = 0;
        int clickedPictures = 0;

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            createPictureButtons();
        }

        private void createPictureButtons()
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

                    //string imageName = "image" + i;
                    //pictureButtons[i+j*size].Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(imageName);
                    //pictureButtons[i + j * size].Tag = i;

                    string imageName = "image" + i;
                    //pictureButtons[i + j * size].Tag = (Bitmap)Properties.Resources.ResourceManager.GetObject(imageName);
                    pictureButtons[i + j * size].Tag = imageName;

                    pictureButtons[i + j * size].Location = new Point(cardSize * (i % size) + cardSize + i * 10, (cardSize * (i / size) + cardSize + 2 * j * cardSize) + 10);

                    // Events
                    pictureButtons[i + j * size].Click += new EventHandler(pictureButtons_ClickHandler);
                }
                this.Controls.AddRange(pictureButtons);
            }
        }

        private void pictureButtons_ClickHandler(object sender, System.EventArgs e)
        {
            // Bitmap tempPic = (Bitmap)Properties.Resources.ResourceManager.GetObject((string)((Button)sender).Tag);
            /*Bitmap tempPic = (Bitmap)((Button)sender).BackgroundImage;
            ((Button)sender).BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject((string)((Button)sender).Tag);
            ((Button)sender).Tag = tempPic;*/

            /*string imageName = "image" + ((Button)sender).Tag;
            ((Button)sender).BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(imageName);*/
        }
    }
}
