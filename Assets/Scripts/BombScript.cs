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
    [HideInInspector] public List<GameObject> bombEffects;

    public void bombEffectsAnimation(float speed)
    {
        StartCoroutine(Animation(speed));
    }
    private IEnumerator Animation(float speed)
    {
        while (bombEffects[0].transform.localScale.x < 10)
        {
            foreach (GameObject be in bombEffects)
            {
                be.transform.localScale += new Vector3(speed, speed, speed);
            }
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.2f);
        while (bombEffects[0].transform.localScale.x > 0)
        {
            foreach (GameObject be in bombEffects)
            {
                be.transform.localScale -= new Vector3(speed, speed, speed);
            }
            yield return new WaitForSeconds(0.01f);
        }
        foreach (GameObject be in bombEffects)
        {
            Destroy(be);
        }
        Destroy(gameObject);
    }

    public void Detonation()
    {
        bombEffects.Add(bombEffectInstantiation(0, 0));
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
        bombEffectsAnimation(0.5f);
    }
    private GameObject bombEffectInstantiation(int offsetI, int offsetJ)
    {
        return Instantiate(
            bombEffectPrefab,
            new Vector3(
                (j + offsetJ) * 10 - 40,
                5,
                (8 - (i + offsetI)) * 10 - 40),
            Quaternion.Euler(0, 0, 0)
        );
    }
}
