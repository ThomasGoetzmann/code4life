using Target = Targets.Target;

public class HeadToDiagnosisState : IRobotState
{
   public IRobotState Execute(Robot robot) 
   {
        robot.UpdateDownloadedSamples();
        robot.GoTo(Target.Diagnosis);
        
        if (!robot.IsMoving)
            return robot.AnalyseSampleState;

        return this;
   }
}