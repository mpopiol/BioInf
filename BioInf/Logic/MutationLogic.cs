using System.Linq;
using BioInf.Model;

namespace BioInf.Logic
{
    public static class MutationLogic
    {
        public static Result GreedMutate(Result item)
        {
            Result result = new Result()
            {
                SequenceIndexes = item.SequenceIndexes.ToArray()
            };

            if (StaticRandom.Rand() % 2 == 0)
                return MutationLogic.Mutate(item);

            var nucleotidIndexes = EvaluationLogic.GetWeakConnectedNucleotidIndexes(item).OrderByDescending(n => n.Item2).ToArray();

            if (nucleotidIndexes.Length >= 10)
            {
                int firstIndex = StaticRandom.Rand(10);
                int secondIndex = StaticRandom.Rand(Global.Nucleotids.Count);
                int tmp = result.SequenceIndexes[firstIndex];
                result.SequenceIndexes[firstIndex] = result.SequenceIndexes[secondIndex];
                result.SequenceIndexes[secondIndex] = tmp;
            }
            else
            {
                return MutationLogic.Mutate(item);
            }

            return result;
        }

        public static Result Mutate(Result item)
        {
            Result result = new Result()
            {
                SequenceIndexes = item.SequenceIndexes.ToArray()
            };

            int index1 = StaticRandom.Rand(result.SequenceIndexes.Length - 1);
            int index2 = StaticRandom.Rand(result.SequenceIndexes.Length - 1);

            int tmp = result.SequenceIndexes[index1];
            result.SequenceIndexes[index1] = result.SequenceIndexes[index2];
            result.SequenceIndexes[index2] = tmp;

            return result;
        }
    }
}