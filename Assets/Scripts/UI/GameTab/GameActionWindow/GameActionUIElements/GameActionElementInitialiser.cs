using UnityEngine;

public class GameActionElementInitialiser
{
    public static IGameActionElement InitialiseTitleLabel(IGameActionStep uiToolGameActionStep)
    {
        GameObject stepLabelPrefab = GameActionAssetHandler.Instance.GetStepLabelPrefab();
        GameObject stepLabelGO = GameObject.Instantiate(stepLabelPrefab);
        IGameActionElement stepLabelElement = stepLabelGO.GetComponent<IGameActionElement>();

        if (stepLabelElement == null)
        {
            Debug.LogError($"could not find stepLabelElement script");
        }

        stepLabelElement.Initialise(uiToolGameActionStep);
        return stepLabelElement;
    }

    public static IGameActionElement InitialiseMainContentLabel(IGameActionStep uiToolGameActionStep, string contentText)
    {
        GameObject mainContentLabelPrefab = GameActionAssetHandler.Instance.GetMainContentLabelPrefab();
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



    public static IGameActionElement InitialiseNextStepButton(IGameActionStep uiToolGameActionStep)
    {
        GameObject nextStepButtonPrefab = GameActionAssetHandler.Instance.GetNextActionStepButtonPrefab();
        GameObject nextStepButtonGO = GameObject.Instantiate(nextStepButtonPrefab);

        IGameActionElement nextStepButton = nextStepButtonGO.GetComponent<IGameActionElement>();

        if (nextStepButton == null)
        {
            Debug.LogError($"could not find nextStepButton script on nextStepButton");
        }

        nextStepButton.Initialise(uiToolGameActionStep);
        return nextStepButton;
    }

    public static IGameActionElement InitialisePlayerSelectionTile(IGameActionStep uiToolGameActionStep)
    {
        GameObject playerSelectionTileElementPrefab = GameActionAssetHandler.Instance.GetPlayerSelectionTilePrefab();
        GameObject playerSelectionTileElementGO = GameObject.Instantiate(playerSelectionTileElementPrefab);

        IGameActionElement playerSelectionTileElement = playerSelectionTileElementGO.GetComponent<IGameActionElement>();

        if (playerSelectionTileElement == null)
        {
            Debug.LogError($"could not find playerSelectionTileElement script");
        }

        playerSelectionTileElement.Initialise(uiToolGameActionStep);
        return playerSelectionTileElement;
    }

    public static GameActionActionSelectionTileElement InitialiseActionSelectionTile(GameActionPickStep actionPickStep, GameActionType gameActionType)
    {
        GameObject actionSelectionTileElementPrefab = GameActionAssetHandler.Instance.GetActionSelectionTilePrefab();
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