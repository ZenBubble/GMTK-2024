using System;
using Unity.VisualScripting;

public enum CurrentLevel
{
    Menu = 0,
    Tutorial = 1,
    One = 2,
    Two = 3,
    Three = 4,
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
        CurrentLevel.Menu => 0,
        CurrentLevel.Tutorial => 1,
        CurrentLevel.One => 2,
        CurrentLevel.Two => 3,
        CurrentLevel.Three => 4,
        CurrentLevel.Boss => 5,
        _ => throw new ArgumentOutOfRangeException("level")
    };

    public static CurrentLevel ToLevel(int level) => level switch
    {
        0 => CurrentLevel.Menu,
        1 => CurrentLevel.Tutorial,
        2 => CurrentLevel.One,
        3 => CurrentLevel.Two,
        4 => CurrentLevel.Three,
        5 => CurrentLevel.Boss,
        _ => throw new ArgumentOutOfRangeException("level")
    };
}