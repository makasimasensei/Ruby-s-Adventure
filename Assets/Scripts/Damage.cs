using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public AudioClip audioclip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyContorler rubycontorler = collision.GetComponent<RubyContorler>();
        if (collision != null)
        {
            rubycontorler.ChangeHealth(-1);
            rubycontorler.IsInvisible = true;
            rubycontorler.animator.SetTrigger("Hit");
            rubycontorler.PlaySound(audioclip);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        RubyContorler rubycontorler = collision.GetComponent<RubyContorler>();
        if (collision != null)
        {
            if (rubycontorler.IsInvisible == false)
            {
                rubycontorler.ChangeHealth(-1);
                rubycontorler.IsInvisible = true;
                rubycontorler.animator.SetTrigger("Hit");
                rubycontorler.PlaySound(audioclip);
            }
        }
    }
}
