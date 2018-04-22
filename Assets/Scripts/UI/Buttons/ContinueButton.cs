using UnityEngine;
using System.Collections;

public class ContinueButton : MonoBehaviour {

    public void OnClickContinueButton()
    {
        Managers.Audio.PlayUIClick();
        Managers.Game.SetState(typeof(GamePlayState));
    }

    public void OnClickNextRound()
    {
        Managers.Input.PostPopupChores();
        Managers.UI.popUps.DeactivateGameDialogPopup();
    }
}
