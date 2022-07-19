using UnityEngine;

public class GameActionPaymentHandler
{
    public void Initialise()
    {
        GameFlowManager.Instance.HireWorkerEvent += OnHireWorkerEvent;
        GameFlowManager.Instance.ExtendWorkerContractEvent += OnExtendWorkerContractEvent;
        GameFlowManager.Instance.BribeWorkerEvent += OnBribeWorkerEvent;
    }

    public void OnHireWorkerEvent(object sender, HireWorkerEvent e)
    {
        if (e.Employer == PlayerNumber.None) return;

        if (e.EventTriggerSourceType == EventTriggerSourceType.Forced) return;

        Player player = PlayerManager.Instance.Players[e.Employer];
        player.SetGold(player.Gold.Value - TempConfiguration.HireWorkingFee);
    }

    public void OnExtendWorkerContractEvent(object sender, ExtendWorkerContractEvent e)
    {
        if (e.Employer == PlayerNumber.None) return;

        if (e.EventTriggerSourceType == EventTriggerSourceType.Forced) return;

        Player player = PlayerManager.Instance.Players[e.Employer];
        player.SetGold(player.Gold.Value - TempConfiguration.ExtendWorkerContractFee);
    }

    public void OnBribeWorkerEvent(object sender, BribeWorkerEvent e)
    {
        if (e.Employer == PlayerNumber.None) return;

        if (e.EventTriggerSourceType == EventTriggerSourceType.Forced) return;

        Player player = PlayerManager.Instance.Players[e.Employer];
        player.SetGold(player.Gold.Value - TempConfiguration.BribeWorkerGoldFee);
        player.SetReputation(player.Reputation.Value - TempConfiguration.BribeWorkerReputationFee);
    }
}
