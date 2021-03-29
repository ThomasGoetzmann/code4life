using System;
using Molecule = Molecules.Type;

public static class Molecules
{
    public enum Type
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5
    }

    public static char ToChar(this Molecule m) => (char)((int)m + 64);
    public static Molecule ToMolecule(this char c)
    {
        return c switch
        {
            'A' => Molecule.A,
            'B' => Molecule.B,
            'C' => Molecule.C,
            'D' => Molecule.D,
            'E' => Molecule.E,
            _ => throw new ArgumentException("Invalid moledcule")
        };
    }
}