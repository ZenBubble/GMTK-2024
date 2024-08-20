using System;
using Unity.VisualScripting;

public enum CurrentLevel
{
    Menu = 0,
    IntroCutScene = 1,
    Tutorial = 2,
    CutScene1 = 3,
    One = 4,
    CutScene2 = 5, 
    Two = 6,
    CutScene3 = 7,
    Three = 8,
}

public static class CurrentLevelMethods
{
    public static CurrentLevel NextLevel(this CurrentLevel level) {
        int index = level.ToIndex();
        if (index >= 0 && index <= 7) {
            return (CurrentLevel) (index + 1);
        } else {
            throw new ArgumentOutOfRangeException("level", level, null);
        }
    }
    
    public static int ToIndex(this CurrentLevel level) => level switch {
        CurrentLevel.Menu => 0,
        CurrentLevel.IntroCutScene => 1,
        CurrentLevel.Tutorial => 2,
        CurrentLevel.CutScene1 => 3,
        CurrentLevel.One => 4,
        CurrentLevel.CutScene2 => 5,
        CurrentLevel.Two => 6,
        CurrentLevel.CutScene3 => 7,
        CurrentLevel.Three => 8,
        _ => throw new ArgumentOutOfRangeException("level", level, null)
    };
}