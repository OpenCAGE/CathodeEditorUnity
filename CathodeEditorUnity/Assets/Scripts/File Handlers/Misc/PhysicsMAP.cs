﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CATHODE.Misc
{
    public class PhysicsMAP
    {
        public static alien_physics_map Load(string FullFilePath)
        {
            alien_physics_map Result = new alien_physics_map();
            BinaryReader Stream = new BinaryReader(File.OpenRead(FullFilePath));

            Result.Header = Utilities.Consume<alien_physics_map_header>(ref Stream);
            Result.Entries = Utilities.ConsumeArray<alien_physics_map_entry>(ref Stream, Result.Header.EntryCount);

            return Result;
        }
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct alien_physics_map_header
{
    public int FileSizeExcludingThis;
    public int EntryCount;
};

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct alien_physics_map_entry
{
    public int UnknownNotableValue_;
    public int UnknownZero;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public int[] IDs; //4
    public UnityEngine.Vector4 Row0; // NOTE: This is a 3x4 matrix, seems to have rotation data on the leftmost 3x3 matrix, and position
    public UnityEngine.Vector4 Row1; //   on the rightmost 3x1 matrix.
    public UnityEngine.Vector4 Row2;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public int[] UnknownZeros_; //2
};

public struct alien_physics_map
{
    public alien_physics_map_header Header;
    public List<alien_physics_map_entry> Entries;
};