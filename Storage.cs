using System;
using System.Collections.Generic;
using Molecule = Molecules.Type;

public class Storage : Dictionary<Molecule, int>
{
    private Dictionary<Molecule, int> storage;

    public Storage()
    {
        storage = Initialize();
    }

    private Dictionary<Molecule, int> Initialize()
    {
        return new Dictionary<Molecule, int>
        {
            {Molecule.A, 0},
            {Molecule.B, 0},
            {Molecule.C, 0},
            {Molecule.D, 0},
            {Molecule.E, 0},
        };
    }

    public void StatusUpdate()
    {
        var inputs = Console.ReadLine().Split(' ');
        storage[Molecule.A] = int.Parse(inputs[0]);
        storage[Molecule.B] = int.Parse(inputs[1]);
        storage[Molecule.C] = int.Parse(inputs[2]);
        storage[Molecule.D] = int.Parse(inputs[3]);
        storage[Molecule.E] = int.Parse(inputs[4]);
    }

    public int AvailableCount(Molecule m)
    {
        return storage[m];
    }
}