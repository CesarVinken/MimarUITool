using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameActionWorkerSelectionTileElement : MonoBehaviour, IGameActionElement
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _titleLabel;
    [SerializeField] private TextMeshProUGUI _costsLabel;
    private PickWorkerGameActionStep _uiToolGameActionStep;
    [SerializeField] protected Image _tileBackground;
    [SerializeField] protected Image _workerIcon;

    public IWorker Worker { get; private set; }
    public WorkerActionType WorkerActionType { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    private Player _player;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void Awake()
    {
        if (_button == null)
        {
            Debug.LogError($"could not find button on {gameObject.name}");
        }
        if (_titleLabel == null)
        {
            Debug.LogError($"could not find _titleLabel on {gameObject.name}");
        }
        if (_costsLabel == null)
        {
            Debug.LogError($"could not find _costsLabel on {gameObject.name}");
        }
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void SetUp(IWorker worker, PickWorkerGameActionStep pickWorkerGameActionStep, WorkerActionType workerActionType)
    {
        Worker = worker;
        WorkerActionType = workerActionType;
        _uiToolGameActionStep = pickWorkerGameActionStep;

        _player = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.Player;
    }

    public void Initialise(IGameActionStep uiToolGameActionStep)
    {
        SetTitleLabel();
        SetCostsLabel();
        SetWorkerIcon();
    }

    private void SetTitleLabel()
    {
        _titleLabel.text = $"Worker";
    }

    private void SetCostsLabel()
    {
        string costsString = "";
        List<IAccumulativePlayerStat> costs = GetCosts();

        for (int i = 0; i < costs.Count; i++)
        {
            IAccumulativePlayerStat playerStat = _player.GetPlayerStat(costs[i]);
            if (playerStat.Value < Math.Abs(costs[i].Value))
            {
                costsString += $"<color={ColourUtility.GetHexadecimalColour(ColourType.ErrorRed)}>{Math.Abs(costs[i].Value)}</color> {costs[i].InlineIcon} ";
            }
            else
            {
                costsString += $"{Math.Abs(costs[i].Value)} {costs[i].InlineIcon} ";
            }
        }

        _costsLabel.text = costsString;
    }

    private List<IAccumulativePlayerStat> GetCosts()
    {
        switch (WorkerActionType)
        {
            case WorkerActionType.Bribe:
                return TempConfiguration.BribeWorkerFee;
            case WorkerActionType.ExtendContract:
                return TempConfiguration.ExtendWorkerContractFee;
            case WorkerActionType.Hire:
                return TempConfiguration.HireWorkingFee;
            default:
                new NotImplementedException("WorkerActionType", WorkerActionType.ToString());
                return null;
        }
    }

    private void SetWorkerIcon()
    {
        _workerIcon.sprite = AssetManager.Instance.GetWorkerIcon(Worker);

        if(Worker.Employer == PlayerNumber.None)
        {
            _workerIcon.color = ColourUtility.GetColour(ColourType.Empty);
        }
        else
        {
            Player player = PlayerManager.Instance.Players[Worker.Employer];
            _workerIcon.color = player.PlayerColour;
        }
    }

    private void OnClick()
    {
        _uiToolGameActionStep.SelectWorker(this);
    }

    public void Select()
    {
        _button.image.color = ColourUtility.GetColour(ColourType.SelectedBackground);
    }

    public void Deselect()
    {
        _button.image.color = ColourUtility.GetColour(ColourType.Empty);
    }

    public void MakeUnavailable()
    {
        _button.interactable = false;
        _button.image.color = ColourUtility.GetColour(ColourType.GrayedOut);
        IsAvailable = false;
    }

    public void MakeAvailable()
    {
        _button.interactable = true;
        IsAvailable = true;
    }
}
