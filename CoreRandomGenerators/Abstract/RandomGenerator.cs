using System;

namespace CoreRandomGenerators.Abstract
{
    public abstract class RandomGenerator
    {
        public byte CountBit { get; private set; }
        public byte CurrentCountBit => _currentCountBit;
        private byte _currentCountBit;
        private int _countLast = 0;
        private ulong _value;

        protected void SetCountBit(byte countBit)
        {
            CountBit = countBit;
            _currentCountBit = countBit;
            _countLast = 0;
            _value = 0;
        }

        public void EditCountBit(byte? countBit = null)
        {
            _countLast = 0;
            _value = 0;
            _currentCountBit = countBit ?? CountBit;
        }
        public abstract ulong Next();
        public ulong Next(int n)
        {
            ulong result = 0;
            while (n != 0)
            {
                if (_countLast == 0)
                {
                    _value = Next();
                    _countLast = _currentCountBit;
                }
                int countBit = Math.Min(n, _countLast);
                result = result << countBit | _value & ulong.MaxValue >> 64 - countBit;
                _countLast -= countBit;
                _value >>= countBit;
                n -= countBit;
            }
            return result;
        }
    }
}
