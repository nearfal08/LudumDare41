using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(GridManager))]
[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(ScoreManager))]
[RequireComponent(typeof(PlayerInputManager))]
[RequireComponent(typeof(SpawnManager))]
[RequireComponent(typeof(ColorManager))]
[RequireComponent(typeof(ItemsManager))] 
public class Managers : MonoBehaviour
{
	private static GameManager _gameManager;
	public static GameManager Game
	{
		get { return _gameManager; }
	}

	private static UIManager _uiManager;
	public static UIManager UI
	{
		get { return _uiManager; }
	}

	private static AudioManager _audioManager;
	public static AudioManager Audio
	{
		get { return _audioManager; }
	}

	private static ScoreManager _scoreManager;
	public static ScoreManager Score
	{
		get { return _scoreManager; }
	}

	private static PlayerInputManager _inputManager;
	public static PlayerInputManager Input
	{
		get { return _inputManager; }
	}

	private static CameraManager _cameraManager;
	public static CameraManager Cam
	{
		get { return _cameraManager; }
	}

	private static GridManager _gridManager;
	public static GridManager Grid
	{
		get { return _gridManager; }
	}

	private static SpawnManager _spawnManager;
	public static SpawnManager Spawner
	{
		get { return _spawnManager; }
	}

	private static ColorManager _colorManager;
	public static ColorManager Palette
	{
		get { return _colorManager; }
    }

    private static ItemsManager _itemManager;
    public static ItemsManager Items
    {
        get { return _itemManager; }
    } 
    void Awake ()
	{
		_gameManager = GetComponent<GameManager>();
		_uiManager = GetComponent<UIManager>();
		_audioManager = GetComponent<AudioManager>();
		_scoreManager = GetComponent<ScoreManager> ();
		_inputManager = GetComponent<PlayerInputManager> (); 
		_cameraManager = GetComponent<CameraManager> ();
		_gridManager = GetComponent<GridManager> ();
		_spawnManager = GetComponent<SpawnManager> ();
		_colorManager = GetComponent<ColorManager> ();
        _itemManager = GetComponent<ItemsManager>();

        DontDestroyOnLoad(gameObject);
	}
    int cnt = 0;

    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            cnt++;
            ScreenCapture.CaptureScreenshot("ScreenShots/"+cnt.ToString() + ".png");
        }
    }
}