using System;
using UnityEngine;


[Serializable]
public class Resource
{
    public string Id;
    public string Label;
    public int StartingValue = 0;
    [Tooltip("-1 For Resources with no applicable Max")]
    public int MaxValue = -1;

    public bool HasMax
    {
        get { return MaxValue != -1; }
    }
}

public class ResourceValue
{
    public Resource Resource { get; private set; }
    public int CurrentValue;

    public ResourceValue(Resource resource)
    {
        Resource = resource;
        CurrentValue = resource.StartingValue;
    }

    public bool ReachedMax
    {
        get { return Resource.HasMax && CurrentValue >= Resource.MaxValue; }
    }
}