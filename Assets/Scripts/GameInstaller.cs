using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public PauseMenu _pauseMenu;
    public PlayerLogic _player1;
    public PlayerLogic _player2;
    public GameObject _pauseBtn;
    public CameraScript _camera;
    public MainMenu _mainMenu;
    public GameObject boulderPrefab;
    public GameObject cratePrefab;
    public GameObject[] pickups;
    public Transform parentEnvironment;
    public override void InstallBindings()
    {
        Container.BindInstance(_pauseMenu).AsSingle();
        Container.BindInstance(_pauseBtn).WithId("pauseBtn");
        Container.BindInstance(_camera).AsSingle();
        Container.BindInstance(_mainMenu).AsSingle();
        Container.BindInstance(_player1).WithId("player1");
        Container.BindInstance(_player2).WithId("player2");
        Container.BindInstance(boulderPrefab).WithId("boulder");
        Container.BindInstance(cratePrefab).WithId("crate");
        Container.BindInstance(pickups).AsSingle();
        Container.BindInstance(parentEnvironment).WithId("environment");

        Container.Bind<GameController>().AsSingle();
        Container.Bind<MapCreator>().AsSingle();
    }
}