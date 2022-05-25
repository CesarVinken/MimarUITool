using UnityEngine;

public class CityWorkerTile : WorkerTile
{
    private void Awake()
    {
        if (_tileBackground == null)
        {
            Debug.LogError($"Could not find _tileBackground");
        }
        if (_statusText == null)
        {
            Debug.LogError($"Could not find _statusText");
        }
        if (_serviceLengthInputField == null)
        {
            Debug.LogError($"Could not find _serviceLengthInputField");
        }

        _serviceLengthInputField.onValueChanged.AddListener(delegate { OnChangeServiceLengthInputField(); });
    }

    public override void Initialise(LocationType locationType, IWorker worker)
    {
        _locationType = locationType;

        Worker = worker;
        Worker.SetUIWorkerTile(this);
        UpdateServiceLength(3);

        switch (_locationType)
        {
            case LocationType.ConstructionSite1:
                SetEmployer(PlayerNumber.Player1);
                break;
            case LocationType.ConstructionSite2:
                SetEmployer(PlayerNumber.Player2);
                break;
            case LocationType.ConstructionSite3:
                SetEmployer(PlayerNumber.Player3);
                break;
            default:
                SetEmployer(PlayerNumber.None);
                break;
        }

        SetTileColour(Worker.Employer);
    }

    public void SetTileColour(PlayerNumber playerNumber)
    {
        if (PlayerManager.Instance.Players.TryGetValue(playerNumber, out Player player))
        {
            _tileBackground.color = player.PlayerColour;
        }
        else
        {
            _tileBackground.color = ColourUtility.GetColour(ColourType.Empty);
        }
    }

    protected override void SetWorkerToNeutral()
    {
        MonumentLocationUIContainer constructionSiteContainer = NavigationManager.Instance.GetMonumentLocationUIContainer(Worker.Location.LocationType);

        constructionSiteContainer.RemoveWorkerFromSite(this);
    }
}
