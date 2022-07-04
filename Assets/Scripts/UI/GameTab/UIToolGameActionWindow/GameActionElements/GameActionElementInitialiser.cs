using UnityEngine;

public class GameActionElementInitialiser
{
    public static IUIToolGameActionElement InitialiseTitleLabel(IUIToolGameActionStep uiToolGameActionStep)
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

    public static IUIToolGameActionElement InitialiseMainContentLabel(IUIToolGameActionStep uiToolGameActionStep, string contentText)
    {
        GameObject mainContentLabelPrefab = UIToolGameActionAssetHandler.Instance.GetMainContentLabelPrefab();
        GameObject mainContentLabelGO = GameObject.Instantiate(mainContentLabelPrefab);
        GameActionMainContentTextBlockElement mainContentLabelElement = mainContentLabelGO.GetComponent<GameActionMainContentTextBlockElement>();

        if (mainContentLabelElement == null)
        {
            Debug.LogError($"could not find mainContentLabelElement script");
        }

        mainContentLabelElement.Setup(contentText);
        mainContentLabelElement.Initialise(uiToolGameActionStep);
        return mainContentLabelElement;
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

    public static GameActionActionSelectionTileElement InitialiseActionSelectionTile(ActionPickStep actionPickStep, UIToolGameActionType gameActionType)
    {
        GameObject actionSelectionTileElementPrefab = UIToolGameActionAssetHandler.Instance.GetActionSelectionTilePrefab();
        GameObject actionSelectionTileElementGO = GameObject.Instantiate(actionSelectionTileElementPrefab);

        GameActionActionSelectionTileElement actionSelectionTileElement = actionSelectionTileElementGO.GetComponent<GameActionActionSelectionTileElement>();

        if (actionSelectionTileElement == null)
        {
            Debug.LogError($"could not find actionSelectionTileElement script");
        }

        actionSelectionTileElement.SetUp(gameActionType, actionPickStep);
        actionSelectionTileElement.Initialise(actionPickStep);
        return actionSelectionTileElement;
    }
}