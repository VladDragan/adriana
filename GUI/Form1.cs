using SpeechEmotionRecognition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        bool completed;
        string emotionFile;
        SpeechRecognizedEventArgs element;
        public SpeechEmotionRecognitionEngine ser;
        const string NEUTRAL = "Neutral";
        const string HAPPY = "Happiness";
        const string ANGER = "Anger";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(@"E:\audio\furie.wav");
            simpleSound.Play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SpeechRecognitionEngine recognizer =
         new SpeechRecognitionEngine())
            {

                // Create and load a grammar.
                Grammar dictation = new DictationGrammar();
                dictation.Name = "Dictation Grammar";

                recognizer.LoadGrammar(dictation);
                // Configure the input to the recognizer.
                recognizer.SetInputToWaveFile(emotionFile);

                // Attach event handlers for the results of recognition.
                recognizer.SpeechRecognized +=
                  new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
                recognizer.RecognizeCompleted +=
                  new EventHandler<RecognizeCompletedEventArgs>(recognizer_RecognizeCompleted);

                completed = false;
                recognizer.Recognize();
            }
        }
        public void Save_Audio(object sender, RecognizeCompletedEventArgs e)
        {
            ser = new SpeechEmotionRecognitionEngine();
            ser.hereIsAudio(e.Result.Audio);
        }

        void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null && e.Result.Text != null)
            {
                ser = new SpeechEmotionRecognitionEngine();

                //ser.MyEvent += OnOnChanged_ObjectA;
                var score = ser.hereIsAudio(e.Result.Audio);
                label1.Text = score.ToString();

                progressBar1.Value = Convert.ToInt32(score);
                if (score < 2100)
                {
                    dataGridView1.Rows.Add(emotionFile, label1.Text, NEUTRAL);
                }
                else if (score < 13000)
                {
                    dataGridView1.Rows.Add(emotionFile, label1.Text, ANGER);
                }
                else
                {
                    dataGridView1.Rows.Add(emotionFile, label1.Text, HAPPY);
                }
            }
            else
            {
            }
        }

        // Handle the RecognizeCompleted event.
        void recognizer_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            completed = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(@"E:\audio\simpleText.wav");
            simpleSound.Play();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(@"E:\audio\happy.wav");
            simpleSound.Play();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDial = new OpenFileDialog();
            if (fileDial.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                emotionFile = fileDial.FileName;
                label4.Text = emotionFile;
                button3.Enabled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
