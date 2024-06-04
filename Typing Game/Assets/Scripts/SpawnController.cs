using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnController : MonoBehaviour
{
    public enum EnemyTypes
    {
        melee,
        ranged,
        Boss
    }
    public GameObject slime;
    public GameObject skeleton;
    public GameObject wraith;
    public bool bossDead;

    public GameObject numEnemiesObj;
    public GameObject levelNumObj;

    public bool endlessMode;
    public GameObject spawn1;
    public GameObject spawn2;

    private int SpawnId;
    public int spawnLocId;
    public bool Spawn = true;
    private int numEnemies;
    public int totalEnemies;
    public int enemiesKilled;
    public int enemiesLeft;
    public int enemiesToSpawn;

    public float spawnTimer;
    public float timeTillSpawn;

    //tracks what level we are on so it knows what enemies to release
    private int levelNumber;

    public GameObject [] enemies;

    public TextAsset dictionaryTextFile;
    private string theWholeFileAsOneLongString;
    private List<string> eachLine;

    public Input playerInput;
    public int bossNumber;


    // Start is called before the first frame update
    void Start()
    {
        endlessMode = GameObject.Find("Endless_Mode_Button").GetComponent<EndlessModeButton>().isEndlessMode;
        //endlessMode = true;
        Destroy(GameObject.Find("Canvas_Start"));
        enemiesKilled = 0;
        numEnemies = 0;
        totalEnemies = 5;
        spawnTimer = 2f;
        timeTillSpawn = 9.0f;
        levelNumber = 1;
        bossNumber = 0;
        enemiesLeft = totalEnemies - enemiesKilled;
        enemiesToSpawn = totalEnemies - numEnemies;
        SpawnId = Random.Range(1, 500);
        theWholeFileAsOneLongString = dictionaryTextFile.text;

        eachLine = new List<string>();
        eachLine.AddRange(theWholeFileAsOneLongString.Split("\n"[0]));

        numEnemiesObj.GetComponent<UnityEngine.UI.Text>().text = enemiesLeft.ToString() + " Enemies left";
        levelNumObj.GetComponent<UnityEngine.UI.Text>().text = "Level "+levelNumber.ToString() ;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForGameWinning();
        //spawnLocId = Random.Range(0, 10) % 2;  //min is inclusive and max is exclusive that's why it's 0-10 so it will only have numbers from 0 - 9
        spawnLocId = 1;
        if (Spawn)
        {
            timeTillSpawn += Time.deltaTime;
            if (timeTillSpawn >= spawnTimer)
            {
                // checks to see if the number of spawned enemies is less than the max num of enemies
                if (enemiesToSpawn != 0)
                {
                    spawnEnemy(spawnLocId);
                    numEnemies++;
                    timeTillSpawn = 0.0f;
                }
              
                else
                {
                    Spawn = false;
                }
            }
        }
        else
        {
            if (endlessMode)
            {
                Debug.Log("in endlessMode");
                if (enemiesLeft == 0 && levelNumber % 2 == 0)
                {
                    if (bossNumber == 0)
                    {
                        bossDead = false;
                        spawnBoss(spawnLocId);
                    }
                    if (bossDead)
                    {
                        levelNumber++;
                        enemiesKilled = 0;
                        numEnemies = 0;
                        bossNumber = 0;

                        //this means totalEnemies will always increase by 2 when a level is finished, regardless of level. Probably just temporary for now given we may want more control over it. 
                        totalEnemies += 3;
                        Spawn = true;

                    }

                   
                }else if (enemiesLeft == 0)
                {
                    levelNumber++;
                    enemiesKilled = 0;
                    numEnemies = 0;
                    bossNumber = 0;

                    //this means totalEnemies will always increase by 2 when a level is finished, regardless of level. Probably just temporary for now given we may want more control over it. 
                    totalEnemies += 3;
                    Spawn = true;

                }
              
            }
            else
            {
                if (enemiesLeft == 0 && levelNumber == 3)
                {


                    if (bossNumber == 0)
                    {
                        spawnBoss(spawnLocId);
                    }

                    if (bossDead)
                    {
                        Debug.Log("Game Won");
                        //trigger game Won scene
                    }

                    //this means totalEnemies will always increase by 5 when a level is finished, regardless of level. Probably just temporary for now given we may want more control over it. 
                    //maxEnemiesOnScreen += 5;
                }
                else if (enemiesLeft == 0)
                {
                    levelNumber++;
                    enemiesKilled = 0;
                    numEnemies = 0;

                    //this means totalEnemies will always increase by 5 when a level is finished, regardless of level. Probably just temporary for now given we may want more control over it. 
                    totalEnemies += 5;
                    Spawn = true;
                    Debug.Log("jere");
                }
            }

        }
        enemiesLeft = totalEnemies - enemiesKilled;
        enemiesToSpawn = totalEnemies - numEnemies;
        numEnemiesObj.GetComponent<UnityEngine.UI.Text>().text = enemiesLeft.ToString() + " Enemies left";
        levelNumObj.GetComponent<UnityEngine.UI.Text>().text = "Level " + levelNumber.ToString();


    }

    private void CheckForGameWinning()
    {
        if (!endlessMode)
        {
            if ( bossDead )
            {
                SceneManager.LoadScene("WinningScene", LoadSceneMode.Single);
            }
        }
    }

    void spawnBoss(int spawnLocId)
    {
        GameObject spawner;
        bool flip = true;
        if (spawnLocId == 1)
        {
            flip = false;
            spawner = spawn1;
        }
        else
        {
            flip = false;
            spawner = spawn2;
        }
        GameObject Enemy = (GameObject)Instantiate(skeleton, spawner.transform.position, Quaternion.identity);
        if (flip)
        {
            Enemy.GetComponent<SpriteRenderer>().flipX = true;
        }
        bossNumber++;
    }
    void spawnEnemy(int spawnLocId)
    {
        GameObject spawner;
        bool flip = true;
        if (spawnLocId == 1)
        {
            flip = false;
            spawner = spawn1;
        }
        else
        {
            flip = false;
            spawner = spawn2;
        }
        if (levelNumber == 1)
        {
            //var ranNum = Random.Range(0, 2);
            GameObject Enemy = (GameObject)Instantiate(slime, spawner.transform.position, Quaternion.identity);
            if (flip)
            {
                Enemy.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else if (levelNumber == 2)
        {
            var ranNum = Random.Range(0, 2);
            
            GameObject Enemy = ranNum == 0
                ? (GameObject)Instantiate(wraith, spawner.transform.position, Quaternion.identity)
                : (GameObject)Instantiate(slime, spawner.transform.position, Quaternion.identity);
            if (flip)
            {
                Enemy.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        else if (levelNumber == 3)
        {
            var ranNum = Random.Range(0, 2);

            GameObject Enemy = ranNum == 0
                ? (GameObject)Instantiate(wraith, spawner.transform.position, Quaternion.identity)
                : (GameObject)Instantiate(slime, spawner.transform.position, Quaternion.identity);
            if (flip)
            {
                Enemy.GetComponent<SpriteRenderer>().flipX = true;
            }
                        
        }else if (endlessMode)
        {
            var ranNum = Random.Range(0, 2);

            GameObject Enemy = ranNum == 0
                ? (GameObject)Instantiate(wraith, spawner.transform.position, Quaternion.identity)
                : (GameObject)Instantiate(slime, spawner.transform.position, Quaternion.identity);
            if (flip)
            {
                Enemy.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        
    }
    public string ReturnWord()
    {
        bool rightWord = false;
        string w = null;
        while (!rightWord)
        {
            if (levelNumber == 1)
            {
                int wordId = Random.Range(4, 466000);
                try
                {

                    if (eachLine[wordId].Length-1 < 5 && eachLine[wordId].Length-1 > 3)
                    {
                        rightWord = true;
                        w = eachLine[wordId];
                        w = w.Replace("\n", "").Replace("\r", "");
                    }
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    continue;
                }
            }
            else if (levelNumber == 2)
            {
                int wordId = Random.Range(4, 466000);
                try
                {

                    if (eachLine[wordId].Length - 1 < 8 && eachLine[wordId].Length - 1 > 4)
                    {
                        rightWord = true;
                        w = eachLine[wordId];
                        w = w.Replace("\n", "").Replace("\r", "");
                    }
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    continue;
                }
            }
            else if (levelNumber == 3)
            {
                int wordId = Random.Range(4, 466000);
                try
                {

                    if (eachLine[wordId].Length - 1 < 10 && eachLine[wordId].Length - 1 > 6)
                    {
                        rightWord = true;
                        w = eachLine[wordId];
                        w = w.Replace("\n", "").Replace("\r", "");
                    }
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    continue;
                }
            }
            else if (levelNumber > 3)
            {
                int wordId = Random.Range(4, 466000);
                try
                {

                    if (eachLine[wordId].Length - 1 < 15 && eachLine[wordId].Length - 1 > 6)
                    {
                        rightWord = true;
                        w = eachLine[wordId];
                        w = w.Replace("\n", "").Replace("\r", "");
                    }
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    continue;
                }
            }
        }
        return w;
    }

    public void KillEnemy()
    {
        enemiesKilled++;
    }
    public void AttemptedKill(string arg0)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int x = 0; x <enemies.Length; x++)
        {
           
            SlimeControl slime = enemies[x].GetComponent<SlimeControl>();

            WraithControl wraith = enemies[x].GetComponent<WraithControl>();
            BossControll boss = enemies[x].GetComponent<BossControll>();
            try
            {
                if (arg0.Equals(wraith.ReturnWord()))
                {
                    wraith.Die();
                    KillEnemy();

                }
            }
            catch { }
            try
            {
                if (arg0.Equals(slime.ReturnWord()))
                {
                    slime.Die();
                    KillEnemy();

                }
            }
            catch { }
            try
            {
                List<string> words = boss.ReturnWord();
                for(int count = 0; count<words.Count; count++ )
                    if (arg0.Equals(words[count]))
                    {
                        if (words.Count == 1)
                        {
                            boss.Die();
                            KillEnemy();
                            bossDead = true;
                        }
                        else
                        {
                            words.RemoveAt(count);
                            boss.recieveRemainingWords(words);
                        }


                    }
            }
            catch { }
        }
    }
    
}
