using System;
using System.Collections.Generic;
using System.Text;

namespace lab4_rle
{
    class Symbol
    {
        public Symbol(Byte sym)
        {
            Meaning = sym;
            Frequency = 0;
        }
        public Byte Meaning { get; set; }
        public int Frequency { get; set; }
    }
}
