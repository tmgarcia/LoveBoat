using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boat : MonoSingleton<Boat>
{
    [SerializeField] private Sprite _angryFace;
    [SerializeField] private Sprite _annoyedFace;
    [SerializeField] private Sprite _blushingFace;
    [SerializeField] private Sprite _happyFace;
    [SerializeField] private Sprite _neutralFace;
    [SerializeField] private Sprite _sadFace;

    public bool IsVisible { get; private set; }
    public bool HasSail { get; private set; }
    public BoatFace CurrentFace { get; private set; }

    public UnityEvent OnUpdate = new UnityEvent();

    public Dictionary<BoatFace, Sprite> BoatFaceSprites = new Dictionary<BoatFace, Sprite>();

    private void Awake()
    {
        BoatFaceSprites.Add(BoatFace.Angry, _angryFace);
        BoatFaceSprites.Add(BoatFace.Annoyed, _annoyedFace);
        BoatFaceSprites.Add(BoatFace.Blushing, _blushingFace);
        BoatFaceSprites.Add(BoatFace.Happy, _happyFace);
        BoatFaceSprites.Add(BoatFace.Neutral, _neutralFace);
        BoatFaceSprites.Add(BoatFace.Sad, _sadFace);
        Reset();
    }

    private void Start()
    {
        

    }

    public void Reset()
    {
        IsVisible = false;
        HasSail = false;
        CurrentFace = BoatFace.Neutral;
        OnUpdate.Invoke();
    }

    public void SetVisible(bool visible)
    {
        IsVisible = visible;
        OnUpdate.Invoke();
    }

    public void SetFace(BoatFace face)
    {
        CurrentFace = face;
        OnUpdate.Invoke();
    }

    public void SetHasSail(bool hasSail)
    {
        HasSail = hasSail;
        OnUpdate.Invoke();
    }
}

public enum BoatFace
{
    Angry,
    Annoyed,
    Blushing,
    Happy,
    Neutral,
    Sad
}