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
                int changes = 0;
                int wildcards = 0;
                int usable = 0;
                int usable_non_00_FF = 0;

                for (int i = 0; i < fxp.Length; ++i)
                {
                    string left = fxp[i].ToLower();
                    string right = sxp[i].ToLower();

                    bool left_wildcard = left == "??" || left == "?";
                    bool right_wildcard = right == "??" || right == "?";

                    if (left_wildcard || right_wildcard)
                    {
                        ++wildcards;
                    }

                    if (left == right)
                    {
                        if (!left_wildcard && !right_wildcard)
                        {
                            ++usable;
                            if(left != "ff" && left != "00" && left != "0")
                            {
                                ++usable_non_00_FF;
                            }
                        }

                        res += fxp[i] + " ";
                    }
                    else
                    {
                        if (!left_wildcard && !right_wildcard)
                        {
                            ++changes;
                        }

                        res += "?? ";
                    }
                }

                wildcards += changes;

                Console.WriteLine("Result: ");
                Console.WriteLine(res);
                Console.WriteLine(
                    "Bytes changed: {0}, wildcards: {1}, usable: {2}, really usable(not 00 or FF): {3}, total: {4}",
                    changes, wildcards, usable, usable_non_00_FF, fxp.Length
                );
            }
        }
    }
}
