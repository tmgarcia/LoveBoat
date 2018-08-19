using System.Collections.Generic;
using UnityEngine;

public class CookBehavior : ActionBehavior
{
    [SerializeField] private AudioClip _cookingSound;

    private Dialogue _dialogue;

    private void Start()
    {
        var lines = new List<DialogueLine>() {
            new DialogueLine("Player", "I COOK", () => {
                Debug.Log("COOK");
                AudioManager.Instance.PlaySoundEffect(_cookingSound, 2);
            }),
            new DialogueLine("Boat", "OH HELLO", () => {
                Boat.Instance.SetVisible(true);
                Boat.Instance.SetFace(BoatFace.Happy);
            })
        };
        var options = new List<DialogueOption>()
        {
            new DialogueOption("GOODBYE", () => {
                Boat.Instance.SetVisible(false);
                EndAction();
            })
        };
        _dialogue = new Dialogue(lines, options, null);
    }

    protected override void OnActionStart()
    {
        SetDialogue(_dialogue);
    }

    protected override void EndAction()
    {
        GameStatus.Instance.FoodLevel.CurrentValue += 50;
        base.EndAction();
    }
}
