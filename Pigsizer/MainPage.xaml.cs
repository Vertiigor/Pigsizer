using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Media.SpeechSynthesis;

namespace Pigsizer
{
    public sealed partial class MainPage : Page
    {
        private const string vowels = "aeiouAEIOU";
        private const string consonants = "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ";

        public MainPage()
        {
            this.InitializeComponent();
        }

        // it just works :)
        private static string PigIt(string str)
        {
            var words = str.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != string.Empty && char.IsLetter(words[i][0]))
                {
                    if (vowels.Contains(words[i][0])) // if a word begins with a vowel
                    {
                        words[i] += "yay";
                    }
                    else if (consonants.Contains(words[i][0])) //if a word begins with a consonant
                    {
                        var end = string.Empty; // consonants before the first vowel to be placed at the end of the word + "yay"

                        for (int j = 0; j < words[i].Length; j++)
                        {
                            if (consonants.Contains(words[i][j]))
                            {
                                end += char.ToLower(words[i][j]);
                            }
                            else if (vowels.Contains(words[i][j]))
                            {
                                words[i] = words[i].Substring(j, words[i].Length - j) + end + "ay";
                                break;
                            }
                        }
                    }
                }
            }

            return string.Join(" ", words);
        }

        private void OriginalText_TextChanged(object sender, TextChangedEventArgs e)
        {
            PigText.Text = PigIt(OriginalText.Text);
        }

        private async void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            var speechText = OriginalTextRadio.IsChecked == true ? OriginalText.Text : PigText.Text;

            if (speechText != string.Empty)
            {
                var speechSynth = new SpeechSynthesizer();

                var speechStream = await speechSynth.SynthesizeTextToStreamAsync(speechText);

                mediaElement.AutoPlay = true;
                mediaElement.SetSource(speechStream, speechStream.ContentType);
                mediaElement.Play();
            }
        }
    }
}
