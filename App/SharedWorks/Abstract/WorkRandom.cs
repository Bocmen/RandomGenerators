using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using CoreRandomGenerators.Abstract;

using ConsoleLibrary.ConsoleExtensions;

namespace WorksRandomGenerator.Abstract
{
    public abstract class WorkRandom : WorkInvoker.Abstract.WorkBase
    {
        private RandomGenerator _randomGenerator;

        public override async Task Start(CancellationToken token)
        {
            _randomGenerator = await GetRandom(token);
            Console.StartCollectionDecorate();
            await Console.WriteLine("Получена модель генератора псевдослучайный чисел", ConsoleIOExtension.TextStyle.IsTitle);
            await Console.WriteLine($"Генератор позволяет выдать: {_randomGenerator.CurrentCountBit} бит");
            do
            {
                Console.StartCollectionDecorate();
                bool isRepeatability = await Console.ReadBool("Визуализировать N первых значений?", token: token);
                bool isHistogram = await Console.ReadBool("Визуализировать гистограмму?");
                if (isRepeatability)
                    await DrawRepeatability(token);
                if (isHistogram)
                    await DrawHistogram(token, 1000);

            } while (await Console.ReadBool("Перезапустить генерацию с другим кол-во бит?"));
        }
        private async Task EditCountBit(CancellationToken token)
        {
            _randomGenerator.EditCountBit((byte)(await Console.ReadInt($"Какое кол-во бит будет выдавать генератор", startRange: 1, endRange: 64, defaultValue: _randomGenerator.CountBit, token: token)));
        }
        private async Task<int> GetCountBitResultValue(int defaultValue, CancellationToken token) => (await Console.ReadInt("Кол-во бит для вывода", startRange: 1, endRange: 64, defaultValue: defaultValue, token: token));
        protected abstract Task<RandomGenerator> GetRandom(CancellationToken token);
        private async Task DrawRepeatability(CancellationToken token)
        {
            Console.StartCollectionDecorate();
            await Console.WriteLine("Визуализация следующих N чисел", ConsoleIOExtension.TextStyle.IsTitle);
            int countValues = await Console.ReadInt("Кол-во чисел для визуализации", startRange: 1, defaultValue: 1000, token: token);
            await EditCountBit(token);
            int countBitResult = await GetCountBitResultValue(_randomGenerator.CurrentCountBit, token);
            AreaSeries ls = new AreaSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColors.Black
            };
            for (int i = 0; i < countValues; i++)
            {
                ls.Points.Add(new DataPoint(i, _randomGenerator.Next(countBitResult)));
                token.ThrowIfCancellationRequested();
            }
            await Console.DrawChartOxyPlot(new PlotModel()
            {
                Series =
                {
                    ls
                }
            });
            Console.SetDecorationOneElem();
        }
        private async Task DrawHistogram(CancellationToken token, int counValues)
        {
            Console.StartCollectionDecorate();
            await Console.WriteLine("Визуализация гистограммы", ConsoleIOExtension.TextStyle.IsTitle);
            await EditCountBit(token);
            int countBitResult = await GetCountBitResultValue(8, token);
            Dictionary<ulong, int> histogramData = new Dictionary<ulong, int>();
            ulong minValue = ulong.MaxValue;
            ulong maxValue = ulong.MinValue;
            for (int i = 0; i < counValues; i++)
            {
                var value = _randomGenerator.Next(countBitResult);
                if (!histogramData.ContainsKey(value))
                    histogramData.Add(value, 1);
                else
                    histogramData[value]++;
                minValue = Math.Min(minValue, value);
                maxValue = Math.Max(maxValue, value);
                token.ThrowIfCancellationRequested();
            }
            StemSeries ls = new StemSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColors.Black
            };
            double th = 1.0 / (maxValue - minValue);
            LineSeries lineSeries = new LineSeries()
            {
                Points =
                {
                    new DataPoint(minValue, th),
                    new DataPoint(maxValue, th)
                },
                Color = OxyColors.Orange
            };
            ls.Points.AddRange(histogramData.Select(x => new DataPoint(x.Key, (double)x.Value / counValues)).OrderBy(x => x.X));
            await Console.DrawChartOxyPlot(new PlotModel()
            {
                Series =
                {
                    ls,
                    lineSeries
                }
            });
            await Console.WriteLine("Визуализация расхождений старших и младших значений распределения", ConsoleIOExtension.TextStyle.IsTitle);
            var sortData = new StemSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColors.Black
            };
            sortData.Points.AddRange(ls.Points.OrderBy(x => x.Y).Select((x, i) => new DataPoint(i, x.Y)));
            await Console.DrawChartOxyPlot(new PlotModel()
            {
                Series =
                {
                    sortData
                }
            });
            Console.SetDecorationOneElem();
        }
    }
}
