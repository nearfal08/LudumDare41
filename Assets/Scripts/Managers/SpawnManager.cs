using UnityEngine;
using System.Collections; 

public class SpawnManager : MonoBehaviour {

	public GameObject[] shapeTypes;
    public GameObject thePlayer;
    
    public void SpawnShape()
	{
		// Random Shape
   		int i = Random.Range(0, shapeTypes.Length);

		// Spawn Group at current Position
		GameObject temp =Instantiate(shapeTypes[i]) ;
        Managers.Game.currentShape = temp.GetComponent<TetrisShape>();
        temp.transform.parent = Managers.Game.blockHolder;
        Managers.Input.isActive = true;
    }

    public void SpawnPlayer()
    {
        // Spawn the player at a random x position in the first row.
        thePlayer = Instantiate(Managers.Game.player);
        int i = Random.Range(0, 9);
        thePlayer.transform.position = new Vector3(i, 0, 0);
    } 
}
