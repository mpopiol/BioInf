using System.Linq;
using BioInf.Model;

namespace BioInf.Logic
{
    public static class CrossingLogic
    {
        public static Result Cross(Result item1, Result item2)
        {
            Result result = new Result()
            {
                SequenceIndexes = new int[item1.SequenceIndexes.Length]
            };

            int crossingPoint = Global.Random.Next(item1.SequenceIndexes.Length, item1.SequenceIndexes.Length);

            if (Global.Random.Next() % 2 == 0)
            {
                FillBeginning(ref result, item1.SequenceIndexes, crossingPoint);
                FillEnding(ref result, item2.SequenceIndexes, crossingPoint);

                return result;
            }
            else
            {
                FillBeginning(ref result, item1.SequenceIndexes.Reverse().ToArray(), crossingPoint);
                FillEnding(ref result, item2.SequenceIndexes.Reverse().ToArray(), crossingPoint);

                result.SequenceIndexes = result.SequenceIndexes.Reverse().ToArray();

                return result;
            }
        }

        private static void FillBeginning(ref Result result, int[] sequence, int crossingPoint)
        {
            for (int i = 0; i < crossingPoint; i++)
            {
                result.SequenceIndexes[i] = sequence[i];
            }
        }

        private static void FillEnding(ref Result result, int[] sequence, int crossingPoint)
        {
            int resultCounter = crossingPoint;
            int itemToCrossCounter = 0;
            while (resultCounter < sequence.Length)
            {
                if (!result.SequenceIndexes.Take(resultCounter).Contains(sequence[itemToCrossCounter]))
                {
                    result.SequenceIndexes[resultCounter++] = sequence[itemToCrossCounter];
                }
                itemToCrossCounter++;
            }
        }
    }
}