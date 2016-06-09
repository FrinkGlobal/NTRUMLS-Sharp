using System;
using System.Runtime.InteropServices;
using NTRUMLS.Params;

namespace NTRUMLS.ffi {

    public static class ffi {

        [DllImport("ntrumls")]
        public static extern int pq_gen_key(IntPtr p, out IntPtr privkey_blob_len, IntPtr privkey_blob, out IntPtr pubkey_blob_len, IntPtr pubkey_blob);

        [DllImport("ntrumls")]
        public static extern int pq_gen_key(IntPtr p, IntPtr privkey_blob_len, out IntPtr privkey_blob, IntPtr pubkey_blob_len, out IntPtr pubkey_blob);

        [DllImport("ntrumls")]
        public static extern int pq_sign(out IntPtr packed_sig_len, IntPtr packed_sig, int private_key_len, IntPtr private_key_blob, int public_key_len, IntPtr public_key_blob, int msg_len, IntPtr msg);

        [DllImport("ntrumls")]
        public static extern int pq_verify(IntPtr packed_sig_len, IntPtr packed_sig, IntPtr public_key_len, IntPtr public_key_blob, IntPtr msg_len, IntPtr msg);


    }
}
