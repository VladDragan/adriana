﻿using SpeechEmotionRecognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognitionRunner
{
    class Program
    {
        static bool completed;
        public static SpeechEmotionRecognitionEngine ser;
        static void Main(string[] args)
        {
            using (SpeechRecognitionEngine recognizer =
          new SpeechRecognitionEngine())
            {

                // Create and load a grammar.
                Grammar dictation = new DictationGrammar();
                dictation.Name = "Dictation Grammar";

                recognizer.LoadGrammar(dictation);

                // Configure the input to the recognizer.
                recognizer.SetInputToWaveFile(@"E:\audio\red.wav");

                // Attach event handlers for the results of recognition.
                recognizer.SpeechRecognized +=
                  new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
                recognizer.RecognizeCompleted +=
                  new EventHandler<RecognizeCompletedEventArgs>(recognizer_RecognizeCompleted);

                // Perform recognition on the entire file.
                Console.WriteLine("Starting asynchronous recognition...");
                completed = false;
                recognizer.RecognizeAsync();

                // Keep the console window open.
                while (!completed)
                {
                    Console.ReadLine();
                }
                Console.WriteLine("Done.");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        public static void Save_Audio(object sender, RecognizeCompletedEventArgs e)
        {
            ser = new SpeechEmotionRecognitionEngine();
            ser.hereIsAudio(e.Result.Audio);
        }

        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null && e.Result.Text != null)
            {
                Console.WriteLine("  Recognized text =  {0}", e.Result.Text);
                ser = new SpeechEmotionRecognitionEngine();
                ser.hereIsAudio(e.Result.Audio);
            }
            else
            {
                Console.WriteLine("  Recognized text not available.");
            }
        }

        // Handle the RecognizeCompleted event.
        static void recognizer_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine("  Error encountered, {0}: {1}",
                e.Error.GetType().Name, e.Error.Message);
            }
            if (e.Cancelled)
            {
                Console.WriteLine("  Operation cancelled.");
            }
            if (e.InputStreamEnded)
            {
                Console.WriteLine("  End of stream encountered.");
            }
            completed = true;
        }
    }
}