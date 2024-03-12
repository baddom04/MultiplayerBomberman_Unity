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
    private IEnumerator Animation(float speed)
    {
        foreach(GameObject be in bombEffects){
            be.GetComponent<BombEffectTrigger>().doEffect();
        }
        yield return new WaitForSeconds(1f);
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
        while(i - r >= 0 && ((i - r) % 2 == 0 || j % 2 == 0) && r <= radius){
            bombEffects.Add(bombEffectInstantiation(-r, 0));
            r++;
        }
        r = 1;
        while(i + r <= 8 && ((i + r) % 2 == 0 || j % 2 == 0) && r <= radius){
            bombEffects.Add(bombEffectInstantiation(r, 0));
            r++;
        }
        r = 1;
        while(j - r >= 0 && ((j - r) % 2 == 0 || i % 2 == 0) && r <= radius){
            bombEffects.Add(bombEffectInstantiation(0, -r));
            r++;
        }
        r = 1;
        while(j + r <= 8 && ((j + r) % 2 == 0 || i % 2 == 0) && r <= radius){
            bombEffects.Add(bombEffectInstantiation(0, r));
            r++;
        }
        transform.localScale = Vector3.zero;
        StartCoroutine(Animation(0.5f));
    }
    private GameObject bombEffectInstantiation(int offsetI, int offsetJ)
    {
        return Instantiate(
            bombEffectPrefab,
            new Vector3(
                (j + offsetJ) * 10 - 40,
                bombEffectPrefab.transform.localScale.y / 2, 
                (8 - (i + offsetI)) * 10 - 40),
            Quaternion.Euler(0, 0, 0)
        );
    }
}
