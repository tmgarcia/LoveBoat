public class BuildCampBehavior : ActionBehavior
{
    private DialogueOption _goodbyeOption;

    private void Start()
    {
        _goodbyeOption = new DialogueOption("GOODBYE");
        _goodbyeOption.OnSelect.AddListener(OnGoodbye);
    }

    protected override void OnActionStart()
    {
        AddDialogueLine("I put some work into expanding my camp.");
        AddDialogueOption(_goodbyeOption);
    }

    void OnGoodbye()
    {
        EndAction();
    }

    protected override void EndAction()
    {
        base.EndAction();
    }
}
