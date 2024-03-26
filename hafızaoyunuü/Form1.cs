using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hafızaoyunuü
{
    public partial class Form1 : Form
    {

        Image[] resimler =
        {
            Properties.Resources.Flag_of_Azerbaijan_svg,
            Properties.Resources.Flag_of_Bosnia_and_Herzegovina_svg,
            Properties.Resources.Flag_of_Cyprus_svg,
            Properties.Resources.Flag_of_Kazakhstan_svg,
            Properties.Resources.Kokbayraq_flag_svg__1_,
            Properties.Resources.Flag_of_Turkey_svg,
            Properties.Resources.Flag_of_Uzbekistan_svg,
            Properties.Resources.Flag_of_Turkmenistan_svg,
            Properties.Resources.Flag_of_the_Turkish_Republic_of_Northern_Cyprus_svg,
            Properties.Resources.Flag_of_Kyrgyzstan__Pantone_colors__svg
        };

        int[] indeksler = {0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9 };

        PictureBox ilkkutu;
        int ilkIndeks,bulunan,deneme;

        PictureBox kutu;


        Timer timer;
        int timerCount;

        int currentPlayer = 1;
        int[] playerScores = { 0, 0 };



        public Form1()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 1000; // Timer interval is set to 1sec
            timer.Tick += Timer_Tick;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            shuffletheimages();

            StartImageDisplayTimer();
        }

        private void StartImageDisplayTimer()
        {
            timerCount = 0;
            timer.Start();
            DisplayImagesForDuration();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timerCount++;
            if (timerCount >= 5)
            {
                timer.Stop();
                HideImages();
            }
        }
        private void DisplayImagesForDuration()
        {
            foreach (Control control in Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    int kutuNo = int.Parse(pictureBox.Name.Substring(10));
                    int indeksNo = indeksler[kutuNo - 1];
                    pictureBox.Image = resimler[indeksNo];
                }
            }
        }
        private void HideImages()
        {
            foreach (Control control in Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    pictureBox.Image = null;
                }
            }
        }

        private void shuffletheimages()
        {
            Random rnd = new Random();
            for(int i = 0; i < 20; i++)
            {
                int sayi = rnd.Next(20);
                int gecici = indeksler[i];
                indeksler[i] = indeksler[sayi];
                indeksler[sayi] = gecici;                 
            }
        }

        private void ResetBoard()
        {
            foreach (Control control in Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    pictureBox.Visible = true;
                }
            }

            shuffletheimages();
        }

        private void SwitchPlayerTurn()
        {
            currentPlayer = (currentPlayer == 1) ? 2 : 1;
            MessageBox.Show($"Player {currentPlayer}, it's your turn!");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox kutu = (PictureBox)sender; //getting the which one is clicked
            int kutuNo = int.Parse(kutu.Name.Substring(10)); //getting the which number of box
            int indeksNo = indeksler[kutuNo-1];
            kutu.Image = resimler[indeksNo];
            kutu.Refresh();
                        
            // we should hold the boxinfo because we gonna exit this func.we need to define this outside 
            if (ilkkutu == null)   // PictureBox firstbox;
            {
                ilkkutu = kutu;
                ilkIndeks = indeksNo;
                deneme++;
            }
            else //clicked on the second box
            {
                System.Threading.Thread.Sleep(1000);
                ilkkutu.Image = null; // covering the picture again
                kutu.Image = null;
                if (ilkIndeks == indeksNo) //if its correct
                {
                    bulunan++;
                    ilkkutu.Visible = false;
                    kutu.Visible=false;

                    playerScores[currentPlayer - 1]++;  //ıncrease the score for the current player

                    if(bulunan == 10)
                    {
                        MessageBox.Show($"Player {currentPlayer} wins with {deneme} attempts! \n\nPlayer 1 Score: {playerScores[0]} \nPlayer 2 Score: {playerScores[1]}");
                        bulunan = 0;    
                        deneme = 0;
                        foreach (Control kontrol in Controls)
                        {
                            kontrol.Visible = true; 
                        }
                        
                        ResetBoard();
                       SwitchPlayerTurn();
                    
                    }
                }
                else { SwitchPlayerTurn(); }
                ilkkutu = null;
               

            }

        }
    }
}
