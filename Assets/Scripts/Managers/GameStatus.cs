using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoSingleton<GameStatus>
{
    [SerializeField] bool _debugUI = true;

    private GameConfig _config { get { return GameManager.Instance.Config; } }

    public Dictionary<string, ResourceValue> ResourceValues { get; private set; }
    public Dictionary<string, InventoryItem> InventoryItems { get; private set; }
    public Dictionary<string, SkillLevel> SkillLevels { get; private set; }
    public Dictionary<string, FlagStatus> Flags { get; private set; }

    public int CurrentDay { get; set; }
    public TimeOfDay CurrentTimeOfDay { get; set; }
    public Weather CurrentWeather { get; set; }

    public bool HasEnergy { get { return EnergyLevel.CurrentValue > 0; } }
    public bool HasFood { get { return FoodLevel.CurrentValue > 0; } }
    public bool BoatFullyRepaired { get { return RepairLevel.ReachedMax; } }
    public bool BoatFullyLoved { get { return LoveLevel.ReachedMax; } }

    public void Initialize()
    {
        CurrentDay = 0;
        CurrentTimeOfDay = TimeOfDay.Morning;
        CurrentWeather = Weather.Sunny;

        var resources = _config.Resources;
        ResourceValues = new Dictionary<string, ResourceValue>();
        foreach (var resource in resources) { ResourceValues.Add(resource.Id, new ResourceValue(resource)); }

        var items = _config.Items;
        InventoryItems = new Dictionary<string, InventoryItem>();
        foreach (var item in items) { InventoryItems.Add(item.Id, new InventoryItem(item)); }

        var skills = _config.Skills;
        SkillLevels = new Dictionary<string, SkillLevel>();
        foreach (var skill in skills) { SkillLevels.Add(skill.Id, new SkillLevel(skill)); }

        var flags = _config.Flags;
        Flags = new Dictionary<string, FlagStatus>();
        foreach(var flag in flags) { Flags.Add(flag.Id, new FlagStatus(flag)); }
    }

    public void OnGUI()
    {
        if(_debugUI)
        {
            GUI.color = Color.black;

            float lineHeight = 20;
            var position = new Rect(30, 20, 150, lineHeight);
            GUI.Label(position, "Day: " + CurrentDay);
            position.y += lineHeight;
            GUI.Label(position, "Time: " + CurrentTimeOfDay);
            position.y += lineHeight;
            GUI.Label(position, "Weather: " + CurrentWeather);

            position.y += lineHeight;

            GUI.Label(position, "RESOURCES");
            position.y += lineHeight;
            position.x += 10;
            foreach(var resourceValue in ResourceValues)
            {
                GUI.Label(position, resourceValue.Key +": " + resourceValue.Value.CurrentValue);
                position.y += lineHeight;
            }
            position.x -= 10;

            position.y += lineHeight;
            position.y += lineHeight;

            GUI.Label(position, "ITEMS");
            position.y += lineHeight;
            position.x += 10;
            foreach (var item in InventoryItems)
            {
                GUI.Label(position, item.Key + ": " + item.Value.Amount);
                position.y += lineHeight;
            }
            position.x -= 10;

            position.y += lineHeight;

            GUI.Label(position, "SKILLS");
            position.y += lineHeight;
            position.x += 10;
            foreach (var skill in SkillLevels)
            {
                GUI.Label(position, skill.Key + ": " + skill.Value.Level);
                position.y += lineHeight;
            }
            position.x -= 10;

            position.y += lineHeight;

            GUI.Label(position, "FLAGS");
            position.y += lineHeight;
            position.x += 10;
            foreach (var flag in Flags)
            {
                GUI.Label(position, flag.Key + ": " + flag.Value.Status);
                position.y += lineHeight;
            }
            position.x -= 10;
        }
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
