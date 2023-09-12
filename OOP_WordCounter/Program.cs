using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OOP_WordCounter
{
    internal class Program
    {
        static List<string> lines = new List<string>();
        static Dictionary<string, int> breakdown = new Dictionary<string, int>();
        static Dictionary<string, int> sortedBD = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            //string fileName = "Jupiter.txt";
            string fileName = "Odin.txt";

            //int num = 0;
            string outputFile = "";

            // create output file name
            outputFile = fileName.Split('.')[0] + "_WordCount.txt";

            readFile(fileName);
            countFile(out int num);
            sortFile();
            createOutput(fileName, outputFile, num);
        }

        static void readFile(string fileName)
        {
            Console.Write("Begin Reading...");

            string line = "";
            using (StreamReader sr = new StreamReader(fileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length > 0)
                        lines.Add(line);
                }
            }

            Console.WriteLine("Done!");
        }

        static void countFile(out int num)
        {
            string[] words = new string[] { };
            char[] letters = { };
            int wordCount = 0;

            Console.Write("Begin Counting...");

            foreach (string l in lines)
            {
                words = l.Split(' ');
                for (int x = 0; x < words.Length; x++)
                //foreach(string word in words)
                {
                    if (words[x].Length > 0)
                    {
                        // special character filter
                        if (words[x].Length > 1)
                        {
                            letters = words[x].ToCharArray();
                            if ((int)letters[letters.Length - 1] == 33 // !
                                || (int)letters[letters.Length - 1] == 44 // ,
                                || (int)letters[letters.Length - 1] == 46 // .
                                || (int)letters[letters.Length - 1] == 63 // ?
                                )
                            {
                                words[x] = "";
                                for (int y = 0; y < letters.Length - 1; y++)
                                {
                                    words[x] += letters[y];
                                }
                            }
                        }


                        if (breakdown.ContainsKey(words[x].ToLower()))
                            breakdown[words[x].ToLower()] += 1;
                        else
                            breakdown[words[x].ToLower()] = 1;

                        wordCount++;
                    }
                }
            }

            num = wordCount;
            Console.WriteLine("Done!");
        }

        static void sortFile()
        {
            int leastCount = 0;
            string sortKey = "";

            Console.Write("Begin Sorting...");

            while (breakdown.Count > 0)
            {
                leastCount = 0;

                foreach (KeyValuePair<string, int> kvp in breakdown)
                {
                    if (leastCount < kvp.Value)
                    {
                        leastCount = kvp.Value;
                        sortKey = kvp.Key;
                    }
                }

                sortedBD[sortKey] = leastCount;
                breakdown.Remove(sortKey);
            }
            Console.WriteLine("Done!");
        }

        static void createOutput(string fileName, string outputFile, int wordCount)
        {
            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                sw.WriteLine("Word count of {0}.", fileName);
                sw.WriteLine("Total Wordcount is {0}.", wordCount);
                foreach (KeyValuePair<string, int> kvp in sortedBD)
                {
                    sw.WriteLine("{0}-{1}", kvp.Key, kvp.Value);
                }
            }
            Console.WriteLine("Done!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}