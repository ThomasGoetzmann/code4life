using Target = Targets.Target;

public class HeadToLaboratoryState : IRobotState
{
    public IRobotState Execute(Robot robot)
    {
        robot.GoTo(Target.Laboratory);

        if (!robot.IsMoving)
            return robot.ProduceDrugState;

        return this;
    }
}