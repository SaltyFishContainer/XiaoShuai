using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationRotator : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float SpinSpeed = 10f;

    public void OnSliderValueChange(float val)
    {
        if (target)
        {
            target.localRotation = Quaternion.Euler(0, 0, val * SpinSpeed);
        }
    }
}
