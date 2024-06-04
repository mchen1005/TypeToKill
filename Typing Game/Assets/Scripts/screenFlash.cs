using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenFlash : MonoBehaviour
{
    public GameObject dmgPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void flashScreen(bool getDamage)
    {
        if (getDamage)
        {

            dmgPanel.SetActive(true);
           /* Color Opaque = new Color(1, 1, 1, 1);
            bloodImage.color = Color.Lerp(bloodImage.color, Opaque, 20 * Time.deltaTime);
            if (bloodImage.color.a >= 0.8) //Almost Opaque, close enough
            {
                getDamage = false;
            }*/
        }
        if (!getDamage)
        {
            dmgPanel.SetActive(false);

            /*Color Transparent = new Color(1, 1, 1, 0);
            bloodImage.color = Color.Lerp(bloodImage.color, Transparent, 20 * Time.deltaTime);*/
        }
    }
}
