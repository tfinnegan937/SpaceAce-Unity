using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelterMovePtMgr : MonoBehaviour
{
    public List<Vector3> positions;
    // Start is called before the first frame update
    public static PelterMovePtMgr inst;

    void Awake()
    {
        inst = this;
    }
    void Start()
    {
        foreach (Transform t in transform)
        {
            positions.Add(t.position);
        }
    }

    // Update is called once per frame

    public Vector3 GetRandomPosition()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        int posIndex = Random.Range(0, positions.Count);
        return positions[posIndex];
    }
}
