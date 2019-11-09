using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();
    }

    public void Damage()
    {
        Debug.Log("MossGaint: Damage()");
        Health--;

        anim.SetTrigger("Hit");
        IsHit = true;
        anim.SetBool("InCombat", true);

        if (Health == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
