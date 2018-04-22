using UnityEngine;
using System.Collections;
using System.Linq;

public enum ITEMS
{
    APPLE,
    CANTEEN,
    CLOAK,
    SLEEPING_BAG,
    SPIKE
}

public class ItemsManager : MonoBehaviour
{
    
    public Sprite[] itemSprites = new Sprite[5];

    [HideInInspector]
    public string[] itemNames = new string[5];

    public string storedItem = "";

    private string[] itemText = new string[] {
        "You have found an apple. An oh so sweet apple! Eating it restores 10 hunger and 5 stamina.",
        "Oh a bomb! Wait, that's a canteen. What idiot would make this delicious water canteen look like a bomb! Oh well, drinking it restores 10 thirst.",
        "A cloak? I hate to wonder what happened to the poor sherpa who owned this. Well finders keepers. Wearing it restores tempature to full.",
        "Okay I'll just cozy up in the sleeping bag for a couple of minutes. What's poking my foot in this...... zzzzzzz. Sleeping restores 20 stamina.",
        "Woah. That was a nasty fall off that ice coated spike. Only an idiot would have tried that. You lose 50 health."
    };

    public void Start()
    {
        itemNames[0] = "Apple";
        itemNames[1] = "Canteen";
        itemNames[2] = "Cloak";
        itemNames[3] = "SleepingBag";
        itemNames[4] = "Spike";
    }

    public int GetRandomItem()
    {
        int i = UnityEngine.Random.Range(0, itemSprites.Length); 

        return i;
    }

    public Sprite GetItemSprite()
    {
        Sprite itemSprite = null;

        if (storedItem == "Apple")
        {
            itemSprite = itemSprites[0];
        }

        if (storedItem == "Canteen")
        {
            itemSprite = itemSprites[1];
        }

        if (storedItem == "Cloak")
        {
            itemSprite = itemSprites[2];
        }

        if (storedItem == "SleepingBag")
        {
            itemSprite = itemSprites[3];
        }

        if (storedItem == "Spike")
        {
            itemSprite = itemSprites[4];
        }

        return itemSprite;
    }

    public string GetItemText()
    {
        string text = "Nothing to pickup this time. What a waste.";

        if (storedItem == "Apple")
        {
            text = itemText[0];
        }

        if (storedItem == "Canteen")
        {
            text = itemText[1];
        }

        if (storedItem == "Cloak")
        {
            text = itemText[2];
        }

        if (storedItem == "SleepingBag")
        {
            text = itemText[3];
        }

        if (storedItem == "Spike")
        {
            text = itemText[4];
        }

        return text;
    }

    public string GetItemName(int index)
    {
        string name = ""; 
        name = itemNames[index]; 

        return name;
    }

    public void FloatItems(GameObject newBlock)
    {
        // Turn on animation 
        foreach (Transform child in newBlock.transform)
        {
            if (child.tag.Equals("Block"))
            {
                foreach (Transform blockChild in child)
                { 
                    blockChild.gameObject.AddComponent<FloatBehavior>();
                    blockChild.gameObject.AddComponent<CircleCollider2D>();  
                }
            } 
        }
    }

    public void RotateItems()
    {
        foreach (Transform child in Managers.Game.currentShape.transform)
        { 
            if (child.tag.Equals("Block"))
            {
                foreach (Transform blockChild in child)
                {
                    // Rotate the item so it is facing upright.
                    blockChild.transform.Rotate(0, 0, 90);
                } 
            }
        }
    }

    public void StoreItem(GameObject item)
    {
        if (item != null)
        {
            storedItem = item.name;
            if (storedItem == "Spike")
            {
                Managers.Audio.PlayHurtSound();
            }
            else
            {
                Managers.Audio.PlayItemPickupSound();
            } 
        }
        else
        {
            storedItem = "";
        }
    }

    public void ConsumeItem()
    {
        if (storedItem == itemNames[(int)ITEMS.APPLE]) 
        {
            if (Managers.Spawner.thePlayer.GetComponent<PlayerController>().hunger - 10 > 0)
            {
                Managers.Spawner.thePlayer.GetComponent<PlayerController>().hunger -= 10;
            }
            else
            {
                Managers.Spawner.thePlayer.GetComponent<PlayerController>().hunger = 0;
            }

            if (Managers.Spawner.thePlayer.GetComponent<PlayerController>().stamina + 5 < 100)
            {
                Managers.Spawner.thePlayer.GetComponent<PlayerController>().stamina += 5;
            }
            else
            {
                Managers.Spawner.thePlayer.GetComponent<PlayerController>().stamina = 100;
            }
        }
        else if (storedItem == itemNames[(int)ITEMS.CANTEEN])
        { 
            if (Managers.Spawner.thePlayer.GetComponent<PlayerController>().thirst - 10 > 0)
            {
                Managers.Spawner.thePlayer.GetComponent<PlayerController>().thirst -= 10;
            }
            else
            {
                Managers.Spawner.thePlayer.GetComponent<PlayerController>().thirst = 0;
            }

            if (Managers.Spawner.thePlayer.GetComponent<PlayerController>().stamina + 5 < 100)
            {
                Managers.Spawner.thePlayer.GetComponent<PlayerController>().stamina += 5;
            }
            else
            {
                Managers.Spawner.thePlayer.GetComponent<PlayerController>().stamina = 100;
            }
        }
        else if (storedItem == itemNames[(int)ITEMS.CLOAK])
        {
            Managers.Spawner.thePlayer.GetComponent<PlayerController>().tempature = 100;
        }
        else if (storedItem == itemNames[(int)ITEMS.SLEEPING_BAG])
        {

            if (Managers.Spawner.thePlayer.GetComponent<PlayerController>().stamina + 20 < 100)
            {
                Managers.Spawner.thePlayer.GetComponent<PlayerController>().stamina += 20;
            }
            else
            {
                Managers.Spawner.thePlayer.GetComponent<PlayerController>().stamina = 100;
            }
        }
        else if (storedItem == itemNames[(int)ITEMS.SPIKE])
        {
            Debug.Log("Take dmg from spike");
            if (Managers.Spawner.thePlayer.GetComponent<PlayerController>().health - 50 > 0)
            {
                Managers.Spawner.thePlayer.GetComponent<PlayerController>().health -= 50;
            }
            else
            {
                Managers.Spawner.thePlayer.GetComponent<PlayerController>().health = 0;
            }
        }
        storedItem = "";
    }
}
