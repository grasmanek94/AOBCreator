using System;
using System.IO;

namespace AOBCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            int bufSize = 1024 * 64;
            Stream inStream = Console.OpenStandardInput(bufSize);
            Console.SetIn(new StreamReader(inStream, Console.InputEncoding, false, bufSize));

            Console.Write("First input: ");
            string first = Console.ReadLine().Trim();

            while (true)
            {
                Console.Write("Next input: ");
                string second = Console.ReadLine().Trim();

                string[] fxp = first.Split(' ');
                string[] sxp = second.Split(' ');

                if (fxp.Length != sxp.Length)
                {
                    Console.WriteLine("Bad length: " + fxp.Length.ToString() + " / " + sxp.Length.ToString());
                    Console.ReadKey();
                    return;
                }

                string res = "";
                int fixups = 0;
                for (int i = 0; i < fxp.Length; ++i)
                {
                    string left = fxp[i].ToLower();
                    string right = sxp[i].ToLower();
                    if (left == right)
                    {
                        res += fxp[i] + " ";
                    }
                    else
                    {
                        if (left != "??" && right != "??")
                        {
                            ++fixups;
                        }
                        res += "?? ";
                    }
                }

                Console.WriteLine("Result: ");
                Console.WriteLine(res);
                Console.WriteLine("Bytes changed: " + fixups.ToString() + "/" + fxp.Length.ToString());
            }
        }
    }
}
