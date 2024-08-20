using System;
using Unity.VisualScripting;

public enum CurrentLevel
{
    Menu = 0,
    IntroScene = 1,
    Tutorial = 2,
    One = 3,
    Two = 4,
    Three = 5,
    Boss = 6 // maybe not yet, decide on boss level.
}

public static class CurrentLevelMethods
{
    public static CurrentLevel NextLevel(this CurrentLevel level) => level switch {

        CurrentLevel.Tutorial => CurrentLevel.IntroScene,
        CurrentLevel.IntroScene => CurrentLevel.One,
        CurrentLevel.One => CurrentLevel.Two,
        CurrentLevel.Two => CurrentLevel.Three,
        CurrentLevel.Three => CurrentLevel.Boss,
        _ => throw new ArgumentOutOfRangeException("level", level, null)
    };
    
    public static int ToIndex(this CurrentLevel level) => level switch {
        CurrentLevel.Menu => 0,
        CurrentLevel.IntroScene => 1,
        CurrentLevel.Tutorial => 2,
        CurrentLevel.One => 3,
        CurrentLevel.Two => 4,
        CurrentLevel.Three => 5,
        CurrentLevel.Boss => 6,
        _ => throw new ArgumentOutOfRangeException("level", level, null)
    };
}