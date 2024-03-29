using UnityEngine;
using Zenject;

public class MapInstaller : MonoInstaller
{
    public GameObject boulderPrefab;
    public GameObject cratePrefab;
    public GameObject[] pickups;
    public Transform parentEnvironment;
    public Transform player1tf;
    public Transform player2tf;
    public override void InstallBindings()
    {
        Container.BindInstance(boulderPrefab).WithId("boulder");
        Container.BindInstance(cratePrefab).WithId("crate");
        Container.BindInstance(pickups).AsSingle();
        Container.BindInstance(parentEnvironment).WithId("environment");
        Container.BindInstance(player1tf).WithId("player1");
        Container.BindInstance(player2tf).WithId("player2");

        Container.Bind<MapCreator>().AsSingle();
    }
}