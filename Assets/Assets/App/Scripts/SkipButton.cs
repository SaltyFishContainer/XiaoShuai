using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    public Action doSkip;
    public Action cancelSkip;


    public void DoSkip()
    {
        doSkip?.Invoke();
    }
    public void CancelSkip()
    {
        cancelSkip?.Invoke();

    }
}
