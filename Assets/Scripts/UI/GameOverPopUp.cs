using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameOverPopUp : MonoBehaviour {

    public Text endGameReason;
    
    void OnEnable()
    {
        if (Managers.Spawner.thePlayer.GetComponent<PlayerController>().lose)
        {
            endGameReason.text = Managers.Spawner.thePlayer.GetComponent<PlayerController>().deathReason;
            Managers.UI.panel.SetActive(true);
            Managers.Audio.PlayLoseSound();
        } 
        else
        {
            endGameReason.text = Managers.Spawner.thePlayer.GetComponent<PlayerController>().winReason;
            Managers.UI.panel.SetActive(true);
            Managers.Audio.PlayWinSound();
        }
    }

    public void BackToMainMenu()
    {
        Managers.Grid.ClearBoard();
        Managers.Audio.PlayUIClick();
        Managers.UI.panel.SetActive(false);
        Managers.Game.SetState(typeof(MenuState));
        gameObject.SetActive(false);
    }
}
