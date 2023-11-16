using UnityEngine;
using UnityEngine.InputSystem;

public class AbilitySelector : MonoBehaviour
{
    [SerializeField] private Canvas _ui;
    private PlayerInput _input;
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

        _input = PlayerInstance.Instance.GetComponent<PlayerInput>();
    }
    public void Activate()
    {
        _ui.gameObject.SetActive(true);
        _input.SwitchCurrentActionMap("Menu");
    }
    public void Deactivate(InputAction.CallbackContext context)
    {
        _ui.gameObject.SetActive(false);
        _input.SwitchCurrentActionMap("Player");
    }
}
