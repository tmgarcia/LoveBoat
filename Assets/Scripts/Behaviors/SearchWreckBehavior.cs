﻿using System.Collections.Generic;

public class SearchWreckBehavior : ActionBehavior
{
    private Dialogue _dialogue;

    private void Start()
    {
        var lines = new List<DialogueLine>() {
            new DialogueLine("There might be something useful in this wreckage...", null),
            new DialogueLine("Hmm... a few rations... and it looks like...", null),
            new DialogueLine("A small book.  'The Castaway's Guide to Survival'.  Sounds useful.", null),
        };
        _dialogue = new Dialogue("Player", lines, null, () => {
            EndAction();
        });
    }

    protected override void OnActionStart()
    {
        SetDialogue(_dialogue);
    }

    protected override void EndAction()
    {
        GameStatus.Instance.Flags["guide"].Status = true;
        base.EndAction();
    }
}
