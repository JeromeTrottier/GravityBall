using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementNiveau : MonoBehaviour
{
    public GameObject fondu;
    public GameObject musique;
    // Start is called before the first frame update
    void Start()
    {
        fondu.SetActive(true);
        bouleMouvement.nbreEtoileRecupere = 0; //On r�initialise la valeur du nombre d'�toile r�cup�r� � 0
    }

    // Update is called once per frame
    void Update()
    {
        if(bouleMouvement.mort) //Si le joueur est mort, on relance la scene
        {
            transitionnerScene();
            Invoke("RelancerScene", 2f);
        }

        if (bouleMouvement.gagner) //Si le joueur fini le niveau, on lance la sc�ne de fin
        {
            transitionnerScene();
            Invoke("changerSceneGagner", 1f);
        }

        if (Input.GetKeyDown(KeyCode.R)) //Si le joueur appuie sur R, il r�initialise le niveau
        {
            Invoke("RelancerScene", 1f);
        }
    }

    void RelancerScene() //Fonction pour relancer la sc�ne
    {
        SceneManager.LoadScene("Niveau1"); //On relance le niveau1
        bouleMouvement.mort = false;
    }

    void changerSceneGagner() //Aller � la sc�ne de conclusion
    {
        SceneManager.LoadScene("Gagner");
        bouleMouvement.gagner = false;
    }

    void transitionnerScene() //Transitions entre les sc�nes
    {
        musique.GetComponent<Animator>().SetBool("musiqueSortant", true);
        fondu.GetComponent<Animator>().SetBool("sortieScene", true);
    }
}
