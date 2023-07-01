using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    [SerializeField] private TriggerArea triggerArea;
    [SerializeField] private GameController gameController;
    [SerializeField] private Animator animator;
    private void Awake()
    {
        triggerArea.onPointerIn.AddListener(() =>
        {
            animator.SetBool("begin", true);
        });
        triggerArea.onPointerOut.AddListener(() =>
       {
           animator.SetBool("begin", false);
       });
    }

}
