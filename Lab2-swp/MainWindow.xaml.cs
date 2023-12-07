using System;
using System.Windows;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using System.Globalization;
using System.Windows.Controls;

namespace Lab2_swp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static SpeechSynthesizer ss = new SpeechSynthesizer();
        static SpeechRecognitionEngine sre;
        static bool done = false;
        public MainWindow()
        {
            InitializeComponent();
            ss = new SpeechSynthesizer();
            ss.SetOutputToDefaultAudioDevice();
            ss.Speak("Witam w kaluklatorze");
            CultureInfo ci = new CultureInfo("pl-PL");
            sre = new SpeechRecognitionEngine(ci);
            sre.SetInputToDefaultAudioDevice();
            sre.SpeechRecognized += Sre_SpeechRecognized;
            Grammar grammar = new Grammar(".\\Grammars\\Grammar1.xml", "rootRule")
            {
                Enabled = true
            };
            sre.LoadGrammar(grammar);
            sre.RecognizeAsync(RecognizeMode.Multiple);        
        }

        private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string txt = e.Result.Text;
            float confidence = e.Result.Confidence;        
            lbl1.Text = txt;
            lbl2.Text = (Math.Round(confidence, 4) * 100).ToString() + " %";

            if (confidence >= 0.5)
            {
                int first = Convert.ToInt32(e.Result.Semantics["first"].Value);
                int second = Convert.ToInt32(e.Result.Semantics["second"].Value);
                string operation = e.Result.Semantics["operation"].Value.ToString();    

                if(operation == "suma")
                {
                    int sum = first + second;
                    if (sum < 0)
                    {
                        ss.Speak("Wynik twojego dodawania to minus" + sum);
                    }
                    else if(sum == 0)
                    {
                        ss.Speak("Wynik twojego dodawania to zero");
                    }
                    else
                    {
                        ss.Speak("Wynik twojego dodawania to " + sum);
                    }
                    
                    Wynik.Text = "Wynik dodawania to " + sum;
                }

                if (operation == "roznica")
                {
                    int sum = first - second;
                    if (sum < 0)
                    {
                        ss.Speak("Wynik twojego odejmowania to minus" + sum);
                    }
                    else if (sum == 0)
                    {
                        ss.Speak("Wynik twojego odejmowania to zero");
                    }
                    else
                    {
                        ss.Speak("Wynik twojego odejmowania to " + sum);
                    }
                    
                    Wynik.Text = "Wynik odejmowania to " + sum;
                }

                if (operation == "iloczyn")
                {
                    int sum = first * second;
                    if (sum < 0)
                    {
                        ss.Speak("Wynik twojego mnożenia to minus" + sum);
                    }
                    else if (sum == 0)
                    {
                        ss.Speak("Wynik twojego mnożenia to zero");
                    }
                    else
                    {
                        ss.Speak("Wynik twojego mnożenia to " + sum);
                    }
                    
                    Wynik.Text = "Wynik mnożenia to " + sum;
                }

                if (operation == "iloraz")
                {
                    if(second == 0)
                    {
                        ss.Speak("Nie da się dzielić przez zero");
                    }
                    else
                    {
                        int sum = first / second;
                        if (sum < 0)
                        {
                            ss.Speak("Wynik twojego dzielenia to minus" + sum);
                        }
                        else if (sum == 0)
                        {
                            ss.Speak("Wynik twojego dzielenia to zero");
                        }
                        else
                        {
                            ss.Speak("Wynik twojego dzielenia to " + sum);
                        }

                        Wynik.Text = "Wynik dzielenia to " + sum;
                    }
                    
                }
            }
            else
            {
                ss.Speak("Proszę powtórzyć");
            }
        }
    }
}
