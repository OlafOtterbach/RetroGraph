using System;

namespace IGraphics.Extensions
{
    public static class ArrayExtensions
    {
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            foreach (T x in array)
            {
                action(x);
            }
        }
    }
}