using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
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

        Vector3 direction = player.transform.localPosition - transform.localPosition;
        if(direction.x > 0 && anim.GetBool("InCombat") == true)
        {
            sprite.flipX = false;
        }
        else if(direction.x < 0 && anim.GetBool("InCombat") == true)
        {
            sprite.flipX = true;
        }
    }

    public void Damage()
    {
        Debug.Log("Damage()");
        Health--;

        anim.SetTrigger("Hit");
        IsHit = true;
        anim.SetBool("InCombat", true);

        if(Health == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
