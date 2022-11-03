using System.Linq;

namespace Tests
{
    internal class Program
    {
        //static List<char> vowels = new List<char>() { 'a', 'e', 'i', 'o', 'u' };
        //static List<char> consonants = new List<char>() { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };

        static string vowels = "aeiou";
        static string consonants = "bcdfghjklmnpqrstvwxyz";

        private static string PigIt(string str)
        {
            var words = str.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (char.IsLetter(words[i][0]))
                {
                    if(vowels.Contains(char.ToLower(words[i][0]))) // if a word begins with a vowel
                    {
                        words[i] += "yay";
                    }
                    else if (consonants.Contains(char.ToLower(words[i][0]))) //if a word begins with a consonant
                    {
                        var end = string.Empty; // consonants before the first vowel, and put to the end of word + "yay"

                        for (int j = 0; j < words[i].Length; j++)
                        {
                            if (consonants.Contains(char.ToLower(words[i][j])))
                            {
                                end += words[i][j];
                            }
                            else if (vowels.Contains(char.ToLower(words[i][j])))
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

        static void Main(string[] args)
        {
            Console.WriteLine(PigIt("Can You help me ?"));
        }
    }
}