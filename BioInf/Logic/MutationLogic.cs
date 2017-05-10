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

            if (Global.Random.Next() % 2 == 0)
                return MutationLogic.Mutate(item);

            var nucleotidIndexes = EvaluationLogic.GetWeakConnectedNucleotidIndexes(item).OrderByDescending(n => n.Item2).ToArray();

            if (nucleotidIndexes.Length >= 2)
            {
                int tmp = result.SequenceIndexes[nucleotidIndexes[0].Item1];
                result.SequenceIndexes[nucleotidIndexes[0].Item1] = result.SequenceIndexes[nucleotidIndexes[1].Item1];
                result.SequenceIndexes[nucleotidIndexes[1].Item1] = tmp;
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

            int index1 = Global.Random.Next(result.SequenceIndexes.Length - 1);
            int index2 = Global.Random.Next(result.SequenceIndexes.Length - 1);

            int tmp = result.SequenceIndexes[index1];
            result.SequenceIndexes[index1] = result.SequenceIndexes[index2];
            result.SequenceIndexes[index2] = tmp;

            return result;
        }
    }
}