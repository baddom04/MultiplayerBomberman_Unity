using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public PauseMenu pauseMenu;
    public PlayerLogic player1;
    public PlayerLogic player2;
    public GameObject pauseBtn;
    public CameraScript _camera;
    public MainMenu mainMenu;
    public GameObject boulderPrefab;
    public GameObject cratePrefab;
    public GameObject[] pickups;
    public Transform parentEnvironment;
    public override void InstallBindings()
    {
        Container.BindInstance(pauseMenu).AsSingle();
        Container.BindInstance(pauseBtn).WithId("pauseBtn");
        Container.BindInstance(_camera).AsSingle();
        Container.BindInstance(mainMenu).AsSingle();
        Container.BindInstance(player1).WithId("player1");
        Container.BindInstance(player2).WithId("player2");
        Container.BindInstance(boulderPrefab).WithId("boulder");
        Container.BindInstance(cratePrefab).WithId("crate");
        Container.BindInstance(pickups).AsSingle();
        Container.BindInstance(parentEnvironment).WithId("environment");

        Container.Bind<MapCreator>().AsSingle();
        Container.Bind<GameController>().AsSingle();
    }
}