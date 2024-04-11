using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [Inject] private readonly MapCreator _mapCreator;
    [Inject] private readonly PauseMenu _pauseMenu;
    [Inject(Id ="player1")] private readonly PlayerLogic _player1;
    [Inject(Id ="player2")] private readonly PlayerLogic _player2;
    [Inject(Id ="pauseBtn")] private readonly GameObject _pauseBtn;
    [Inject] private readonly CameraScript _camera;
    [Inject] private readonly MainMenu _mainMenu;
    private static GameObject[,] level;
    public static bool gameOn = true;
    public static int gridSize = 9;

    // [Inject]
    // private void DIInit
    // (
    //     PauseMenu pauseMenu,
    //     [Inject(Id ="player1")] PlayerLogic player1,
    //     [Inject(Id ="player2")] PlayerLogic player2,
    //     [Inject(Id ="pauseBtn")] GameObject pauseBtn,
    //     CameraScript camera,
    //     MainMenu mainMenu
    // )
    // {
    //     _pauseMenu = pauseMenu;
    //     _player1 = player1;
    //     _player2 = player2;
    //     _pauseBtn = pauseBtn;
    //     _camera = camera;
    //     _mainMenu = mainMenu;
    // }
    void Start()
    {
        level = CreateMap();
    }
    void Update()
    {
        if (gameOn && !_pauseMenu.isPausedState())
        {
            if (Input.GetKeyDown(KeyCode.M)) _player1.PlaceBomb();
            if (Input.GetKeyDown(KeyCode.Space)) _player2.PlaceBomb();
            if (Input.GetKeyDown(KeyCode.Escape)) _pauseMenu.Pause();
        }
    }
    public static void GameOver()
    {
        GameObject.Find("PauseBtn").SetActive(false);
        Camera.main.GetComponent<CameraScript>().TheEnd();
        GameObject.Find("GameOverMenu").GetComponent<MainMenu>().Show();
    }
    private GameObject[,] CreateMap()
    {
        _mapCreator.InitiateMap(gridSize);
        return _mapCreator.GetLevel();
    }
    public static GameObject[,] GetLevel()
    {
        return level;
    }
}
