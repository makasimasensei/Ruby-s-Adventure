using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyContorler : MonoBehaviour
{
    public float speed = 3f;
    private float h;
    private float v;
    private Rigidbody2D Rigidbody2D;
    private readonly int maxHealth = 5;
    private int currentHealth;
    private bool isInvisible = false;
    private float invisibleTimer;
    private readonly float invisibleTime = 1.5f;
    public Animator animator;
    public GameObject bulletprefab;
    private Vector2 lookdirection = new(0, -1);
    private bool hasspeed;
    private AudioSource audiosource;
    public AudioClip audioclip;
    public bool hasTask = false;
    private Vector3 rebornPos;

    public int MaxHealth { get => maxHealth;}
    public int CurrentHealth { get => currentHealth;}
    public bool IsInvisible { get => isInvisible; set => isInvisible = value; }
    public float H { get => h; set => h = value; }
    public float V { get => v; set => v = value; }

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = MaxHealth;
        invisibleTimer = invisibleTime;
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        rebornPos = transform.position;
    }
    private void Update()
    {
        if (hasTask == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Launch");
                Launch();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(Rigidbody2D.position +
                Vector2.up * 0.2f, lookdirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NPCDialog npcdialog = hit.collider.GetComponent<NPCDialog>();
                if (npcdialog != null)
                {
                    npcdialog.Displaydialog();
                    hasTask = true;
                }
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");
        Vector2 move = new(H, V);
        if (H != 0 || V != 0)
        {
            hasspeed = true;
            lookdirection.Set(move.x, move.y);
            lookdirection.Normalize();
        }
        else
        {
            hasspeed = false;
        }

        animator.SetBool("Hasspeed", hasspeed);
        animator.SetFloat("MoveX", lookdirection.x);
        animator.SetFloat("MoveY", lookdirection.y);
        Vector2 pos = transform.position;
        pos.x += move.x * Time.fixedDeltaTime * speed;
        pos.y += move.y * Time.fixedDeltaTime * speed;
        Rigidbody2D.MovePosition(pos);

        if (IsInvisible)
        {
            invisibleTimer -= Time.fixedDeltaTime;
            if (invisibleTimer <= 0)
            {
                isInvisible = false;
                invisibleTimer = invisibleTime;
            }
        }
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, MaxHealth);
        UIHealth.Instance.SetValue(currentHealth / (float)maxHealth);
        if (currentHealth <=0) { Reborn(); }
    }

    private void Launch()
    {
        GameObject bulletobject = Instantiate(bulletprefab,
            Rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Bullet bullet = bulletobject.GetComponent<Bullet>();
        bullet.Launch(lookdirection, 300);
        animator.SetTrigger("Launch");
        PlaySound(audioclip);
    }

    public void PlaySound(AudioClip audioclip)
    {
        audiosource.PlayOneShot(audioclip);
    }
    private void Reborn()
    {
        ChangeHealth(maxHealth);
        transform.position = rebornPos;
    }
}
