using System;

namespace QrCodeGenerator
{
    internal static class RandomExtension
    {

        public static long Next(this Random rand)
        {
           return rand.Next(Int64.MaxValue);
        }

        public static long Next(this Random rand, long max)
        {
            return rand.Next(0, max);
        }

        public static long Next(this Random rand, long min, long max)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return Math.Abs(longRand % (max - min)) + min;
        }
    }
}
