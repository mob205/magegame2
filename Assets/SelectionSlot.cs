using UnityEngine;
using UnityEngine.UI;

public class SelectionSlot : Selectable
{
    public AbilitySelector Selector;
    public int Index;
    public Sprite Sprite { set { _image.sprite = value; } }

    private Image _image;

    public void Awake()
    {
        _image = GetComponentInChildren<Image>();
    }
    public override void Select()
    {
        Selector.ProcessSelect(Index);
    }
}
