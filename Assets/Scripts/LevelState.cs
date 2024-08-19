using System;
using Unity.VisualScripting;

public enum CurrentLevel
{
    Tutorial = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Boss // maybe not yet, decide on boss level.
}

public static class CurrentLevelMethods
{
    public static CurrentLevel NextLevel(this CurrentLevel level) => level switch {
        CurrentLevel.Tutorial => CurrentLevel.One,
        CurrentLevel.One => CurrentLevel.Two,
        CurrentLevel.Two => CurrentLevel.Three,
        CurrentLevel.Three => CurrentLevel.Boss,
        _ => throw new ArgumentOutOfRangeException("level")
    };
    
    public static int ToIndex(this CurrentLevel level) => level switch {
        CurrentLevel.Tutorial => 0,
        CurrentLevel.One => 1,
        CurrentLevel.Two => 2,
        CurrentLevel.Three => 3,
        CurrentLevel.Boss => 4,
        _ => throw new ArgumentOutOfRangeException("level")
    };
}