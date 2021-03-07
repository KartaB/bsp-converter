﻿using System.IO;
using BinaryFileReader;

namespace Decompiler
{
    abstract class CoDExtension : BaseBSP
    {
        public CoDLump[] Lumps { get; set; }

        protected CoDLump[] ReadLumpList(BinaryReader stream)
        {
            BinLib.SetStreamOffset(stream, 8);
            return BinLib.ReadBinary<CoDLumpsList>(stream).LumpList;
        }

        protected T[] ReadLump<T>(BinaryReader stream, int lumpIndex) where T : struct
        {
            BinLib.SetStreamOffset(stream, Lumps[lumpIndex].Offset);
            int count = Lumps[lumpIndex].Length / BinLib.SizeOf<T>();

            T[] objects = new T[count];
            for (int i = 0; i < count; i++)
            {
                objects[i] = BinLib.ReadBinary<T>(stream);
            }

            return objects;
        }

    }
}