
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] private bool hideOthers = true;
    public UnityEvent<bool> onTogglePerformed;
    private static HashSet<ToggleHide> openedPages = new HashSet<ToggleHide>();

#if ENABLE_INPUT_SYSTEM
    private void PerformToggleHide(InputAction.CallbackContext _)
    {
        PerformToggleHide();
    }
    private void Start()
    {
        if (actionRef != null)
        {
            action = actionRef.ToInputAction();
            if (!action.enabled)
            {
                action.Enable();
            }
            action.performed += PerformToggleHide;
        }
        if (initialHide)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        if (actionRef != null)
            action.performed -= PerformToggleHide;
    }
#else
    private void Start()
    {
        if (initialHide)
        {
            gameObject.SetActive(false);
        }
    }
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
        if (!gameObject.activeSelf)
        {
            if (hideOthers)
            {
                foreach (var page in openedPages)
                {
                    if (page != this)
                        page.gameObject.SetActive(false);
                }
                openedPages.Clear();
                openedPages.Add(this);
            }
            if (displayOnTop)
            {
                transform.SetAsLastSibling();
            }
        }

        gameObject.SetActive(!gameObject.activeSelf);
        onTogglePerformed?.Invoke(gameObject.activeSelf);
    }
    public void PerformHide()
    {
        gameObject.SetActive(false);
        onTogglePerformed?.Invoke(false);
    }
    public void PerformShow()
    {
        if (hideOthers)
        {
            foreach (var page in openedPages)
            {
                if (page != this)
                    page.gameObject.SetActive(false);
            }
            openedPages.Clear();
            openedPages.Add(this);
        }
        if (displayOnTop)
        {
            transform.SetAsLastSibling();
        }

        gameObject.SetActive(true);
        onTogglePerformed?.Invoke(true);

    }
}