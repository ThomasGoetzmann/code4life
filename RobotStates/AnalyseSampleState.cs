using System.Linq;

public class AnalyseSampleState : IRobotState
{
    public IRobotState Execute(Robot robot)
    {
        var undiagnosedSample = robot.OwnedSamples.First(s => !s.Value.IsDiagnosed);
        robot.AnalyseSample(undiagnosedSample.Key);
        return robot.HeadToMoleculesState;
    }
}