using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        Label firstClicked = null;
        Label secondClicked = null;
        Random random = new Random();
        List<string> icons = new List<string>()
        {
        "a", "a", "L", "L", ";", ";", "j", "j", "h", "h", ".", ".", "t", "t", "R", "R",
        "d", "d", "x", "x", "y", "y", "f", "f", "@", "@", "N", "N", "5", "5"
        };
        int timePassed;
        int thirdTimer;

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }

            timer2.Start();
            timePassed = 0;
            thirdTimer = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Red;
                    timer3.Start();
                    return;
                }
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Red;
                timer3.Stop();
                thirdTimer = 0;
                CheckForWinner();
                if (firstClicked.Text != secondClicked.Text)
                {
                    System.Media.SystemSounds.Asterisk.Play();
                }
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    System.Media.SystemSounds.Hand.Play();
                    return;
                }
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
            System.Media.SystemSounds.Beep.Play();
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            timer2.Stop();
            MessageBox.Show($"You matched all the icons! You did it in {timePassed} seconds. Congratulations");
            Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timePassed++;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            thirdTimer++;
            if (secondClicked == null && thirdTimer > 5)
            {
                firstClicked.ForeColor = firstClicked.BackColor;
                firstClicked = null;
                timer3.Stop();
                thirdTimer = 0;
                return;
            }
        }
    }
}
