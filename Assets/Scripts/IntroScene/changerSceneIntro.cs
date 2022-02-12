using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changerSceneIntro : MonoBehaviour
{
    public GameObject fondu;
    public GameObject musique;
    // Start is called before the first frame update
    void Start()
    {
        fondu.SetActive(true); //On active le fondu
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Si on appuie sur espace, on démarre le jeu
        {
            fondu.GetComponent<Animator>().SetBool("sortieScene", true);
            musique.GetComponent<Animator>().SetBool("musiqueSortant", true);
            Invoke("changerSceneNiveau", 1f);
        }

        if (Input.GetKeyDown(KeyCode.I)) //Si on appuie sur I, on ouvre les instructions
        {
            fondu.GetComponent<Animator>().SetBool("sortieScene", true);
            musique.GetComponent<Animator>().SetBool("musiqueSortant", true);
            Invoke("changerSceneIntrusction", 1f);
        }

    }

    void changerSceneNiveau()
    {
        SceneManager.LoadScene("Niveau1");
    }

    void changerSceneIntrusction()
    {
        SceneManager.LoadScene("Instructions");
    }
}
