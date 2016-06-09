using System;
using NTRUMLS.Library;
using NTRUMLS.Params;

namespace NTRUMLS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            KeyPair keypair = NTRUMLSWrapper.generate_keys(ParamSets.Xxx20140508_743);

            Console.WriteLine("Generated Keys!");

            for (int x = 0; x < keypair.getPublic().get_bytes().Length; x++)
                Console.WriteLine(keypair.getPublic().get_bytes()[x]);
            // TODO Sign, than Verify to confirm test

        }
    }
}
