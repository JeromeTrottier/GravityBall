using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class controlePages : MonoBehaviour
{
    public GameObject backgound;
    public Text pagination;
    public GameObject[] contenuPage1;
    public GameObject[] contenuPage2;
    public GameObject fondu;
    // Start is called before the first frame update
    void Start()
    {
        fondu.SetActive(true); //On active le fondu
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) //Si on appuie sur la flèche droite, on passe à la page 2
        {
            for (int i=0; i< contenuPage1.Length; i++)
            {
                contenuPage1[i].SetActive(false);
                contenuPage2[i].SetActive(true);
            }
            pagination.GetComponent<Text>().text = 2.ToString();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) //Si on appuie sur la flèche gauche, on passe à la page 1
        {
            for (int i = 0; i < contenuPage1.Length; i++)
            {
                contenuPage1[i].SetActive(true);
                contenuPage2[i].SetActive(false);
            }
            pagination.GetComponent<Text>().text = 1.ToString();
        }

        if (Input.GetKeyDown(KeyCode.M)) //Si on appuie sur M, on revient au menu
        {
            fondu.GetComponent<Animator>().SetBool("sortieScene", true);
            Invoke("quitterScene", 1f);
        }
    }
    void quitterScene()
    {
        SceneManager.LoadScene("Intro");
    }
}
