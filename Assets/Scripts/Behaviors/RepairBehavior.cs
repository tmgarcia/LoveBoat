using System.Collections.Generic;

public class RepairBehavior : ActionBehavior
{
    private Dialogue _dialogue;

    private void Start()
    {
        var lines = new List<DialogueLine>() { new DialogueLine("Player", "I spent some repairing Boat.", null) };
        _dialogue = new Dialogue(lines, null, () => {
            EndAction();
        });
    }

    protected override void OnActionStart()
    {
        SetDialogue(_dialogue);
    }

    protected override void EndAction()
    {
        GameStatus.Instance.InventoryItems["wood"].Amount -= 5;
        GameStatus.Instance.RepairLevel.CurrentValue += 5;
        GameStatus.Instance.LoveLevel.CurrentValue += 3;
        base.EndAction();
    }



    private List<DialogueLine> RepairMilestone0()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "I walked along the beach..."));
        lines.Add(new DialogueLine("Player", "Hmm... this boat’s been in better shape... the hull’s been smashed up, and there’s no sail.  Looks like it’s going to take a lot of work to get it seaworthy again."));
        lines.Add(new DialogueLine("Player", "There’s a dedication plaque on the front... looks like this boat’s name is..."));
        lines.Add(new DialogueLine("Player", "Let’s see if <Boat> has any supplies left on board..."));
        lines.Add(new DialogueLine("Boat", "Hey, what do you think you’re doing?!"));
        lines.Add(new DialogueLine("Player", "What? Who said that?!"));


        return lines;
    }
    private List<DialogueLine> RepairMilestone1()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "I walked along the beach..."));
        lines.Add(new DialogueLine("Player", "Hmm... this boat’s been in better shape... the hull’s been smashed up, and there’s no sail.  Looks like it’s going to take a lot of work to get it seaworthy again."));
        lines.Add(new DialogueLine("Player", "There’s a dedication plaque on the front... looks like this boat’s name is..."));
        lines.Add(new DialogueLine("Player", "Let’s see if <Boat> has any supplies left on board..."));
        lines.Add(new DialogueLine("Boat", "Hey, what do you think you’re doing?!"));
        lines.Add(new DialogueLine("Player", "What? Who said that?!"));


        return lines;
    }
    private List<DialogueLine> RepairMilestone2()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "I walked along the beach..."));
        lines.Add(new DialogueLine("Player", "Hmm... this boat’s been in better shape... the hull’s been smashed up, and there’s no sail.  Looks like it’s going to take a lot of work to get it seaworthy again."));
        lines.Add(new DialogueLine("Player", "There’s a dedication plaque on the front... looks like this boat’s name is..."));
        lines.Add(new DialogueLine("Player", "Let’s see if <Boat> has any supplies left on board..."));
        lines.Add(new DialogueLine("Boat", "Hey, what do you think you’re doing?!"));
        lines.Add(new DialogueLine("Player", "What? Who said that?!"));


        return lines;
    }
    private List<DialogueLine> RepairMilestone3()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "I walked along the beach..."));
        lines.Add(new DialogueLine("Player", "Hmm... this boat’s been in better shape... the hull’s been smashed up, and there’s no sail.  Looks like it’s going to take a lot of work to get it seaworthy again."));
        lines.Add(new DialogueLine("Player", "There’s a dedication plaque on the front... looks like this boat’s name is..."));
        lines.Add(new DialogueLine("Player", "Let’s see if <Boat> has any supplies left on board..."));
        lines.Add(new DialogueLine("Boat", "Hey, what do you think you’re doing?!"));
        lines.Add(new DialogueLine("Player", "What? Who said that?!"));


        return lines;
    }
    private List<DialogueLine> RepairMilestone4()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "I walked along the beach..."));
        lines.Add(new DialogueLine("Player", "Hmm... this boat’s been in better shape... the hull’s been smashed up, and there’s no sail.  Looks like it’s going to take a lot of work to get it seaworthy again."));
        lines.Add(new DialogueLine("Player", "There’s a dedication plaque on the front... looks like this boat’s name is..."));
        lines.Add(new DialogueLine("Player", "Let’s see if <Boat> has any supplies left on board..."));
        lines.Add(new DialogueLine("Boat", "Hey, what do you think you’re doing?!"));
        lines.Add(new DialogueLine("Player", "What? Who said that?!"));


        return lines;
    }
}
