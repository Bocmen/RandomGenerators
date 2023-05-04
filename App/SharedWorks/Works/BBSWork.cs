using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using ConsoleLibrary.ConsoleExtensions;
using CoreRandomGenerators.Abstract;

namespace WorksRandomGenerator.Works
{
    [WorkInvoker.Attributes.LoaderWorkBase(Const.BBSWork, "", Const.NameGroup)]
    public class BBSWork : Abstract.WorkRandom
    {
        protected override async Task<RandomGenerator> GetRandom(CancellationToken token)
        {
            Console.StartCollectionDecorate();
            await Console.WriteLine("Некоторые параметры первый явно показывает шаблонное поведение", ConsoleIOExtension.TextStyle.IsTitle);
            await Console.DrawTableUseGrid(new List<List<object>>()
            {
                new List<object>()
                {
                    "P",
                    "Q",
                    "Seed"
                },
                new List<object>()
                {
                    11,
                    19,
                    3
                },
                new List<object>()
                {
                    431,
                    719,
                    98907
                },
                new List<object>()
                {
                    151,
                    191,
                    20302
                },
                new List<object>()
                {
                    71,
                    127,
                    3517
                },
                new List<object>()
                {
                    127,
                    239,
                    14930
                },
                new List<object>()
                {
                    359,
                    607,
                    166646
                }
            });
        restart:
            try
            {
                ulong p = await Console.ReadULong("Введите P", defaultValue: 359, token: token);
                ulong q = await Console.ReadULong("Введите Q", defaultValue: 607, token: token);
                ulong seed = await Console.ReadULong("Введите seed", defaultValue: 166646, token: token);
                return new CoreRandomGenerators.BBS(p, q, seed);
            }
            catch (Exception e)
            {
                await Console.WriteLine(e.Message);
                goto restart;
            }
        }
    }
}
