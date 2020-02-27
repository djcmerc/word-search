/* Group Leader: Derrick Mercado
                 Mark Lawrence Galvan
                 Kevin Mendoza
   PA 3 - Word Search, Part Deux
   Date: 9/14/2016
   Description: This program enables the user to choose from different tools for solving
                search problems that involve words such as listing all words, rhyming words,
                scrabble words, morph words, and morph chains.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PA3
{
    /// <summary>
    /// Word Search Tools
    /// </summary>
    class Program
    {
        /// <summary>
        /// Prints all words, rhyming, scrabble, and morph words read in from a text file
        /// </summary>
        static void Main(string[] args)
        {
            //allocates space for the text file
            string[] words = new string[25000];

            // Reads the text file and puts it in the words array
            ReadText(words);

            Console.WriteLine("Welcome to Word Search Tools!");

            // Asks the user what they want to do in the word search tool program
            Choices(words);

        }

        // Reads the text file 
        static void ReadText(string[] words)
        {
            int i = 0;
            string line;

            // Reads in words from the text file and arranges them into a string array
            StreamReader myFile = new StreamReader("WordList.txt");
            while ((line = myFile.ReadLine()) != null)
            {
                words[i] = line;
                i++;
            }
            myFile.Close();
        }

        // Function to implement the choices
        static void Choices(string[] words)
        {
            int choice = 0;

            // Do-while loop will enable the user go back from the main menu
            do
            {
                DisplayMenu();
                Console.Write("\nPlease enter your choice (1-5): ");
                choice = int.Parse(Console.ReadLine());

                // Switch statement will determine which method to run
                switch (choice)
                {
                    case 1:
                        AllWords(words);
                        break;
                    case 2:
                        RhymingWords(words);
                        break;
                    case 3:
                        ScrabbleWords(words);
                        break;
                    case 4:
                        MorphWords(words);
                        break;
                    case 5:
                        MorphChain(words);
                        break;
                    case 6:
                        Quit();
                        break;
                    default:
                        {
                            Console.WriteLine("ERROR! Please enter a valid choice.");
                            Console.Clear();
                            break;
                        }
                }
            }
            while (choice != 6);
        }

        // DisplayMenu method will display the menu to the user
        static void DisplayMenu()
        {
            Console.WriteLine("\nWhat would you like to display?\n");
            Console.WriteLine("1. All words");
            Console.WriteLine("2. Rhyming Words");
            Console.WriteLine("3. Scrabble words");
            Console.WriteLine("4. Morph words");
            Console.WriteLine("5. Morph chain");
            Console.WriteLine("6. Quit");
        }

        // AllWords method will print all the words contained in the WordsList.txt text file
        static void AllWords(string[] words)
        {
            Console.WriteLine("\nYour choice: 1");
            int i = 0;

            // While loop will read the text file until it reaches the last word
            while (words[i] != null)
            {
                Console.WriteLine("{0}", words[i]);
                i++;
            }

            Console.Write("\nPress any key to clear the screen...");
            Console.ReadKey();
            Console.Clear();
        }

        // RhymingWords will print the rhyming words based on the user-inputted string
        static void RhymingWords(string[] words)
        {
            Console.WriteLine("\nYour choice: 2");
            Console.Write("\nPlease enter a desired ending string for your rhyming words: ");
            string endString = Console.ReadLine();
            int numRhyming = 0,
                    i = 0;
            string line;

            // while loop with nested if statement will evaluate the ending letters of the inputted string
            while (words[i] != null)
            {
                line = words[i];
                if (line.EndsWith(endString) == true)
                {
                    Console.WriteLine("{0}", line);
                    numRhyming++;
                }
                i++;
            }
            if (numRhyming == 0)
                Console.WriteLine("No words rhyme with that ending string.");
            Console.Write("\nPress any key to clear the screen. ");
            Console.ReadKey();
            Console.Clear();
        }

        // ScrabbleWords method will print words that can be made of letters based on the word inputted by the user
        static void ScrabbleWords(string[] words)
        {
            Console.WriteLine("\nYour choice: 3\n");
            string scrabbleLetters, tempWord, tempScrabble;
            char[] replace;
            int i = 0,
                j = 0,
                k = 0,
                numMatches = 0,
                numScrabbleWords = 0;

            // Prompts the user to input random letters and loops back if less than 3 or more than 7 are inputted
            do
            {
                Console.Write("Enter scrabble letters (3-7 letters): ");
                scrabbleLetters = Console.ReadLine();
                if (scrabbleLetters.Length < 3 || scrabbleLetters.Length > 7)
                    Console.WriteLine("The number of letters must be between 3 and 7. Please try again. ");
            } while (scrabbleLetters.Length < 3 || scrabbleLetters.Length > 7);

            // Parses the entire list of words
            while (words[i] != null)
            {
                numMatches = 0;
                tempWord = words[i];
                tempScrabble = scrabbleLetters;

                // Compares if the current words can be made from the user inputted letters
                for (j = 0; j < tempWord.Length; j++)
                {
                    for (k = 0; k < tempScrabble.Length; k++)
                    {
                        if (tempWord[j] == tempScrabble[k])
                        {
                            replace = tempScrabble.ToCharArray();
                            replace[k] = ' ';
                            tempScrabble = new string(replace);
                            numMatches++;
                            break;
                        }
                    }
                }
                if ((numMatches == tempWord.Length) && (numMatches > 2))
                {
                    Console.WriteLine("{0}", tempWord);
                    numScrabbleWords++;
                }
                i++;
            }
            if (numScrabbleWords == 0)
                Console.WriteLine("There are no words that can be made from those letters. ");
            Console.Write("\nPress any key to clear the screen. ");
            Console.ReadKey();
            Console.Clear();
        }

        // MorphWords method will print words that are different by one letter from the user inputted word 
        static void MorphWords(string[] words)
        {
            Console.WriteLine("\nYour choice: 4\n");
            Console.Write("Enter start word: ");
            string morphWord = Console.ReadLine();
            string tempWord;
            int i = 0, j, numMatches, numMorphWords = 0;

            // Parses the entire list of words
            while (words[i] != null)
            {
                numMatches = 0;
                tempWord = words[i];

                // For loop compares if current word is one letter different from user inputted word
                for (j = 0; j < tempWord.Length; j++)
                {
                    if (tempWord.Length != morphWord.Length)
                        break;
                    else if (tempWord[j] == morphWord[j])
                        numMatches++;
                }

                if (numMatches == ((morphWord.Length) - 1))
                {
                    Console.WriteLine("{0}", tempWord);
                    numMorphWords++;
                }
                i++;
            }
            if (numMorphWords == 0)
                Console.WriteLine("There are no morph words associated with that word. ");
            Console.Write("\nPress any key to clear the screen. ");
            Console.ReadKey();
            Console.Clear();
        }

        static void GetMorphWords(string[] words, List<string> morphWords)
        {
            string tempWord;
            int i ,j, numMatches;

            int n = 0;
            for (int k = 0; k < n; k++)
            {
                Console.WriteLine(morphWords.IndexOf(k));
                morphWords.Add("dirt");
                n++;
            }

            morphWords.Add("dirt");
            // Parses the entire list of words
            foreach (string word in morphWords)
            {
                i = 0;
                while (words[i] != null)
                {
                    numMatches = 0;
                    tempWord = words[i];

                    // For loop compares if current word is one letter different from user inputted word
                    for (j = 0; j < tempWord.Length; j++)
                    {
                        if (tempWord.Length != word.Length)
                            break;
                        else if (tempWord[j] == word[j])
                            numMatches++;
                    }
                    if (numMatches == ((word.Length) - 1))
                    {
                        Console.WriteLine("{0}", tempWord);
                        //morphWords.Add(tempWord);
                    }
                    i++;
                }
            }
        }

        // MorphChain method will print a chain of words that will differ by one letter from the starting word to the end word
        static void MorphChain(string[] words)
        {
            Console.WriteLine("\nYour choice: 5\n");
            Console.Write("Enter start word: ");
            string startWord = Console.ReadLine();
            Console.Write("Enter end word: ");
            string endWord = Console.ReadLine();
            Console.Write("Enter maximum chain length: ");
            int chainLength = int.Parse(Console.ReadLine());
            int i, j , numMorphWords = 0;
            List<string> list = new List<string>();
            list.Add(startWord);
            GetMorphWords(words, list);
            //foreach (string word in list)
            //{
            //    Console.WriteLine("{0}", word);
            //}
            //if (numMorphWords == 0)
            //{
            //    Console.WriteLine("There are no morph words for this start word. ");
            
            //}
        }

        // Quit method will terminate the program
        static void Quit()
        {
            Console.WriteLine("\nYour choice: 5\n");
            Console.Write("Please hit any key to terminate the program...\n");
            Console.ReadKey();
        }
    }
}
