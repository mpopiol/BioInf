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

        public bool StartsWith(string subSequence)
        {
            throw new NotImplementedException();
        }

        public bool EndsWith(string subSequence)
        {
            throw new NotImplementedException();
        }
    }
}
