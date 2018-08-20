using System.Collections.Generic;

public class RepairBehavior : ActionBehavior
{

    protected override void OnActionStart()
    {
        var lines = new List<DialogueLine>();
        var repairs = GetRepairAmount();
        var currentRepair = GameStatus.Instance.RepairLevel.CurrentValue;

        if (currentRepair == 0)
        {
            lines = RepairMilestone1();
        }
        else if (currentRepair < 20 && currentRepair + repairs >= 20)
        {
            lines = RepairMilestone2();
        }
        else if (currentRepair < 40 && currentRepair + repairs >= 40)
        {
            lines = RepairMilestone3();
        }
        else if (currentRepair < 60 && currentRepair + repairs >= 60)
        {
            lines = RepairMilestone4();
        }
        else if (currentRepair < 80 && currentRepair + repairs >= 80)
        {
            lines = RepairMilestone5();
        }
        else
        {
            lines = NormalRepair();
        }


        SetDialogue(new Dialogue(lines, null));
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
        var repairs = 3;
        if (GameStatus.Instance.SkillLevels["crafting"].Level < 10)
        {
            repairs = 3;
        }
        else if (GameStatus.Instance.SkillLevels["crafting"].Level < 20)
        {
            repairs = 5;
        }
        else
        {
            repairs = 7;
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

        lines.Add(new DialogueLine("", "I’ve collected a fair amount of driftwood... I think I could take a stab at patching some of the holes in <Boat>, if they’ll let me..."));
        lines.Add(new DialogueLine("Boat", "Well?  Have you found Captain Hornbeck yet?"));
        lines.Add(new DialogueLine("Player", "No, but I’ve got an idea.  How about if I start repairing you, so that when she gets back, we can set sail as soon as possible?"));
        lines.Add(new DialogueLine("Boat", "Repairing me?  What are you trying to say?!  "));
        lines.Add(new DialogueLine("Player", "Player: Well, uh..."));
        lines.Add(new DialogueLine("Player", ">You look like you’ve seen better days."));
		lines.Add(new DialogueLine("Player", "You look pretty bashed up.  I’m surprised you even made it to shore like this."));
        lines.Add(new DialogueLine("Boat", "Oh yeah? Well you don’t look so great yourself!  "));
        lines.Add(new DialogueLine("Player", "Hey!  For surviving a plane crash, I think I look all right!"));
		lines.Add(new DialogueLine("Boat", "So your face looked like that before you crashed?"));
        lines.Add(new DialogueLine("Player", "What’s that supposed to-"));
        lines.Add(new DialogueLine("Player", " Look, instead of calling each other names, why don’t we try to get us and Captain Hornbeck off this island?"));
		lines.Add(new DialogueLine("Player", ">Change the subject"));
		lines.Add(new DialogueLine("Player", "Uh... hey!  Captain Hornbeck’s probably looking for supplies, right?"));
		lines.Add(new DialogueLine("Boat", "So?"));
		lines.Add(new DialogueLine("Player", "So, wouldn’t she be happy if she came back and you were ready to sail?  That would be a lot of stress off her mind!"));
		lines.Add(new DialogueLine("Boat", "..."));
		lines.Add(new DialogueLine("Boat", "Fine. ...do you know anything about carpentry?"));
		lines.Add(new DialogueLine("Player", "...I made a spice rack in shop class?"));
		lines.Add(new DialogueLine("Boat", "Ugh.  I suppose it can’t be helped.  I guess I can talk you through it."));
		lines.Add(new DialogueLine("", "<Boat> taught me some of the basics of carpentry, and we started planning the repairs."));
		


        return lines;
    }
    private List<DialogueLine> RepairMilestone2()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "I’ve been making steady progress, shoring up some of the worst holes in <Boat>’s hull."));
        lines.Add(new DialogueLine("Boat", "Ouch!  Careful!"));
        lines.Add(new DialogueLine("Player", "Sorry, I don’t have the best tools to work with..."));
        lines.Add(new DialogueLine("Boat", "I’d offer to let you use my captain’s tools, but they went overboard in the storm.  Not that they would have done Captain Hornbeck much good... she had her hands full trying to keep us moving in the right direction, bailing me out, keeping herself from falling overboard.."));
        lines.Add(new DialogueLine("Player", "Wow... it’s amazing that she managed to get you all the way to the beach with all this damage.  She must be a great sailor."));
		lines.Add(new DialogueLine("Boat", "...yeah.  She is."));
        lines.Add(new DialogueLine("", "We worked in silence for a little while."));
        

        return lines;
    }
    private List<DialogueLine> RepairMilestone3()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "I’ve managed to patch up most of <Boat>’s hull, and I’ve started work on a mast"));
        lines.Add(new DialogueLine("Player", "I think I’m getting better at this!"));
        lines.Add(new DialogueLine("Boat", "Hmph.  ...You aren’t the worst student."));
        lines.Add(new DialogueLine("Player", "Hey, that wave looks pretty nasty..."));
        lines.Add(new DialogueLine("", "A wave was growing in the distance..."));
		lines.Add(new DialogueLine("Boat", " ...Hurricane season is only a few days away... the waves are going to keep getting worse."));
        lines.Add(new DialogueLine("Player", "This one isn’t breaking... It’s just getting bigger..."));
        lines.Add(new DialogueLine("Boat", "...<Player>?  It’s not going to hit us... is it?"));
		lines.Add(new DialogueLine("", "The wave loomed over us..."));
        lines.Add(new DialogueLine("Player", "It’s coming right at us!"));
        lines.Add(new DialogueLine("Boat", "Captain Hornbeck! <Player>! Anyone... help!"));
		lines.Add(new DialogueLine("", "I grabbed my supplies and held onto <Boat> for dear life..."));
        lines.Add(new DialogueLine("", "*wave crashes*"));
		lines.Add(new DialogueLine("", "<Boat> started slipping into the water, as I pulled us against the current with all my strength..."));
        lines.Add(new DialogueLine("Player", "*cough* Whew... that was close..."));
		lines.Add(new DialogueLine("Player", "Guess we better get these repairs finished before hurricane season starts, huh?"));
        lines.Add(new DialogueLine("Boat", "..."));
		lines.Add(new DialogueLine("Player", "<Boat>? Are you all right?"));
        lines.Add(new DialogueLine("Boat", "...I’m fine."));
		lines.Add(new DialogueLine("Player", "Are you sure?  You seemed pretty shaken up..."));
		lines.Add(new DialogueLine("Boat", "I’m fine!  Quit slacking off and get back to work!"));
		lines.Add(new DialogueLine("Player", "Okay, okay..."));
		


        return lines;
    }
    private List<DialogueLine> RepairMilestone4()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("Player", "I still haven’t seen any sign of Captain Hornbeck.  Are you sure she’s here?"));
        lines.Add(new DialogueLine("Boat", "When the storm hit its peak, the captain managed to signal a nearby trawler.  It wasn’t easy, but we fought through the waves and managed to get within swimming distance."));
        lines.Add(new DialogueLine("Boat", "The captain... she jumped overboard, swam to the trawler, and they pulled her up to the deck.."));
        lines.Add(new DialogueLine("Boat", "and that was the last time I saw her."));
        lines.Add(new DialogueLine("Boat", "She didn’t look back, she didn’t try to save me too... the trawler pulled away and I was alone in the storm..."));
		lines.Add(new DialogueLine("", "wow... the captain abandoned <Boat>? What should I say?"));
        lines.Add(new DialogueLine("", ">Hornbeck was a terrible captain"));
        lines.Add(new DialogueLine("Player", "I can’t believe Hornbeck did that to you! A captain should always go down with her ship!"));
		lines.Add(new DialogueLine("Boat", "It wasn’t her fault... I should have protected her in the storm, and I let her down..."));
        lines.Add(new DialogueLine("Player", "A captain’s duty is to keep her boat safe, no matter what!  She could have had the trawler tow you to safety, but she left you behind.  You don’t deserve that!"));
        lines.Add(new DialogueLine("", ">Hornbeck had to save herself"));
		lines.Add(new DialogueLine("Player", "I can understand Hornbeck’s decision.  She wouldn’t have made it through the storm without signaling for help."));
        lines.Add(new DialogueLine("Boat", "It’s my fault... I couldn’t protect her..."));
        lines.Add(new DialogueLine("Player", "I’m sure it wasn’t personal!  A captain has to make those decisions.  I know she wouldn’t have abandoned you unless it was the only option."));
		lines.Add(new DialogueLine("Boat", "I guess you’re right, <Player>..."));
       


        return lines;
    }
	private List<DialogueLine> RepairMilestone5()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "Just applying the finishing touches... <Boat> should be just about ready to set sail!"));
		lines.Add(new DialogueLine("Player", "Have you been thinking about Captain Hornbeck?"));
		lines.Add(new DialogueLine("Boat", "Yeah... do you think she made it to the mainland safely?"));
		lines.Add(new DialogueLine("", ">Probably"));
		lines.Add(new DialogueLine("Player", "I’m sure she made it back safely."));
		lines.Add(new DialogueLine("", ">Probably not"));
		lines.Add(new DialogueLine("Player", "You said the storm was pretty bad.  I’d be surprised if the trawler made it back."));
		lines.Add(new DialogueLine("", ">Who cares?"));
		lines.Add(new DialogueLine("Player", "Who cares?  She abandoned you.  Worrying about her is a waste of your time."));
		lines.Add(new DialogueLine("Boat", "I guess you’re right."));
		lines.Add(new DialogueLine("Boat", "I’ll still need a sail before we leave.  <Player>, do you think you’ll be able to work with this?"));
		lines.Add(new DialogueLine("", "<Boat> gives me a sewing kit."));
		     
		

        return lines;
    }
	private List<DialogueLine> Ending1()
    {
        var lines = new List<DialogueLine>();

      
		lines.Add(new DialogueLine("", "That night, a storm came..."));
		lines.Add(new DialogueLine("", "I woke up to the tide rising all the way up the shore."));
		lines.Add(new DialogueLine("Player", "The tide’s going to wash away all of my stuff!"));
		lines.Add(new DialogueLine("", "I quickly tried to pack up camp and move further inland, but I was forced to abandon most of my supplies as the tide rose."));
		lines.Add(new DialogueLine("", "The wind picked up as the storm gained strength..."));
		lines.Add(new DialogueLine("Player", "I’ve got to see if <Boat> is okay!"));
		lines.Add(new DialogueLine("", "I couldn’t see <Boat> anywhere along the shore..."));
		lines.Add(new DialogueLine("Player", "<Boat>! Where are you?"));
		lines.Add(new DialogueLine("", "I never saw <Boat> again, and I never escaped the island..."));
	     
		


        return lines;
    }
	private List<DialogueLine> Ending2()
    {
        var lines = new List<DialogueLine>();

      
		lines.Add(new DialogueLine("", "I woke up the next morning, ready to set sail..."));
		lines.Add(new DialogueLine("Player", "Where did <Boat> go?"));
		lines.Add(new DialogueLine("", "There were tracks in the sand, leading out to sea..."));
		lines.Add(new DialogueLine("Player", "Did <Boat> leave without me?"));
		lines.Add(new DialogueLine("Player", "<Boat>! Where are you?"));
		lines.Add(new DialogueLine("", "I never saw <Boat> again, and I never escaped the island..."));
		      
		


        return lines;
    }
	private List<DialogueLine> Ending3()
    {
        var lines = new List<DialogueLine>();

      
		lines.Add(new DialogueLine("", "Three days into our journey, we saw storm clouds on the horizon..."));
		lines.Add(new DialogueLine("Player", "Uh oh... that doesn’t look good."));
		lines.Add(new DialogueLine("Boat", "I think we left the island too late... we’ll have to do our best to brave the storm together, <Player>."));
		lines.Add(new DialogueLine("Player", "We’ve come this far together... I believe in us!"));
		lines.Add(new DialogueLine("", "The storms started, and only got worse... we found ourselves in the midst of a hurricane."));
		lines.Add(new DialogueLine("Boat", "<Player>, watch out!"));
		lines.Add(new DialogueLine("", "A huge wave washed over the boat, nearly throwing me overboard."));
		lines.Add(new DialogueLine("", "CRACK!"));
		lines.Add(new DialogueLine("", "<Boat>’s mast cracked as the wave slammed into it, then buckled under the force of the wind."));
		lines.Add(new DialogueLine("Player", "<Boat>! Are you all right?"));
		lines.Add(new DialogueLine("", "<Boat> was filling with water faster than I could bail it out.  One of the holes I repaired opened back up as <Boat> tipped violently starboard."));
		lines.Add(new DialogueLine("Boat", "<Player>! Grab onto my mast!"));
		lines.Add(new DialogueLine("Player", "I don’t think I can fix it!"));
		lines.Add(new DialogueLine("Boat", "Just hold onto it and don’t let go!"));
		lines.Add(new DialogueLine("", "I threw my arms around <Boat>’s mast, just as another wave nearly overturned us.  <Boat>’s mast snapped off its base and I was knocked off my feet, overboard into the turbulent sea."));
		lines.Add(new DialogueLine("Player", "*gasp* *gasp*"));
		lines.Add(new DialogueLine("", "My head went under as <Boat>’s mast spun in the water.  I thrashed my legs as I held on tight, trying to stabilize myself."));
		lines.Add(new DialogueLine("Player", "*splash* *cough* <Boat>!"));
		lines.Add(new DialogueLine("", "I surfaced and saw the waves had carried me further away.  I barely saw <Boat>’s bow as the waves crashed over us."));
		lines.Add(new DialogueLine("Boat", "<Player>!  Please hold on tight... I know you have it in you to make it home"));
		lines.Add(new DialogueLine("Player", "We’ll make it home together, <Boat>!  I’m not going to abandon you!"));
		lines.Add(new DialogueLine("Boat", "<Player>, I’m just happy *splash*"));
		lines.Add(new DialogueLine("Boat", "That you were here with me *splash*"));
		lines.Add(new DialogueLine("Boat", "At the end..."));
		lines.Add(new DialogueLine("", "<Boat> disappeared beneath the ocean..."));
		lines.Add(new DialogueLine("Player", "<Boat>! No!"));
		lines.Add(new DialogueLine("", "..."));
		lines.Add(new DialogueLine("", "..."));
		lines.Add(new DialogueLine("", "..."));
		lines.Add(new DialogueLine("", "I drifted across the ocean as the storm cleared..."));
		lines.Add(new DialogueLine("", "..."));
		lines.Add(new DialogueLine("", "A passing freighter saw me, and the crew lifted me aboard..."));
		lines.Add(new DialogueLine("", "..."));
		lines.Add(new DialogueLine("Crewman", "You’re lucky you’re alive!  Was there anyone else with you?"));
		lines.Add(new DialogueLine("", ">Yes"));
		lines.Add(new DialogueLine("Player", "Yes... <Boat> was with me."));
		lines.Add(new DialogueLine("Crewman", "Are they still out there?"));
		lines.Add(new DialogueLine("Player", "...No.  They didn’t make it."));
		lines.Add(new DialogueLine("", ">No"));
		lines.Add(new DialogueLine("Player", "...No.  Just me."));
		lines.Add(new DialogueLine("", "..."));
		
        
		


        return lines;
    }
	private List<DialogueLine> Ending4()
    {
        var lines = new List<DialogueLine>();

      
		
		lines.Add(new DialogueLine("", "BEST ENDING BEST ENDING BEST BEST BABIES"));
		
        
		


        return lines;
    }
}
