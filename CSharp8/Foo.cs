using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8
{
    public class Foo
    {
        public int MethodWithStaticLocal()
        {
            int y = 5;
            int x = 7;
            return Add(x, y);

            static int Add(int left, int right) => left + right;
        }

        public async Task<int> AsyncMethodWithAsyncStaticLocal()
        {
            int y = 5;
            int x = 7;
            return await AsyncAdd(x, y);

            static async Task<int> AsyncAdd(int left, int right)
            {
                return await Task.Run(() => left + right);
            }
        }

        public void MethodWithArrayArgument(int[] values)
        {

        }

        public static async IAsyncEnumerable<int> GetAsyncCollection()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }
    }
}
