
using System;

public class Player
{
    static void Main(string[] args)
    {
        // Console.Error.WriteLine(Environment.Version); //Shows the dotnet runtime version used by Codingame if needed.
        var projects = GetScientificProjects();
        var storage = new Storage();
        var myRobot = new Robot(storage);
        var opponentRobot = new Robot(storage);

        // game loop
        while (true)
        {
            // Read all information coming from the inputs and update the game status
            myRobot.StatusUpdate();
            opponentRobot.StatusUpdate();

            storage.StatusUpdate();

            var samples = GetSamples();
            myRobot.SamplesUpdate(samples);

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            myRobot.Update();
        }
    }

    static Project[] GetScientificProjects()
    {
        string[] inputs;
        int projectCount = int.Parse(Console.ReadLine());
        var projects = new Project[projectCount];
        for (int i = 0; i < projectCount; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            projects[i] = new Project
            {
                ExpertiseA = int.Parse(inputs[0]),
                ExpertiseB = int.Parse(inputs[1]),
                ExpertiseC = int.Parse(inputs[2]),
                ExpertiseD = int.Parse(inputs[3]),
                ExpertiseE = int.Parse(inputs[4]),
            };
        }
        return projects;
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