using System;
using UnityEngine;

[Serializable]
public class Skill
{
    public string Id;
    public string Label;
    public int StartingValue = 0;
    [Tooltip("-1 For Skills with no applicable Max")]
    public int MaxValue = -1;

    public bool HasMax
    {
        get { return MaxValue != -1; }
    }
}

[Serializable]
public class SkillRequirement
{
    public string SkillId;
    public Comparator Comparator;
    public int Level;
}

public class SkillLevel
{
    public Skill Skill { get; private set; }
    public int Level;

    public SkillLevel(Skill skill)
    {
        Skill = skill;
        Level = skill.StartingValue;
    }

    public bool ReachedMax
    {
        get { return Skill.HasMax && Level >= Skill.MaxValue; }
    }
}