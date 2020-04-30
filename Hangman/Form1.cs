﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


/*
 * https://www.youtube.com/watch?v=kbAksctUEzc
 * */

namespace Hangman
{
    public partial class FrmHangman : Form
    {
        private Bitmap[] hangImages = { Hangman.Properties.Resources._1, Hangman.Properties.Resources._2, Hangman.Properties.Resources._3, Hangman.Properties.Resources._4, Hangman.Properties.Resources._5, Hangman.Properties.Resources._6, Hangman.Properties.Resources._7 };

        private int wrongGuesses = 0;
        private string[] words;
        private string currentWord = "";
        private string copyCurrent = "";

        public FrmHangman()
        {
            InitializeComponent();
        }

        private void loadWords()
        {
            char[] delimiterChar = { ',' };
            string[] readWords = File.ReadAllLines("Words.txt");
            
        
            words = new string[readWords.Length];
            int index = 0;

            foreach (string s in readWords)
            {
                string [] line = s.Split(delimiterChar);
                words[index++] = line[1];
                
            }
        }

        private void setUpWordChoice()
        {
            wrongGuesses = 0;
            hangImage.Image = hangImages[wrongGuesses];
            int guessIndex = (new Random()).Next(words.Length);
            currentWord = words[guessIndex];
            
            copyCurrent = "";
            for(int i=0; i<currentWord.Length; i++)
            {
                copyCurrent += "_";
            }
            displayCopy();
        }


        private void displayCopy()
        {
            lblShowWord.Text = "";
            for (int i = 0; i < currentWord.Length; i++)
            {
                lblShowWord.Text += copyCurrent.Substring(i,1);
                lblShowWord.Text += " ";

            }
        }


        private void guessClick(object sender, EventArgs e)
        {
            Button choice = sender as Button;
            choice.Enabled = false;

            if(currentWord.Contains(choice.Text))
            {
                char[] temp = copyCurrent.ToCharArray();
                char[] find=currentWord.ToCharArray();
                char guessChar = choice.Text.ElementAt(0);

                for(int i=0; i<find.Length;i++)
                {
                    if(find[i] == guessChar)
                    {
                        temp[i] = guessChar;
                    }
                }

                copyCurrent = new string(temp);
                displayCopy();
            }
            else
            {
                wrongGuesses++;
            }

            if(wrongGuesses < 7)
            {
                hangImage.Image = hangImages[wrongGuesses];

            }
            else
            {
                lblResult.Text = "You Lose!!";
                foreach (Button b in Controls.OfType<Button>().Where(x => x.Name != "restart"))
                {
                    b.Enabled = false;
                }



            }

            if(copyCurrent.Equals(currentWord))
            {
                lblResult.Text = "You Win!!";
            }
        }

        private void FrmHangman_Load(object sender, EventArgs e)
        {
            loadWords();
            setUpWordChoice();
        }

        private void restart_Click(object sender, EventArgs e)
        {
            setUpWordChoice();
            lblResult.Text = "";

            
            foreach (Button b in Controls.OfType<Button>()) 
            {
                b.Enabled = true;
            }
        }
    }
}
