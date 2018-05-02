using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CetnostZnakuProjekt
{
    class Program
    {

        static void Main(string[] args)
        {
            string text = "";
            if (args.Length > 0)
            {
                text = File.ReadAllText("text.txt");
            }

            char ch = ' ';
            int celkovyPocet = 0;
            List<Pismeno> listP = new List<Pismeno>();

            if (args.Length == 0)
            {
                Console.WriteLine("Nebyl zadán parametr, zadej text ke kontrole: (K ukončení pouzij # na konci)");
                while (!text.EndsWith("#"))
                {
                    text += Console.ReadLine();
                }
            }
            text = text.ToUpper();
            char[] znaky = text.ToArray();
            int kompletPocetZnaku = text.Length;
            if (args.Length == 0) kompletPocetZnaku = text.Length - 1;


            Pismeno d = new Pismeno(' ');

            for (int i = 0; i < kompletPocetZnaku; i++)
            {
                d.ch = znaky[i];

                if ((int)znaky[i] >= 65 && (int)znaky[i] <= 90)
                {
                    Pismeno result = listP.Find(x => x.ch == d.ch);

                    if (result == null)
                    {
                        listP.Add(new Pismeno(znaky[i], 1));
                        celkovyPocet++;
                    }
                    else
                    {
                        celkovyPocet++;
                        listP.Find(x => x.ch == d.ch).pocet++;
                    }

                }

            }

            if (listP.Count > 0)
            {

                listP = listP.OrderBy(x => x.pocet).ToList();

                foreach (var item in listP)
                {

                    Console.WriteLine($"Pismeno {item.ch} bylo pouzito {item.pocet} průměrná četnost znaku {(item.pocet / (float)celkovyPocet) * 100} %");
                }
                int pocetZnakuVListu = listP.Count;
                Console.WriteLine("Celkový počet použítých znaků {0}", celkovyPocet);
                Console.WriteLine($"Průměrné využití znaků z abecedy bylo: {pocetZnakuVListu / (float)26 * 100} %");
                Console.WriteLine($"Nejméně použitý znak: {listP[0].ch} s poctem znaku : {listP[0].pocet}");
                Console.WriteLine($"Nejvíce použitý znak: {listP[pocetZnakuVListu - 1].ch} s poctem znaku: {listP[pocetZnakuVListu - 1].pocet}");
            }
            else
            {
                Console.WriteLine("Nebyl použit žádný znak z abecedy");
            }



            Console.ReadLine();
        }



        class Pismeno
        {
            public Pismeno(int pocet)
            {
                this.pocet = pocet;
            }

            public Pismeno(char ch)
            {
                this.ch = ch;
            }

            public Pismeno(char ch, int i)
            {
                this.ch = ch;
                pocet = i;
            }

            public char ch { get; set; }
            public int pocet { get; set; }


        }


    }
}
