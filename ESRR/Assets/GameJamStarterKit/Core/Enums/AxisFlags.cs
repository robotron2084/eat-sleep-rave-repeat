using System;

namespace GameJamStarterKit
{
    [Flags]
    [Serializable]
    public enum AxisFlags
    {
        X = 0,
        Y = 1,
        Z = 1 << 1
    }
}