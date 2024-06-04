using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CastleController : MonoBehaviour
{
    public int hp;
    public GameObject hpObject;
    public GameObject dmgFlash;
    public GameObject spawnObj;
    public AudioSource dmgSound;

    // Start is called before the first frame update
    void Start()
    {
        hp = 6;

    }

    // Update is called once per frame
    void Update()
    {
        CheckForEndGame();
    }

    private void UpdateHeart()
    {
        for(int i = 6; i > hp; i--)
        {
            var str = "h" + i;
            var heart = GameObject.Find(str);
            if( heart != null && heart.gameObject.activeSelf) 
                heart.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.GetComponent<EnemyControll>().isCollided == false)
        {
            hp--;
            UpdateHeart();
            StartCoroutine(flashScreen());
            collision.gameObject.GetComponent<EnemyControll>().isCollided = true;
            Destroy(collision.gameObject);
            spawnObj.GetComponent<SpawnController>().KillEnemy();
            if (collision.gameObject.GetComponent<BossControll>())
            {
                hp = 0;
            }
        }

    }
    IEnumerator flashScreen()
    {
        dmgSound.Play();
        dmgFlash.SetActive(true);
        yield return new WaitForSeconds(.1f);
        dmgFlash.SetActive(false);
    }
    private void CheckForEndGame()
    {
        if (hp <= 0) {
            SceneManager.LoadScene("LosingScene", LoadSceneMode.Single);
        }
    }
}
