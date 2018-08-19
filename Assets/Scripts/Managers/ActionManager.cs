using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionManager : MonoSingleton<ActionManager>
{
    private GameConfig _config { get { return GameManager.Instance.Config; } }

    private Dictionary<ActionLocation, List<Action>> _actionsByLocation = new Dictionary<ActionLocation, List<Action>>();

    public Action ActiveAction{ get; set; }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _actionsByLocation = _config.Actions.GroupBy(a => a.Location).ToDictionary(group => group.Key, group => group.ToList());
    }

    public Dictionary<ActionLocation, Action> GetAvailableActions()
    {
        var actionsToPresent = new Dictionary<ActionLocation, Action>();

        var outsideActions = GetAvailableActionsForLocation(ActionLocation.OutsideCamp);
        var highestPriorityOutside = outsideActions.OrderByDescending(a => a.Priority).FirstOrDefault();
        if (highestPriorityOutside != null) actionsToPresent.Add(ActionLocation.OutsideCamp, highestPriorityOutside);

        var insideActions = GetAvailableActionsForLocation(ActionLocation.InsideCamp);
        var highestPriorityInside = insideActions.OrderByDescending(a => a.Priority).FirstOrDefault();
        if (highestPriorityInside != null) actionsToPresent.Add(ActionLocation.InsideCamp, highestPriorityInside);

        var boatActions = GetAvailableActionsForLocation(ActionLocation.Boat);
        var highestPriorityBoat = boatActions.OrderByDescending(a => a.Priority).FirstOrDefault();
        if (highestPriorityBoat != null) actionsToPresent.Add(ActionLocation.Boat, highestPriorityBoat);

        return actionsToPresent;
    }

    private List<Action> GetAvailableActionsForLocation(ActionLocation location)
    {
        List<Action> actionsAtLocation = null;
        if (!_actionsByLocation.TryGetValue(location, out actionsAtLocation)) {
            Debug.LogError("No " + location + " Actions have been configured!");
            return new List<Action>();
        }

        var available = actionsAtLocation.Where(a => IsActionAvailable(a)).ToList();
        if (!available.Any())
        {
            Debug.Log("No "+location+" Actions are currently available. Hopefully this is intentional.");
        }

        return available;
    }

    private bool IsActionAvailable(Action action)
    {
        var status = GameStatus.Instance;
        return
            IsRequiredTimeOfDay(action.RequiredTimeOfDay) &&
            IsRequiredWeather(action.RequiredWeather) &&
            HaveRequiredResources(action.RequiredResources) &&
            HaveRequiredItems(action.RequiredItems) &&
            HaveRequiredSkills(action.RequiredSkills) &&
            HaveRequiredFlags(action.RequiredFlags);
    }

    private bool IsRequiredTimeOfDay(TimeOfDay requiredTimeOfDay)
    {
        var result = ((requiredTimeOfDay & GameStatus.Instance.CurrentTimeOfDay) != 0);
        return result;
    }
    private bool IsRequiredWeather(Weather requiredWeather)
    {
        var result = ((requiredWeather & GameStatus.Instance.CurrentWeather) != 0);
        return result;
    }
    private bool HaveRequiredResources(List<ResourceRequirement> resourceRequirements)
    {
        if (resourceRequirements == null || resourceRequirements.Count == 0) return true;

        var allReqsMet = true;
        for(int i = 0; i < resourceRequirements.Count && allReqsMet; i++)
        {
            var requirement = resourceRequirements[i];
            var resourceValue = GameStatus.Instance.ResourceValues[requirement.ResourceId].CurrentValue;

            allReqsMet = MeetsValueRequirement(requirement.Value, resourceValue, requirement.Comparator);
        }

        return allReqsMet;
    }
    private bool HaveRequiredItems(List<ItemRequirement> itemRequirements)
    {
        if (itemRequirements == null || itemRequirements.Count == 0) return true;

        var allReqsMet = true;
        for (int i = 0; i < itemRequirements.Count && allReqsMet; i++)
        {
            var item = itemRequirements[i];
            var value = GameStatus.Instance.InventoryItems[item.ItemId].Amount;

            allReqsMet = MeetsValueRequirement(item.Amount, value, item.Comparator);
        }

        return allReqsMet;
    }
    private bool HaveRequiredSkills(List<SkillRequirement> skillRequirements)
    {
        if (skillRequirements == null || skillRequirements.Count == 0) return true;

        var allReqsMet = true;
        for (int i = 0; i < skillRequirements.Count && allReqsMet; i++)
        {
            var skill = skillRequirements[i];
            var level = GameStatus.Instance.SkillLevels[skill.SkillId].Level;

            allReqsMet = MeetsValueRequirement(skill.Level, level, skill.Comparator);
        }

        return allReqsMet;
    }
    private bool HaveRequiredFlags(List<FlagRequirement> flagRequirements)
    {
        if (flagRequirements == null || flagRequirements.Count == 0) return true;

        var allReqsMet = true;
        for (int i = 0; i < flagRequirements.Count && allReqsMet; i++)
        {
            var flag = flagRequirements[i];
            var flagStatus = GameStatus.Instance.Flags[flag.FlagId].Status;

            allReqsMet = flagStatus == flag.Status;
        }
        return allReqsMet;
    }


    private bool MeetsValueRequirement(int requiredValue, int actualValue, Comparator comparator)
    {
        var meetsReq = true;
        switch (comparator)
        {
            case Comparator.LessThan:
                meetsReq = actualValue < requiredValue;
                break;
            case Comparator.LessThanOrEqualTo:
                meetsReq = actualValue <= requiredValue;
                break;
            case Comparator.EqualTo:
                meetsReq = actualValue == requiredValue;
                break;
            case Comparator.GreaterThanOrEqualTo:
                meetsReq = actualValue >= requiredValue;
                break;
            case Comparator.GreaterThan:
                meetsReq = actualValue > requiredValue;
                break;
            default:
                meetsReq = false;
                Debug.LogError("WHERE DID THIS COMPARATOR COME FROM: "+comparator);
                break;
        }
        return meetsReq;
    }
}
