public interface IWorker : IGameActionTarget
{
    public PlayerNumber Employer { get; }
    public int ServiceLength { get; }
    public ILocation Location { get; }
    public WorkerTile UIWorkerTile { get; }
    public float BaseProductionPower { get; } // production power = how much resources the worker collects per turn, or how much labour time a worker builds in a turn.

    public void SetEmployer(PlayerNumber playerNumber);
    public void SetServiceLength(int newValue);

    public void SetUIWorkerTile(WorkerTile workerTile);

}