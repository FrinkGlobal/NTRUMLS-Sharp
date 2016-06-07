using System;
using System.Runtime.InteropServices;
using NTRUMLS.Params;

namespace NTRUMLS.ffi {

    public static class ffi {

        [DllImport("ntrumls")]
        public static extern IntPtr pq_gen_key(ParamSet p, out IntPtr privkey_blob_len, out byte[] privkey_blob, out IntPtr pubkey_blob_len, out byte[] pubkey_blob);

        [DllImport("ntrumls")]
        public static extern IntPtr pq_sign(IntPtr packed_sig_len, byte[] packed_sig, IntPtr private_key_len, byte[] private_key_blob, IntPtr public_key_len, byte[] public_key_blob, IntPtr msg_len, byte[] msg);

        [DllImport("ntrumls")]
        public static extern IntPtr pq_verify(IntPtr packed_sig_len, byte[] packed_sig, IntPtr public_key_len, byte[] public_key_blob, IntPtr msg_len, byte[] msg);


    }
}
