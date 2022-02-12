using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashMouvementFleche : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posSouris = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Position de la souris par rapport à la scène
        Vector3 direction = posSouris - transform.position; //Création du vecteur qui pointe dans la direction de la souris par rapport au personnage
        direction.z = 0; //position z = 0
        transform.right = direction;
    }
}
