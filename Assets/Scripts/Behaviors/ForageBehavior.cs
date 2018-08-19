public class ForageBehavior : ActionBehavior
{
    private DialogueOption _goodbyeOption;

    private void Start()
    {
        _goodbyeOption = new DialogueOption("GOODBYE");
        _goodbyeOption.OnSelect.AddListener(OnGoodbye);
    }

    protected override void OnActionStart()
    {
        AddDialogueLine("You go out to find some supplies...");
        AddDialogueLine("You root around in the sand for a while...");
        AddDialogueLine("You end up finding some crabs that look like they might taste all right.");
        AddDialogueOption(_goodbyeOption);
    }

    void OnGoodbye()
    {
        EndAction();
    }

    protected override void EndAction()
    {
        GameStatus.Instance.InventoryItems["foodRaw"].Amount += 1;
        base.EndAction();
    }
}
