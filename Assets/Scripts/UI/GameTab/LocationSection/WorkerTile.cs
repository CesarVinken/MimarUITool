using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class WorkerTile : MonoBehaviour
{
    public IWorker Worker;
    protected LocationType _locationType = LocationType.Rome;

    [SerializeField] protected Image _tileBackground;
    [SerializeField] protected Image _workerIcon;

    [SerializeField] protected TMP_InputField _serviceLengthInputField;
    protected int _contractLength = 0;

    // called when changing the input field
    public void OnChangeServiceLengthInputField()
    {
        if (GameActionStepHandler.CurrentGameActionSequence != null) return;

        int newContractLength = 1;

        if (int.TryParse(_serviceLengthInputField.text, out int result))
        {
            newContractLength = result;
        }

        UpdateServiceLength(newContractLength);    
    }

    public PlayerNumber GetNextEmployer()
    {
        switch (Worker.Employer)
        {
            case PlayerNumber.None:
                return PlayerNumber.Player1;
            case PlayerNumber.Player1:
                return PlayerNumber.Player2;
            case PlayerNumber.Player2:
                return PlayerNumber.Player3;
            case PlayerNumber.Player3:
            default:
                return PlayerNumber.None;
        }
    }

    public void UpdateServiceLength(int newContractLength)
    {
        // The user should not be allowed to set a contract to anything less than 1 in the input field. Because 0 would effectively mean the contract has ended, and should assign the employer too None.
        if (newContractLength <= 0)
        {
            newContractLength = 1;
        }

        _serviceLengthInputField.text = newContractLength.ToString();
        _contractLength = newContractLength;
        Worker.SetServiceLength(newContractLength);
    }

    public void SubtractServiceLength()
    {
        int newContractLength = Worker.ServiceLength - 1;

        if (newContractLength <= 0)
        {
            SetWorkerToNeutral();
        }
        else
        {
            UpdateServiceLength(newContractLength);
        }
    }

    protected abstract void SetWorkerToNeutral();

    public abstract void Initialise(LocationType locationType, IWorker worker);

    public abstract void SetEmployer(PlayerNumber newEmployer);

    public abstract void Destroy();
}
