using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    float timer = 4f;
    public float currentTimer = 4;
    // Update is called once per frame
    void Update()
    {
        if (currentTimer < timer)
        {
            currentTimer += Time.deltaTime;
        }
        else
        {
            anim.SetBool("Growed", true);
        }
    }

    public void Eat()
    {
        anim.SetBool("Growed", false);
        currentTimer = 0;
    }
}
