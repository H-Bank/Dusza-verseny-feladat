using System;
using System.IO;

namespace dusza
{
    class megnezo
    {
        private string fullpath;
        private string[] filePaths;
        private string[] nov = { "1" };
        private int novi = 1;
        private int x = 0;
        private string szulo = "";
        private string szerzo = "";
        private string comment = "";
        public void kezdo()
        {
            try
            {
                string[] pasz = File.ReadAllLines("head.txt");
                novi = Convert.ToInt32(pasz[0]);
            }
            catch
            {

            }
            Console.Write("Kérem a mappa nevét:");
            string mappaneve = Console.ReadLine();
            Console.Write("\nKérek egy felhaszánló nevet : ");
             szerzo = Console.ReadLine();
            Console.Write("\nKérem a kommentet : ");
            comment = Console.ReadLine();
            fullpath = Path.GetFullPath(mappaneve);
            /*DirectoryInfo megnezo = new DirectoryInfo(fullpath);
            Console.WriteLine(megnezo.CreationTime);*/
             filePaths = Directory.GetFiles(fullpath, "*.*", SearchOption.TopDirectoryOnly);
            /*for(int i = 0; i< filePaths.Length; i++)
            {
                 FileInfo megnez = new FileInfo(filePaths[i]);
                Console.Write(megnez.CreationTime); Console.WriteLine(megnez.Name);
            }*/
            x = filePaths.Length;
        }
        public void mappaletrehozas()
        {
            string hova = @"adatok";
            string alap = ".dusza";
            string lh = System.IO.Path.Combine(hova, alap);
            System.IO.Directory.CreateDirectory(lh);
        }
        public void commitletrehozasa()
        {
           string  hova = @"adatok\.dusza";
            string lh = System.IO.Path.Combine(hova,  (novi-1)+"commit");
            System.IO.Directory.CreateDirectory(lh);
        }
        public void hedesz()
        {
            System.IO.File.WriteAllLines(@"adatok/.dusza/head.txt", nov);
            novi = novi + 1;
            nov[0] = Convert.ToString(novi);

            System.IO.File.WriteAllLines("head.txt", nov);
        }
        public void letrehozas()
        {
            string[] szoveg = new string[4 + x];
            szoveg[0] = "Szulo:" + szulo;
            szoveg[1] = "Szerzo:" + szerzo;
            szoveg[2] = "Comment:" + comment;
            szoveg[3] = "Valtozo:";
            if (x != 0)
            {
                for (int a = 0; a < x; a++)
                {
                    FileInfo megnez = new FileInfo(filePaths[a]);
                    szoveg[4 + a] =megnez.CreationTime +" " + megnez.Name; // ide kerülnek a fájl nevek
                    
                    fullpath = @"adatok/.dusza/"+ (novi-1)+"commit/" + megnez.Name;
                    File.Copy(filePaths[a], fullpath);
                }
            }

            System.IO.File.WriteAllLines(@"adatok/.dusza/"+ (novi-1) +"commit/commit.details.txt", szoveg);
        }
        public void kilepes(ref bool kilepes)
        {
            
            /*if (novi == 5)
                kilepes = true;*/
            Console.WriteLine("Kérem kilépéshez nyomjon egy 'i' betűt!");
            ConsoleKeyInfo gomb = new ConsoleKeyInfo();
            gomb = Console.ReadKey(true);
            if (gomb.Key == ConsoleKey.I)
            {
                kilepes = true;
            }


        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            bool kilepes = false;
            megnezo asd = new megnezo();
            asd.mappaletrehozas();
            while (kilepes == false)
            {
                asd.kezdo();
                asd.hedesz();
                asd.commitletrehozasa();
                asd.letrehozas();
                asd.kilepes(ref kilepes);
            } 
        }
    }
}
