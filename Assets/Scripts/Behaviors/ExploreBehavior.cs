using UnityEngine;
using System.Collections.Generic;

public class ExploreBehavior : ActionBehavior
{
    [SerializeField] private AudioClip _discoverMusic;
    private Dialogue _dialogue;

    protected override void OnActionStart()
    {
        var lines = new List<DialogueLine>() { new DialogueLine("Player", "I decided to spend some time exploring the island...", null) };
        lines.AddRange(ExplorationStages());
        _dialogue = new Dialogue(lines, null, () => {
            EndAction();
        });
        SetDialogue(_dialogue);
    }

    protected override void EndAction()
    {
        base.EndAction();
    }

    private List<DialogueLine> ExplorationStages()
    {
        var lines = new List<DialogueLine>();
        var skill = GameStatus.Instance.SkillLevels["scouting"].Level;
        if (!GameStatus.Instance.Flags["boatFound"].Status)
        {
            lines.AddRange(DiscoverTheBoat());
        }
        else if (skill >= 3 && !GameStatus.Instance.Flags["axe"].Status)
        {
            lines.Add(new DialogueLine("Player", "I found an axe!"));
            GameStatus.Instance.Flags["axe"].Status = true;
        }
        else if (skill >= 5 && !GameStatus.Instance.Flags["rod"].Status)
        {
            lines.Add(new DialogueLine("Player", "I found a fishing rod!"));
            GameStatus.Instance.Flags["rod"].Status = true;
        }
        else if (skill >= 8 && !GameStatus.Instance.Flags["coconutGrove"].Status)
        {
            lines.Add(new DialogueLine("Player", "I found a coconut grove!"));
            GameStatus.Instance.Flags["coconutGrove"].Status = true;
        }
        else
        {
            lines.Add(new DialogueLine("", "I feel like I'm getting to know the island better.", () => { }));
        }

        GameStatus.Instance.SkillLevels["scouting"].Level += 1;
        return lines;
    }

    private List<DialogueLine> DiscoverTheBoat()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "I walked along the beach...", () => { AudioManager.Instance.PlayMusic(_discoverMusic, true); }));
        lines.Add(new DialogueLine("Player", "A boat... over there, on the shore!  I’m saved!", () => {
            Boat.Instance.SetVisible(true);
            Boat.Instance.SetFace(BoatFace.Hidden);
        }));
        lines.Add(new DialogueLine("Player", "Hmm... this boat’s been in better shape... the hull’s been smashed up, and there’s no sail.  Looks like it’s going to take a lot of work to get it seaworthy again."));
        lines.Add(new DialogueLine("Player", "There’s a dedication plaque on the front... looks like this boat’s name is..."));
        lines.Add(new DialogueLine("Player", "Let’s see if <Boat> has any supplies left on board..."));
        lines.Add(new DialogueLine("???", "Hey, what do you think you’re doing?!"));
        lines.Add(new DialogueLine("Player", "What? Who said that?!"));
        lines.Add(new DialogueLine("???", "We’ve been boarded!  All hands, prepare to fight off intruders!"));
        lines.Add(new DialogueLine("", "I jumped out of the boat and whirled around to face my opponent..."));
        lines.Add(new DialogueLine("Player", "What the..."));
        lines.Add(new DialogueLine("Boat", "How dare you?!  You can’t just board a ship without the captain’s permission!", () => {
            Boat.Instance.SetFace(BoatFace.Angry);
        }));
        lines.Add(new DialogueLine("Player", "Oh... I- I’m sorry!  I didn’t know you had an owner!"));
        lines.Add(new DialogueLine("Boat", "Of course I do!  All ships have a captain, and when Captain Hornbeck gets back, she’ll throw you to the sharks!", () => {
            Boat.Instance.SetFace(BoatFace.Annoyed);
        }));
        lines.Add(new DialogueLine("Player", "Your captain’s on this island?  Then I’m saved!  Do you know when she’ll be back?"));
        lines.Add(new DialogueLine("Boat", "What makes you think I’d let you sail with us?"));
        lines.Add(new DialogueLine("Player", "I just thought-"));
        lines.Add(new DialogueLine("Boat", "You’ve got some nerve!  First you board me without permission, then you invite yourself to go sailing with my captain and I-", () => {
            Boat.Instance.SetFace(BoatFace.Angry);
        }));
        lines.Add(new DialogueLine("Player", "Look, I’m sorry!  ...Do you at least know which way your captain went?"));
        lines.Add(new DialogueLine("Boat", "Hmph.  She’s probably somewhere inland.", () => {
            Boat.Instance.SetFace(BoatFace.Annoyed);
        }));


        GameStatus.Instance.Flags["boatFound"].Status = true;
        return lines;
    }
}
