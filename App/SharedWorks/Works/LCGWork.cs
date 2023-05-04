using CoreRandomGenerators.Abstract;
using System.Threading.Tasks;
using System.Threading;
using ConsoleLibrary.ConsoleExtensions;

namespace WorksRandomGenerator.Works
{
    [WorkInvoker.Attributes.LoaderWorkBase(Const.LCGWork, "", Const.NameGroup)]
    public class LCGWork : Abstract.WorkRandom
    {
        protected override async Task<RandomGenerator> GetRandom(CancellationToken token)
        {
            Console.StartCollectionDecorate();
            ulong m = await Console.ReadULong("Введите M", token: token, defaultValue: 4294967296);
            ulong a = await Console.ReadULong("Введите A", token: token, defaultValue: 1664525);
            ulong c = await Console.ReadULong("Введите C", token: token, defaultValue: 1013904223);
            ulong seed = await Console.ReadULong("Введите Seed", token: token, defaultValue: 638158814482368301);
            return new CoreRandomGenerators.LCG(seed, m, a, c);
        }
    }
}
