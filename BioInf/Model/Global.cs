﻿using System;
using System.Collections.Generic;

namespace BioInf.Model
{
    public static class Global
    {
        public static List<Nucleotid> Nucleotids { get; set; }
        public static int ErrorToleration { get; set; }
        public static int MaxLength { get; set; }
        public static Random Random = new Random();
    }
}