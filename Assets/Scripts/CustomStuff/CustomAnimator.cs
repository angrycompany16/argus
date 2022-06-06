// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CustomAnimator : MonoBehaviour
{
    Animator anim;
    string currentState;

    void Start() {
        anim = GetComponent<Animator>();    
    }

    public void ChangeAnimationState(string state) {
        if (state == currentState) return;

        currentState = state;
        anim.Play(state);
    }
}
