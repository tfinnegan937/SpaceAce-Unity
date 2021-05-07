using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public static InputMgr inst;
    public Entity player;
    public PauseMgr pauseMgr;
    public ProjectileMgr projMgr;
    private float timeToFire = 0;
    void Awake()
    {
        inst = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleKeyboard();
        HandlePause();
    }

    void HandleKeyboard()
    {
        Vector3 totalVelocity = new Vector3(0, 0, 0);
        Vector3 decelVector = player.velocity.normalized * -2 * player.maxSpeed;
        player.Accelerate(decelVector); //Decelerate rapidly if not pressed
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 accelVector = new Vector3(0, 1, 0);

            totalVelocity += accelVector;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 accelVector = new Vector3(0, -1, 0);

            totalVelocity += accelVector;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 accelVector = new Vector3(-1, 0, 0);

            totalVelocity += accelVector;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 accelVector = new Vector3(1, 0, 0);
            totalVelocity += accelVector;
        }

        if (Input.GetKey(KeyCode.Space) && timeToFire < Time.time)
        {
            timeToFire = Time.time + 1 / projMgr.playerProjectile.fireRate;
            projMgr.SpawnPlayerProjectile();
        }

        totalVelocity *= player.maxSpeed;
        player.SetVelocity(totalVelocity);

    }

    void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMgr.TogglePause();
        }
    }
}
