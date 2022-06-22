
using UnityEngine;

public class CityWorker : IWorker
{
    public PlayerNumber Employer { get; private set; } = PlayerNumber.None;
    public int ServiceLength { get; private set; } = 0;
    public ILocation Location { get; private set; }
    public WorkerTile UIWorkerTile { get; private set; }
    public MonumentComponent CurrentBuildingTask { get; private set; } = null;
    public float BaseProductionPower { get; private set; } = 1;

    public CityWorker(ILocation location)
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
        if (playerNumber == Employer) return;

        if(Employer != PlayerNumber.None)
        {
            Player oldEmployerPlayer = PlayerManager.Instance.Players[Employer];
            oldEmployerPlayer.RemoveWorker(this);
        }

        Employer = playerNumber;

        if (playerNumber != PlayerNumber.None)
        {
            Player newEmployerPlayer = PlayerManager.Instance.Players[Employer];
            newEmployerPlayer.AddWorker(this);
        }
    }

    public void SetServiceLength(int newValue)
    {
        ServiceLength = newValue;
    }

    public void SetCurrentBuildingTask(MonumentComponent monumentComponent)
    {
        CurrentBuildingTask = monumentComponent;
    }

    public void Build()
    {
        Debug.Log($"build");

        CurrentBuildingTask.UpdateRemainingLabourTime(-BaseProductionPower);

        if (CurrentBuildingTask.IsComplete)
        {
            PlayersTabContainer playersTabContainer = NavigationManager.Instance.GetMainTabContainer(MainTabType.PlayersTab) as PlayersTabContainer;
            Debug.Log($"CurrentPlayerTab {playersTabContainer.CurrentPlayerTab.PlayerNumber}");
            if (playersTabContainer.CurrentPlayerTab.PlayerNumber == Employer)
            {
                Debug.Log($"Update display for {Employer}");
                playersTabContainer.UpdateMonumentDisplay();
            }
            GameFlowManager.Instance.ExecuteMonumentComponentCompletionEvent(Employer, CurrentBuildingTask, true);
        }
    }
}
