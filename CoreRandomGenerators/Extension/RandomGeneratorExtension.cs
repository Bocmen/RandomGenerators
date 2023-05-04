using CoreRandomGenerators.Abstract;
using System;

namespace CoreRandomGenerators.Extension
{
    public static class RandomGeneratorExtension
    {
        public static short NextShort(this RandomGenerator random) => (short)random.Next(16);
        public static int NextInt(this RandomGenerator random) => (int)random.Next(32);
        public static long NextLong(this RandomGenerator random) => (long)random.Next(64);

        public static ushort NextUShort(this RandomGenerator random) => (ushort)random.Next(16);
        public static uint NextUInt(this RandomGenerator random) => (uint)random.Next(32);
        public static ulong NextULong(this RandomGenerator random) => random.Next(64);

        public static byte NextByte(this RandomGenerator random) => (byte)random.Next(8);

        public static double NextDouble(this RandomGenerator random) => BitConverter.Int64BitsToDouble((long)(random.Next(52) | 0x3FF0000000000008)) - 1;
        public static double OriginalNextDouble(this RandomGenerator random) => BitConverter.Int64BitsToDouble((long)(random.Next() | 0x3FF0000000000008)) - 1;
    }
}
