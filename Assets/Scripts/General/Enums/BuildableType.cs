using System;

namespace General.Enums
{
    [Flags] public enum BuildableType
    {
        None  = 0,
        Unit  = 1 << 0, // 1
        Tower = 1 << 1, // 2
        Root  = 1 << 2  // 4
    }
}