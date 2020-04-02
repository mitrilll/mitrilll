using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedStateMachineBehaviour : StateMachineBehaviour
{
    [SerializeField] private int _stateCount = 0;
    [SerializeField] private string _randomizedParametrName = string.Empty;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("RandomizedStateMachineBehaviour.OnStateEnter");
        int result = UnityEngine.Random.Range(0, _stateCount);
        animator.SetInteger(_randomizedParametrName, result);
    }

}
