using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInf.Model
{
    public class Nucleotid
    {
        public string Sequence { get; set; }

        public bool StartsWith(string subSequence, int delta = 0)
        {
            int errors = 0;

            if ((subSequence.Length <= 0) ||
                (Sequence.Length <= 0) ||
                (Sequence[0] != subSequence[0]))
                return false;

            for (int i=1; i<subSequence.Length; i++)
            {
                if (Sequence[i] != subSequence[i])
                    errors++;
            }

            if (errors > delta)
                return false;
            else
                return true;
        }

        public bool EndsWith(string subSequence, int delta = 0)
        {
            int errors = 0;
            int startingPoint = Sequence.Length - subSequence.Length;

            if (Sequence[startingPoint] != subSequence[0])
                return false;

            for (int i = startingPoint + 1; i < Sequence.Length; i++)
            {
                if (Sequence[i] != subSequence[i - startingPoint])
                    errors++;
            }

            if (errors > delta)
                return false;
            else
                return true;
        }
    }
}
