using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var input = gameObject.GetComponent<InputField>();
        input.ActivateInputField();

        var se = new InputField.SubmitEvent();
        se.AddListener(SubmitWord);
        input.onEndEdit = se;
        


    }

  private void SubmitWord(string arg0)
    {
        GameObject go = GameObject.Find("SpawnController");
        SpawnController other = (SpawnController)go.GetComponent(typeof(SpawnController));
        other.AttemptedKill(arg0);
        var input = gameObject.GetComponent<InputField>();
        input.text = "";
        input.ActivateInputField();
    }
}
