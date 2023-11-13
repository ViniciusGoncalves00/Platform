using UnityEngine;

public abstract class NPCBaseClass : MonoBehaviour
{
    public virtual void EnterIdleState()
    {
    }

    public virtual void ExitIdleState()
    {
    }

    public virtual void EnterSearchState()
    {
    }

    public virtual void ExitSearchState()
    {
    }

    public virtual void EnterChaseState()
    {
    }

    public virtual void ExitChaseState()
    {
    }

    public virtual void EnterAttackState()
    {
    }

    public virtual void ExitAttackState()
    {
    }

    public virtual void Idle()
    {
    }

    public virtual void Search()
    {
    }

    public virtual void Chase()
    {
    }

    public virtual void Attack()
    {
    }
}