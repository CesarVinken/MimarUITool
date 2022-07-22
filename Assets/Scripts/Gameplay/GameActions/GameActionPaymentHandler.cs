using UnityEngine;

public class GameActionPaymentHandler
{
    public void Initialise()
    {
        GameFlowManager.Instance.HireWorkerEvent += OnHireWorkerEvent;
        GameFlowManager.Instance.ExtendWorkerContractEvent += OnExtendWorkerContractEvent;
        GameFlowManager.Instance.BribeWorkerEvent += OnBribeWorkerEvent;
        GameFlowManager.Instance.MakeSacrificeEvent += OnMakeSacrificeEvent;
    }

    public void OnHireWorkerEvent(object sender, HireWorkerEvent e)
    {
        if (e.Employer == PlayerNumber.None) return;

        if (e.EventTriggerSourceType == EventTriggerSourceType.Forced) return;

        Player player = PlayerManager.Instance.Players[e.Employer];

        for (int i = 0; i < TempConfiguration.HireWorkingFee.Count; i++)
        {
            IAccumulativePlayerStat playerStat = player.GetPlayerStat(TempConfiguration.HireWorkingFee[i]);
            playerStat.AddValue(TempConfiguration.HireWorkingFee[i].Value);
        }
    }

    public void OnExtendWorkerContractEvent(object sender, ExtendWorkerContractEvent e)
    {
        if (e.Employer == PlayerNumber.None) return;

        if (e.EventTriggerSourceType == EventTriggerSourceType.Forced) return;

        Player player = PlayerManager.Instance.Players[e.Employer];

        for (int i = 0; i < TempConfiguration.ExtendWorkerContractFee.Count; i++)
        {
            IAccumulativePlayerStat playerStat = player.GetPlayerStat(TempConfiguration.ExtendWorkerContractFee[i]);
            playerStat.AddValue(TempConfiguration.ExtendWorkerContractFee[i].Value);
        }
    }

    public void OnBribeWorkerEvent(object sender, BribeWorkerEvent e)
    {
        if (e.Employer == PlayerNumber.None) return;

        if (e.EventTriggerSourceType == EventTriggerSourceType.Forced) return;

        Player player = PlayerManager.Instance.Players[e.Employer];
        for (int i = 0; i < TempConfiguration.BribeWorkerFee.Count; i++)
        {
            IAccumulativePlayerStat playerStat = player.GetPlayerStat(TempConfiguration.BribeWorkerFee[i]);
            playerStat.AddValue(TempConfiguration.BribeWorkerFee[i].Value);
        }
    }

    public void OnMakeSacrificeEvent(object sender, MakeSacrificeEvent e)
    {
        if (e.PlayerNumber == PlayerNumber.None) return;

        Player player = PlayerManager.Instance.Players[e.PlayerNumber];
        for (int i = 0; i < TempConfiguration.MakeSacrificeTransaction.Count; i++)
        {
            IAccumulativePlayerStat playerStat = player.GetPlayerStat(TempConfiguration.MakeSacrificeTransaction[i]);
            playerStat.AddValue(TempConfiguration.MakeSacrificeTransaction[i].Value);
        }
    }
}
