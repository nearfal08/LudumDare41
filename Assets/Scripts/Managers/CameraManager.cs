using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraManager : MonoBehaviour {

	public Camera main;

    private float _mainMenuSize = 13.5f;
    private float _inGameSize = 8f;

    [HideInInspector]
	public CameraShake shaker;

	void Awake()
	{
		shaker = main.gameObject.GetComponent<CameraShake> (); 
	}

    public void ZoomIn()
    {
        if (main.orthographicSize != _inGameSize)
        {
            // Move the camera to a new Y position and zoom it in.
            main.transform.DOLocalMoveY(7.5f, 1);
            main.DOOrthoSize(_inGameSize, 1f).SetEase(Ease.OutCubic).OnComplete(() =>
            { 
                StartCoroutine(StartGamePlay());
             });
        }
        else
        {
            // Spawn the player then spawn a block
            StartGameChores();
        }
    }

    public void ZoomOut()
    {
        if (main.orthographicSize != _mainMenuSize)
            main.DOOrthoSize(_mainMenuSize, 1f).SetEase(Ease.OutCubic); ;
    }

    IEnumerator StartGamePlay()
    {
        yield return new WaitForEndOfFrame();

        if (!Managers.Game.isGameActive)
        {
            // Spawn the player then spawn a block
            StartGameChores();
        }
        yield break;
    }

    public void StartGameChores()
    {
        Managers.Spawner.SpawnPlayer();
        Managers.Spawner.SpawnShape();
        Managers.Game.isGameActive = true;
    }


}
