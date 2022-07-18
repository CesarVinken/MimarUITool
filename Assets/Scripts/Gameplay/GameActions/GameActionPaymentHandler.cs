using UnityEngine;

public class GameActionPaymentHandler : MonoBehaviour
{
    public void Initialise()
    {
        GameFlowManager.Instance.HireWorkerEvent += OnHireWorkerEvent;
        GameFlowManager.Instance.ExtendWorkerContractEvent += OnExtendWorkerContractEvent;
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
}
