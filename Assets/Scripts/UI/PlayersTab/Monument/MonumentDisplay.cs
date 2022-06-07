
using UnityEngine;

public class MonumentDisplay : MonoBehaviour
{
    public Player Player { get; private set; }

    public void SetPlayer(Player player)
    {
        Player = player;
    }
}
