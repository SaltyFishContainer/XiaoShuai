using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class EventHandler : MonoBehaviour
{
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public void Call(string id)
    {
        var action = GetEvent(id);
        action?.Invoke();
    }
    public Action GetEvent(string id)
    {
        actions.TryGetValue(id, out Action aciton);
        return aciton;
    }
    public void Unsubscribe(string id, Action callback)
    {
        var action = GetEvent(id);
        if (action != null)
        {
            action -= callback;
            actions.Remove(id);
            if (action != null)
            {
                actions.Add(id, action);
            }
        }
    }
    public void Subscribe(string id, Action callback)
    {
        var action = GetEvent(id);
        if (action != null)
        {
            action += callback;
            actions.Remove(id);
            actions.Add(id, action);

        }
        else
        {
            action += callback;
            actions.Add(id, action);

        }
    }
    public void SubscribeOnce(string id, Action callback)
    {
        Action warpper = null;
        warpper = () =>
        {
            callback.Invoke();
            Unsubscribe(id, warpper);
        };
        var action = GetEvent(id);
        if (action != null)
        {
            action += warpper;
            actions.Remove(id);
            actions.Add(id, action);

        }
        else
        {
            action += warpper;
            actions.Add(id, action);

        }
    }



    public static bool IsMouseOverUI()
    {
        return Application.isFocused && EventSystem.current.IsPointerOverGameObject();
    }
}