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



            var result = ffi.ffi.pq_gen_key(parameter, out privkey_blob_len, IntPtr.Zero, out pubkey_blob_len, IntPtr.Zero);

            Console.WriteLine("Result: " + result + " Private Key BLob Length: " + privkey_blob_len.ToInt32() + " Public Key Blob Lengh: " + pubkey_blob_len);


           if (result != 0)
              Console.WriteLine("We got problems");

            byte[] pv = new byte[privkey_blob_len.ToInt32()];
            byte[] pb = new byte[pubkey_blob_len.ToInt32()];


            IntPtr pv_ptr = Marshal.AllocHGlobal(pv.Length);
            IntPtr pb_ptr = Marshal.AllocHGlobal(pb.Length);

            Marshal.Copy(pv, 0, pv_ptr, pv.Length);
            Marshal.Copy(pb, 0, pb_ptr, pb.Length);



            result = ffi.ffi.pq_gen_key(parameter, out privkey_blob_len, pv_ptr, out pubkey_blob_len, pb_ptr);

            if (result != 0)
                Console.WriteLine("We got problems");

           Console.WriteLine("Result: " + result.ToString() + " Private Key BLob Length: " + privkey_blob_len + " Public Key Blob Lengh: " + pubkey_blob_len);

            byte[] privkeyBytes = new byte[privkey_blob_len.ToInt32()];
            byte[] pubkeyBytes = new byte[pubkey_blob_len.ToInt32()];

            Marshal.Copy(pv_ptr, privkeyBytes, 0, privkey_blob_len.ToInt32());
            Marshal.Copy(pb_ptr, pubkeyBytes, 0, pubkey_blob_len.ToInt32());


            Marshal.FreeHGlobal(pv_ptr);
            Marshal.FreeHGlobal(pb_ptr);
            Marshal.FreeHGlobal(parameter);


            return new KeyPair(new PrivateKey(privkeyBytes), new PublicKey(pubkeyBytes));

        }

        public static byte[] sign (PrivateKey private_key, PublicKey public_key, byte[] message) {
            uint sign_len = 0;

            IntPtr sign = IntPtr.Zero;

            IntPtr sign_length_ptr = new IntPtr(sign_len);

            IntPtr private_key_blob = Marshal.AllocHGlobal(Marshal.SizeOf(private_key.get_bytes()));

            Marshal.StructureToPtr(private_key.get_bytes(), private_key_blob, false);

            IntPtr public_key_blob = Marshal.AllocHGlobal(Marshal.SizeOf(public_key.get_bytes()));

            Marshal.StructureToPtr(public_key.get_bytes(), public_key_blob, false);

            IntPtr message_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(message));

            Marshal.StructureToPtr(message, message_ptr, false);



            var result = ffi.ffi.pq_sign(out sign_length_ptr, IntPtr.Zero, private_key.get_bytes().Length, private_key_blob, public_key.get_bytes().Length, public_key_blob, message.Length, message_ptr);

            if (result != 0)
                Console.WriteLine("We got problems");

            result = ffi.ffi.pq_sign(out sign_length_ptr, sign, private_key.get_bytes().Length, private_key_blob, public_key.get_bytes().Length, public_key_blob, message.Length, message_ptr);

            if (result != 0)
                Console.WriteLine("We got problems");

            return new byte[sign.ToInt32()];
        }

        public static bool verify(byte[] signature, PublicKey public_key, byte[] message) {

        //  var result = ffi.ffi.pq_verify(IntPtr packed_sig_len, IntPtr packed_sig, IntPtr public_key_len, IntPtr public_key_blob, IntPtr msg_len, IntPtr msg)

          return false;
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
