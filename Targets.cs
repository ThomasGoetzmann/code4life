using System;

public static class Targets
{
    public enum Target
    {
        Samples,
        Diagnosis,
        Molecules,
        Laboratory,
        Start_Pos
    }

    public static string ToString(this Target t)
    {
        return t switch
        {
            Target.Samples => "SAMPLES",
            Target.Diagnosis => "DIAGNOSIS",
            Target.Molecules => "MOLECULES",
            Target.Laboratory => "LABORATORY",
            Target.Start_Pos => "START_POS",
            _ => throw new ArgumentException($"Invalid target {t}")
        };
    }

    public static Target ToTarget(this string s)
    {
        return s switch
        {
            "SAMPLES" => Target.Samples,
            "DIAGNOSIS" => Target.Diagnosis,
            "MOLECULES" => Target.Molecules,
            "LABORATORY" => Target.Laboratory,
            "START_POS" => Target.Start_Pos,
            _ => throw new ArgumentException($"Invalid target {s}")
        };
    }
}