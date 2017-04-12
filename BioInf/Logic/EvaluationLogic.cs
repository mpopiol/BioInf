using BioInf.Model;

namespace BioInf.Logic
{
    public static class EvaluationLogic
    {
        public static int Evaluate(Result item)
        {
            int max = 0;
            for (int i = 0; i < item.sequenceIndexes.Length; i++)
            {
                int subResult = EvaluationLogic.HandleFromPositionToMax(item, i);
                if (subResult > max)
                    max = subResult;
            }
            return max;
        }

        private static int HandleFromPositionToMax(Result item, int startingPosition)
        {
            int position = Global.Nucleotids[0].Sequence.Length;
            int nucleotidsCounter = startingPosition;
            while (nucleotidsCounter < item.sequenceIndexes.Length - 1 && position < Global.MaxLength)
            {
                position += EvaluationLogic.GetSinglePartialSum(Global.Nucleotids[item.sequenceIndexes[nucleotidsCounter] - 1],
                    Global.Nucleotids[item.sequenceIndexes[nucleotidsCounter + 1] - 1]);
                if (position <= Global.MaxLength)
                    nucleotidsCounter++;
            }
            return nucleotidsCounter + 1 - startingPosition;
        }

        private static int GetSinglePartialSum(Nucleotid parentNucleotid, Nucleotid childNucleotid)
        {
            int result = 1;
            while (!childNucleotid.StartsWith(parentNucleotid.Sequence.Substring(result), Global.ErrorToleration) && result < parentNucleotid.Sequence.Length)
                result++;

            return result;
        }
    }
}