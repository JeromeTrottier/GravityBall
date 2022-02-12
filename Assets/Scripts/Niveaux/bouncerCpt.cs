using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncerCpt : MonoBehaviour
{
    public GameObject boule;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Bonhomme") //Si le bouncer rentre en contact avec le joueur
        {
            GetComponent<Animator>().SetBool("activer", true); //On active son animation
            Invoke("DesactiverBouncer", 0.2f); //On désactiver la condition d'animation dans 0.2s.
        }
   }

    void DesactiverBouncer() //Fonction de désactivation
    {
        GetComponent<Animator>().SetBool("activer", false);
    }
}
