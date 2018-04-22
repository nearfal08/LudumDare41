using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public enum ShapeType
{
    I,
    T,
    O,
    L,
    J,
    S,
    Z
}

public class TetrisShape : MonoBehaviour
{
    [HideInInspector]
    public ShapeType type;

    [HideInInspector]
    public ShapeMovementController movementController;

    [HideInInspector]
    public Color originalColor;

    void Awake()
    {
        movementController = GetComponent<ShapeMovementController>();
        //AssignRandomColor();
        AssignRandomItem();
    }

    void Start()
    { 
        // Default position not valid? Then it's game over
        if (!Managers.Grid.IsValidGridPosition(this.transform))
        {
            Managers.Game.SetState(typeof(GameOverState));
            Destroy(this.gameObject);
        }
    }

    void AssignRandomColor()
    {
        Color temp = Managers.Palette.TurnRandomColorFromTheme();
        originalColor = temp;
        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>().ToList())
        {
            if (renderer.gameObject.tag.Equals("Block"))
            {
                renderer.color = temp;
            } 
        } 
    }

    void AssignRandomItem()
    { 
        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>().ToList())
        {
            if (!renderer.gameObject.tag.Equals("Block"))
            { 
                if (CanSpawnItem())
                {
                    int randomItemIndex = Managers.Items.GetRandomItem();
                    string itemName = Managers.Items.GetItemName(randomItemIndex);
                    renderer.sprite = Managers.Items.itemSprites[randomItemIndex];

                    // Patch to prevent items from showing over ui elements. Needs to be fixed within Unity on the item prefab.
                    renderer.sortingOrder = 12;
                    renderer.gameObject.name = itemName;
                } 
                else
                {
                    Destroy(renderer.gameObject);
                }
            }
        }
    } 

    bool CanSpawnItem()
    {
        int i = UnityEngine.Random.Range(1, 10);
        if (i % 2 == 0)
        {
            return true;
        }
        return false;
    } 
}
