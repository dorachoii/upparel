using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerState : MonoBehaviourPunCallbacks
{
    public State currState;
    public Animator animator;
    public PhotonView PV;

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
                PV.RPC("idleRpc", RpcTarget.All);
                break;
            case State.WALK:
                animator.SetBool("isMoving", true);
                PV.RPC("walkRpc", RpcTarget.All);
                break;
            case State.JUMP:
                animator.SetTrigger("JumpTrigger");
                PV.RPC("jumpRpc", RpcTarget.All);
                break;
            case State.DANCE:
                animator.SetTrigger("DanceTrigger");
                PV.RPC("danceRpc", RpcTarget.All);
                break;
        }
    }

    [PunRPC]
    void idleRpc()
    {
       animator.SetBool("isMoving", false);
    }

    [PunRPC]
    void walkRpc()
    {
       animator.SetBool("isMoving", true);
    }

    [PunRPC]
    void jumpRpc()
    {
       animator.SetTrigger("JumpTrigger");
    }

    [PunRPC]
    void danceRpc()
    {
       animator.SetTrigger("DanceTrigger");
    }
}
