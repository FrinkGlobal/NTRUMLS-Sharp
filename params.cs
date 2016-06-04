using System;
using System.Text;
using System.Runtime.InteropServices;

namespace NTRUMLS.Params {


    public enum ParamSetId {
        Xxx20140508401,
        Xxx20140508439,
        Xxx20140508593,
        Xxx20140508743,

        Xxx20151024401,
        Xxx20151024443,
        Xxx20151024563,
        // Xxx20151024509,
        Xxx20151024743,
        Xxx20151024907,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ParamSet {

        ParamSetId id;
        [MarshalAs(UnmanagedType.LPStr)]
        string name;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        byte[] oid;
        [MarshalAs(UnmanagedType.U1)]
        byte n_bits;
        [MarshalAs(UnmanagedType.U1)]
        byte q_bits;
        [MarshalAs(UnmanagedType.U2)]
        ushort n;
        [MarshalAs(UnmanagedType.I1)]
        sbyte p;
        [MarshalAs(UnmanagedType.I8)]
        long q;
        [MarshalAs(UnmanagedType.I8)]
        long b_s;
        [MarshalAs(UnmanagedType.I8)]
        long b_t;
        [MarshalAs(UnmanagedType.I8)]
        long norm_bound_s;
        [MarshalAs(UnmanagedType.I8)]
        long norm_bound_t;
        [MarshalAs(UnmanagedType.U1)]
        byte d1;
        [MarshalAs(UnmanagedType.U1)]
        byte d2;
        [MarshalAs(UnmanagedType.U1)]
        byte d3;
        [MarshalAs(UnmanagedType.U2)]
        ushort padded_n;


        public ushort get_n() {
            return n;
        }

        public sbyte get_p() {
            return p;
        }

        public byte get_d1() {
            return d1;
        }

        public byte get_d2() {
            return d2;
        }

        public byte get_d3() {
            return d3;
        }

        public uint product_form_bytes() {
            return (uint)(4 * (d1 + d2 + d3));
        }

        public uint polynomial_bytes() {
            return (uint)(padded_n * 8);
        }

        public uint privkey_packed_bytes() {
            return (uint)(5 + 2 * ((2 * ( d1 + d2 + d3) * n_bits + 7) / 8) + ((n +4) / 5));
        }

        public uint pubkey_packed_bytes() {
            return (uint)(5 + (n * q_bits + 7) / 8 + 64);
        }

        public ParamSet (ParamSetId ID, string NAME, byte[] OID, byte N_BITS, byte Q_BITS, ushort N, sbyte P, long Q, long B_S, long B_T, long NORM_BOUND_S, long NORM_BOUND_T, byte D1, byte D2, byte D3, ushort PADDED_N) {
            id = ID;
            name = NAME;
            oid = OID;
            n_bits = N_BITS;
            q_bits = Q_BITS;
            n = N;
            p = P;
            q = Q;;
            b_s = B_S;
            b_t = B_T;
            norm_bound_s = NORM_BOUND_S;
            norm_bound_t = NORM_BOUND_T;
            d1 = D1;
            d2 = D2;
            d3 = D3;
            padded_n = PADDED_N;
        }


    }

    public static class ParamSets {

        /// <summary>
        /// 112 bit security parameter
        /// </summary>
        public static readonly ParamSet Xxx20140508_401 = new ParamSet(
                ParamSetId.Xxx20140508401,
                "Xxx20140508401",
                new byte[] {0xff, 0xff, 0xff},
                9,
                18,
                401,
                3,
                1 << 18,
                240,
                80,
                (1 << 17) - 240,
                (1 << 17) - 80,
                8,
                8,
                6,
                416);

        /// <summary>
        /// 128 bit security parameter
        /// </summary>
        public static readonly ParamSet Xxx20140508_439 = new ParamSet(
            ParamSetId.Xxx20140508439,
            "Xxx20140508439",
            new byte[] {0xff, 0xff, 0xfe},
            9,
            19,
            439,
            3,
            1 << 19,
            264,
            80,
            (1 << 18) - 264,
            (1 << 18) - 80,
            9,
            8,
            5,
            448);

        /// <summary>
        /// 192 bit security parameter
        /// </summary>
        public static readonly ParamSet Xxx20140508_593 = new ParamSet(
            ParamSetId.Xxx20140508593,
            "Xxx20140508593",
            new byte[] {0xff, 0xff, 0xfd},
            10,
            19,
            593,
            3,
            1 << 19,
            300,
            100,
            (1 << 18) - 300,
            (1 << 18) - 100,
            10,
            10,
            8,
            608);


        /// <summary>
        /// 256 bit security parameter
        /// </summary>
        public static readonly ParamSet Xxx20140508_743 = new ParamSet(
            ParamSetId.Xxx20140508743,
            "Xxx20140508743",
            new byte[] {0xff, 0xff, 0xfc},
            10,
            20,
            743,
            3,
            1 << 20,
            336,
            112,
            (1 << 19) - 336,
            (1 << 19) - 112,
            11,
            11,
            15,
            768);

        /// <summary>
        /// New 2015 parameter
        /// </summary>
        public static readonly ParamSet Xxx20151024_401 = new ParamSet(
            ParamSetId.Xxx20151024401,
            "Xxx20151024401",
            new byte[] {0xff, 0xff, 0xfb},
            9,
            15,
            401,
            3,
            1 << 15,
            138,
            46,
            (1 << 14) - 138,
            (1 << 14) - 46,
            8,
            8,
            6,
            416);


        /// <summary>
        /// New 2015 parameter
        /// </summary>
        public static readonly ParamSet Xxx20151024_443 = new ParamSet(
            ParamSetId.Xxx20151024443,
            "Xxx20151024443",
            new byte[] {0xff, 0xff, 0xfa},
            9,
            16,
            443,
            3,
            1 << 16,
            138,
            46,
            (1 << 15) - 138,
            (1 << 15) - 46,
            9,
            8,
            5,
            448);


        /// <summary>
        /// New 2015 parameter
        /// </summary>
        public static readonly ParamSet Xxx20151024_563 = new ParamSet(
            ParamSetId.Xxx20151024563,
            "Xxx20151024563",
            new byte[] {0xff, 0xff, 0xf9},
            10,
            16,
            563,
            3,
            1 << 16,
            174,
            58,
            (1 << 15) - 174,
            (1 << 15) - 58,
            10,
            9,
            8,
            592);

        /// <summary>
        /// New 2015 parameter
        /// </summary>
        public static readonly ParamSet Xxx20151024_743 = new ParamSet(
            ParamSetId.Xxx20151024743,
            "Xxx20151024743",
            new byte[] {0xff, 0xff, 0xf7},
            10,
            17,
            743,
            3,
            1 << 17,
            186,
            62,
            (1 << 16) - 186,
            (1 << 16) - 62,
            11,
            11,
            6,
            752);

        /// <summary>
        /// New 2015 parameter
        /// </summary>
        public static readonly ParamSet Xxx20151024_907 = new ParamSet(
            ParamSetId.Xxx20151024907,
            "Xxx20151024907",
            new byte[] {0xff, 0xff, 0xf6},
            10,
            17,
            907,
            3,
            1 << 17,
            225,
            75,
            (1 << 16) - 225,
            (1 << 16) - 75,
            13,
            12,
            7,
            912);

    }


}
