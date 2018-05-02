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
                try
                {
                    text = File.ReadAllText(args[0]);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Bohužel jste zadali neexistující soubor, zkontrolujte si prosím název. Zadali jste:  {args[0]}");
                    Environment.Exit(-1);
                }

            }

            int celkovyPocet = 0;
            int pismenZAbecedy = 0;
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
            /// Deklarace proměnné kompletPocetZnaků a nastavení její velikost na délku textového řetězce včetně mezer
            int kompletPocetZnaku = text.Length;
            ///Pokud není zadán textový soubor jako vstup, snížíme celkový počet znaků o 1, abychom nepočítali ukončovací znak #
            if (args.Length == 0) kompletPocetZnaku = text.Length - 1;


            for (int i = 0; i < kompletPocetZnaku; i++)
            {
                ///kontrola jestli se jedná o písmena od A - Z v DEC tvaru
                if (znaky[i] >= 'A' && znaky[i] <= 'Z')
                {
                    ///Hledání, jestli vytvořený list již obsahuje aktuální písmeno
                    Pismeno result = listP.Find(x => x.ch == znaky[i]);

                    ///Pokud písmeno není obsaženo
                    if (result == null)
                    {
                        ///Do listu se přidá nový objekt s atributy aktuálního znaku a aktuální počet se nastaví na 1
                        listP.Add(new Pismeno(znaky[i]));

                        pismenZAbecedy++;
                    }

                    else
                    {
                        ///Pokud list již aktuální písmeno obsahuje, zvýšíme pouze jeho počet o 1
                        listP.Find(x => x.ch == znaky[i]).pocet++;
                        pismenZAbecedy++;
                    }
                }
                ///Kontrola, jestli znak není mezera, jestli není, zvýší se celkový počet znaků o 1
                if (znaky[i] != ' ')
                {
                    celkovyPocet++;
                }

            }

            if (listP.Count > 0)
            {

                listP = listP.OrderByDescending(x => x.pocet).ToList();

                foreach (var item in listP)
                {

                    Console.WriteLine($"Pismeno {item.ch} bylo pouzito {item.pocet} průměrná četnost znaků z abecedy {(item.pocet / (float)pismenZAbecedy) * 100} %");
                }
                int pocetZnakuVListu = listP.Count;
                Console.WriteLine("Celkový počet použítých znaků {0}", celkovyPocet);
                Console.WriteLine($"Průměrné využití znaků z abecedy bylo: {pocetZnakuVListu / (float)26 * 100} %");
                Console.WriteLine($"Nejméně použitý znak: {listP[0].ch} s poctem znaku : {listP[0].pocet}");
                Console.WriteLine($"Nejvíce použitý znak: {listP[pocetZnakuVListu - 1].ch} s poctem znaku: {listP[pocetZnakuVListu - 1].pocet}");
            }
            else
            {
                Console.WriteLine("Celkový počet použítých znaků {0}", celkovyPocet);
                Console.WriteLine("Nebyl však použit žádný znak z abecedy (Bez diakritiky)");
            }

            Console.ReadLine();
        }



        /// Deklarace třídy Pismeno
        class Pismeno
        {
            /// <summary>
            /// Vytvoření konstruktoru pro třídu Pismeno s 1 argumentem
            /// </summary>
            /// <param name="ch"> Argument, kterým nastavujeme proměnnou ch na znak, 
            /// který chceme evidovat/param>

            public Pismeno(char ch)
            {
                this.ch = ch;
            }
            /// <summary>
            /// Property ve které se ukládá znak
            /// </summary>
            public char ch { get; set; }
            /// <summary>
            /// Property, ve které se ukládá kolikrát byl znak použit, při vytvoření nového objektu je automaticky nastaven na 1
            /// </summary>
            public int pocet { get; set; } = 1;
        }


    }
}
