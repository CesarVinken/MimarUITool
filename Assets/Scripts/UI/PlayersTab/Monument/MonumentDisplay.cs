using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonumentDisplay : MonoBehaviour
{
    public Monument Monument { get; private set; }
    public Player Player { get; private set; }
    List<MonumentDisplayComponent> MonumentDisplayComponents = new List<MonumentDisplayComponent>();
    private int _componentsToInitialise = 0;

    public void CreateMonumentComponents()
    {
        CreateMonumentComponent(MonumentComponentType.FloorFirst);
        CreateMonumentComponent(MonumentComponentType.FloorSecond);
        CreateMonumentComponent(MonumentComponentType.FloorThird);
        CreateMonumentComponent(MonumentComponentType.Arches);
        CreateMonumentComponent(MonumentComponentType.Dome);
        CreateMonumentComponent(MonumentComponentType.GroundPlane);
        CreateMonumentComponent(MonumentComponentType.OuterWalls);
        CreateMonumentComponent(MonumentComponentType.TowersBack);
        CreateMonumentComponent(MonumentComponentType.TowersFront);
        CreateMonumentComponent(MonumentComponentType.TowersMiddle);
    }

    private void CreateMonumentComponent(MonumentComponentType monumentComponentType)
    {
        _componentsToInitialise++;
        AssetManager.Instance.InstantiateMonumentComponent(this, monumentComponentType);
    }

    public void OnMonumentComponentAssetLoaded()
    {
        _componentsToInitialise--;

        if(_componentsToInitialise == 0)
        {
            PlayersTabContainer playersTabContainer = NavigationManager.Instance.GetMainTabContainer(MainTabType.PlayersTab) as PlayersTabContainer;
            playersTabContainer.OnInitialisationFinished();
        }
    }

    public void SetPlayer(Player player)
    {
        Player = player;
        SetMonument(player);
    }

    private void SetMonument(Player player)
    {
        Monument = player.Monument;
    }

    public void AddToMonumentDisplayComponents(MonumentDisplayComponent monumentDisplayComponent)
    {
        MonumentDisplayComponents.Add(monumentDisplayComponent);
    }

    public void UpdateComponentsVisibility()
    {
        List<MonumentComponent> monumentComponents = Player.Monument.GetMonumentComponents();

        // go over all components and check if the component is completed for the player
        for (int i = 0; i < monumentComponents.Count; i++)
        {
            if (monumentComponents[i].State == MonumentComponentState.Complete)
            {
                SetComponentVisibility(monumentComponents[i], MonumentComponentVisibility.Complete);
            }
            else if(monumentComponents[i].State == MonumentComponentState.InProgress)
            {
                SetComponentVisibility(monumentComponents[i], MonumentComponentVisibility.InProgress);
            }
            else
            {
                SetComponentVisibility(monumentComponents[i], MonumentComponentVisibility.Hidden);
            }
        }
    }

    public void SetComponentVisibility(MonumentComponent monumentComponent, MonumentComponentVisibility visibility)
    {
        MonumentDisplayComponent monumentDisplayComponent = GetMonumentDisplayComponent(monumentComponent);

        switch (visibility)
        {
            case MonumentComponentVisibility.Hidden:
                monumentDisplayComponent.gameObject.SetActive(false);
                break;
            case MonumentComponentVisibility.InProgress:

                Debug.Log($"Todo: set different material");
                monumentDisplayComponent.gameObject.SetActive(true);
                break;
            case MonumentComponentVisibility.Complete:
                monumentDisplayComponent.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private MonumentDisplayComponent GetMonumentDisplayComponent(MonumentComponent monumentComponent)
    {
        MonumentDisplayComponent monumentDisplayComponent = MonumentDisplayComponents.FirstOrDefault(c => c.MonumentComponentType.Equals(monumentComponent.MonumentComponentType));

        if(monumentDisplayComponent == null)
        {
            Debug.LogError($"Could not find a monumentDisplayComponent of type {monumentComponent.MonumentComponentType}");
        }

        return monumentDisplayComponent;
    }
}
