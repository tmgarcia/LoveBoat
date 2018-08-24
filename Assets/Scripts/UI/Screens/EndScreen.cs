using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Screen))]
public class EndScreen : MonoBehaviour
{
    [Header("End Screens")]
    [SerializeField] private GameObject _starvedScreen;
    [SerializeField] private GameObject _abandonedScreen;
    [SerializeField] private GameObject _sacrificedScreen;
    [SerializeField] private GameObject _trueWinScreen;
    [SerializeField] private GameObject _hurricaneScreen;
    [Header("End Screens")]
    [SerializeField] private AudioClip _starvedMusic;
    [SerializeField] private AudioClip _abandonedMusic;
    [SerializeField] private AudioClip _sacrificedMusic;
    [SerializeField] private AudioClip _trueWinMusic;
    [SerializeField] private AudioClip _hurricaneMusic;


    public bool ShortcutEnd = false;
    public int ShortcuttedEnding = -1;

    private Screen _screen = null;

    void OnEnable()
    {
        if (_screen == null)
            _screen = gameObject.GetComponent<Screen>();
        if (_screen.IsActive)
            ShowEnding();
    }

    public void OnRestartClick()
    {
        DisableAll();
        GameManager.Instance.RestartGame();
    }

    public void OnCreditsClick()
    {
        DisableAll();
        ScreenManager.Instance.GoToScreen("credits");
    }

    void DisableAll()
    {
        ShortcutEnd = false;
        _starvedScreen.SetActive(false);
        _abandonedScreen.SetActive(false);
        _sacrificedScreen.SetActive(false);
        _trueWinScreen.SetActive(false);
        _hurricaneScreen.SetActive(false);
    }

    void ShowEnding()
    {
        if (ShortcutEnd)
        {
            DoShortcutEnding();
            return;
        }

        if (!GameStatus.Instance.HasFood)
        {
            StarveEnd();
        }
        else if (GameStatus.Instance.BoatFullyRepaired)
        {
            if (!GameStatus.Instance.BoatFullyLoved)
            {
                AbandonEnd();
            }
            else if (GameStatus.Instance.CurrentDay >= GameManager.Instance.Config.LateDay)
            {
                SacrificeEnd();
            }
            else
            {
                BestEnd();
            }
        }
        else
        {
            HurricaneEnd();
        }
    }

    void DoShortcutEnding()
    {
        ShortcutEnd = false;
        switch (ShortcuttedEnding)
        {
            case 0:
                StarveEnd();
                break;
            case 1:
                HurricaneEnd();
                break;
            case 2:
                AbandonEnd();
                break;
            case 3:
                SacrificeEnd();
                break;
            case 4:
                BestEnd();
                break;
            default:
                break;
        }
    }

    void StarveEnd()
    {
        _starvedScreen.SetActive(true);
        AudioManager.Instance.PlayMusic(_starvedMusic, true);
        _starvedScreen.GetComponent<DialogueScreen>().SetDialogue(Ending0());
    }
    void HurricaneEnd()
    {
        _hurricaneScreen.SetActive(true);
        AudioManager.Instance.PlayMusic(_hurricaneMusic, true);
        _hurricaneScreen.GetComponent<DialogueScreen>().SetDialogue(Ending1());
    }
    void AbandonEnd()
    {
        _abandonedScreen.SetActive(true);
        AudioManager.Instance.PlayMusic(_abandonedMusic, true);
        _abandonedScreen.GetComponent<DialogueScreen>().SetDialogue(Ending2());
    }
    void SacrificeEnd()
    {
        _sacrificedScreen.SetActive(true);
        AudioManager.Instance.PlayMusic(_sacrificedMusic, true);
        _sacrificedScreen.GetComponent<DialogueScreen>().SetDialogue(Ending3());
    }
    void BestEnd()
    {
        _trueWinScreen.SetActive(true);
        AudioManager.Instance.PlayMusic(_trueWinMusic, true);
    }
    

    // Starved
    private Dialogue Ending0()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "You starved to death."));

        var options = new List<DialogueOption>()
        {
            new DialogueOption("Play Again", OnRestartClick)
        };

        return new Dialogue(lines, options);
    }

    // Hurricane
    private Dialogue Ending1()
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

        var options = new List<DialogueOption>()
        {
            new DialogueOption("Play Again", OnRestartClick)
        };

        return new Dialogue(lines, options);
    }
    // Abandoned
    private Dialogue Ending2()
    {
        var lines = new List<DialogueLine>();

        lines.Add(new DialogueLine("", "I woke up the next morning, ready to set sail..."));
        lines.Add(new DialogueLine("Player", "Where did <Boat> go?"));
        lines.Add(new DialogueLine("", "There were tracks in the sand, leading out to sea..."));
        lines.Add(new DialogueLine("Player", "Did <Boat> leave without me?"));
        lines.Add(new DialogueLine("Player", "<Boat>! Where are you?"));
        lines.Add(new DialogueLine("", "I never saw <Boat> again, and I never escaped the island..."));

        var options = new List<DialogueOption>()
        {
            new DialogueOption("Play Again", OnRestartClick)
        };

        return new Dialogue(lines, options);
    }
    // Too late/sacrifice
    private Dialogue Ending3()
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

        var yesDialogue = new Dialogue(
            new List<DialogueLine>()
            {
                new DialogueLine("Player", "Yes... <Boat> was with me."),
                new DialogueLine("Crewman", "Are they still out there?"),
                new DialogueLine("Player", "...No.  They didn’t make it.")
            },
            new List<DialogueOption>()
            {
                new DialogueOption("Play Again", OnRestartClick)
            }
        );

        var noDialogue = new Dialogue(
           new List<DialogueLine>()
           {
                new DialogueLine("Player", "...No.  Just me."),
                new DialogueLine("", "...")
           },
           new List<DialogueOption>()
           {
                new DialogueOption("Play Again", OnRestartClick)
           }
       );

        var options = new List<DialogueOption>()
        {
            new DialogueOption("Yes", () =>
            {
                _sacrificedScreen.GetComponent<DialogueScreen>().SetDialogue(yesDialogue);
            }),
            new DialogueOption("No", () =>
            {
                _sacrificedScreen.GetComponent<DialogueScreen>().SetDialogue(noDialogue);
            })
        };

        return new Dialogue(lines, options);
    }
}
