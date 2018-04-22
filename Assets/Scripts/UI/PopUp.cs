using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour
{
    public GameObject gameOverPopUp;
    public GameObject settingsPopUp;
    public GameObject playerStatsPopUp;
    public GameObject DialogPopup;

    public void ActivateGameOverPopUp()
    {
        gameOverPopUp.transform.parent.gameObject.SetActive(true);
        gameOverPopUp.SetActive(true);
        Managers.UI.activePopUp = gameOverPopUp;
    }

    public void ActivateSettingsPopUp()
    {
        settingsPopUp.transform.parent.gameObject.SetActive(true);
        settingsPopUp.SetActive(true);
        Managers.UI.activePopUp = settingsPopUp;
    }

    public void ActivatePlayerStatsPopUp()
    {
        playerStatsPopUp.transform.parent.gameObject.SetActive(true);
        playerStatsPopUp.SetActive(true);
        Managers.UI.activePopUp = playerStatsPopUp;
    }

    public void ActivateGameDialogPopup()
    {
        if (!DialogPopup.activeSelf && !gameOverPopUp.activeSelf)
        {
            DialogPopup.transform.parent.gameObject.SetActive(true);
            DialogPopup.SetActive(true);
            Managers.UI.activePopUp = DialogPopup;
        } 
    }

    public void DeactivateGameDialogPopup()
    {
        DialogPopup.SetActive(false);
    }



}
