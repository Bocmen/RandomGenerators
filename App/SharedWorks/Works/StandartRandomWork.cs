using ConsoleLibrary.ConsoleExtensions;
using CoreRandomGenerators.Abstract;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace WorksRandomGenerator.Works
{
    [WorkInvoker.Attributes.LoaderWorkBase(Const.StandartRandomWork, "", Const.NameGroup)]
    public class StandartRandomWork : Abstract.WorkRandom
    {
        protected override async Task<RandomGenerator> GetRandom(CancellationToken token)
        {
            int seed = await Console.ReadInt("Введите seed", defaultValue: (int)DateTime.Now.Ticks, token: token);
            return new CoreRandomGenerators.StandartRandom(seed);
        }
    }
}
