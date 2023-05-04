using CoreRandomGenerators.Abstract;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static ConsoleLibrary.ConsoleExtensions.ConsoleIOExtension;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;

namespace WorksRandomGenerator.Works
{
    [WorkInvoker.Attributes.LoaderWorkBase(Const.LFSRWork, "", Const.NameGroup)]
    public class LFSRWork : Abstract.WorkRandom
    {
        protected override async Task<RandomGenerator> GetRandom(CancellationToken token)
        {
            await Console.WriteLine($"Другие известные {ConsoleLibrary.Extensions.FormattedStringExtension.LinkPattern("полиомы", "https://docs.xilinx.com/v/u/en-US/xapp052")}", TextStyle.IsTitle);
            Console.StartCollectionDecorate();
            int size = await Console.ReadInt("Введите размер буфера", defaultValue: 8, startRange: 1, token: token);
            var startValue = await ReadBitArray("Введите стартовое значение", size, new int[] { 8 });
            var polynomial = await ReadBitArray("Введите полином (пустая строка равносильна заполнению всех битов в 1)", size, new int[] { 8, 6, 5, 4 });
            if (polynomial.Count == 0)
            {
                polynomial = new BitArray(startValue.Count);
                polynomial.SetAll(true);
            }
            await Console.WriteLine($"{ConsoleLibrary.Extensions.FormattedStringExtension.ColorPattern("Внимание", Color.Yellow)} в дальнейшем увеличение кол-ва бит не приведёт к увеличению значения");
            int countBit = await Console.ReadInt("Введите кол-во бит", defaultValue: startValue.Length, startRange: 1, endRange: startValue.Length, token: token);
            int posStart = await Console.ReadInt("Введите позицию чтения", defaultValue: 0, startRange: 0, endRange: startValue.Length - countBit, token: token);
            return new CoreRandomGenerators.LFSR(polynomial, startValue, (byte)countBit, posStart);
        }
        private async Task<BitArray> ReadBitArray(string title, int size, IEnumerable<int> defaultValue, char separation = ',', OptionReadValue options = OptionReadValue.None, CancellationToken? token = null)
        {
            int[] values = await Console.ReadArrayInt(title, startRange: 0, token: token, options: options, separator: separation, defaultsValue: defaultValue);
            int maxVal = values.Max();
            bool typeInput = maxVal > 1;
            if (typeInput)
            {
                BitArray bitArray = new BitArray(size);
                for (int i = 0; i < values.Length; i++)
                {
                    int bitIndex = values[i] - 1;
                    if (bitIndex < size)
                        bitArray[bitIndex] = true;
                }
                return bitArray;
            }
            else
            {
                BitArray bitArray = new BitArray(size);
                for (int i = 0; i < size; i++)
                    if (i < values.Length)
                        bitArray[i] = values[i] == 1;
                return bitArray;
            }
        }
    }
}
