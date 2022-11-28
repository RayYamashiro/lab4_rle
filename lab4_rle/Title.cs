using System;
using System.Collections.Generic;
using System.Text;

namespace lab4_rle
{
    class Title
    {
        public string SignatureNew { get; set; }

        public int Lab { get; set; }

        public int Version { get; set; }

        public int A1 { get; set; }

        public int A2 { get; set; }

        public int A3 { get; set; }

        public int A4 { get; set; }

        public long SizeOld { get; set; }

        public string SignatureOld { get; set; }

        public Title()
        {
            SignatureNew = "abcd";
            Lab = 4;
            Version = 1;
            A1 = 0;
            A2 = 1;
            A3 = 0;
            A4 = 0;
            SizeOld = 0;
        }
    }
}
