using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneretorVehicule : MonoBehaviour
{
    public int numberCars;
    public GameObject vehicule;
    // Start is called before the first frame update
    void Start()
    {
        generatePoison();
    }

    private void generatePoison()
    {
        for (int i = 0; i < numberCars; i++)
        {
            Instantiate(vehicule, new Vector3(Random.Range(-20, 20), 0, Random.Range(20, -20)), Quaternion.identity);
        }

    }
}
