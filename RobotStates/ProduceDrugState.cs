using System;
using System.Linq;

public class ProduceDrugState : IRobotState
{
    public IRobotState Execute(Robot robot)
    {
        var id = robot.OwnedSamples.First().Key;
        Console.WriteLine($"CONNECT {id}");
        robot.OwnedSamples.Remove(id);

        return robot.HeadToSamplesState;
    }
}