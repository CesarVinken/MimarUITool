using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseResource : IResource
{
    public int Amount { get; protected set; }

    public void SetAmount(int newAmount)
    {
        Amount = newAmount;
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
    }
}