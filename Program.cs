using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StarEnigma
{
    class Program
    {
        static void Main(string[] args)
        {           
            int n = int.Parse(Console.ReadLine()); //read the lines of reading
            string patternLetters = @"[starSTAR]";  //pattern to catch all letters caseInsensitive
            Regex regex = new Regex(patternLetters);

            string searchPattern = @"^[^@\-!:>]*@(?<name>[A-Za-z]+)[^@\-!:>]*\:(?<population>\d+)[^@\-!:>]*!(?<type>[AD])![^@\-!:>]*->(?<soldiers>\d+)[^@\-!:>]*$";
            Regex validLine = new Regex(searchPattern);

            List<string> attackedPlanets = new List<string>();
            List<string> destroyedPlanets = new List<string>();

            for (int i = 0; i < n; i++)  //iterate line 
            {
                string message = Console.ReadLine();                

                MatchCollection matches = regex.Matches(message);
                int countLetters = matches.Count;

                string changedMessage = DecreaseLettersMessage(countLetters, message);                         

                Match matchLine = validLine.Match(changedMessage);

                if (matchLine.Success == false)
                {
                    continue;
                }

                string planetName = matchLine.Groups["name"].Value;                
                char typeAttack = (char.Parse)(matchLine.Groups["type"].Value);               

                if (typeAttack == 'A')
                {
                    attackedPlanets.Add(planetName);                   
                }
                else //(typeAttack == 'B')
                {
                    destroyedPlanets.Add(planetName);                  
                }               
            }

            Console.WriteLine($"Attacked planets: {attackedPlanets.Count}");

            PrintAttackedPlanets(attackedPlanets);

            Console.WriteLine($"Destroyed planets: {destroyedPlanets.Count}");

            PrintDestroyedPlanets(destroyedPlanets);
        }

        private static void PrintDestroyedPlanets(List<string> destroyedPlanets)
        {
            List<string> sortedDestroyedPlanets = destroyedPlanets
                .OrderBy(x => x)
                .ToList();

            foreach (var planet in sortedDestroyedPlanets)
            {
                Console.WriteLine($"-> {planet}");
            }
        }
        
        private static void PrintAttackedPlanets(List<string> attackedPlanets)
        {
            List <string> sortedAttackedPlanets = attackedPlanets
                .OrderBy(x => x)
                .ToList();

            foreach (var planet in sortedAttackedPlanets)
            {
                Console.WriteLine($"-> {planet}");
            }
        }

        private static string DecreaseLettersMessage(int count, string text)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
               char currChar = (char)(text[i] - count);
               sb.Append(currChar);
            }

            return sb.ToString();
        }
    }
}
