using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionDefinition
{
    public string Id;
    public string Label;
    public int Priority;
    public Sprite Icon;
    public ActionLocation Location = ActionLocation.OutsideCamp;
    [EnumFlag] public TimeOfDay RequiredTimeOfDay = TimeOfDay.Any;
    [EnumFlag] public Weather RequiredWeather = Weather.Any;
    public List<ResourceRequirement> RequiredResources = new List<ResourceRequirement>();
    public List<ItemRequirement> RequiredItems = new List<ItemRequirement>();
    public List<SkillRequirement> RequiredSkills = new List<SkillRequirement>();
    public List<FlagRequirement> RequiredFlags = new List<FlagRequirement>();
}