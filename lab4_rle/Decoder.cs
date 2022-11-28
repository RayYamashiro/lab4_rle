using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab4_rle
{
    class Decoder
    {
        public Decoder()
        {

        }
        public void work_without(string pathOld, Title title_)
        {
            string pathNew;

            Console.WriteLine("Enter the path of the file to decoder: ");

            FileStream streamOld = new FileStream(pathOld, FileMode.Open);
            BinaryReader reader = new BinaryReader(streamOld);

            int lab, version, a1, a2, a3, a4;
            string signatureOld, signatureNew;
            long sizeOld;

            signatureOld = reader.ReadString();
            lab = reader.ReadInt32();
            version = reader.ReadInt32();
            a1 = reader.ReadInt32();
            a2 = reader.ReadInt32();
            a3 = reader.ReadInt32();
            a4 = reader.ReadInt32();
            sizeOld = reader.ReadInt64();
            signatureNew = reader.ReadString();

            pathNew = "copy_" + pathOld.Substring(0, pathOld.Length - title_.SignatureNew.Length) + signatureNew;

            FileStream streamNew = new FileStream(pathNew, FileMode.OpenOrCreate);
            BinaryWriter writer = new BinaryWriter(streamNew);

            for (int i = 0; i < sizeOld; i++)
            {
                writer.Write(reader.ReadByte());
            }

            writer.Close();
            reader.Close();
            streamNew.Close();
            streamOld.Close();

            Console.WriteLine($"The file is decodered. His path is {pathNew}");
        }

        public void work()
        {
            Title title = new Title();
            string pathOld;

            Console.WriteLine("Enter the path of the file to decoder: ");
            while (true)
            {
                pathOld = Console.ReadLine();
                if (File.Exists(pathOld))
                {
                    if (pathOld.EndsWith("." + title.SignatureNew))
                    {
                        Console.WriteLine("File found.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"The found file does not have an {title.SignatureNew} signature. Try again:");
                    }
                }
                else
                {
                    Console.WriteLine("File not found. Try again: ");
                }
            }

            FileStream streamOld = new FileStream(pathOld, FileMode.Open);
            BinaryReader reader = new BinaryReader(streamOld);



            title.SignatureNew = reader.ReadString();
            title.Lab = reader.ReadInt32();
            title.Version = reader.ReadInt32();
            title.A1 = reader.ReadInt32();
            title.A2 = reader.ReadInt32();
            title.A3 = reader.ReadInt32();
            title.A4 = reader.ReadInt32();
            title.SizeOld = reader.ReadInt64();
            title.SignatureOld = reader.ReadString();
            reader.Close();
            streamOld.Close();
            if (title.A1 == 0)
            {
                if (title.A2 == 0)
                    work_without(pathOld, title);
                else
                {
                    //rle
                    RLE rle = new RLE();
                    rle.decode(pathOld, title);
                }
            }
            else
            {
                Console.WriteLine("Nothing");
            }
        }

    }
}
