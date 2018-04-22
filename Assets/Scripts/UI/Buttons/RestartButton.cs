using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {

    public void OnClickRestartButton()
    {
        Managers.Audio.PlayUIClick();
        Managers.Grid.ClearBoard();
        Managers.Game.isGameActive = false;
        Managers.Game.SetState(typeof(GamePlayState));
        Managers.UI.popUps.gameOverPopUp.SetActive(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
