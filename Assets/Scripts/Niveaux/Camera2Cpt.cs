using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2Cpt : MonoBehaviour
{
    public GameObject laCible;
    public float positionY;

    public float limiteGauche;
    public float limiteDroite;
    public float limiteHaut;
    public float limiteBas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 maPosition = laCible.transform.position; //Position de la camera
        if (laCible.transform.position.x < limiteGauche) //Limite la camera à gauche
        {
            maPosition.x = limiteGauche; 
        }
        if (laCible.transform.position.x > limiteDroite) //Limite la camera à droite
        {
            maPosition.x = limiteDroite;
        }
        if (laCible.transform.position.y > limiteHaut) //Limite la camera en haut 
        {
            maPosition.y = limiteHaut;
        }
        if (laCible.transform.position.y < limiteBas) //Limite la camera en bas
        {
            maPosition.y = limiteBas;
        }
        maPosition.z = -646.04f; //Position Z de la camera
        transform.position = maPosition;
    }
}
