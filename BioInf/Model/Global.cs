using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInf.Model
{
    public static class Global
    {
        public static List<Nucleotid> Nucleotids { get; set; }
        public static int ErrorToleration { get; set; }
        public static int MaxLength { get; set; }
    }
}
