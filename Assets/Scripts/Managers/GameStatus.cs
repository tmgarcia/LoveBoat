using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoSingleton<GameStatus>
{
    private GameConfig _config { get { return GameManager.Instance.Config; } }

    public Dictionary<string, ResourceValue> ResourceValues { get; private set; }
    public Dictionary<string, InventoryItem> InventoryItems { get; private set; }
    public Dictionary<string, SkillLevel> SkillLevels { get; private set; }
    public Dictionary<string, FlagStatus> Flags { get; private set; }

    public int CurrentDay { get; set; }
    public TimeOfDay CurrentTimeOfDay { get; set; }

    public bool HasEnergy { get { return EnergyLevel.CurrentValue > 0; } }
    public bool HasFood { get { return FoodLevel.CurrentValue > 0; } }
    public bool BoatFullyRepaired { get { return RepairLevel.ReachedMax; } }
    public bool BoatFullyLoved { get { return LoveLevel.ReachedMax; } }

    public void Initialize()
    {
        CurrentDay = 0;
        CurrentTimeOfDay = TimeOfDay.Morning;

        var resources = _config.Resources;
        ResourceValues = new Dictionary<string, ResourceValue>();
        foreach (var resource in resources) { ResourceValues.Add(resource.Id, new ResourceValue(resource)); }

        var items = _config.Items;
        InventoryItems = new Dictionary<string, InventoryItem>();
        foreach (var item in items) { InventoryItems.Add(item.Id, new InventoryItem(item)); }

        var skills = _config.Skills;
        SkillLevels = new Dictionary<string, SkillLevel>();
        foreach (var skill in skills) { SkillLevels.Add(skill.Id, new SkillLevel(skill)); }
    }

    public ResourceValue EnergyLevel
    {
        get
        {
            ResourceValue resourceValue = null;
            if (!ResourceValues.TryGetValue(_config.EnergyResourceId, out resourceValue)) Debug.LogError("Energy Resource Missing!");
            return resourceValue;
        }
    }
    public ResourceValue FoodLevel
    {
        get
        {
            ResourceValue resourceValue = null;
            if (!ResourceValues.TryGetValue(_config.FoodResourceId, out resourceValue)) Debug.LogError("Food Resource Missing!");
            return resourceValue;
        }
    }
    public ResourceValue RepairLevel
    {
        get
        {
            ResourceValue resourceValue = null;
            if (!ResourceValues.TryGetValue(_config.RepairResourceId, out resourceValue)) Debug.LogError("Repair Resource Missing!");
            return resourceValue;
        }
    }
    public ResourceValue LoveLevel
    {
        get
        {
            ResourceValue resourceValue = null;
            if (!ResourceValues.TryGetValue(_config.LoveResourceId, out resourceValue)) Debug.LogError("Love Resource Missing!");
            return resourceValue;
        }
    }
}
