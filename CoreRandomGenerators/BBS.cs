using System;

namespace CoreRandomGenerators
{
    /// <summary>
    /// https://intuit.ru/studies/courses/691/547/lecture/12383?page=3&ysclid=lg19e5ipov227363384
    /// </summary>
    public class BBS : Abstract.RandomGenerator
    {
        private readonly ulong _m;
        private ulong _lastValue;

        public BBS(ulong p, ulong q, ulong seed)
        {
            if (p % 4 != 3 || q % 4 != 3) throw new ArgumentException("p или q по модулю 4 не равны 3");
            _m = p * q;
            _lastValue = seed;
            byte countBit = (byte)Math.Log10(Math.Log10(_m));
            SetCountBit(countBit <= 0 ? (byte)1 : countBit);
        }
        public BBS() : this(359, 607, 166646) { }
        public override ulong Next()
        {
            _lastValue = (_lastValue * _lastValue) % _m;
            return _lastValue;
        }
    }
}
