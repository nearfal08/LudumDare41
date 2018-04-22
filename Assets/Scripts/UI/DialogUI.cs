using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour {

    public Text DialogText;
    public Image ItemIcon;
    public Text ItemText;

    private string[] playerDialog = new string[20];

    void Start()
    { 
        // Filling dialog in method to not clutter top of class.
        FillPlayerDialog();
    }

    void OnEnable()
    {
        RefreshText();
    }

    void RefreshText()
    {
        Managers.Audio.PlayWindSound(); 
        DialogText.text = GetRandomPlayerDialog();

        Sprite itemSprite = Managers.Items.GetItemSprite();
        if (itemSprite != null)
        {
            ItemIcon.enabled = true;
            ItemIcon.sprite = Managers.Items.GetItemSprite();
        }
        else
        {
            ItemIcon.enabled = false;
        }
        ItemText.text = Managers.Items.GetItemText();
    }

    public string GetRandomPlayerDialog()
    {
        int i = UnityEngine.Random.Range(0, playerDialog.Length);

        return playerDialog[i];
    }


    void FillPlayerDialog()
    {
        playerDialog[0] = "Hmmmm I wonder if any other Sherpa's are up there? Only one way to find out!";
        playerDialog[1] = "I could go for a beer right now. Or 20.";
        playerDialog[2] = "I could be watching Breaking Bad right now but instead i'm out here. What am I doing?";
        playerDialog[3] = "Why is it so damn cold on this mountain?";
        playerDialog[4] = "A couple more climbs and we got this. Keep on pushing.";
        playerDialog[5] = "What kind of people don't like peanut butter and jelly? I mean come on, get it together.";
        playerDialog[6] = "I always kind of like Nickelback. Glad no one can hear me out here.";
        playerDialog[7] = "Frosted flakes or frosted wheat? Hmmm tough choice but I think I would go with frosted wheat. Much more filling.";
        playerDialog[8] = "Did anyone ever think to put this fire closer to the ground? Why am I even doing this?";
        playerDialog[9] = "It's nice to be so active. Think of all those losers at home on their computers haha.";
        playerDialog[10] = "Oh man, I don't have any cell reception up here.";
        playerDialog[11] = "Hope I don't get pulled over. My license is expired.";
        playerDialog[12] = "This view is beautiful... let me take a selfie.";
        playerDialog[13] = "Left, left, left, left-right, left.";
        playerDialog[14] = "Wonder if my bitcoin miner is plugged in.";
        playerDialog[15] = "What if the Hokey-Pokey really is what it's all about? ";
        playerDialog[16] = "As long as I make it back by Taco Tuesday...";
        playerDialog[17] = "I am the best climber. Everyone knows it. Everyone talks about me. I'm huuge.";
        playerDialog[18] = "I wonder if anyone else has made it up to the fire. Well, I guess someone had to start it.";
        playerDialog[19] = "What bozo put that fire on a wooden bridge. That doesn't seem like such a good idea.";
    }
}
