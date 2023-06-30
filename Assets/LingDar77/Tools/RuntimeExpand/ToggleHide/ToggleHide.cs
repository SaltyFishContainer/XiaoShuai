
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class ToggleHide : MonoBehaviour
{
#if ENABLE_INPUT_SYSTEM
    [SerializeField] private InputActionReference actionRef;
    private InputAction action;
#else
    [SerializeField] private KeyCode keyCode;
#endif
    [SerializeField] private bool initialHide = true;
    [SerializeField] private bool displayOnTop = true;


#if ENABLE_INPUT_SYSTEM
    private void PerformToggleHide(InputAction.CallbackContext _)
    {
        PerformToggleHide();
    }
    private void Awake()
    {
        if (actionRef == null) return;
        action = actionRef.ToInputAction();
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
#else
    private void Update()
    {
        if (keyCode != KeyCode.None)
            if (Input.GetKey(keyCode))
            {
                PerformToggleHide();
            }
    }
#endif
    public void PerformToggleHide()
    {
        if (displayOnTop && !gameObject.activeSelf)
        {
            transform.SetAsLastSibling();
        }
        gameObject.SetActive(!gameObject.activeSelf);
    }
}