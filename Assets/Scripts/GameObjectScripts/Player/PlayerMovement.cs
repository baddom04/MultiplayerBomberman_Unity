using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float upperSpeedBound = 16f;
    [SerializeField] private float bound;
    [SerializeField] private string[] keys = new string[4];
    [SerializeField] private Dictionary<string, int> keyBinds = new Dictionary<string, int>();
    [SerializeField] private PauseMenu pauseMenu;
    private Animator anim;
    private void Start()
    {
        bound = GameController.gridSize / 2f * 10;
        anim = GetComponent<Animator>();
        for (int i = 0; i < 4; i++)
        {
            keyBinds.Add(keys[i], i * 90);
        }
    }
    private void FixedUpdate()
    {
        if (GameController.gameOn && !pauseMenu.isPausedState())
        {
            MovePlayer();
            RestrictPlayer();
        }
    }
    private void MovePlayer()
    {
        int keys = 0;
        foreach (KeyValuePair<string, int> keyBind in keyBinds)
        {
            if (Input.GetKey(keyBind.Key))
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, keyBind.Value, 0), 0.2f);
                keys++;
                Run();
            }
        }
        if (keys == 0) Stop();
        else transform.Translate(0, 0, speed * Time.deltaTime);
    }
    private void RestrictPlayer()
    {
        if (transform.position.x > bound)
            transform.position = new Vector3(bound, transform.position.y, transform.position.z);
        if (transform.position.x < -bound)
            transform.position = new Vector3(-bound, transform.position.y, transform.position.z);
        if (transform.position.z > bound)
            transform.position = new Vector3(transform.position.x, transform.position.y, bound);
        if (transform.position.z < -bound)
            transform.position = new Vector3(transform.position.x, transform.position.y, -bound);
    }
    private void Run()
    {
        anim.SetFloat("Speed_f", 1f);
        anim.SetBool("Static_b", true);
    }
    private void Stop()
    {
        anim.SetBool("Static_b", false);
        anim.SetFloat("Speed_f", 0f);
    }
    //*Power-up functions
    public void SpeedUp(){
        if(speed < upperSpeedBound) speed = upperSpeedBound;
    }
}
