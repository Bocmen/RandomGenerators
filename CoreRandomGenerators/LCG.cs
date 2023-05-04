namespace CoreRandomGenerators
{
    public class LCG : Abstract.RandomGenerator
    {
        public ulong M { get; private set; }
        public ulong A { get; private set; }
        public ulong C { get; private set; }

        private ulong _lastValue;

        public LCG(ulong seed, ulong m, ulong a, ulong c)
        {
            M = m;
            A = a;
            C = c;
            _lastValue = seed;
            byte countBit = 0;
            for (byte i = 63; i > 0; i--)
            {
                if (m >> i != 0)
                {
                    countBit = i;
                    break;
                }
            }
            SetCountBit(countBit);
        }
        public LCG() : this(638158814482368301, 4294967296, 1664525, 1013904223) { }

        public override ulong Next()
        {
            _lastValue = (A * _lastValue + C) % M;
            return _lastValue;
        }
    }
}
