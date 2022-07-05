using UnityEngine;

public class NextMoveButton : MonoBehaviour
{
    // TODO: we should differentiate between NextMove and NextRound and update the ButtonText.
    public void ExecuteNextRound()
    {
        if (GameActionHandler.CurrentGameActionSequence != null) return;

        GameFlowManager.Instance.ExecuteNextGameStep();
    }
}
