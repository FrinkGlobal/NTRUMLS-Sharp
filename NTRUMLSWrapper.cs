using NTRUMLS.ffi;
using NTRUMLS.Params;
using System;
using System.Runtime.InteropServices;

namespace NTRUMLS.Library {

    public static class NTRUMLSWrapper {

        public static KeyPair generate_keys(ParamSet param) {
            int pub_len = 0;
            int priv_len = 0;


            Console.WriteLine("Param N: " + param.get_n());
            Console.WriteLine("Param P: " + param.get_p());
            Console.WriteLine("Param D1: " + param.get_d1());
            Console.WriteLine("Param D2: " + param.get_d2());
            Console.WriteLine("Param D3: " + param.get_d3());



            GCHandle pub_len_handle = GCHandle.Alloc(pub_len);
            GCHandle priv_len_handle = GCHandle.Alloc(priv_len);

            IntPtr privkey_blob_len = (IntPtr)pub_len_handle;
            IntPtr pubkey_blob_len = (IntPtr)priv_len_handle;

            GCHandle handle = GCHandle.Alloc(param);

            IntPtr paramater = (IntPtr)handle;

            byte[] pv = new byte[priv_len];
            byte[] pb = new byte[pub_len];

        //    GCHandle pv_handle = GCHandle.Alloc(pv);
        //    GCHandle pb_handle = GCHandle.Alloc(pb);


            var result = ffi.ffi.pq_gen_key(param, out privkey_blob_len, out pv, out pubkey_blob_len, out pb);

            Console.WriteLine("Result: " + result + " Private Key BLob Length: " + priv_len + " Public Key Blob Lengh: " + pub_len);



           if (result != 0)
              Console.WriteLine("We got problems");

             byte[] privatekey_blob = new byte[privkey_blob_len.ToInt64()];
             byte[] pubkey_blob = new byte[pubkey_blob_len.ToInt64()];

             result = ffi.ffi.pq_gen_key(param, out privkey_blob_len, out privatekey_blob, out pubkey_blob_len, out pubkey_blob);

            if (result != 0)
                Console.WriteLine("We got problems");

           Console.WriteLine("Result: " + result.ToString() + " Private Key BLob Length: " + privkey_blob_len + " Public Key Blob Lengh: " + pubkey_blob_len);

            // byte[] privkeyBytes = new byte[priv_len.ToInt32()];
            // byte[] pubkeyBytes = new byte[pub_len.ToInt32()];



            return new KeyPair(new PrivateKey(pubkey_blob), new PublicKey(privatekey_blob));;

        }

    }


    public struct PrivateKey {
        byte[] ffi_key;

        public PrivateKey (byte[] bytes) {
            ffi_key = bytes;
        }

        public byte[] get_bytes() {
            return ffi_key;
        }
    }

    public struct PublicKey {
        byte[] ffi_key;

        public PublicKey (byte[] bytes) {
            ffi_key = bytes;
        }

        public byte[] get_bytes() {
            return ffi_key;
        }
    }

    public struct KeyPair {

        PublicKey publicKey;
        PrivateKey privateKey;

        public KeyPair(PrivateKey privKey, PublicKey pubkey)
        {
            publicKey = pubkey;
            privateKey = privKey;
        }

        public PublicKey getPublic()
        {
            return publicKey;
        }

        public PrivateKey getPrivate()
        {
            return privateKey;
        }

    }

}
