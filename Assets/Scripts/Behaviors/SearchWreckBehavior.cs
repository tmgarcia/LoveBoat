public class SearchWreckBehavior : ActionBehavior
{
    private DialogueOption _continueOption;

    private void Start()
    {
        _continueOption = new DialogueOption("Continue");
        _continueOption.OnSelect.AddListener(OnContinue);
    }

    protected override void OnActionStart()
    {
        AddDialogueLine("<Player>: There might be something useful in this wreckage...");
        AddDialogueLine("Hmm... a few rations... and it looks like...");
        AddDialogueLine("A small book.  'The Castaway's Guide to Survival'.  Sounds useful.");
        AddDialogueOption(_continueOption);
    }

    void OnContinue()
    {
        EndAction();
    }

    protected override void EndAction()
    {
        GameStatus.Instance.Flags["guide"].Status = true;
        base.EndAction();
    }
}
