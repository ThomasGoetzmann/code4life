
using System;
using System.Linq;
using Target = Targets.Target;



public class Player
{

    static void Main(string[] args)
    {
        Console.Error.WriteLine(Environment.Version);
        string[] inputs;
        int projectCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < projectCount; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int a = int.Parse(inputs[0]);
            int b = int.Parse(inputs[1]);
            int c = int.Parse(inputs[2]);
            int d = int.Parse(inputs[3]);
            int e = int.Parse(inputs[4]);
        }

        var myRobot = new Robot();
        var opponentRobot = new Robot();
        var storage = new Storage();

        // game loop
        while (true)
        {
            // Read all information coming from the inputs and update the game status
            myRobot.StatusUpdate();
            opponentRobot.StatusUpdate();
            storage.StatusUpdate();
            var samples = GetSamples();

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            switch (myRobot.State)
            {
                case State.HeadToSamples:
                    myRobot.GoTo(Target.Samples);
                    if (!myRobot.IsMoving)
                    {
                        myRobot.State = State.DownloadSample;
                    }
                    break;
                case State.DownloadSample:
                    myRobot.DownloadSample(Sample.Ranks.Two);
                    myRobot.UpdateDownloadedSamples(samples);
                    if (myRobot.OwnedSamples.Count + 1 >= Robot.SAMPLE_MAX)
                    {
                        myRobot.State = State.HeadToDiagnosis;
                    }
                    break;
                case State.HeadToDiagnosis:
                    myRobot.UpdateDownloadedSamples(samples);
                    myRobot.GoTo(Target.Diagnosis);
                    if (!myRobot.IsMoving)
                    {
                        myRobot.State = State.AnalyseSample;
                    }
                    break;
                case State.AnalyseSample:
                    var undiagnosedSample = myRobot.OwnedSamples.First(s => !s.Value.IsDiagnosed);
                    myRobot.AnalyseSample(undiagnosedSample.Key);
                    myRobot.State = State.HeadToMolecules;
                    break;
                case State.HeadToMolecules:
                    myRobot.GoTo(Target.Molecules);
                    if (!myRobot.IsMoving)
                    {
                        myRobot.UpdateDiagnosedSamples(samples);
                        myRobot.State = State.GetMolecules;
                    }
                    break;
                case State.GetMolecules:
                    var s = myRobot.OwnedSamples.First();
                    myRobot.GetMolecule(s.Value.Molecules.Dequeue(), storage);
                    if (s.Value.Molecules.Count == 0)
                    {
                        myRobot.State = State.HeadToLaboratory;
                    }
                    break;
                case State.HeadToLaboratory:
                    myRobot.GoTo(Target.Laboratory);
                    if (!myRobot.IsMoving)
                    {
                        myRobot.State = State.ProduceDrug;
                    }
                    break;
                case State.ProduceDrug:
                    var firstSample = myRobot.OwnedSamples.First();
                    myRobot.ProduceDrug(firstSample.Key);
                    myRobot.State = State.HeadToSamples;
                    break;
                default:
                    throw new ArgumentException("Robot is in an unhandled state.");
            }
        }
    }

    static Sample[] GetSamples()
    {
        int sampleCount = int.Parse(Console.ReadLine());
        var samples = new Sample[sampleCount];
        for (int i = 0; i < sampleCount; i++)
        {
            var sample = new Sample();
            sample.StatusUpdate();
            samples[i] = sample;
        }
        return samples;
    }
}