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
            int PocetZnakuVAbecede = 26;
            int celkovypocetznaku = 0;
            string fileName = "text.txt";

            if (args.Length > 0)
            {
                fileName = @"" + args[0];
            }



            if (args.Length == 0)
            {
                StreamWriter sw = File.CreateText(fileName);
                sw.Dispose();
                sw.Close();
            }

            StreamReader reader = new StreamReader(fileName);

            char ch = ' ';
            string text = "";
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
            text = text.ToLower();

            int pozice = 0;
            string temp = null;
            bool run = true;
            Pismeno d = new Pismeno(' ');
            while (run)
            {

                if (args.Length > 0 || pozice == 0)
                {

                    ch = (char)reader.Read();
                    temp = ch.ToString();
                    temp = temp.ToLower();
                    ch = temp[0];
                }
                else
                {
                    ch = text[pozice];
                }
                d.ch = ch;

                Pismeno result = listP.Find(x => x.ch == d.ch);


                if (result == null)
                {
                    if ((int)ch >= 97 && (int)ch <= 122)
                    {
                        listP.Add(new Pismeno(ch, 1));
                        celkovyPocet++;

                    }

                }
                else
                {
                    celkovyPocet++;
                    listP.Find(x => x.ch == d.ch).pocet++;
                }


                if (reader.EndOfStream && args.Length > 0)
                {
                    run = false;
                }

                if (ch == '#' && args.Length == 0)
                {
                    run = false;
                }

                pozice++;
                if (ch != ' ' && ch != '#')
                {
                    celkovypocetznaku++;
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
                Console.WriteLine("Celkový počet použítých znaků {0}", celkovypocetznaku);
                Console.WriteLine($"Průměrné využití znaků z abecedy bylo: {pocetZnakuVListu / (float)PocetZnakuVAbecede * 100} %");
                Console.WriteLine($"Nejméně použitý znak: {listP[0].ch} s poctem znaku : {listP[0].pocet}");
                Console.WriteLine($"Nejvíce použitý znak: {listP[pocetZnakuVListu - 1].ch} s poctem znaku: {listP[pocetZnakuVListu - 1].pocet}");
            }
            else
            {
                Console.WriteLine("Nebyl použit žádný znak z abecedy");
            }
            reader.Dispose();
            reader.Close();
            if (args.Length == 0) File.Delete(fileName);

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
