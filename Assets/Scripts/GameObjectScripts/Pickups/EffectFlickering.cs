using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFlickering : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float bound;
    [SerializeField] private new Renderer renderer;
    private void Awake() {
        renderer = GetComponent<Renderer>();
        color = renderer.material.color;
        bound = color.a;
    }
    public void EndIndicator(){
        StartCoroutine(Wobble());
    }
    private IEnumerator Wobble(){
        float change = 0.05f;
        while (true){
            color = new Color(color.r, color.g, color.b, color.a + change);
            renderer.material.color = color;
            if(color.a > bound || color.a < 0) change = -change;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
