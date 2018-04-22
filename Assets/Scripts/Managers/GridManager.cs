using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[System.Serializable]
public class Column
{
    public Transform[] row = new Transform[20];
}

public class GridManager : MonoBehaviour
{
    public Column[] gameGridcol = new Column[10]; 
    public List<GameObject> validBlockMove = new List<GameObject>();
    public GameObject backgroundGrid;
    public Color originalGridColor;

    public Transform yUpOne = null;
    public Transform yDownOne = null;
    public Transform xLeftOne = null;
    public Transform xRightOne = null;

    public bool InsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < 10 && (int)pos.y >= 0);
    }

    public bool InsideBorderY(Vector2 pos)
    {
        return ((int)pos.y >= 0);
    }

    public bool InsideBorderX(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < 10);
    }

    public void PlaceShape()
    {
        int y = 0; 
        // TO DO: Switch to player perspective.
        if (Managers.Input.isTetrisTurn)
        {
            Managers.Spawner.SpawnShape();
        } 

    }

    public bool IsValidGridPosition(Transform obj)
    {
        foreach (Transform child in obj)
        {
            if (child.gameObject.tag.Equals("Block"))
            {
                Vector2 v = Vector2Extension.roundVec2(child.position);

                bool insideX = InsideBorderX(v);
                bool insideY = InsideBorderY(v);

                if (!insideX)
                {
                    Debug.Log("Block hit side.");
                    return false;
                }

                if (!insideY)
                {
                    Debug.Log("Block hit bottom.");
                    PostLandingChores(obj.gameObject);
                    return false;
                }
                //if (!InsideBorderX(v))
                //{
                //    if (obj.transform.position.y != -1)
                //    {
                //        Debug.Log("Block hit side.");
                //        return false; 
                //    }
                //    Debug.Log("Block hit bottom.");
                //    PostLandingChores(obj.gameObject);
                //    return false;
                //}

                // Block in grid cell (and not part of same group)?
                if (gameGridcol[(int)v.x].row[(int)v.y] != null &&
                    gameGridcol[(int)v.x].row[(int)v.y].parent != obj)
                {
                    Debug.Log("Block hit another block.");
                    PostLandingChores(obj.gameObject);
                    return false; 
                } 
            }
        }
        return true;
    } 

    public void UpdateGrid(Transform obj)
    {
        if (Managers.Input.isTetrisTurn)
        {

            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (gameGridcol[x].row[y] != null)
                    { 
                        if (gameGridcol[x].row[y].parent == obj)
                        {
                            gameGridcol[x].row[y] = null; 
                        } 
                    }
                }
            }

            foreach (Transform child in obj)
            {
                if (child.gameObject.tag.Equals("Block"))
                {
                    Vector2 v = Vector2Extension.roundVec2(child.position);
                    gameGridcol[(int)v.x].row[(int)v.y] = child;
                }
            }
        } 
    }

    public void ClearBoard()
    {
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                if (gameGridcol[x].row[y] != null)
                {
                    Destroy(gameGridcol[x].row[y].gameObject);
                    gameGridcol[x].row[y] = null;
                }
            }
        }

        // Destory existing shapes
        foreach (Transform t in Managers.Game.blockHolder)
        {
            Destroy(t.gameObject);
        }

        // Destroy the player. 
        Destroy(Managers.Spawner.thePlayer);

        Managers.Input.isTetrisTurn = true;

        RevertGridColor();
    }

    public void PostLandingChores(GameObject block)
    {
        Managers.Input.isTetrisTurn = false;
        Managers.Input.isActive = true;
        Managers.Items.FloatItems(block);
        CheckforValidPlayerMoves(); // TO DO
    } 

    public void RevertGridColor()
    {
        if (yUpOne != null)
        {
            yUpOne.gameObject.GetComponent<SpriteRenderer>().color = originalGridColor;
        }
        if (yDownOne != null)
        {
            yDownOne.gameObject.GetComponent<SpriteRenderer>().color = originalGridColor;
        }
        if (xLeftOne != null)
        {
            xLeftOne.gameObject.GetComponent<SpriteRenderer>().color = originalGridColor;
        }
        if (xRightOne != null)
        {
            xRightOne.gameObject.GetComponent<SpriteRenderer>().color = originalGridColor;
        }

        yUpOne = null;
        yDownOne = null;
        xLeftOne = null;
        xRightOne = null;
    }

    public bool ValidClick(float toX, float toY)
    {
        bool isValid = false;

        if (toX >= 0 && toX < 10 && toY >= 0 && toY < 17)
        {
            if (yUpOne != null && yUpOne.position.x == toX && yUpOne.position.y == toY)
            {
                isValid = true;
            }
            if (yDownOne != null && yDownOne.position.x == toX && yDownOne.position.y == toY)
            {
                isValid = true;
            }
            if (xLeftOne != null && xLeftOne.position.x == toX && xLeftOne.position.y == toY)
            {
                isValid = true;
            }
            if (xRightOne != null && xRightOne.position.x == toX && xRightOne.position.y == toY)
            {
                isValid = true;
            }
        }

        return isValid;
    }

    public void CheckforValidPlayerMoves()
    {
        Vector3 playerPos = Managers.Spawner.thePlayer.transform.position;
        int playerPosY = (int)playerPos.y;
        int playerPosX = (int)playerPos.x;

        int xLeft = (int)playerPos.x - 1;
        int xRight = (int)playerPos.x + 1; 
        int yUp = (int)playerPos.y + 1;
        int yDown = (int)playerPos.y - 1;

        // Highlight blocks we can move to.
         
        // Y+1 highlight 
        if (playerPosY + 1 < 17)
        {
            // Check to make sure no shape is in the spot already.
            if (gameGridcol[playerPosX].row[playerPosY + 1] == null)
            {
                if (playerPosX - 1 >= 0 && gameGridcol[playerPosX - 1].row[playerPosY] != null //left 
                    || playerPosX + 1 < 10 && gameGridcol[playerPosX + 1].row[playerPosY] != null //right
                    || playerPosX + 1 < 10 && playerPosY + 1 < 17 && gameGridcol[playerPosX + 1].row[playerPosY + 1] != null //upright
                    || playerPosX - 1 >= 0 && playerPosY + 1 < 17 && gameGridcol[playerPosX - 1].row[playerPosY + 1] != null //upleft
                    //|| playerPosX + 1 < 10 && playerPosY + 2 < 17 && gameGridcol[playerPosX + 1].row[playerPosY + 2] != null //uuppright
                    //|| playerPosX - 1 >= 0 && playerPosY + 2 < 17 && gameGridcol[playerPosX - 1].row[playerPosY + 2] != null //upupleft
                    || playerPosY + 2 < 17 && gameGridcol[playerPosX].row[playerPosY + 2] != null) //up
                {
                    // Make sure a block can support us.
                    Transform rowUpOne = backgroundGrid.transform.GetChild(playerPosY + 1);
                    yUpOne = rowUpOne.GetChild(playerPosX);
                    yUpOne.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
                 
            } 
        }
        // Y-1 highlight 
        if (playerPosY - 1 >= 0)
        {
            // Check to make sure no shape is in the spot already.
            if (gameGridcol[playerPosX].row[playerPosY - 1] == null)
            {
                // Make sure a block can support us.
                if (playerPosX - 1 >= 0 && gameGridcol[playerPosX - 1].row[playerPosY] != null //left 
                    || playerPosX + 1 < 10 && gameGridcol[playerPosX + 1].row[playerPosY] != null //right
                    || playerPosX + 1 < 10 && playerPosY - 1 >= 0 && gameGridcol[playerPosX + 1].row[playerPosY - 1] != null //downright
                    || playerPosX - 1 >= 0 && playerPosY - 1 >= 0 && gameGridcol[playerPosX - 1].row[playerPosY - 1] != null //downleft
                    //|| playerPosX + 1 < 10 && playerPosY - 2 >= 0 && gameGridcol[playerPosX + 1].row[playerPosY - 2] != null //downdownright
                    //|| playerPosX - 1 >= 0 && playerPosY - 2 >= 0 && gameGridcol[playerPosX - 1].row[playerPosY - 2] != null //downdownleft
                    || playerPosY - 2 >= 0 && gameGridcol[playerPosX].row[playerPosY - 2] != null) //down
                {
                    Transform rowDownOne = backgroundGrid.transform.GetChild(playerPosY - 1);
                    yDownOne = rowDownOne.GetChild(playerPosX);
                    yDownOne.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                } 
            } 
        }
        // x+1 highlight 
        if (playerPosX + 1 < 10)
        {
            // Check to make sure no shape is in the spot already.
            if (gameGridcol[playerPosX + 1].row[playerPosY] == null)
            {
                if (playerPosX + 1 < 10 && playerPosY == 0 ||
                    playerPosY + 1 < 17 && gameGridcol[playerPosX].row[playerPosY + 1] != null //up
                    || playerPosY - 1 >= 0 && gameGridcol[playerPosX].row[playerPosY - 1] != null //down
                    || playerPosX + 1 < 10 && playerPosY + 1 < 17 && gameGridcol[playerPosX + 1].row[playerPosY + 1] != null //rightup
                    || playerPosX + 1 < 10 && playerPosY - 1 >= 0 && gameGridcol[playerPosX + 1].row[playerPosY - 1] != null //rightdown
                    //|| playerPosX + 2 < 10 && playerPosY + 1 < 17 && gameGridcol[playerPosX + 2].row[playerPosY + 1] != null //rightrightup
                    //|| playerPosX + 2 < 10 && playerPosY - 1 >= 0 && gameGridcol[playerPosX + 2].row[playerPosY - 1] != null //rightrightdown
                    || playerPosY + 2 < 10 && gameGridcol[playerPosX].row[playerPosY + 2] != null) //right
                {
                    Transform sameRow = backgroundGrid.transform.GetChild(playerPosY);
                    xRightOne = sameRow.GetChild(playerPosX + 1);
                    xRightOne.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
                 
            } 
        }
        // x-1 highlight 
        if (playerPosX - 1 >= 0)
        {
            // Check to make sure no shape is in the spot already.
            if (gameGridcol[playerPosX-1].row[playerPosY] == null)
            {
                if (playerPosX - 1 >= 0 && playerPosY == 0 ||
                    playerPosY + 1 < 17 && gameGridcol[playerPosX].row[playerPosY + 1] != null //up
                    || playerPosY - 1 >= 0 && gameGridcol[playerPosX].row[playerPosY - 1] != null //down
                    || playerPosX - 1 >= 0 && playerPosY + 1 < 17 && gameGridcol[playerPosX - 1].row[playerPosY + 1] != null //leftup
                    || playerPosX - 1 >= 0 && playerPosY - 1 >= 0 && gameGridcol[playerPosX - 1].row[playerPosY - 1] != null //leftdown
                    //|| playerPosX + 2 < 10 && playerPosY + 1 < 17 && gameGridcol[playerPosX + 2].row[playerPosY + 1] != null //rightrightup
                    //|| playerPosX + 2 < 10 && playerPosY - 1 >= 0 && gameGridcol[playerPosX + 2].row[playerPosY - 1] != null //rightrightdown
                    || playerPosY - 2 >= 0 && gameGridcol[playerPosX].row[playerPosY - 2] != null) //left
                {
                    Transform sameRow = backgroundGrid.transform.GetChild(playerPosY);
                    xLeftOne = sameRow.GetChild(playerPosX - 1);
                    xLeftOne.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                } 
            } 
        }


        //row.GetComponent<SpriteRenderer>().color = Color.green;

        //Vector3 tempBlockPos;
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                if (y == yUp || y == yDown && x == (int)playerPos.x)
                {
                    if (gameGridcol[x].row[y] != null)
                    {
                        //gameGridcol[x].row[y].gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                }
                else if (x == xRight || x == xLeft && y == (int)playerPos.y)
                {
                    if (gameGridcol[x].row[y] != null)
                    {
                        //gameGridcol[x].row[y].gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                }

                if (gameGridcol[x].row[y] != null)
                {
                    //tempBlockPos = gameGridcol[x].row[y].gameObject.transform.position;
                    //float posDiffX = Managers.Spawner.thePlayer.transform.position.x - tempBlockPos.x;
                    //float posDiffY = (Managers.Spawner.thePlayer.transform.position.y - 1) - tempBlockPos.y;

                    //if (Math.Abs(posDiffX) <= 2 && Math.Abs(posDiffY) == 0)
                    //{
                    //    //gameGridcol[x].row[y].gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    //    //GameObject tempArrow = Instantiate(validArrow);
                    //    //tempArrow.transform.parent = gameGridcol[x].row[y].gameObject.transform;
                    //    //tempArrow.transform.position = new Vector3(0, 0, 0);
                    //}
                    //else if (Math.Abs(posDiffX) <= 2 && Math.Abs(posDiffY) <= 2)
                    //{
                    //    //gameGridcol[x].row[y].gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    //    //GameObject tempArrow = Instantiate(validArrow); 
                    //    //tempArrow.transform.parent = gameGridcol[x].row[y].gameObject.transform;
                    //    //tempArrow.transform.position = new Vector3(0, 0, 0);
                    //}
                    //else
                    //{

                    //}
                }
            }
        }
    }
}
