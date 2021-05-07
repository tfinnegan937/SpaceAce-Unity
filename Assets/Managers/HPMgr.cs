using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public Entity player;
    public void DamagePlayer()
    {
        player.GetComponent<PlayerInvulnerabilityTime>().enabled = true;
        if (heart1.activeInHierarchy)
        {
            heart1.SetActive(false);
        }else if (heart2.activeInHierarchy)
        {
            heart2.SetActive(false);
        }else if (heart3.activeInHierarchy)
        {
            heart3.SetActive(false);
            Debug.Log("Here");
            player.entityMgr.RemoveEntity(player.gameObject.name);
        }
    }

    public void GainLife()
    {
        if (!heart3.activeInHierarchy)
        {
            heart3.SetActive(true);
        }
        else if (!heart2.activeInHierarchy)
        {
            heart2.SetActive(true);
        }

        else if (!heart1.activeInHierarchy)
        {
            heart1.SetActive(true);
        }
    }

}
