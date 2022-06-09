
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonumentDisplay : MonoBehaviour
{
    public Player Player { get; private set; }
    List<MonumentDisplayComponent> MonumentDisplayComponents = new List<MonumentDisplayComponent>();

    private void Awake()
    {
        MonumentDisplayComponents.Clear();
    }

    public void CreateMonumentComponents()
    {
        CreateMonumentComponent(MonumentComponentType.FirstFloor);
    }

    private void CreateMonumentComponent(MonumentComponentType monumentComponentType)
    {
        GameObject prefab = AssetManager.Instance.GetMonumentComponentPrefab(monumentComponentType);
        GameObject componentGO = GameObject.Instantiate(prefab, transform);
        MonumentDisplayComponent monumentDisplayComponent = componentGO.GetComponent<MonumentDisplayComponent>();
        monumentDisplayComponent.SetMonumentComponentType(monumentComponentType);
    }

    public void SetPlayer(Player player)
    {
        Player = player;
    }

    public void AddToMonumentDisplayComponents(MonumentDisplayComponent monumentDisplayComponent)
    {

        Debug.Log($"added monumentDisplayComponent of type {monumentDisplayComponent.MonumentComponentType}");
        MonumentDisplayComponents.Add(monumentDisplayComponent);
    }

    public void UpdateComponentVisibility()
    {
        List<MonumentComponent> monumentComponents = Player.Monument.GetMonumentComponents();

        // go over all components and check if the component is completed for the player
        for (int i = 0; i < monumentComponents.Count; i++)
        {

            Debug.Log($"Check component visibility for {monumentComponents[i].Name}.");
            //if (monumentComponents[i].Complete)
            //{
            //    ShowComponent();
            //}
            //else
            //{
            //    HideComponent();
            //}
        }
    }

    public void ShowComponent(MonumentComponent monumentComponent)
    {
        MonumentDisplayComponent monumentDisplayComponent = GetMonumentDisplayComponent(monumentComponent);
        monumentDisplayComponent.gameObject.SetActive(true);
    }

    public void HideComponent(MonumentComponent monumentComponent)
    {
        MonumentDisplayComponent monumentDisplayComponent = GetMonumentDisplayComponent(monumentComponent);
        monumentDisplayComponent.gameObject.SetActive(false);
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
