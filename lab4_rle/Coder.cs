using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab4_rle
{
    class Coder
    {
        public Coder() { }

        public void work_without(Title title)
        {
            string pathNew = "";
            string pathOld;
            Console.WriteLine("Enter the path of the file to coder: ");
            while (true)
            {
                pathOld = Console.ReadLine();
                if (File.Exists(pathOld))
                {
                    Console.WriteLine("File found.");
                    break;
                }
                else
                {
                    Console.WriteLine("File not found. Try again: ");
                }
            }

            string[] subs = pathOld.Split('.');
            title.SignatureOld = subs[subs.Length - 1];
            Console.WriteLine(title.SignatureOld);
            for (int i = 0; i < subs.Length - 1; i++)
            {
                pathNew = pathNew + subs[i] + ".";
            }
            pathNew += title.SignatureNew;
            Console.WriteLine(pathNew);

            FileStream streamOld = new FileStream(pathOld, FileMode.Open);
            FileStream streamNew = new FileStream(pathNew, FileMode.OpenOrCreate);
            BinaryReader reader = new BinaryReader(streamOld);
            BinaryWriter writer = new BinaryWriter(streamNew);

            title.SizeOld = streamOld.Length;
            writer.Write(title.SignatureNew);
            writer.Write(title.Lab);
            writer.Write(title.Version);
            writer.Write(title.A1);
            writer.Write(title.A2);
            writer.Write(title.A3);
            writer.Write(title.A4);
            writer.Write(title.SizeOld);
            writer.Write(title.SignatureOld);

            //int - 4 bytes
            //string - string.Length + 1 bytes
            //long - 8 bytes


            for (int i = 0; i < title.SizeOld; i++)
            {
                writer.Write(reader.ReadByte());
            }

            writer.Close();
            reader.Close();
            streamNew.Close();
            streamOld.Close();

            Console.WriteLine($"The file is codered. His path is {pathNew}");
        }

        public void work()
        {
            Title title = new Title();

            if (title.A1 == 0)
            {
                if (title.A2 == 0)
                    work_without(title);
                else
                {
                    RLE rle = new RLE();
                    rle.code(title);
                    // rle алгоритм
                }
            }
            else
            {
                Console.WriteLine("Nothing");
            }
        }


    }
}
