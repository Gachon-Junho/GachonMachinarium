using System;
using System.Drawing;
using UnityEngine;

public static class Interpolation
{
    public static byte ValueAt(double time, byte val1, byte val2, double startTime, double endTime, in EasingFunction easing)
        => (byte)Math.Round(ValueAt(time, (double)val1, val2, startTime, endTime, easing));

    public static sbyte ValueAt(double time, sbyte val1, sbyte val2, double startTime, double endTime, in EasingFunction easing)
        => (sbyte)Math.Round(ValueAt(time, (double)val1, val2, startTime, endTime, easing));

    public static short ValueAt(double time, short val1, short val2, double startTime, double endTime, in EasingFunction easing)
        => (short)Math.Round(ValueAt(time, (double)val1, val2, startTime, endTime, easing));

    public static ushort ValueAt(double time, ushort val1, ushort val2, double startTime, double endTime, in EasingFunction easing)
        => (ushort)Math.Round(ValueAt(time, (double)val1, val2, startTime, endTime, easing));

    public static int ValueAt(double time, int val1, int val2, double startTime, double endTime, in EasingFunction easing)
        => (int)Math.Round(ValueAt(time, (double)val1, val2, startTime, endTime, easing));

    public static uint ValueAt(double time, uint val1, uint val2, double startTime, double endTime, in EasingFunction easing)
        => (uint)Math.Round(ValueAt(time, (double)val1, val2, startTime, endTime, easing));

    public static long ValueAt(double time, long val1, long val2, double startTime, double endTime, in EasingFunction easing)
        => (long)Math.Round(ValueAt(time, (double)val1, val2, startTime, endTime, easing));

    public static ulong ValueAt(double time, ulong val1, ulong val2, double startTime, double endTime, in EasingFunction easing)
        => (ulong)Math.Round(ValueAt(time, (double)val1, val2, startTime, endTime, easing));

    public static float ValueAt(double time, float val1, float val2, double startTime, double endTime, in EasingFunction easing)
        => (float)ValueAt(time, (double)val1, val2, startTime, endTime, easing);

    public static decimal ValueAt(double time, decimal val1, decimal val2, double startTime, double endTime, in EasingFunction easing)
        => (decimal)ValueAt(time, (double)val1, (double)val2, startTime, endTime, easing);

    public static double ValueAt(double time, double val1, double val2, double startTime, double endTime, in EasingFunction easing)
    {
        if (val1 == val2)
            return val1;

        double current = time - startTime;
        double duration = endTime - startTime;

        if (current == 0)
            return val1;
        if (duration == 0)
            return val2;

        double t = easing.ApplyEasing(current / duration);
        return val1 + t * (val2 - val1);
    }

    public static Vector2 ValueAt(double time, Vector2 val1, Vector2 val2, double startTime, double endTime, in EasingFunction easing)
    {
        float current = (float)(time - startTime);
        float duration = (float)(endTime - startTime);

        if (duration == 0 || current == 0)
            return val1;

        float t = (float)easing.ApplyEasing(current / duration);
        return val1 + t * (val2 - val1);
    }

    public static Vector3 ValueAt(double time, Vector3 val1, Vector3 val2, double startTime, double endTime, in EasingFunction easing)
    {
        float current = (float)(time - startTime);
        float duration = (float)(endTime - startTime);

        if (duration == 0 || current == 0)
            return val1;

        float t = (float)easing.ApplyEasing(current / duration);
        return val1 + t * (val2 - val1);
    }

    public static RectangleF ValueAt(double time, RectangleF val1, RectangleF val2, double startTime, double endTime, in EasingFunction easing)
    {
        float current = (float)(time - startTime);
        float duration = (float)(endTime - startTime);

        if (duration == 0 || current == 0)
            return val1;

        float t = (float)easing.ApplyEasing(current / duration);

        return new RectangleF(
            val1.X + t * (val2.X - val1.X),
            val1.Y + t * (val2.Y - val1.Y),
            val1.Width + t * (val2.Width - val1.Width),
            val1.Height + t * (val2.X - val1.Height));
    }
    
    public static double ApplyEasing(Easing easing, double time)
        => new EasingFunction(easing).ApplyEasing(time);
}