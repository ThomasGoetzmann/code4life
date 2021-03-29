using System.Linq;

public class DownloadSampleState : IRobotState
{
    public IRobotState Execute(Robot robot)
    {
        var expertiseScore = robot.Expertise.Sum(e => e.Value);
        if (expertiseScore < 10)
        {
            robot.DownloadSample(Sample.Ranks.One);
        }
        else if (expertiseScore < 20)
        {
            robot.DownloadSample(Sample.Ranks.Two);
        }
        else
        {
            robot.DownloadSample(Sample.Ranks.Three);
        }

        robot.UpdateDownloadedSamples();
        if (robot.OwnedSamples.Count + 1 >= Robot.SAMPLE_MAX)
            return robot.HeadToDiagnosisState;

        return this;
    }
}