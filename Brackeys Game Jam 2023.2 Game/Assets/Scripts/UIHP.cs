using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHP : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private TMP_Text HpText;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerCotrol>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            HpText.SetText(player.GetComponent<HPComponent>().Hp.ToString());
        }
        else
        {
            HpText.SetText("0");
        }

        
    }
}
