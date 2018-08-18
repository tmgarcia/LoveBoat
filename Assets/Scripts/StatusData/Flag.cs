using System;

[Serializable]
public class Flag
{
    public string Id;
    public string Label;
    public bool StartingValue = false;
}

[Serializable]
public class FlagRequirement
{
    public string FlagId;
    public bool Status;
}

public class FlagStatus
{
    public Flag Flag { get; private set; }
    public bool Status;

    public FlagStatus(Flag flag)
    {
        Flag = flag;
        Status = flag.StartingValue;
    }
}
