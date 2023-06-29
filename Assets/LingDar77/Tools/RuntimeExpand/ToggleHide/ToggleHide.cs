
using UnityEngine.InputSystem;
using UnityEngine;

public class ToggleHide : MonoBehaviour
{
    [SerializeField] private InputActionReference key;
    [SerializeField] private bool initialHide = true;
    [SerializeField] private bool displayOnTop = true;
    private InputAction action;

    private void PerformToggleHide(InputAction.CallbackContext _)
    {
        if (!gameObject.activeSelf)
        {
            transform.SetAsLastSibling();
        }
        gameObject.SetActive(!gameObject.activeSelf);
    }
    private void Awake()
    {
        action = key.ToInputAction();
        if (!action.enabled)
        {
            action.Enable();
        }
        action.performed += PerformToggleHide;
        if (initialHide)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        action.performed -= PerformToggleHide;
    }
}