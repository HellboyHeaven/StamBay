using UnityEngine;

public class GameManager : MonoBehaviour
{
    private FigureGenerator _figureGenerator;
    private GameStateManager _stateManager;
    private Coroutine _spawnCoroutine;

    [SerializeField] private Figure prefab;
    [SerializeField] private FigureDatabase figureDatabase;
    [SerializeField] private float spawnTime = 0.1f;
    [SerializeField] private int duplicate = 3;
    [SerializeField] private int spawnCount = 20;
    [SerializeField] private Transform point;
    [SerializeField] private ActionBarController actionBarController;
    [SerializeField] private ButtonController buttonController;
    [SerializeField] private UIController stateUIController;
    private void Awake()
    {
        _stateManager = new(prefab, figureDatabase, point.position, duplicate, spawnCount, spawnTime);
        _stateManager.OnStarted += Spawn;
        _stateManager.OnStarted += stateUIController.SetRunningUI;
        _stateManager.OnWon += stateUIController.SetWonUI;
        _stateManager.OnLost += stateUIController.SetLostUI;
        _stateManager.HookEvents(f => { }, actionBarController.SetData);
        buttonController.RerollButton.clicked += _stateManager.ReRoll;
        stateUIController.OnRestartClicked += _stateManager.Start;
    }

    private void Start()
    {
        _stateManager.Start();
    }

    private void Spawn()
    {
        _spawnCoroutine = StartCoroutine(_stateManager.Spawn());
    }

    private void OnDestroy()
    {
        _stateManager.OnStarted -= Spawn;
        _stateManager.OnStarted -= stateUIController.SetRunningUI;
    }
}
