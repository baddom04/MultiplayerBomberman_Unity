using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int _i;
    [SerializeField] private int _j;
    [SerializeField] private int _radius;
    [SerializeField] private GameObject bombEffectPrefab;
    [SerializeField] private List<GameObject> bombEffects;
    [SerializeField] private float particleDuration = 0.9f;
    [SerializeField] private float sensorDuration = 0.1f;
    public void Construct(Vector3 pos, int i, int j, int radius){
        transform.position = pos;
        _i = i;
        _j = j;
        _radius = radius;
    }
    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(sensorDuration);
        DisableColliders();
        yield return new WaitForSeconds(particleDuration);
        DestroyAll();
    }
    private void DisableColliders()
    {
        foreach (GameObject be in bombEffects)
        {
            be.GetComponent<BoxCollider>().enabled = false;
        }
    }
    private void DestroyAll()
    {
        foreach (GameObject be in bombEffects)
        {
            Destroy(be);
        }
        Destroy(gameObject);
    }

    public void Detonation()
    {
        bombEffects = new List<GameObject> { bombEffectInstantiation(0, 0) };
        bool upFlag = true;
        bool downFlag = true;
        bool leftFlag = true;
        bool rightFlag = true;
        for (int r = 0; r <= _radius; r++)
        {
            BombEffectDirections(-r, 0, ref upFlag);
            BombEffectDirections(r, 0, ref downFlag);
            BombEffectDirections(0, -r, ref leftFlag);
            BombEffectDirections(0, r, ref rightFlag);
        }
        GetComponent<Renderer>().enabled = false;
        StartCoroutine(Explosion());
    }
    private void BombEffectDirections(int offsetI, int offsetJ, ref bool headingFlag)
    {
        if (headingFlag && InBounds(offsetI, offsetJ) && NotHitABoulder(offsetI, offsetJ))
        {
            bombEffects.Add(bombEffectInstantiation(offsetI, offsetJ));
            if (HitACrate(offsetI, offsetJ)) headingFlag = false;
        }
        else headingFlag = false;
    }
    private bool HitACrate(int offsetI, int offsetJ)
    {
        GameObject[,] level = GameController.GetLevel();
        if(level[_i + offsetI, _j + offsetJ] != null){
            return level[_i + offsetI, _j + offsetJ].CompareTag("Crate"); 
        }
        return false;
    }
    private bool InBounds(int offsetI, int offsetJ)
    {
        int maxBoundIndex = GameController.gridSize - 1;
        return _j + offsetJ <= maxBoundIndex && _j + offsetJ >= 0 && _i + offsetI >= 0 && _i + offsetI <= maxBoundIndex;
    }
    private bool NotHitABoulder(int offsetI, int offsetJ)
    {
        return (_j + offsetJ) % 2 == 0 || (_i + offsetI) % 2 == 0;
    }
    private GameObject bombEffectInstantiation(int offsetI, int offsetJ)
    {
        float x = (_j + offsetJ - GameController.gridSize / 2) * 10;
        float y = bombEffectPrefab.transform.localScale.y / 2;
        float z = (GameController.gridSize - 1 - (_i + offsetI) - GameController.gridSize / 2) * 10;
        return Instantiate(
            bombEffectPrefab,
            new Vector3(x, y, z),
            Quaternion.identity
        );
    }
}
