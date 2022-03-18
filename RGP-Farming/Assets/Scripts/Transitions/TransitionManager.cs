using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : Singleton<TransitionManager>
{
    [SerializeField] private GameObject _circleTransitionUI;

    private Animator _circleAnimator;

    private void Awake()
    {
        _circleAnimator = _circleTransitionUI.GetComponent<Animator>();
    }

    public void CallTransition(float pSeconds)
    {
        StartCoroutine(Transition(pSeconds));
    }

    IEnumerator Transition(float pSeconds)
    {
        _circleTransitionUI.SetActive(true);
        yield return new WaitForSeconds(pSeconds);
        _circleAnimator.SetTrigger("End");
    }
}
