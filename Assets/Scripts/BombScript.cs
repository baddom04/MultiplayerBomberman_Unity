using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [HideInInspector] public int i;
    [HideInInspector] public int j;
    [HideInInspector] public int radius;
    [SerializeField] private GameObject bombEffectPrefab;
    private List<GameObject> bombEffects;
    private float particleDuration = 0.8f;
    private GameObject[,] level;
    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(particleDuration);
        foreach (GameObject be in bombEffects)
        {
            Destroy(be);
        }
        Destroy(gameObject);
    }

    public void Detonation()
    {
        bombEffects = new List<GameObject> { bombEffectInstantiation(0, 0) };
        level = GameObject.Find("GameController").GetComponent<GameController>().level;
        bool upFlag = true;
        bool downFlag = true;
        bool leftFlag = true;
        bool rightFlag = true;
        for (int r = 0; r <= radius; r++)
        {
            BombEffectDirections(-r, 0, ref upFlag);
            BombEffectDirections(r, 0, ref downFlag);
            BombEffectDirections(0, -r, ref leftFlag);
            BombEffectDirections(0, r, ref rightFlag);
        }
        GetComponent<Renderer>().enabled = false;
        StartCoroutine(Animation());
    }
    private void BombEffectDirections(int offsetI, int offsetJ, ref bool headingFlag){
        if(InBounds(offsetI, offsetJ) && HitABoulder(offsetI, offsetJ) && headingFlag){
            bombEffects.Add(bombEffectInstantiation(offsetI, offsetJ));
            if(HitACrate(offsetI, offsetJ)) headingFlag = false;
        }
        else headingFlag = false;
    }
    private bool HitACrate(int offsetI, int offsetJ)
    {
        // if(InBounds(i + offsetI, j + offsetJ))
            return level[i + offsetI, j + offsetJ] != null && level[i + offsetI, j + offsetJ].CompareTag("Crate");
        // return false;
    }
    private bool InBounds(int offsetI, int offsetJ){
        int maxBoundIndex = GameController.gridSize - 1;
        return j + offsetJ <= maxBoundIndex && j + offsetJ >= 0 && i + offsetI >= 0 && i + offsetI <= maxBoundIndex;
    }
    private bool HitABoulder(int offsetI, int offsetJ){
        return (j + offsetJ) % 2 == 0 || (i + offsetI) % 2 == 0;
    }
    private GameObject bombEffectInstantiation(int offsetI, int offsetJ)
    {
        return Instantiate(
            bombEffectPrefab,
            new Vector3(
                (j + offsetJ - GameController.gridSize / 2) * 10,
                bombEffectPrefab.transform.localScale.y / 2,
                (GameController.gridSize - 1 - (i + offsetI) - GameController.gridSize / 2) * 10),
            Quaternion.Euler(0, 0, 0)
        );
    }
}
