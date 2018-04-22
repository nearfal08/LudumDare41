using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


    public int stamina;
    public int health;
    public int hunger;
    public int thirst;
    public int tempature;
    public string deathReason = "";
    public string winReason = "";
    public bool lose = false;
    public bool keepPlaying = true;

    void Awake()
    { 
    }

    void Start () {
        deathReason = "";
    }
	
	void Update () { 
        if (health <= 0)
        {
            deathReason = "You have ran out of health. You crumble on the mountain hoping someone finds you. On the bright side, the snow covering you is cheaper than the casket your family would have bought.";
            Managers.Game.SetState(typeof(GameOverState));
            lose = true;
        }
        if (stamina <= 0)
        {
            deathReason = "You have ran out of stamina. You lay down for a rest and quietly fall asleep, dreaming of a warm campfire, the last one you will ever see.";
            lose = true;
            Managers.Game.SetState(typeof(GameOverState));
        }
        if (hunger >= 100)
        {
            deathReason = "You have starved to death. If only you wouldn't have forgotten that twinkie dumbass.";
            lose = true;
            Managers.Game.SetState(typeof(GameOverState));
        }
        if (thirst >= 100)
        {
            deathReason = "You are thirstier than a frat boy on a Friday night. If only you would have took the time to pickup those random sacks of water that are everywhere.";
            lose = true;
            Managers.Game.SetState(typeof(GameOverState));
        }
        if (tempature <= 0)
        {
            deathReason = "BRRRRR. You have frozen to death. Maybe you should have put something warm on since, ya know, you are climbing a frozen mountain.";
            lose = true;
            Managers.Game.SetState(typeof(GameOverState));
        }
    }  

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Shape")
        {
            Debug.Log("Hit by a shape."); 
            deathReason = "A piece of the mountain has came loose and fell onto you. The impact kills you quicker than the slow death of climbing the mountain.";
            lose = true;
            Managers.Game.SetState(typeof(GameOverState));
            gameObject.SetActive(false); 
        }
        if (other.gameObject.tag == "Item")
        {
            Managers.Items.StoreItem(other.gameObject); 
            Destroy(other.gameObject);
            Debug.Log("Hit by a item.");
        }
        if (other.gameObject.tag == "Fire")
        {
            winReason = "You have reached the bonfire! Your clothes are tattered, your face is frozen, and you can't feel your balls but your pride is intact. Rest and prepare for your next climb!";
            lose = false;
            Managers.Game.SetState(typeof(GameOverState));
        }
    }
     

    // Everyone round dock these stats.
    public void PostRoundStatUpdate()
    {
        stamina -= 10;
        hunger += 5;
        thirst += 5;
        tempature -= 5;
    }
}
