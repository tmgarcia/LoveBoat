public class CookBehavior : ActionBehavior
{
    private DialogueOption _goodbyeOption;

    private void Start()
    {
        _goodbyeOption = new DialogueOption("GOODBYE");
        _goodbyeOption.OnSelect.AddListener(OnGoodbye);
    }

    protected override void OnActionStart()
    {
        AddDialogueLine("HI HOW ARE YOU");
        AddDialogueOption(_goodbyeOption);
    }

    void OnGoodbye()
    {
        EndAction();
    }

    protected override void EndAction()
    {
        GameStatus.Instance.LoveLevel.CurrentValue += 50;
        base.EndAction();
    }
}
