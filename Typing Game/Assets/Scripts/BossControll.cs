using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControll : EnemyControll
{
    public int hp;
    public Transform castle;
    public float speed;
    public List <string> words;
    public GameObject EnemyName;
    public int numWords;
    //public GameObject TextName;
    //public GameObject TextingText;

    // Start is called before the first frame update
    void Start()
    {
        speed = .6f;
        castle = GameObject.FindGameObjectWithTag("Castle").transform;
        numWords = 4;
        SetWords();
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

    public void SetWords()
    {
        GameObject go = GameObject.Find("SpawnController");
        SpawnController other = (SpawnController)go.GetComponent(typeof(SpawnController));
        for (int x = 0; x < numWords; x++)
        {
            words.Add(other.ReturnWord());
        }
    }
    private void SetName()
    {
        if (EnemyName)
        {
            var word = "";
            for(int x = 0; x < words.Count; x++)
            {
                word += words[x] + ", ";
            }
            transform.GetChild(0).GetComponent<TextMesh>().text = word;
        }
    }

    public List<string> ReturnWord()
    {
        return words;
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
    public void recieveRemainingWords(List<string> w)
    {
        words = w;
        SetName();
    }
}
