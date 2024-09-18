using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public State currState;
    public Animator animator;

    public enum State
    {
        IDLE,
        WALK,
        JUMP,
        DANCE
    }

    public void ChangeState(State s)
    {
        currState = s;

        switch (currState)
        {
            case State.IDLE:
                animator.SetBool("isMoving", false);
                break;
            case State.WALK:
                animator.SetBool("isMoving", true);
                break;
            case State.JUMP:
                animator.SetTrigger("JumpTrigger");
                break;
            case State.DANCE:
                animator.SetTrigger("DanceTrigger");
                break;
        }
    }
}
