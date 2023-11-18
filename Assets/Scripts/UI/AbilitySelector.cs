using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class AbilitySelector : MonoBehaviour
{
    [Header("Display Settings")]
    [SerializeField] private Transform _startPos;
    [SerializeField] private int _slotOffset;
    [SerializeField] private int _slotsPerRow;
    [SerializeField] private int _maxRows;
    [Header("References")]
    [SerializeField] private Canvas _ui;
    [SerializeField] private SelectionSlot _selectionSlot;

    private Ability[] _spells;
    private PlayerInput _input;
    private PlayerCaster _caster;
    private SelectionSlot[] _slots;

    public static AbilitySelector Instance { get; private set; }
    private void Start()
    {
        _input.actions["Quit"].started += Deactivate;
    }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } 
        else
        {
            Instance = this;
        }

        var player = PlayerInstance.Instance;
        _input = player.GetComponent<PlayerInput>();
        _caster = player.GetComponent<PlayerCaster>();
    }
    private void GenerateSelectionSlots()
    {
        _spells = SpellUnlocker.GetUnlockedAbilities();
        Debug.Log($"Found {_spells.Length} spells");
        _slots = new SelectionSlot[_spells.Length];
        int cnt = 0;
        while(cnt < _spells.Length)
        {
            var pos = _startPos.position + new Vector3(cnt % _slotsPerRow * _slotOffset, -1 * (cnt / _slotsPerRow) * _slotOffset, 0);
            var slot = Instantiate(_selectionSlot, pos, Quaternion.identity, _ui.transform);
            slot.Selector = this;
            slot.Index = cnt;
            slot.Sprite = _spells[cnt].Icon;
            _slots[cnt] = slot;
            cnt++;
        }
    }
    private void ClearSelectionSlots()
    {
        for(int i = 0; i < _slots.Length; i++)
        {
            Destroy(_slots[i].gameObject);
        }
    }
    public void Activate()
    {
        _ui.gameObject.SetActive(true);
        _input.SwitchCurrentActionMap("Menu");
        GenerateSelectionSlots();
    }
    public void Deactivate(InputAction.CallbackContext context)
    {
        ClearSelectionSlots();
        _ui.gameObject.SetActive(false);
        _input.SwitchCurrentActionMap("Player");
    }
    public void ProcessSelect(int index)
    {
        _caster.AddAbility(_spells[index]);
    }
}
