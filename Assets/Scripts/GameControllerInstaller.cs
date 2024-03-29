using UnityEngine;
using Zenject;

public class GameControllerInstaller : MonoInstaller
{
    public PauseMenu _pauseMenu;
    public PlayerLogic _player1;
    public PlayerLogic _player2;
    public GameObject _pauseBtn;
    public CameraScript _camera;
    public MainMenu _mainMenu;
    public override void InstallBindings()
    {
        Container.BindInstance(_pauseMenu).AsSingle();
        Container.BindInstance(_pauseBtn).WithId("pauseBtn");
        Container.BindInstance(_camera).AsSingle();
        Container.BindInstance(_mainMenu).AsSingle();
        Container.BindInstance(_player1).WithId("player1");
        Container.BindInstance(_player2).WithId("player2");

        Container.Bind<GameController>().AsSingle();
    }
}