using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      GameObject boll=  PhotonNetwork.Instantiate("ball", new Vector3(Random.Range(-11, -3), 2.5f, Random.Range(-4, 0)),Quaternion.identity);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
