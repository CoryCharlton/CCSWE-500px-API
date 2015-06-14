using System;

namespace CCSWE.FiveHundredPx
{
    [Flags]
    public enum Categories
    {
        None = 0,
        Abstract = 1,
        Animals = 2,
        BlackAndWhite = 4,
        Celebrities = 8,
        CityAndArchitecture = 16,
        Commercial = 32,
        Concert = 64,
        Family = 128,
        Fashion = 256,
        Film = 512,
        FineArt = 1024,
        Food = 2048,
        Journalism = 4096,
        Landscapes = 8192,
        Macro = 16384,
        Nature = 32768,
        Nude = 65536,
        People = 131072,
        PerformingArts = 262144,
        Sport = 524288,
        StillLife = 1048576,
        Street = 2097152,
        Transportation = 4194304,
        Travel = 8388608,
        Uncategorized = 16777216,
        Underwater = 33554432,
        UrbanExploration = 67108864,
        Wedding = 134217728,
    }
}
