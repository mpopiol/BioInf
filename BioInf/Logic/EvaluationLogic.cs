using System;
using BioInf.Model;
using System.Collections.Generic;

namespace BioInf.Logic
{
    public static class EvaluationLogic
    {
        public static int Evaluate(Result item)
        {
            int max = 0;
            for (int i = 0; i < item.SequenceIndexes.Length; i++)
            {
                int subResult = EvaluationLogic.HandleFromPositionToMax(item, i);
                if (subResult > max)
                    max = subResult;
            }
            return max;
        }

        public static List<Tuple<int, int>> GetWeakConnectedNucleotidIndexes(Result item)
        {
            var results = new List<Tuple<int, int>>();

            for (int i = 1; i < item.SequenceIndexes.Length-1; i++)
            {
                int distanceLeft = EvaluationLogic.GetSinglePartialSum(Global.Nucleotids[item.SequenceIndexes[i - 1] - 1], Global.Nucleotids[item.SequenceIndexes[i] - 1]);
                int distanceRight = EvaluationLogic.GetSinglePartialSum(Global.Nucleotids[item.SequenceIndexes[i] - 1], Global.Nucleotids[item.SequenceIndexes[i + 1] - 1]);

                if (distanceLeft + distanceRight >= Global.Nucleotids[0].Sequence.Length)
                    results.Add(new Tuple<int, int>(i, distanceLeft + distanceRight));
            }

            return results;
        }

        public static int GetTotalLength(Result item)
        {
            int result = 0;
            for (int i = 0; i < item.SequenceIndexes.Length - 1; i++)
            {
                result += EvaluationLogic.GetSinglePartialSum(Global.Nucleotids[item.SequenceIndexes[i] - 1],
                    Global.Nucleotids[item.SequenceIndexes[i + 1] - 1]);
            }
            return result;
        }

        private static int HandleFromPositionToMax(Result item, int startingPosition)
        {
            int position = Global.Nucleotids[0].Sequence.Length;
            int nucleotidsCounter = startingPosition;
            while (nucleotidsCounter < item.SequenceIndexes.Length - 1 && position < Global.MaxLength)
            {
                position += EvaluationLogic.GetSinglePartialSum(Global.Nucleotids[item.SequenceIndexes[nucleotidsCounter] - 1],
                    Global.Nucleotids[item.SequenceIndexes[nucleotidsCounter + 1] - 1]);
                if (position <= Global.MaxLength)
                    nucleotidsCounter++;
            }
            return nucleotidsCounter + 1 - startingPosition;
        }

        public static int GetSinglePartialSum(Nucleotid parentNucleotid, Nucleotid childNucleotid)
        {
            int result = 1;
            while (!childNucleotid.StartsWith(parentNucleotid.Sequence.Substring(result), Global.ErrorToleration) && result < parentNucleotid.Sequence.Length)
                result++;

            return result;
        }
    }
}