using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
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
        Debug.Log("You have clicked the button!");
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
