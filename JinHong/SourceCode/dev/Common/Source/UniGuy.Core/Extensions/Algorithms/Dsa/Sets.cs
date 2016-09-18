// <copyright file="Sets.cs" company="Data Structures and Algorithms">
//   Copyright (C) Data Structures and Algorithms Team.
// </copyright>
// <summary>
//   Algorithms for common set problems implemented as extension methods.
// </summary>
using System;
using UniGuy.Core.DataStructures.Dsa;
using UniGuy.Core.Resources.Dsa;
using UniGuy.Core.Utility.Dsa;

namespace UniGuy.Core.Extensions.Algorithms.Dsa
{
    /// <summary>
    /// <see cref="OrderedSet{T}"/> algorithms.
    /// </summary>
    public static class Sets
    {
        /// <summary>
        /// Determines the number of permutations a set of <em>n</em> items can have with different permutations of <em>setCount</em> items.
        /// </summary>
        /// <remarks>
        /// <em>Permutations(n, k) = n!/(n-k)!</em>. 
        /// </remarks>
        /// <typeparam name="T">Type of <see cref="OrderedSet{T}"/>.</typeparam>
        /// <param name="set">Set to count permutations of.</param>
        /// <param name="setCount">Permutations of setCount items.</param>
        /// <returns>The number of set permutations of <em>setCount</em> items.</returns>
#if DOT_NET_35
        public static int Permutations<T>(this OrderedSet<T> set, int setCount)
#else
        public static int Permutations<T>(OrderedSet<T> set, int setCount)
#endif
        
            where T : IComparable<T>
        {
            Guard.ArgumentNull(set, "set");
            Guard.OutOfRange(setCount < 1, "setCount", Resource.PermutationsKGreaterThanZero);

#if DOT_NET_35
            return (set.Count - setCount) < 0 ? 0 : set.Count.Factorial() / (set.Count - setCount).Factorial();
#else
            return (set.Count - setCount) < 0 ? 0 : Numbers.Factorial(set.Count) / Numbers.Factorial(set.Count - setCount);
#endif
        }
    }
}
