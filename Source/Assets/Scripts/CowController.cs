using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CowController : MonoBehaviour
{
    NavMeshAgent agent;
    public Grass currentGrass;

    public GameObject[] Flowers;

    public string[] Messages;

    public Material Red;
    public Material Blue;

    float timer = 8f;
    public float currentTimer = 8;
    public float MooTimer = 2f;
    public int currentIndex;
    public Transform Moo;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void HandleFlowers(int j)
    {
        for (int i = 0; i < 8; i++)
        {
            if (Messages[j][i] == '0')
                Flowers[i].GetComponentInChildren<Renderer>().material = Red;
            else
                Flowers[i].GetComponentInChildren<Renderer>().material = Blue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Movement();
        }

        Moo.rotation = Camera.main.transform.rotation;

        if (currentTimer < timer)
            currentTimer += Time.deltaTime;
        else
        {
            HandleFlowers(currentIndex);
            currentIndex++;
            currentTimer = 0;
            MooTimer = 2f;
            Moo.gameObject.SetActive(true);
        }

        if (MooTimer > 0)
            MooTimer -= Time.deltaTime;
        else
        {
            Moo.gameObject.SetActive(false);
        }


        if (currentIndex >= Messages.Length)
        {
            currentIndex = 0;
        }
        if (currentIndex == Messages.Length - 1)
        {
            Moo.GetComponentInChildren<Text>().text = "MOOO!";
        }
        if (currentIndex < Messages.Length - 1)
        {
            Moo.GetComponentInChildren<Text>().text = "MOO!";
        }

        if (currentGrass)
        {
            if (Vector3.Distance(transform.position, currentGrass.transform.position) < 0.5f)
            {
                currentGrass.Eat();
                Debug.Log("Eated " + currentGrass.name);
                currentGrass = null;
            }
        }
    }

    public void Movement()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            currentGrass = hit.collider.GetComponent<Grass>();
            agent.SetDestination(hit.point);
        }
    }
}
