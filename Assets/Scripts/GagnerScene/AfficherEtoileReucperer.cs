using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfficherEtoileReucperer : MonoBehaviour
{
    public GameObject points;
    public GameObject etoile; //objet étoile de base
    public GameObject[] texteCorrection; //2 textes possibles selon le nombre d'étoile ramassé

    public AudioClip etoileApparitionSon;

    private int index = 0; //index du for loop
    void Start()
    {
        points.GetComponent<Text>().text = bouleMouvement.nbreEtoileRecupere.ToString(); //Affiche le nombre d'etoiles récupérer
        for (int i = 0; i < bouleMouvement.nbreEtoileRecupere; i++) //Affiche le nombre d'étoile ramassé
        {
            Invoke("creerNouvelleEtoile", i * 1f); //Affiche chaque étoile avec un interval de 1 seconde
        }

        if(bouleMouvement.nbreEtoileRecupere <= 1) //Si on récupère 1 étoile ou moins, on affiche l'ortographe
        {
            texteCorrection[1].SetActive(true);
        } else if(bouleMouvement.nbreEtoileRecupere >= 2) //Si on récupère 2 étoiles ou plus, on affiche l'ortographe au pluriel
        {
            texteCorrection[0].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void creerNouvelleEtoile()
    {
        GameObject etoileClone = Instantiate(etoile, new Vector3(etoile.transform.position.x + index * 2.0f, etoile.transform.position.y, 0), Quaternion.identity); //Instantiation des étoiles avec une distance de 2f entre chaque
        etoileClone.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(etoileApparitionSon);
        index++; //on incrémente l'index
    }
}
