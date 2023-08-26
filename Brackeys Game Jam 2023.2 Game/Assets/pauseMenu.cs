using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (popup.activeSelf)
            {
                popup.SetActive(false);
            }
            else
            {
                popup.SetActive(true);
            }
        }




    }
}
