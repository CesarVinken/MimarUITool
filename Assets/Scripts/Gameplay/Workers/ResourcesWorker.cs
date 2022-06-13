using UnityEngine;
public class ResourcesWorker : IWorker
{
    public PlayerNumber Employer { get; private set; } = PlayerNumber.None;
    public int ServiceLength { get; private set; }
    public ILocation Location { get; private set; }
    public WorkerTile UIWorkerTile { get; private set; }
    public float BaseProductionPower { get; private set; } = 1;

    public ResourcesWorker(ILocation location)
    {
        Employer = PlayerNumber.None;
        Location = location;
    }

    public void SetUIWorkerTile(WorkerTile uiWorkerTile)
    {
        UIWorkerTile = uiWorkerTile;
    }

    public void SetEmployer(PlayerNumber playerNumber)
    {
        if (Employer != PlayerNumber.None)
        {
            Player oldEmployerPlayer = PlayerManager.Instance.Players[Employer];
            oldEmployerPlayer.RemoveWorker(this);
        }

        Employer = playerNumber;

        if(playerNumber != PlayerNumber.None)
        {
            Player newEmployerPlayer = PlayerManager.Instance.Players[Employer];
            newEmployerPlayer.AddWorker(this);
        }
    }

    public void SetServiceLength(int newValue)
    {
        ServiceLength = newValue;
    }
}
