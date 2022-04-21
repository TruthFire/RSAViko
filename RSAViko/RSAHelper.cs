using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSAViko
{
    public static class RSAHelper
    {

        public static UInt16 GetBitsize(this BigInteger num)
        {
            UInt16 bitSize = 0;
            while (num != 0)
            {
                num /= 2;
                bitSize++;
            }
            return bitSize;
        }

    }
}
