using System;
using NTRUMLS.Params;

namespace NTRUMLS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            KeyPair keypair = NTRUMLSWrapper.generate_keys(ParamSets.Xxx20140508_743);


            for (int x = 0; x < keypair.getPublic().get_bytes().Length; x++)
                Console.Write(keypair.getPublic().get_bytes()[x]);
            // TODO Sign, than Verify to confirm test

            Console.WriteLine("\nGenerated Keys!");

            byte[] msg = System.Text.Encoding.UTF8.GetBytes("Hello from NTRUMLS");

            byte[] signature = NTRUMLSWrapper.sign(keypair.getPrivate(), keypair.getPublic(), msg);

            for (int x = 0; x < signature.Length; x++)
                Console.Write(signature[x]);

            Console.WriteLine("\nSigned Message!");

            if (NTRUMLSWrapper.verify(signature, keypair.getPublic(), msg))
            {
              Console.WriteLine("\n Message Verified!");
            }
            else
            {
              Console.WriteLine("\n Message Not Verified!");
            }

        }
    }
}
