using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public GameObject test;

    void Start()
    {
        for (int i = 0; i < 50000; i++) {
            GameObject del = Instantiate(test);
            Destroy(del);
        }
    }
}
