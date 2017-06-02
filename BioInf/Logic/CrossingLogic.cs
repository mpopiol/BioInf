using System.Linq;
using BioInf.Model;
using System;
using System.Collections.Generic;

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

            int crossingPoint = StaticRandom.Rand((int)Math.Floor(item1.SequenceIndexes.Length * 0.1), (int)Math.Floor(item1.SequenceIndexes.Length * 0.9));

            if (StaticRandom.Rand() % 2 == 0)
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

        public static Result Cross2Points(Result item1, Result item2)
        {
            Result result = new Result()
            {
                SequenceIndexes = new int[item1.SequenceIndexes.Length]
            };

            int crossingPoint1 = StaticRandom.Rand((int)(item1.SequenceIndexes.Length * 0.6));
            int crossingPoint2 = StaticRandom.Rand(crossingPoint1, (int)(item1.SequenceIndexes.Length));
            FillSequenceBetweenPoints(result, item1.SequenceIndexes, crossingPoint1, crossingPoint2);

            List<int> order = GetFillOrderFromSequence(result, item2.SequenceIndexes, crossingPoint2);

            FillResultFromOrder(result, crossingPoint1, crossingPoint2, order);

            return result;
        }

        private static void FillSequenceBetweenPoints(Result result, int[] sequence, int crossingPoint1, int crossingPoint2)
        {
            for (int i = crossingPoint1; i < crossingPoint2; i++)
            {
                result.SequenceIndexes[i] = sequence[i];
            }
        }

        private static List<int> GetFillOrderFromSequence(Result result, int[] sequence, int crossingPoint2)
        {
            var order = new List<int>();

            for (int i = crossingPoint2; i < sequence.Length; i++)
            {
                if (!result.SequenceIndexes.Contains(sequence[i]))
                {
                    order.Add(sequence[i]);
                }
            }
            for (int i = 0; i < crossingPoint2; i++)
            {
                if (!result.SequenceIndexes.Contains(sequence[i]))
                {
                    order.Add(sequence[i]);
                }
            }

            return order;
        }

        private static void FillResultFromOrder(Result result, int crossingPoint1, int crossingPoint2, List<int> order)
        {
            for (int i = crossingPoint2; i < result.SequenceIndexes.Length; i++)
            {
                FillResultItemFromOrder(result, i, order);
            }
            for (int i = 0; i < crossingPoint1; i++)
            {
                FillResultItemFromOrder(result, i, order);
            }
        }

        private static void FillResultItemFromOrder(Result result, int i, List<int> order)
        {
            var chosenNucleotid = order.FirstOrDefault();
            result.SequenceIndexes[i] = chosenNucleotid;
            order.Remove(chosenNucleotid);
        }
    }
}