using System;
using System.Collections.Generic;
using System.Linq;

namespace AWE_Wetterfrosch
{
    class Program
    {
        static void Main(string[] args)
        { /// begin inside of main
          /// declare a multidimensional array for [timestamp, temp] value pairs
            int arrayLen = 1440; /// # Minuten im Tag
            int[,] wetterDaten = new int [arrayLen, 2];
          /// fill array with one randomized value for each minute, using random.next()
            Random zufall = new Random();
            for (int i = 0; i < arrayLen; i++)
            { /// begin value-creation for loop
                int wert = zufall.Next(-5, 40); /// Minutewert erstellen
                wetterDaten[i, 0] = 0000 + i; /// Zeiterfassung eintragen
                wetterDaten[i, 1] = wert; /// Minutewert eintragen
                int stunde = wetterDaten[i, 0] / 60;
                int minute = wetterDaten[i, 0] % 60;
                Console.WriteLine("{0}:{1} - {2}°C", stunde, minute, wetterDaten[i, 1]);
            } /// end value-creation for loop

            /// Here the non-calculated/simple values
            int minimalwert = 0;
            int maximalwert = 0;
            int wertSumme = 0;
            for (int i = 0; i < arrayLen; i++)
            { /// begin zusatzaufgabenschliefe
                if (wetterDaten[i, 1] < minimalwert) { minimalwert = wetterDaten[i, 1]; }
                if (wetterDaten[i, 1] > maximalwert) { maximalwert = wetterDaten[i, 1]; }
                wertSumme = wertSumme + wetterDaten[i, 1];
            } /// end zusatzaufgabenschliefe


            /// calculated values
            int mittelwert = wertSumme / wetterDaten.Length;
            int spannweite = maximalwert - minimalwert;
            
            /// calculate medianwert
            int[] sortedWetter = new int[arrayLen];
            for (int i = 0; i < arrayLen; i++)
            {
                sortedWetter[i] = wetterDaten[i, 1];
            }
            Array.Sort(sortedWetter);
            int median;
            if (sortedWetter.Length % 2 == 0)
                median = ((int)sortedWetter[sortedWetter.Length / 2] + (int)sortedWetter[sortedWetter.Length / 2 - 1]) / 2;
            else
                median = (int)sortedWetter[sortedWetter.Length / 2];

            /// calculate spannweite
            int spannWeite = maximalwert - minimalwert - 1;

            /// calculate abweichung
            int abweichung = (wertSumme - (mittelwert * arrayLen)) / arrayLen;

            ///calculate rangelist
            Dictionary<int, int> frequency = new Dictionary<int, int>();
            for (int i = 0; i < arrayLen; i++)
            {
                if (frequency.ContainsKey(wetterDaten[i, 1]))
                {
                    frequency[wetterDaten[i, 1]]++;
                }
                else
                {
                    frequency.Add(wetterDaten[i, 1], 1);
                }
            }
                /// Rangelist can't be an int, needs to be distance between min
                /// and max+2nd row, so 2d array with rows = distance+1
                ///int distanz = maximalwert - minimalwert + 1;
                ///int[,] rangeListe = new int[distanz, 2];
                /** 
                 * for the standard deviation, we have two options.
                 * 1 - hardcode the length of the array we'll use for tracking
                 * 2 - run a second loop after we know the min/max of temps
                **/

                Console.WriteLine("Temperaturdaten für heute:");
            Console.WriteLine("Minimalwert: {0}", minimalwert);
            Console.WriteLine("Maximalwert: {0}", maximalwert);
            Console.WriteLine("Medianwert: {0}", median);
            Console.WriteLine("Spannweite: {0}", spannweite);
            Console.WriteLine("Abweichung: {0}", abweichung);
            /// - output rangelist
            Console.WriteLine("Häufigkeiten":);
            Console.WriteLine();
            foreach (var item in frequency.OrderBy(i => i.Key))
            {
                Console.WriteLine(item);
            }
            Console.ReadKey(); /// don't close program while people are reading.

        } //end inside of main
    } ///end program class
} /// end namespace
