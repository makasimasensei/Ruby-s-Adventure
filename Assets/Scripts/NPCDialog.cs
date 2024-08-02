using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialog : MonoBehaviour
{

    public GameObject dialogbox;
    public Text textGua, textHelp;
    public AudioClip completeTask;
    private AudioSource audiosource;
    // Start is called before the first frame update
    public void Start()
    {
        dialogbox.SetActive(false);
        audiosource = GetComponent<AudioSource>();
    }

    public void Displaydialog()
    {
        dialogbox.SetActive (true);
        if (EmeryContorler.instance.fixedNum>=2) 
        {
            audiosource.PlayOneShot(completeTask);
            textGua.text = "机器人修好了~呱呱~";
            textHelp.text = string.Empty;
        }
    }
}
