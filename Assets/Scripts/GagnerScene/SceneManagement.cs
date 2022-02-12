using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public GameObject fondu;

    // Start is called before the first frame update
    void Start()
    {
        fondu.SetActive(true); //On active le fondu
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) //Si on appuie sur la touche R, on revient à la scène du niveau
        {
            fondu.GetComponent<Animator>().SetBool("sortieScene", true);
            Invoke("changerScene", 1f);
        }
    }

    void changerScene()
    {
        SceneManager.LoadScene("Niveau1");
    }
}
