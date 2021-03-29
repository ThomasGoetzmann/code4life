using Target = Targets.Target;

public class HeadToMoleculesState : IRobotState 
{
    public IRobotState Execute(Robot robot)
    {
        robot.GoTo(Target.Molecules);
        if (!robot.IsMoving)
        {
            robot.UpdateDiagnosedSamples();
            return robot.GetMoleculesState;
        }

        return this;
    }
}