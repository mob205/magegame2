using UnityEngine;
using UnityEngine.UI;

public class SpellbarUI : MonoBehaviour
{
    private SpellbarSlot[] _slots;
    private Ability[] _abilities;
    private void OnEnable()
    {
        PlayerCaster.OnAbilityChange += UpdateUI;
    }
    private void OnDisable()
    {
        PlayerCaster.OnAbilityChange -= UpdateUI;
    }
    private void Awake()
    {
        _slots = GetComponentsInChildren<SpellbarSlot>();
    }
    private void UpdateUI()
    {
        _abilities = PlayerCaster.Abilities;
        for(int i = 0; i < _abilities.Length; i++)
        {
            _slots[i].UpdateAbility(_abilities[i]);
        }
    }
}
