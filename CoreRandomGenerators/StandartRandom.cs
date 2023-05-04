using System;

namespace CoreRandomGenerators
{
    public class StandartRandom : Abstract.RandomGenerator
    {
        private const byte RandomCountBit = 16;
        private readonly Random _random;

        public StandartRandom(int seed)
        {
            _random = new Random(seed);
            SetCountBit(RandomCountBit);
        }
        public StandartRandom()
        {
            _random = new Random();
            SetCountBit(RandomCountBit);
        }
        public override ulong Next() => (ulong)_random.Next();
    }
}
