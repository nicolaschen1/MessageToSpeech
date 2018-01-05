/*
MessageToSpeech
VERSION: 1.0

Input: Message.

Output: Voice.

Description: This software tool allows to move from a written message to a vocal message.

Developer: Nicolas CHEN
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.IO;

namespace MessageToSpeech
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer voice = new SpeechSynthesizer();

        public Form1()
        {
            InitializeComponent();
        }

        /* METHODS */
        private void Play()
        {
            if (String.IsNullOrEmpty(YourMessage.Text))
            {
                MessageBox.Show("No message. Please write a message.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            else
            {
                voice.SpeakAsync(YourMessage.Text);
            }
        }

        private void Pause()
        {
            if (!(String.IsNullOrEmpty(YourMessage.Text)))
            {
                try
                {
                    voice.Pause();

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Message.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                
        }

        private void Resume()
        {

            if (!(String.IsNullOrEmpty(YourMessage.Text)))
            {
                try
                {
                    voice.Resume();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Message.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                
        }

        private void Save()
        {

            if (!(String.IsNullOrEmpty(YourMessage.Text)))
            {
                try
                {
                    using (SaveFileDialog saveFile = new SaveFileDialog())
                    {
                        saveFile.Filter = "wav files|*.wav";
                        saveFile.Title = "Save to a wave file";
                        if (saveFile.ShowDialog() == DialogResult.OK)
                        {
                            FileStream fStream = new FileStream(saveFile.FileName, FileMode.Create, FileAccess.Write);
                            voice.SetOutputToWaveStream(fStream);
                            voice.Speak(YourMessage.Text);
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Message.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        public void MessageBoxMultiLines(IEnumerable<string> lines)
        {
            var instructionLine = new StringBuilder();
            bool firstLine = false;
            foreach (string line in lines)
            {
                if (firstLine)
                    instructionLine.Append(Environment.NewLine);

                instructionLine.Append(line);
                firstLine = true;
            }
            MessageBox.Show(instructionLine.ToString(), "Information");
        }

        /* Menu */
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] instructions = new string[]
            {
                "Welcome to MessageToSpeech"
                , ""
                , "This software tool aims to vocalize the text you type."
                , ""
                , "5 modes: PLAY, PAUSE, RESUME, SAVE (*.wav) and Clear the message."
            };

            MessageBoxMultiLines(instructions);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] about = new string[]
            {
                "MessageToSpeech"
                , ""
                , "VERSION: 1.0"
                , ""
                , "Developed by Nicolas Chen"
            };

            MessageBoxMultiLines(about);
        }

        /* BUTTON */
        private void PlayButton_Click(object sender, EventArgs e)
        {
            Play();
        }        

        private void PauseButton_Click(object sender, EventArgs e)
        {
            try
            {
                voice.Pause();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            Resume();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resume();
        }

        private void savewavToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void YourMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }

        private void ClearMessageButton_Click(object sender, EventArgs e)
        {
            YourMessage.Text = String.Empty;
        }
    }
}
