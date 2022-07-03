using UnityEngine;

public class GameActionElementInitaliser
{
    public static IUIToolGameActionElement InitialiseLabel(IUIToolGameActionStep uiToolGameActionStep)
    {
        GameObject stepLabelPrefab = UIToolGameActionAssetHandler.Instance.GetStepLabelPrefab();
        GameObject stepLabelGO = GameObject.Instantiate(stepLabelPrefab);
        IUIToolGameActionElement stepLabelElement = stepLabelGO.GetComponent<IUIToolGameActionElement>();

        if (stepLabelElement == null)
        {
            Debug.LogError($"could not find stepLabelElement script");
        }

        stepLabelElement.Initialise(uiToolGameActionStep);
        return stepLabelElement;
    }

    public static IUIToolGameActionElement InitialiseNextStepButton(IUIToolGameActionStep uiToolGameActionStep)
    {
        GameObject nextStepButtonPrefab = UIToolGameActionAssetHandler.Instance.GetNextActionStepButtonPrefab();
        GameObject nextStepButtonGO = GameObject.Instantiate(nextStepButtonPrefab);

        IUIToolGameActionElement nextStepButton = nextStepButtonGO.GetComponent<IUIToolGameActionElement>();

        if (nextStepButton == null)
        {
            Debug.LogError($"could not find nextStepButton script on nextStepButton");
        }

        nextStepButton.Initialise(uiToolGameActionStep);
        return nextStepButton;
    }

    public static IUIToolGameActionElement InitialisePlayerSelectionTile(IUIToolGameActionStep uiToolGameActionStep)
    {
        GameObject playerSelectionTileElementPrefab = UIToolGameActionAssetHandler.Instance.GetPlayerSelectionTilePrefab();
        GameObject playerSelectionTileElementGO = GameObject.Instantiate(playerSelectionTileElementPrefab);

        IUIToolGameActionElement playerSelectionTileElement = playerSelectionTileElementGO.GetComponent<IUIToolGameActionElement>();

        if (playerSelectionTileElement == null)
        {
            Debug.LogError($"could not find playerSelectionTileElement script");
        }

        playerSelectionTileElement.Initialise(uiToolGameActionStep);
        return playerSelectionTileElement;
    }

    public static IUIToolGameActionElement InitialiseActionSelectionTile(IUIToolGameActionStep uiToolGameActionStep, UIToolGameActionType gameActionType)
    {
        GameObject actionSelectionTileElementPrefab = UIToolGameActionAssetHandler.Instance.GetActionSelectionTilePrefab();
        GameObject actionSelectionTileElementGO = GameObject.Instantiate(actionSelectionTileElementPrefab);

        GameActionActionSelectionTileElement actionSelectionTileElement = actionSelectionTileElementGO.GetComponent<GameActionActionSelectionTileElement>();

        if (actionSelectionTileElement == null)
        {
            Debug.LogError($"could not find actionSelectionTileElement script");
        }

        actionSelectionTileElement.SetUp(gameActionType);
        actionSelectionTileElement.Initialise(uiToolGameActionStep);
        return actionSelectionTileElement;
    }
}