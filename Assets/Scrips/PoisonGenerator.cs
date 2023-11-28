using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGenerator : MonoBehaviour
{
    public GameObject poison;
    // Start is called before the first frame update
    void Start()
    {
        generatePoison();
    }

    private void generatePoison()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(poison, new Vector3(Random.Range(-20, 20), 0, Random.Range(20, -20)), Quaternion.identity);
        }

    }
}
