public interface IWorker
{
    public PlayerNumber Employer { get; }
    public int ServiceLength { get; }
    public ILocation Location { get; }
    public WorkerTile UIWorkerTile { get; }

    public void SetEmployer(PlayerNumber playerNumber);
    public void SetServiceLength(int newValue);

    public void SetUIWorkerTile(WorkerTile workerTile);

}