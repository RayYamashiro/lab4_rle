using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab4_rle
{
    class RLE
    {
        public static Title title { get; set; }
        private List<Symbol> symbols;
        public Byte Prefix { get; set; }
        public RLE() { }
        public void code(Title title_)
        {
            title = title_;
            string name = "";
            string pathNew = "";
            Console.WriteLine("Enter the path of the file to coder: ");
            while (true)
            {
                name = Console.ReadLine();
                if (File.Exists(name))
                {
                    Console.WriteLine("File found.");
                    break;
                }
                else
                {
                    Console.WriteLine("File not found. Try again: ");
                }
            }
            string[] subs = name.Split('.');
            title.SignatureOld = subs[subs.Length - 1];
            Prefix = find_prefix(name);
            for (int i = 0; i < subs.Length - 1; i++)
            {
                pathNew = pathNew + subs[i] + ".";
            }
            pathNew += title.SignatureNew;
            FileStream streamOld = new FileStream(name, FileMode.Open);
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
            writer.Write(Prefix);
            Console.WriteLine((char)Prefix);
            Byte instance = 0, current = 0;
            byte count = 0;
            bool flag = false;
            //string d = "";
            for (int i = 0; i < title.SizeOld; i++)
            {
                current = reader.ReadByte();
                //Console.Write((char)current);
                if (i == 0)
                {
                    instance = current;
                    count++;
                }
                else
                {
                    if (instance == current)
                    {
                        count++;
                    }
                    else
                    {
                        //instance = current;
                        if (count > 1)
                        {
                            // d = d + "|" + (char)Prefix + "  " + count + "  " + (char)instance + "|";
                            writer.Write(Prefix);
                            writer.Write(count);
                            writer.Write(instance);
                            //count = 1;
                            //instance = current;
                        }
                        else
                        {
                            if (instance == Prefix)
                            {
                                // d = d + "|" + (char)Prefix + "  " + count + "  " + (char)instance + "|";
                                writer.Write(Prefix);
                                writer.Write(count);
                                writer.Write(instance);
                                //count = 1;
                                //instance = current;
                            }
                            else
                            {
                                //d = d + (char)instance;
                                writer.Write(instance);
                                //count = 1;
                                //instance = current;
                            }
                        }
                        if (i != title.SizeOld - 1)
                        {
                            count = 1;
                            instance = current;
                        }

                    }
                }

            }

            if (instance == current)
            {
                
                writer.Write(Prefix);
                writer.Write(count);
                writer.Write(instance);
            }
            else
            {
                if (current == Prefix)
                {
                    count = 1;
                    
                    instance = current;
                    writer.Write(Prefix);
                    writer.Write(count);
                    writer.Write(instance);
                }
                else
                {
                    
                    writer.Write(current);
                }
            }

            Console.WriteLine("");

            
            writer.Close();
            reader.Close();
            streamNew.Close();
            streamOld.Close();

            Console.WriteLine("\nФайл успешно зашифрован. Новое имя файла: " + subs[0] + "." + title.SignatureNew + "\n");
        }
        public void find_first(Byte sym)
        {
            if (symbols.Exists(x => x.Meaning == sym))
            {
                find(sym);
            }
            else
            {
                symbols.Add(new Symbol(sym));
                find(sym);
            }
        }
        public void find(Byte a)
        {
            for (int i = 0; i < symbols.Count; i++)
            {
                if (symbols[i].Meaning == a)
                {
                    symbols[i].Frequency = symbols[i].Frequency + 1;
                }
            }
        }
        public Symbol find_min()
        {
            Symbol min = symbols[0];
            for (int i = 0; i < symbols.Count; i++)
            {
                if (min.Frequency > symbols[i].Frequency)
                    min = symbols[i];
            }
            return min;
        }

        public Byte find_prefix(string name)
        {
            symbols = new List<Symbol>();
            FileStream streamOld = new FileStream(name, FileMode.Open);
            BinaryReader reader = new BinaryReader(streamOld);
            long sizeOld = streamOld.Length;
            Byte v;
            int n = 0;
            for (int i = 0; i < sizeOld; i++)
            {
                v = reader.ReadByte();
                n++;
                find_first(v);
            }
            reader.Close();
            Symbol symbol = find_min();

            return symbol.Meaning;
        }

        public void decode(string name, Title title_)
        {
            title = title_;
            FileStream streamOld = new FileStream(name, FileMode.Open);
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
            Prefix = reader.ReadByte();
            long pos = reader.BaseStream.Position;
           


            //string pathNew =  "copy_" + name.Substring(0, name.Length - title.SignatureNew.Length) + title.SignatureOld;

            string pathNew = "C:\\Users\\RayYamashiro\\source\\repos\\lab4_rle\\lab4_rle\\ff(copy).txt";  // менять под файл
            FileStream streamNew = new FileStream(pathNew, FileMode.OpenOrCreate);
            BinaryWriter writer = new BinaryWriter(streamNew);
            Byte f = 0, l = 0;
            byte count = 0;
            

            for (long i = pos; i < streamOld.Length; i++)
            {
                f = reader.ReadByte();
               

                if (f == Prefix)
                {
                    count = reader.ReadByte();
                    i++;
                    l = reader.ReadByte();
                    i++;
                    
                    for (byte j = 0; j < count; j++)
                    {
                        writer.Write(l);
                    }
                }
                else
                {
                    writer.Write(f);
                }
            }
            writer.Close();
            reader.Close();
            streamNew.Close();
            streamOld.Close();

        }

    }
}
