using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeControl : EnemyControll
{
    public int hp;
    public Transform castle;
    public float speed;
    public string word;
    public GameObject EnemyName;
    //public GameObject TextName;
    //public GameObject TextingText;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
        castle = GameObject.FindGameObjectWithTag("Castle").transform;
        SetWord();
        SetName();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, castle.position, speed * Time.deltaTime);

    }

    public void SetWord()
    {
        GameObject go = GameObject.Find("SpawnController");
        SpawnController other = (SpawnController)go.GetComponent(typeof(SpawnController));
        word = other.ReturnWord();
        
    }
    private void SetName()
    {
        if (EnemyName)
        {
            transform.GetChild(0).GetComponent<TextMesh>().text = word;
        }
    }

    public string ReturnWord()
    {
        return word;
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
