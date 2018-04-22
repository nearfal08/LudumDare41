using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFUCKYOU : MonoBehaviour {

    public Text[] statText = new Text[5];
    
    // Use this for initialization
    void Start ()
    {
        UpdatePlayerStatsUI();
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdatePlayerStatsUI();
    }

    void UpdatePlayerStatsUI()
    {
        if (Managers.Spawner.thePlayer != null)
        {
            statText[0].text = Managers.Spawner.thePlayer.GetComponent<PlayerController>().health.ToString();
            statText[1].text = Managers.Spawner.thePlayer.GetComponent<PlayerController>().hunger.ToString();
            statText[2].text = Managers.Spawner.thePlayer.GetComponent<PlayerController>().stamina.ToString();
            statText[3].text = Managers.Spawner.thePlayer.GetComponent<PlayerController>().thirst.ToString();
            statText[4].text = Managers.Spawner.thePlayer.GetComponent<PlayerController>().tempature.ToString();
        } 
    }
}
