using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollection : MonoBehaviour
{
    public AudioClip audioclip;
    public ParticleSystem pickup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyContorler rubycontorler = collision.GetComponent<RubyContorler>();
        if (collision != null)
        {
            if(rubycontorler.CurrentHealth< rubycontorler.MaxHealth)
            {
                Instantiate(pickup, transform.position, Quaternion.identity);
                rubycontorler.ChangeHealth(1);
                rubycontorler.IsInvisible = true;
                Destroy(gameObject);
                rubycontorler.PlaySound(audioclip);
            }
        }

    }
}
