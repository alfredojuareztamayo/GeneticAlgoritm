using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public GameObject food;
   
    // Start is called before the first frame update
    void Start()
    {
       
        generateFood();
    }

    private void Update()
    {
        if (Random.Range(0, 100) == 10)
        {
            GameObject tempFood = Instantiate(food, new Vector3(Random.Range(-20, 20), 0, Random.Range(20, -20)), Quaternion.identity);
            
        }
    }
    private void generateFood()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject tempFood = Instantiate(food,new Vector3(Random.Range(-20,20),0,Random.Range(20,-20)), Quaternion.identity);
           
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("player"))
    //    {
    //        Destroy(food);
    //    }
    //}
}
