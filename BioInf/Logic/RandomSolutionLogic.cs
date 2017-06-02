using BioInf.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BioInf.Logic
{
    public static class RandomSolutionLogic
    {
        public static Result GenerateRandomSolution()
        {
            int[] result = new int[Global.Nucleotids.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = i + 1;
            }

            return new Result()
            {
                SequenceIndexes = result.OrderBy(i => StaticRandom.Rand()).ToArray()
            };
        }

        public static Result GenerateGreedySolution()
        {
            IList<int> sequence = new List<int>();
            IList<int> notUsed = Enumerable.Range(0, Global.Nucleotids.Count).ToList();

            int starting = StaticRandom.Rand(Global.Nucleotids.Count - 1);
            sequence.Add(starting);
            notUsed.Remove(starting);

            while (sequence.Count != Global.Nucleotids.Count)
            {
                var current = sequence.Last();
                var best = notUsed.Select(i => new Tuple<int, int>(i, EvaluationLogic.GetSinglePartialSum(Global.Nucleotids[current], Global.Nucleotids[i]))).OrderBy(i => i.Item2).First().Item1;
                sequence.Add(best);
                notUsed.Remove(best);
            }

            return new Result()
            {
                SequenceIndexes = sequence.Select(i => i + 1).ToArray()
            };
        }
    }
}