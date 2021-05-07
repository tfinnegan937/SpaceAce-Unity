using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update
    public static Entity inst;
    void Awake()
    {
        inst = this;
    }

    public Vector3 velocity;
    public float maxSpeed;
    public float minSpeed;
    public float speed;
    public float acceleration;
    public int health;
    public Rigidbody rigidbody;
    public EntityMgr entityMgr;
    void Start()
    {
        velocity = new Vector3(0, 0, 0);
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = velocity.normalized * speed;
        rigidbody.velocity = velocity;
        ConstrainVelocity();
    }
    
    public void SetVelocity(Vector3 vel)
    {
        Vector3 tempVel = vel;


        velocity = tempVel;
        speed = velocity.magnitude;

        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
    }

    public void Accelerate(Vector3 accelVec)
    {
        //Debug.Log(accelVec);
        velocity += (accelVec * Time.deltaTime);
        speed = velocity.magnitude;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

    }

    public void AddSpeed(float speed_in)
    {
        speed += speed_in;
    }

    public void SetSpeed(float speed_in)
    {
        speed = speed_in;
    }

    public void DestroyEntity()
    {
        entityMgr.RemoveEntity(gameObject.transform.name);
    }

    private void ConstrainVelocity()
    {
        Camera mainCamera = Camera.main;
        Rect cameraRect = mainCamera.rect;
        Vector3 cameraZVec = mainCamera.WorldToViewportPoint(new Vector3(0, 0, transform.position.z));
        Vector3 min_point = mainCamera.ViewportToWorldPoint(new Vector3(cameraRect.xMin, cameraRect.yMin, cameraZVec.z));
        Vector3 max_point = mainCamera.ViewportToWorldPoint(new Vector3(cameraRect.xMax, cameraRect.yMax, cameraZVec.z));
        cameraRect.xMin = min_point.x;
        cameraRect.xMax = max_point.x;
        cameraRect.yMin = min_point.y;
        cameraRect.yMax = max_point.y;
        
        if (gameObject.name == "Player") //The player must ALWAYS be on screen
        {
            transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax),
            Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax),
            -5);
        }
        else
        {
            //Keep all AI on screen vertically
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax),
                -5);
        }
    }

    public void Damage(int multiplier=1)
    {
        health -= 1 * multiplier;
        if (health <= 0)
        {
            AIAspect ai = gameObject.GetComponent<AIAspect>();
            Debug.Log(ai);
            //Debug.Log("here");

            if (ai != null)
            {
                Debug.Log("here");
                ai.destroyedByProjectile = true;
            }
            entityMgr.RemoveEntity(this.gameObject.name);
 

        }
    }
    
}
