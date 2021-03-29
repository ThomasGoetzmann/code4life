using System;
using System.Collections.Generic;
using Molecule = Molecules.Type;

public class OwnedSample
{
    public bool IsDiagnosed { get; set; }
    public Queue<Molecule> Molecules { get; set; }
}

public class Sample
{
    public enum Ranks
    {
        One = 1,
        Two = 2,
        Three = 3
    }
    public int Id { get; private set; }

    public int CarriedBy { get; private set; }
    public bool IsInCloud => CarriedBy == -1;
    public bool IsCarriedByYou => CarriedBy == 0;
    public bool IsCarriedByOpponent => CarriedBy == 1;

    public int CostA { get; private set; }
    public int CostB { get; private set; }
    public int CostC { get; private set; }
    public int CostD { get; private set; }
    public int CostE { get; private set; }
    public Ranks Rank { get; private set; }

    public int TotalCost => CostA + CostB + CostC + CostD + CostE;

    public void StatusUpdate()
    {
        var inputs = Console.ReadLine().Split(' ');
        Id = int.Parse(inputs[0]);
        CarriedBy = int.Parse(inputs[1]);
        Rank = (Ranks)int.Parse(inputs[2]);
        string expertiseGain = inputs[3];
        int health = int.Parse(inputs[4]);
        CostA = int.Parse(inputs[5]);
        CostB = int.Parse(inputs[6]);
        CostC = int.Parse(inputs[7]);
        CostD = int.Parse(inputs[8]);
        CostE = int.Parse(inputs[9]);
    }
}