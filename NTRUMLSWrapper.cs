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

            IntPtr pv_ptr = IntPtr.Zero;
            IntPtr pb_ptr = IntPtr.Zero;

            var result = ffi.ffi.pq_gen_key(parameter, out privkey_blob_len, pv_ptr, out pubkey_blob_len, pb_ptr);

           if (result != 0)
              Console.WriteLine("We got problems");


            pv_ptr = Marshal.AllocHGlobal(privkey_blob_len.ToInt32());
            pb_ptr = Marshal.AllocHGlobal(pubkey_blob_len.ToInt32());


            result = ffi.ffi.pq_gen_key(parameter, out privkey_blob_len, pv_ptr, out pubkey_blob_len, pb_ptr);

            if (result != 0)
                Console.WriteLine("We got problems");

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

            IntPtr private_key_blob = Marshal.AllocHGlobal(private_key.get_bytes().Length);
            IntPtr public_key_blob = Marshal.AllocHGlobal(public_key.get_bytes().Length);
            IntPtr message_ptr = Marshal.AllocHGlobal(message.Length);

            IntPtr pub_key_len = new IntPtr(public_key.get_bytes().Length);
            IntPtr priv_key_len = new IntPtr(private_key.get_bytes().Length);
            IntPtr message_len = new IntPtr(message.Length);

            Marshal.Copy(private_key.get_bytes(), 0, private_key_blob, private_key.get_bytes().Length);
            Marshal.Copy(public_key.get_bytes(), 0, public_key_blob, public_key.get_bytes().Length);
            Marshal.Copy(message, 0, message_ptr, message.Length);

            var result = ffi.ffi.pq_sign(out sign_length_ptr, IntPtr.Zero, priv_key_len, private_key_blob, pub_key_len, public_key_blob, message_len, message_ptr);

            if (result != 0)
                Console.WriteLine("We got problems");


            sign = Marshal.AllocHGlobal(sign_length_ptr.ToInt32());

            result = ffi.ffi.pq_sign(out sign_length_ptr, sign, priv_key_len, private_key_blob, pub_key_len, public_key_blob, message_len, message_ptr);


            if (result != 0)
                Console.WriteLine("We got problems");


            byte[] sign_slice = new byte[sign_length_ptr.ToInt32()];
            Marshal.Copy(sign, sign_slice, 0, sign_length_ptr.ToInt32());

            Marshal.FreeHGlobal(private_key_blob);
            Marshal.FreeHGlobal(sign);
            Marshal.FreeHGlobal(public_key_blob);
            Marshal.FreeHGlobal(message_ptr);

            return sign_slice;
        }

        public static bool verify(byte[] signature, PublicKey public_key, byte[] message) {

            IntPtr sig_len = new IntPtr(signature.Length);
            IntPtr pub_len = new IntPtr(public_key.get_bytes().Length);
            IntPtr msg_len = new IntPtr(message.Length);

            IntPtr sig = Marshal.AllocHGlobal(signature.Length);
            IntPtr msg = Marshal.AllocHGlobal(message.Length);
            IntPtr pub_blob = Marshal.AllocHGlobal(public_key.get_bytes().Length);

            Marshal.Copy(signature, 0, sig, signature.Length);
            Marshal.Copy(message, 0, msg, message.Length);
            Marshal.Copy(public_key.get_bytes(), 0, pub_blob, public_key.get_bytes().Length);

            var result = ffi.ffi.pq_verify(sig_len, sig, pub_len, pub_blob, msg_len, msg);


            Marshal.FreeHGlobal(sig);
            Marshal.FreeHGlobal(pub_blob);
            Marshal.FreeHGlobal(msg);

            return result == 0;
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
