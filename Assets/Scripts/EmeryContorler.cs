using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmeryContorler : MonoBehaviour
{
    public static EmeryContorler instance;
    public float speed = 3.0f;
    private bool hasspeed;
    private float direction = 1;
    private float timer = 2f;
    public bool vertical;
    public float y_max;
    public float y_min;
    public float x_max;
    public float x_min;
    private Animator animator;
    private bool broken = true;
    public ParticleSystem smokeeffect;
    public AudioClip audiohit, audiofixed;
    private AudioSource audiosource;
    public ParticleSystem hiteffect;
    public int fixedNum;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (vertical)
        {
            animator.SetFloat("Vertical", direction);
        }
        else
        {
            animator.SetFloat("Direction", direction);
        }
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!broken)
        {
            animator.SetBool("Broken", broken);
            return;
        }
        Vector2 vector2 = transform.position;
        float x_axis = transform.position.x;
        float y_axis = transform.position.y;
        if (vertical)
        {
            if (y_axis <= y_min)
            {
                if (timer > 0)
                {
                    speed = 0;
                    hasspeed = false;
                    animator.SetBool("HasSpeed", hasspeed);
                    timer -= Time.fixedDeltaTime;
                }
                if (timer <= 0)
                {
                    direction = 1;
                    animator.SetFloat("Direction", 0);
                    animator.SetFloat("Vertical", direction);
                    speed = 3f;
                    hasspeed = true;
                    animator.SetBool("HasSpeed", hasspeed);
                    vector2.y += speed * Time.fixedDeltaTime;
                    transform.position = vector2;
                }
            }
            if (y_axis < y_max && y_axis > y_min)
            {
                timer = 2f;
                vector2.y += speed * Time.fixedDeltaTime;
                transform.position = vector2;
                audiosource.Stop();
            }
            if (y_axis >= y_max)
            {
                if (timer > 0)
                {
                    speed = 0;
                    hasspeed = false;
                    animator.SetBool("HasSpeed", hasspeed);
                    timer -= Time.fixedDeltaTime;
                }
                if (timer <= 0)
                {
                    direction = -1;
                    animator.SetFloat("Direction", 0);
                    animator.SetFloat("Vertical", direction);
                    speed = -3f;
                    hasspeed = true;
                    animator.SetBool("HasSpeed", hasspeed);
                    vector2.y += speed * Time.fixedDeltaTime;
                    transform.position = vector2;
                }
            }
        }
        else
        {
            if (x_axis <= x_min)
            {
                if (timer > 0)
                {
                    speed = 0;
                    hasspeed = false;
                    animator.SetBool("HasSpeed", hasspeed);
                    timer -= Time.fixedDeltaTime;
                }
                if (timer <= 0)
                {
                    direction = 1;
                    animator.SetFloat("Direction", direction);
                    animator.SetFloat("Vertical", 0);
                    speed = 3f;
                    hasspeed = true;
                    animator.SetBool("HasSpeed", hasspeed);
                    vector2.x += speed * Time.fixedDeltaTime;
                    transform.position = vector2;
                }
            }
            if (x_axis < x_max && x_axis > x_min)
            {
                timer = 2f;
                vector2.x += speed * Time.fixedDeltaTime;
                transform.position = vector2;
            }
            if (x_axis >= x_max)
            {
                if (timer > 0)
                {
                    speed = 0;
                    hasspeed = false;
                    animator.SetBool("HasSpeed", hasspeed);
                    timer -= Time.fixedDeltaTime;
                }
                if (timer <= 0)
                {
                    direction = -1;
                    animator.SetFloat("Direction", direction);
                    animator.SetFloat("Vertical", 0);
                    speed = -3f;
                    hasspeed = true;
                    animator.SetBool("HasSpeed", hasspeed);
                    vector2.x += speed * Time.fixedDeltaTime;
                    transform.position = vector2;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyContorler rubycontorler = collision.gameObject.GetComponent<RubyContorler>();
        if (rubycontorler != null)
        {
            rubycontorler.ChangeHealth(-1);
            rubycontorler.animator.SetTrigger("Hit");
            rubycontorler.PlaySound(audiohit);
        }
    }

    public void Fixed()
    {
        Instantiate(hiteffect, transform.transform.position, Quaternion.identity);
        broken = false;
        smokeeffect.Stop();
        audiosource.volume = 1;
        audiosource.Stop();
        audiosource.PlayOneShot(audiofixed);
    }
}
