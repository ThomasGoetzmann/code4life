using System.Linq;

public class GetMoleculesState : IRobotState
{
    public IRobotState Execute(Robot robot)
    {
        var sample = robot.OwnedSamples.First().Value;
        robot.GetMolecule(sample.Molecules.Dequeue());

        if (sample.Molecules.Count == 0)
            return robot.HeadToLaboratoryState;

        return this;
    }
}