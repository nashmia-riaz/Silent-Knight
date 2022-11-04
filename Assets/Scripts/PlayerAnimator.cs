using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerJump()
    {
        // Check if animator is present, if so set Jump Trigger
        if (anim != null && anim.GetBool("isGrounded")) {
            GroundedBool(false);
            anim.SetTrigger("Jump");
        }
    }

    public void GroundedBool(bool isGrounded)
    {
        anim.SetBool("isGrounded", isGrounded);
    }

    public void TriggerRunning()
    {
        if (anim != null)
            anim.SetTrigger("Running");
    }

    public void TriggerPain()
    {
        if (anim != null)
        {
            anim.SetTrigger("Hurt");
        }
    }

    public void TriggerDeath()
    {
        if (anim != null)
        {
            anim.SetTrigger("Death");
        }
    }
}
