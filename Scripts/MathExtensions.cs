using Godot;
using System;

public static class MathExtensions
{
    public static Vector2 Where(this Vector2 original, float? x = null, float? y = null)
    {
        return new Vector2(x ?? original.x, y ?? original.y);
    }
}