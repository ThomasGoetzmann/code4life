using System;
using System.Linq;
using System.Collections.Generic;
using Molecule = Molecules.Type;
using Target = Targets.Target;

public class Robot
{
    public IRobotState CurrentState {get; set;}
    public IRobotState HeadToSamplesState { get; }
    public IRobotState DownloadSampleState { get; }
    public IRobotState HeadToDiagnosisState { get; }
    public IRobotState AnalyseSampleState { get; }
    public IRobotState HeadToMoleculesState { get; }
    public IRobotState GetMoleculesState { get; }
    public IRobotState HeadToLaboratoryState { get; }
    public IRobotState ProduceDrugState { get; }

    public static int SAMPLE_MAX = 3;
    public static int MOLECULES_MAX = 10;

    public int Score { get; private set; }

    public Target Target { get; private set; }
    public Storage MoleculesStorage {get; private set;}
    public Dictionary<Molecule, int> Inventory { get; private set; }
    public Dictionary<Molecule, int> Expertise { get; private set; }
    public IEnumerable<Sample> Samples {get; private set;}
    public Dictionary<int, OwnedSample> OwnedSamples { get; private set; }
    public Dictionary<Tuple<Target, Target>, int> Movements { get; }
    public int MoveDistance { get; private set; }
    public bool IsMoving => MoveDistance != 0;

    public Robot(Storage storage)
    {
        Inventory = InitializeMolecules();
        Expertise = InitializeMolecules();
        Movements = InitializeMovements();
        OwnedSamples = new Dictionary<int, OwnedSample>();
        Samples = new List<Sample>();

        HeadToSamplesState = new HeadToSamplesState();
        DownloadSampleState = new DownloadSampleState();
        HeadToDiagnosisState = new HeadToDiagnosisState();
        AnalyseSampleState = new AnalyseSampleState();
        HeadToMoleculesState = new HeadToMoleculesState();
        GetMoleculesState = new GetMoleculesState();
        HeadToLaboratoryState = new HeadToLaboratoryState();
        ProduceDrugState = new ProduceDrugState();
        CurrentState = HeadToSamplesState;
        
        MoleculesStorage = storage;
    }

    private static Dictionary<Tuple<Target, Target>, int> InitializeMovements()
    {
        return new Dictionary<Tuple<Target, Target>, int>{
                // From Start_Pos
                {new Tuple<Target,Target>(Target.Start_Pos,Target.Samples), 2},
                {new Tuple<Target,Target>(Target.Start_Pos,Target.Diagnosis), 2},
                {new Tuple<Target,Target>(Target.Start_Pos,Target.Molecules), 2},
                {new Tuple<Target,Target>(Target.Start_Pos,Target.Laboratory), 2},
                // From Samples
                {new Tuple<Target,Target>(Target.Samples,Target.Diagnosis), 3},
                {new Tuple<Target,Target>(Target.Samples,Target.Molecules), 3},
                {new Tuple<Target,Target>(Target.Samples,Target.Laboratory), 3},
                // From Diagnosis
                {new Tuple<Target,Target>(Target.Diagnosis,Target.Samples), 3},
                {new Tuple<Target,Target>(Target.Diagnosis,Target.Molecules), 3},
                {new Tuple<Target,Target>(Target.Diagnosis,Target.Laboratory), 4},
                // From Molecules
                {new Tuple<Target,Target>(Target.Molecules,Target.Samples), 3},
                {new Tuple<Target,Target>(Target.Molecules,Target.Diagnosis), 3},
                {new Tuple<Target,Target>(Target.Molecules,Target.Laboratory), 3},
                // From Laboratory
                {new Tuple<Target,Target>(Target.Laboratory,Target.Samples), 3},
                {new Tuple<Target,Target>(Target.Laboratory,Target.Diagnosis), 4},
                {new Tuple<Target,Target>(Target.Laboratory,Target.Molecules), 3},
            };
    }

    private Dictionary<Molecule, int> InitializeMolecules() => new Dictionary<Molecule, int>
            {
                {Molecule.A, 0},
                {Molecule.B, 0},
                {Molecule.C, 0},
                {Molecule.D, 0},
                {Molecule.E, 0},
            };


    public void StatusUpdate()
    {
        //ToDo: not handled yet
        var inputs = Console.ReadLine().Split(' ');
        Target = inputs[0].ToTarget();
        int eta = int.Parse(inputs[1]);

        //Update general information
        Score = int.Parse(inputs[2]);

        // Update Inventory
        Inventory[Molecule.A] = int.Parse(inputs[3]);
        Inventory[Molecule.B] = int.Parse(inputs[4]);
        Inventory[Molecule.C] = int.Parse(inputs[5]);
        Inventory[Molecule.D] = int.Parse(inputs[6]);
        Inventory[Molecule.E] = int.Parse(inputs[7]);

        // Update Expertise
        Expertise[Molecule.A] = int.Parse(inputs[8]);
        Expertise[Molecule.B] = int.Parse(inputs[9]);
        Expertise[Molecule.C] = int.Parse(inputs[10]);
        Expertise[Molecule.D] = int.Parse(inputs[11]);
        Expertise[Molecule.E] = int.Parse(inputs[12]);
    }

    public void SamplesUpdate(IEnumerable<Sample> samples)
    {
        Samples = samples;
    }

    public void GoTo(Target t)
    {
        if (!IsMoving)
        {
            var path = new Tuple<Target, Target>(Target, t);
            MoveDistance = Movements[path];
        }

        if (MoveDistance == 0)
        {
            Wait();
            return;
        }

        Console.WriteLine($"GOTO {t.ToString()}");
        MoveDistance--;
    }

    public void Wait()
    {
        Console.WriteLine("WAIT");
    }

    public void DownloadSample(Sample.Ranks rank)
    {
        Console.WriteLine($"CONNECT {(int)rank}");
    }

    public void UpdateDownloadedSamples()
    {
        OwnedSamples.Clear();
        foreach (var s in Samples.Where(s => s.IsCarriedByYou))
        {
            var ownedSample = new OwnedSample
            {
                IsDiagnosed = false,
                Molecules = new Queue<Molecule>()
            };
            OwnedSamples.Add(s.Id, ownedSample);
        }
    }

    public void AnalyseSample(int id)
    {
        Console.WriteLine($"CONNECT {id}");
        OwnedSamples[id].IsDiagnosed = true;
    }

    public void UpdateDiagnosedSamples()
    {
        foreach (var sample in Samples.Where(s => s.IsCarriedByYou))
        {
            var molecules = new Queue<Molecule>();

            for (int i = 0; i < sample.CostA - Expertise[Molecule.A]; i++)
            {
                molecules.Enqueue(Molecule.A);
            }
            for (int i = 0; i < sample.CostB - Expertise[Molecule.B]; i++)
            {
                molecules.Enqueue(Molecule.B);
            }
            for (int i = 0; i < sample.CostC - Expertise[Molecule.C]; i++)
            {
                molecules.Enqueue(Molecule.C);
            }
            for (int i = 0; i < sample.CostD - Expertise[Molecule.D]; i++)
            {
                molecules.Enqueue(Molecule.D);
            }
            for (int i = 0; i < sample.CostE - Expertise[Molecule.E]; i++)
            {
                molecules.Enqueue(Molecule.E);
            }

            OwnedSamples[sample.Id].Molecules = molecules;
        }
    }

    public void GetMolecule(Molecule m)
    {
        if (MoleculesStorage.AvailableCount(m) > 0)
        {
            Console.WriteLine($"CONNECT {m.ToChar()}");
        }
        else
        {
            Wait();
        }
    }

    public void Update()
    {
        CurrentState = CurrentState.Execute(this);
    }
}