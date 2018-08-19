using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StatPane : MonoBehaviour
{
    [SerializeField] private Screen _screen;

    [Header("Day Info")]
    [SerializeField] private Text _dayText;

    [SerializeField] private Text _timeText;
    [SerializeField] private Text _weatherText;
    [SerializeField] private Image _timeIcon;
    [SerializeField] private Image _weatherIcon;

    [Header("Resources")]
    [SerializeField] private Image _repairFill;
    [SerializeField] private Image _loveFill;
    [SerializeField] private Text _foodText;

    [Header("Items")]
    [SerializeField] private GameObject _itemsContainer;
    [SerializeField] private GameObject _itemPrefab;

    [Header("Flags")]
    [SerializeField] private GameObject _flagsContainer;
    [SerializeField] private GameObject _flagPrefab;

    [Header("Time Icons")]
    [SerializeField] private Sprite _morningIcon;
    [SerializeField] private Sprite _afternoonIcon;
    [SerializeField] private Sprite _eveningIcon;

    [Header("Weather Icons")]
    [SerializeField] private Sprite _sunnyIcon;
    [SerializeField] private Sprite _rainyIcon;
    [SerializeField] private Sprite _cloudyIcon;
    [SerializeField] private Sprite _clearIcon;

    private List<GameObject> _itemPool = new List<GameObject>();
    private List<GameObject> _flagPool = new List<GameObject>();

    void Start ()
    {
        foreach (Transform child in _itemsContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in _flagsContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        _screen.OnActiveChange.AddListener(OnScreenActiveChange);
        if (GameStatus.Instance.ResourceValues != null)
            UpdateStats();
    }

    void OnScreenActiveChange(bool isActive)
    {
        if(isActive)
            UpdateStats();
    }

    void UpdateStats()
    {
        foreach(var item in _itemPool)
        {
            item.SetActive(false);
        }
        foreach (var flag in _flagPool)
        {
            flag.SetActive(false);
        }

        _dayText.text = ""+GameStatus.Instance.CurrentDay;
        _timeText.text = GameStatus.Instance.CurrentTimeOfDay.ToString();
        _weatherText.text = GameStatus.Instance.CurrentWeather.ToString();
        _foodText.text = ""+GameStatus.Instance.FoodLevel.CurrentValue;

        Sprite timeSprite = null;
        switch (GameStatus.Instance.CurrentTimeOfDay)
        {
            case TimeOfDay.Morning:
                timeSprite = _morningIcon;
                break;
            case TimeOfDay.Afternoon:
                timeSprite = _afternoonIcon;
                break;
            case TimeOfDay.Evening:
                timeSprite = _eveningIcon;
                break;
            default:
                timeSprite = _morningIcon;
                break;
        }
        _timeIcon.sprite = timeSprite;

        Sprite weatherSprite = null;
        switch (GameStatus.Instance.CurrentWeather)
        {
            case Weather.Clear:
                weatherSprite = _clearIcon;
                break;
            case Weather.Sunny:
                weatherSprite = _sunnyIcon;
                break;
            case Weather.Cloudy:
                weatherSprite = _cloudyIcon;
                break;
            case Weather.Rainy:
                weatherSprite = _rainyIcon;
                break;
            default:
                weatherSprite = _clearIcon;
                break;
        }
        _weatherIcon.sprite = weatherSprite;

        var repair = GameStatus.Instance.RepairLevel;
        _repairFill.fillAmount = (float)repair.CurrentValue / (float)repair.Resource.MaxValue;

        var love = GameStatus.Instance.LoveLevel;
        _loveFill.fillAmount = (float)love.CurrentValue / (float)love.Resource.MaxValue;

        foreach (var item in GameStatus.Instance.InventoryItems)
        {
            if (item.Value.Amount <= 0) continue;

            var itemObject = GetItemFromPool();
            var lv = itemObject.GetComponent<LabelValue>();
            lv.SetLabel(item.Value.Item.Label+":");
            lv.SetValue(""+item.Value.Amount);
            itemObject.SetActive(true);
        }

        foreach (var flag in GameStatus.Instance.Flags)
        {
            if (!flag.Value.Status) continue;

            var flagObject = GetFlagFromPool();
            var txt = flagObject.GetComponent<Text>();
            txt.text = flag.Value.Flag.Label;
            flagObject.SetActive(true);
        }
    }

    private GameObject GetItemFromPool()
    {
        var itemObject = _itemPool.FirstOrDefault(i => i.activeSelf == false);
        if(itemObject == null)
        {
            itemObject = GameObject.Instantiate(_itemPrefab, _itemsContainer.transform);
            _itemPool.Add(itemObject);
        }
        return itemObject;
    }

    private GameObject GetFlagFromPool()
    {
        var flagObject = _flagPool.FirstOrDefault(i => i.activeSelf == false);
        if (flagObject == null)
        {
            flagObject = GameObject.Instantiate(_flagPrefab, _flagsContainer.transform);
            _flagPool.Add(flagObject);
        }
        return flagObject;
    }
}
