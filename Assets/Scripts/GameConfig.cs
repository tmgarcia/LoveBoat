using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Love Boat/Game Config", order = 1)]
public class GameConfig : ScriptableObject
{
    [Header("End Game Stats")]
    public int HurricaneDay;
    public int LateDay;

    [Header("Resources")]
    public string EnergyResourceId = "energy";
    public string FoodResourceId = "food";
    public string RepairResourceId = "repair";
    public string LoveResourceId = "love";
    public List<Resource> Resources = new List<Resource>();

    [Header("Items")]
    public List<Item> Items = new List<Item>();

    [Header("Skills")]
    public List<Skill> Skills = new List<Skill>();

    [Header("Flags")]
    public List<Flag> Flags = new List<Flag>();

    [Header("Actions")]
    public List<ActionDefinition> Actions = new List<ActionDefinition>();
}
