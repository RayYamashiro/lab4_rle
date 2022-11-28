using System;

namespace lab4_rle
{
    class Program
    {
        static void Main(string[] args)
        {
            Coder coder = new Coder();
            coder.work();

            Decoder decoder = new Decoder();
            decoder.work();
        }
    }
}
