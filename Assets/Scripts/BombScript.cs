using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [HideInInspector] public int i;
    [HideInInspector] public int j;
    [HideInInspector] public int radius;
    public GameObject bombEffectPrefab;
    [HideInInspector] private List<GameObject> bombEffects;
    private float particleDuration = 1f;
    private int gridSize;
    void Awake()
    {
        gridSize = GameObject.FindWithTag("GameController").GetComponent<GameController>().gridSize;
    }
    private IEnumerator Animation()
    {
        foreach (GameObject be in bombEffects)
        {
            be.GetComponent<BombEffectTrigger>().doEffect();
        }
        yield return new WaitForSeconds(particleDuration);
        foreach (GameObject be in bombEffects)
        {
            Destroy(be);
        }
        Destroy(gameObject);
    }

    public void Detonation()
    {
        bombEffects = new List<GameObject>
        {
            bombEffectInstantiation(0, 0)
        };
        int r = 1;
        while (i - r >= 0 && ((i - r) % 2 == 0 || j % 2 == 0) && r <= radius)
        {
            bombEffects.Add(bombEffectInstantiation(-r, 0));
            r++;
        }
        r = 1;
        while (i + r <= gridSize - 1 && ((i + r) % 2 == 0 || j % 2 == 0) && r <= radius)
        {
            bombEffects.Add(bombEffectInstantiation(r, 0));
            r++;
        }
        r = 1;
        while (j - r >= 0 && ((j - r) % 2 == 0 || i % 2 == 0) && r <= radius)
        {
            bombEffects.Add(bombEffectInstantiation(0, -r));
            r++;
        }
        r = 1;
        while (j + r <= gridSize - 1 && ((j + r) % 2 == 0 || i % 2 == 0) && r <= radius)
        {
            bombEffects.Add(bombEffectInstantiation(0, r));
            r++;
        }
        GetComponent<Renderer>().enabled = false;
        StartCoroutine(Animation());
    }
    private GameObject bombEffectInstantiation(int offsetI, int offsetJ)
    {
        return Instantiate(
            bombEffectPrefab,
            new Vector3(
                (j + offsetJ - gridSize / 2) * 10,
                bombEffectPrefab.transform.localScale.y / 2,
                (gridSize - 1 - (i + offsetI) - gridSize / 2) * 10),
            Quaternion.Euler(0, 0, 0)
        );
    }
}
