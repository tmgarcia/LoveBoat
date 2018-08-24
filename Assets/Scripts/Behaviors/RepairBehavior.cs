using UnityEngine;
using System.Collections.Generic;

public class RepairBehavior : ActionBehavior
{
    [Header("Milestone Music")]
    [SerializeField] private AudioClip _milestone1Music;
    [SerializeField] private AudioClip _milestone2Music;
    [SerializeField] private AudioClip _milestone3Music;
    [SerializeField] private AudioClip _milestone4Music;
    [SerializeField] private AudioClip _milestone5Music;

    protected override void OnActionStart()
    {
        var lines = new List<DialogueLine>();
        var options = new List<DialogueOption>();
        var repairs = GetRepairAmount();
        var currentRepair = GameStatus.Instance.RepairLevel.CurrentValue;
        System.Action end = null;

        if (currentRepair == 0)
        {
            lines = RepairMilestone1();
            options = new List<DialogueOption>() {
                new DialogueOption("You look like you’ve seen better days.", () => { SetDialogue(new Dialogue(RepairMilestone1_OptionA(), null, EndAction)); }),
                new DialogueOption("Change the subject", () => { SetDialogue(new Dialogue(RepairMilestone1_OptionB(), null, EndAction)); }),
            };
        }
        else if (currentRepair < 20 && currentRepair + repairs >= 20)
        {
            lines = RepairMilestone2();
            options = null;
            end = EndAction;
        }
        else if (currentRepair < 40 && currentRepair + repairs >= 40)
        {
            lines = RepairMilestone3();
            options = null;
            end = EndAction;
        }
        else if (currentRepair < 60 && currentRepair + repairs >= 60)
        {
            lines = RepairMilestone4();
            options = new List<DialogueOption>() {
                new DialogueOption("Hornbeck was a terrible captain", () => { SetDialogue(new Dialogue(RepairMilestone4_OptionA(), null, EndAction)); }),
                new DialogueOption("Hornbeck had to save herself", () => { SetDialogue(new Dialogue(RepairMilestone4_OptionB(), null, EndAction)); }),
            };
        }
        else if (currentRepair < 80 && currentRepair + repairs >= 80)
        {
            lines = RepairMilestone5();
            options = new List<DialogueOption>() {
                new DialogueOption("Probably", () => { SetDialogue(new Dialogue(RepairMilestone5_OptionA(), null, EndAction)); }),
                new DialogueOption("Probably not", () => { SetDialogue(new Dialogue(RepairMilestone5_OptionB(), null, EndAction)); }),
                new DialogueOption("Who cares?", () => { SetDialogue(new Dialogue(RepairMilestone5_OptionC(), null, EndAction)); }),
            };
        }
        else
        {
            lines = NormalRepair();
            options = null;
            end = EndAction;
        }


        SetDialogue(new Dialogue(lines, options, end));
    }

    protected override void EndAction()
    {
        GameStatus.Instance.InventoryItems["wood"].Amount -= 3;

        
        GameStatus.Instance.RepairLevel.CurrentValue += GetRepairAmount();
        GameStatus.Instance.LoveLevel.CurrentValue += 2;
        base.EndAction();
    }

    private int GetRepairAmount()
    {
        var repairs = 8;
        if (GameStatus.Instance.SkillLevels["crafting"].Level < 10)
        {
            repairs = 8;
        }
        else if (GameStatus.Instance.SkillLevels["crafting"].Level < 20)
        {
            repairs = 12;
        }
        else
        {
            repairs = 18;
        }

        return repairs;
    }

    private List<DialogueLine> NormalRepair()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "I spent some time repairing <Boat>..."));

        return lines;
    }

    private List<DialogueLine> RepairMilestone1()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "I’ve collected a fair amount of driftwood... I think I could take a stab at patching some of the holes in <Boat>, if they’ll let me...", () => { AudioManager.Instance.PlayMusic(_milestone1Music, true); }));
        lines.Add(new DialogueLine("Boat", "Well?  Have you found Captain Hornbeck yet?", () => {
            Boat.Instance.SetFace(BoatFace.Neutral);
        }));
        lines.Add(new DialogueLine("Player", "No, but I’ve got an idea.  How about if I start repairing you, so that when she gets back, we can set sail as soon as possible?"));
        lines.Add(new DialogueLine("Boat", "Repairing me?  What are you trying to say?!  ", () => {
            Boat.Instance.SetFace(BoatFace.Angry);
        }));
        lines.Add(new DialogueLine("Player", "Player: Well, uh..."));

        return lines;
    }

    private List<DialogueLine> RepairMilestone1_OptionA()
    {
        var lines = new List<DialogueLine>();
        GameStatus.Instance.LoveLevel.CurrentValue += 5;

        lines.Add(new DialogueLine("Player", "You look pretty bashed up.  I’m surprised you even made it to shore like this."));
        lines.Add(new DialogueLine("Boat", "Oh yeah? Well you don’t look so great yourself!  "));
        lines.Add(new DialogueLine("Player", "Hey!  For surviving a plane crash, I think I look all right!"));
        lines.Add(new DialogueLine("Boat", "So your face looked like that before you crashed?", () => {
            Boat.Instance.SetFace(BoatFace.Annoyed);
        }));
        lines.Add(new DialogueLine("Player", "What’s that supposed to-"));
        lines.Add(new DialogueLine("Player", " Look, instead of calling each other names, why don’t we try to get us and Captain Hornbeck off this island?"));

        lines.AddRange(RepairMilestone1_Final());

        return lines;
    }

    private List<DialogueLine> RepairMilestone1_OptionB()
    {
        var lines = new List<DialogueLine>();
        GameStatus.Instance.LoveLevel.CurrentValue += 8;

        lines.Add(new DialogueLine("Player", "Uh... hey!  Captain Hornbeck’s probably looking for supplies, right?"));
        lines.Add(new DialogueLine("Boat", "So?", () => {
            Boat.Instance.SetFace(BoatFace.Annoyed);
        }));
        lines.Add(new DialogueLine("Player", "So, wouldn’t she be happy if she came back and you were ready to sail?  That would be a lot of stress off her mind!"));
        lines.Add(new DialogueLine("Boat", "...", () => {
            Boat.Instance.SetFace(BoatFace.Neutral);
        }));

        lines.AddRange(RepairMilestone1_Final());

        return lines;
    }

    private List<DialogueLine> RepairMilestone1_Final()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("Boat", "Fine. ...do you know anything about carpentry?", () => {
            Boat.Instance.SetFace(BoatFace.Neutral);
        }));
        lines.Add(new DialogueLine("Player", "...I made a spice rack in shop class?"));
        lines.Add(new DialogueLine("Boat", "Ugh.  I suppose it can’t be helped.  I guess I can talk you through it."));
        lines.Add(new DialogueLine("", "<Boat> taught me some of the basics of carpentry, and we started planning the repairs."));

        return lines;
    }

    private List<DialogueLine> RepairMilestone2()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "I’ve been making steady progress, shoring up some of the worst holes in <Boat>’s hull.", () => { AudioManager.Instance.PlayMusic(_milestone2Music, true); }));
        lines.Add(new DialogueLine("Boat", "Ouch!  Careful!", () => {
            Boat.Instance.SetFace(BoatFace.Angry);
        }));
        lines.Add(new DialogueLine("Player", "Sorry, I don’t have the best tools to work with..."));
        lines.Add(new DialogueLine("Boat", "I’d offer to let you use my captain’s tools, but they went overboard in the storm.  Not that they would have done Captain Hornbeck much good... she had her hands full trying to keep us moving in the right direction, bailing me out, keeping herself from falling overboard..", () => {
            Boat.Instance.SetFace(BoatFace.Neutral);
        }));
        lines.Add(new DialogueLine("Player", "Wow... it’s amazing that she managed to get you all the way to the beach with all this damage.  She must be a great sailor."));
		lines.Add(new DialogueLine("Boat", "...yeah.  She is.", () => {
            Boat.Instance.SetFace(BoatFace.Sad);
        }));
        lines.Add(new DialogueLine("", "We worked in silence for a little while."));
        

        return lines;
    }
    private List<DialogueLine> RepairMilestone3()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "I’ve managed to patch up most of <Boat>’s hull, and I’ve started work on a mast", () => { AudioManager.Instance.PlayMusic(_milestone3Music, true); }));
        lines.Add(new DialogueLine("Player", "I think I’m getting better at this!"));
        lines.Add(new DialogueLine("Boat", "Hmph.  ...You aren’t the worst student.", () => {
            Boat.Instance.SetFace(BoatFace.Happy);
        }));
        lines.Add(new DialogueLine("Player", "Hey, that wave looks pretty nasty..."));
        lines.Add(new DialogueLine("", "A wave was growing in the distance..."));
		lines.Add(new DialogueLine("Boat", " ...Hurricane season is only a few days away... the waves are going to keep getting worse.", () => {
            Boat.Instance.SetFace(BoatFace.Neutral);
        }));
        lines.Add(new DialogueLine("Player", "This one isn’t breaking... It’s just getting bigger..."));
        lines.Add(new DialogueLine("Boat", "...<Player>?  It’s not going to hit us... is it?"));
		lines.Add(new DialogueLine("", "The wave loomed over us..."));
        lines.Add(new DialogueLine("Player", "It’s coming right at us!"));
        lines.Add(new DialogueLine("Boat", "Captain Hornbeck! <Player>! Anyone... help!", () => {
            Boat.Instance.SetFace(BoatFace.Angry);
        }));
		lines.Add(new DialogueLine("", "I grabbed my supplies and held onto <Boat> for dear life..."));
        lines.Add(new DialogueLine("", "*wave crashes*"));
		lines.Add(new DialogueLine("", "<Boat> started slipping into the water, as I pulled us against the current with all my strength..."));
        lines.Add(new DialogueLine("Player", "*cough* Whew... that was close..."));
		lines.Add(new DialogueLine("Player", "Guess we better get these repairs finished before hurricane season starts, huh?"));
        lines.Add(new DialogueLine("Boat", "...", () => {
            Boat.Instance.SetFace(BoatFace.Sad);
        }));
		lines.Add(new DialogueLine("Player", "<Boat>? Are you all right?"));
        lines.Add(new DialogueLine("Boat", "...I’m fine.", () => {
            Boat.Instance.SetFace(BoatFace.Sad);
        }));
		lines.Add(new DialogueLine("Player", "Are you sure?  You seemed pretty shaken up..."));
		lines.Add(new DialogueLine("Boat", "I’m fine!  Quit slacking off and get back to work!", () => {
            Boat.Instance.SetFace(BoatFace.Annoyed);
        }));
		lines.Add(new DialogueLine("Player", "Okay, okay..."));
		


        return lines;
    }
    private List<DialogueLine> RepairMilestone4()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("Player", "I still haven’t seen any sign of Captain Hornbeck.  Are you sure she’s here?", () => { AudioManager.Instance.PlayMusic(_milestone4Music, true); }));
        lines.Add(new DialogueLine("Boat", "When the storm hit its peak, the captain managed to signal a nearby trawler.  It wasn’t easy, but we fought through the waves and managed to get within swimming distance.", () => {
            Boat.Instance.SetFace(BoatFace.Sad);
        }));
        lines.Add(new DialogueLine("Boat", "The captain... she jumped overboard, swam to the trawler, and they pulled her up to the deck.."));
        lines.Add(new DialogueLine("Boat", "and that was the last time I saw her."));
        lines.Add(new DialogueLine("Boat", "She didn’t look back, she didn’t try to save me too... the trawler pulled away and I was alone in the storm..."));
		lines.Add(new DialogueLine("", "wow... the captain abandoned <Boat>? What should I say?"));

        return lines;
    }

    private List<DialogueLine> RepairMilestone4_OptionA()
    {
        var lines = new List<DialogueLine>();
        GameStatus.Instance.LoveLevel.CurrentValue += 10;

        lines.Add(new DialogueLine("Player", "I can’t believe Hornbeck did that to you! A captain should always go down with her ship!"));
        lines.Add(new DialogueLine("Boat", "It wasn’t her fault... I should have protected her in the storm, and I let her down..."));
        lines.Add(new DialogueLine("Player", "A captain’s duty is to keep her boat safe, no matter what!  She could have had the trawler tow you to safety, but she left you behind.  You don’t deserve that!"));

        lines.AddRange(RepairMilestone4_Final());

        return lines;
    }

    private List<DialogueLine> RepairMilestone4_OptionB()
    {
        var lines = new List<DialogueLine>();
        GameStatus.Instance.LoveLevel.CurrentValue += 6;

        lines.Add(new DialogueLine("Player", "I can understand Hornbeck’s decision.  She wouldn’t have made it through the storm without signaling for help."));
        lines.Add(new DialogueLine("Boat", "It’s my fault... I couldn’t protect her..."));
        lines.Add(new DialogueLine("Player", "I’m sure it wasn’t personal!  A captain has to make those decisions.  I know she wouldn’t have abandoned you unless it was the only option."));

        lines.AddRange(RepairMilestone4_Final());

        return lines;
    }

    private List<DialogueLine> RepairMilestone4_Final()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("Boat", "I guess you’re right, <Player>...", () => {
            Boat.Instance.SetFace(BoatFace.Blushing);
        }));

        return lines;
    }


    private List<DialogueLine> RepairMilestone5()
    {
        var lines = new List<DialogueLine>();

        Boat.Instance.SetFace(BoatFace.Sad);
        lines.Add(new DialogueLine("", "Just applying the finishing touches... <Boat> should be just about ready to set sail!", () => { AudioManager.Instance.PlayMusic(_milestone5Music, true); }));
		lines.Add(new DialogueLine("Player", "Have you been thinking about Captain Hornbeck?"));
		lines.Add(new DialogueLine("Boat", "Yeah... do you think she made it to the mainland safely?"));
		

        return lines;
    }

    private List<DialogueLine> RepairMilestone5_OptionA()
    {
        var lines = new List<DialogueLine>();
        GameStatus.Instance.LoveLevel.CurrentValue += 8;

        lines.Add(new DialogueLine("Player", "I’m sure she made it back safely."));

        lines.AddRange(RepairMilestone5_Final());

        return lines;
    }

    private List<DialogueLine> RepairMilestone5_OptionB()
    {
        var lines = new List<DialogueLine>();
        GameStatus.Instance.LoveLevel.CurrentValue += 4;

        lines.Add(new DialogueLine("Player", "You said the storm was pretty bad.  I’d be surprised if the trawler made it back."));

        lines.AddRange(RepairMilestone5_Final());

        return lines;
    }

    private List<DialogueLine> RepairMilestone5_OptionC()
    {
        var lines = new List<DialogueLine>();
        GameStatus.Instance.LoveLevel.CurrentValue += 6;

        lines.Add(new DialogueLine("Player", "Who cares?  She abandoned you.  Worrying about her is a waste of your time."));

        lines.AddRange(RepairMilestone5_Final());

        return lines;
    }

    private List<DialogueLine> RepairMilestone5_Final()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("Boat", "I guess you’re right."));
        lines.Add(new DialogueLine("Boat", "I’ll still need a sail before we leave.  <Player>, do you think you’ll be able to work with this?", () => {
            Boat.Instance.SetFace(BoatFace.Happy);
        }));
        lines.Add(new DialogueLine("", "<Boat> gives me a sewing kit.", () => {
            GameStatus.Instance.Flags["sewKit"].Status = true;
        }));

        return lines;
    }
}
