using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessModeButton : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isEndlessMode = false;
    void Awake()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        isEndlessMode = true;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        //GameObject.Find("SpawnController").GetComponent<SpawnController>().endlessMode = true;
    }
}
