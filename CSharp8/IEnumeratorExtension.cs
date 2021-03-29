using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

namespace CSharp8
{
    public static class IEnumeratorExtension
    {
        public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerator<T> iterator)
        {
            while (iterator.MoveNext())
            {
                await Task.Yield();
                yield return iterator.Current;
            }
        }

        public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerator iterator)
        {
            while (iterator.MoveNext())
            {
                await Task.Yield();
                yield return (T)iterator.Current;
            }
        }
    }
}
