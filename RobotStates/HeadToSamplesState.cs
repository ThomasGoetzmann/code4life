using Target = Targets.Target;


public class HeadToSamplesState : IRobotState
{
    public IRobotState Execute(Robot robot)
    {
        robot.GoTo(Target.Samples);
        if (!robot.IsMoving)
            return robot.DownloadSampleState;
        

        return this;
    }
}