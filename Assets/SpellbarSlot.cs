using UnityEngine;
using UnityEngine.UI;

public class SpellbarSlot : MonoBehaviour
{
    private Sprite _defaultSprite;
    private Image _slotImage;
    private Slider _timer;
    private Ability _ability;
    public void UpdateAbility(Ability ability)
    {
        _ability = ability;
        if (_ability)
        {
            _slotImage.sprite = _ability.Icon;
        }
        else
        {
            _slotImage.sprite = _defaultSprite;
        }
    }
    private void Awake()
    {
        _slotImage = GetComponent<Image>();
        _timer = GetComponentInChildren<Slider>();
        _defaultSprite = _slotImage.sprite;
    }
    private void Update()
    {
        if (_ability)
        {
            _timer.value = _ability.RemainingCooldown / _ability.BaseCooldown;
        }
        else
        {
            _timer.value = 0;
        }
    }
}
