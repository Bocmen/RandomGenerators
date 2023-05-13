using System;
using System.Collections;

namespace CoreRandomGenerators
{
    public class LFSR : Abstract.RandomGenerator
    {
        private readonly static BitArray DefaultPolynomial;
        private readonly static BitArray DefaultStartValue;
        static LFSR()
        {
            DefaultStartValue = new BitArray(8);
            DefaultPolynomial = new BitArray(8);
            DefaultStartValue[7] = true;
            DefaultPolynomial[3] = true;
            DefaultPolynomial[4] = true;
            DefaultPolynomial[5] = true;
            DefaultPolynomial[7] = true;
        }

        private readonly BitArray _polynomial;
        private readonly BitArray _lastValue;
        private readonly int _posRead;
        private readonly uint _maskZeroPos;
        private readonly int _endIndexParseValue;

        public LFSR(BitArray polynomial, BitArray startValue, byte countBit, int posRead)
        {
            if (countBit < 1 || countBit > 64) throw new ArgumentOutOfRangeException(nameof(countBit));
            if (polynomial == null || startValue == null) throw new ArgumentNullException($"{nameof(polynomial)} или {nameof(startValue)}");
            if (polynomial.Length != startValue.Length) throw new ArgumentException($"Длина {nameof(polynomial)} и {nameof(startValue)} должна быть одинаковой");
            if (posRead > polynomial.Length - countBit) throw new ArgumentException($"При данной позиции невозможно извлечь {countBit} бит", nameof(posRead));
            _polynomial = polynomial;
            _lastValue = startValue;
            _posRead = posRead;
            _maskZeroPos = 1u << countBit - 1;
            _endIndexParseValue = countBit + _posRead;
            SetCountBit(countBit);
        }
        public LFSR() : this((BitArray)DefaultPolynomial.Clone(), (BitArray)DefaultStartValue.Clone(), 8, 0) { }

        public override ulong Next()
        {
            uint xorResult = 0;
            ulong valueResult = 0;
            int countValueOffset = 0;
            for (int i = _lastValue.Length - 1; i >= 0; i--)
            {
                xorResult += _lastValue[i] && _polynomial[i] ? 1u : 0u;
                if (i != 0)
                    _lastValue[i] = _lastValue[i - 1];
                if (i >= _posRead && i < _endIndexParseValue)
                {
                    valueResult |= _lastValue[i] ? 1u  << countValueOffset : 0u;
                    countValueOffset++;
                }
            }
            _lastValue[0] = (xorResult & 1u) == 1u; // TODO перенести эту строку в цикл и запустить тесты
            if (_posRead == 0)
            {
                valueResult &= ~_maskZeroPos;
                if (_lastValue[0])
                    valueResult |= _maskZeroPos;
            }
            return valueResult;
        }
    }
}
