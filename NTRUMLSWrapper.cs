using NTRUMLS.ffi;
using NTRUMLS.Params;
using System;
using System.Runtime.InteropServices;

namespace NTRUMLS.Library {

    public static class NTRUMLSWrapper {

        public static KeyPair generate_keys(ParamSet param) {
            uint pub_len = 0;
            uint priv_len = 0;

            IntPtr privkey_blob_len = new IntPtr(priv_len);
            IntPtr pubkey_blob_len = new IntPtr(pub_len);

            IntPtr parameter = Marshal.AllocHGlobal(Marshal.SizeOf(param));

            Marshal.StructureToPtr(param, parameter, false);

            byte[] pv = new byte[priv_len];
            byte[] pb = new byte[pub_len];

            IntPtr pv_ptr = IntPtr.Zero;
            IntPtr pb_ptr = IntPtr.Zero;

            var result = ffi.ffi.pq_gen_key(parameter, out privkey_blob_len, IntPtr.Zero, out pubkey_blob_len, IntPtr.Zero);

            Console.WriteLine("Result: " + result + " Private Key BLob Length: " + priv_len + " Public Key Blob Lengh: " + pub_len);


           if (result != 0)
              Console.WriteLine("We got problems");

             result = ffi.ffi.pq_gen_key(parameter, out privkey_blob_len, pv_ptr, out pubkey_blob_len, pb_ptr);

            if (result != 0)
                Console.WriteLine("We got problems");

           Console.WriteLine("Result: " + result.ToString() + " Private Key BLob Length: " + privkey_blob_len + " Public Key Blob Lengh: " + pubkey_blob_len);

            byte[] privkeyBytes = new byte[privkey_blob_len.ToInt32()];
            byte[] pubkeyBytes = new byte[pubkey_blob_len.ToInt32()];


            return new KeyPair(new PrivateKey(privkeyBytes), new PublicKey(pubkeyBytes));

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
