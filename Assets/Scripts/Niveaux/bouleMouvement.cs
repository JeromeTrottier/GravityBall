using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal; //Utilise le render pipeline experimental de Univeral pour pouvoir gerer les lumieres en 2D

public class bouleMouvement : MonoBehaviour
{
    public GameObject boule; //Joueur
    public GameObject cleBleue; //Clé Bleue
    public GameObject porteBleue; //Porte bleue
    public GameObject LumierePersonnage; //Lumiere ambiante du personnage
    public GameObject LumiereAmbiante; //Lumiere ambiante du niveau
    public GameObject flecheDash;

    public GameObject cameraGenerale;
    public GameObject cameraRapprochee;

    public Sprite BonhommeMort; //Sprite mort
    public Sprite BonhommeMortVert; //Sprite bonhomme vert mort
    public Sprite BonhommeVert; //Sprite bonhomme vert

    private bool graviteSwitched; //Détermine si la gravité est switched (gauche a droite) ou non (haut vers la bas)

    private bool graviteAxeX; //Détermine sur quel axe le personnage se deplace
    private bool graviteAxeY;
    public static bool mort; //Détermine si le personnage est mort ou pas
    public static bool gagner;
    private bool dash;

    public AudioClip tremplinSon;
    public AudioClip ouverturePorteSon;
    public AudioClip mortSon;
    public AudioClip changementGraviteSon;
    public AudioClip etoileRamasseSon;


    public static int nbreEtoileRecupere = 0;

    void Start()
    {
        //valeurs par defaut
        graviteSwitched = false;
        graviteAxeX = false;
        graviteAxeY = true;

        dash = false;
    }

    void Update()
    {
        //Si le personnage n'est pas mort (n'a pas touché de piques) les  actions d'update peuvent se produire
        if (!mort)
        {
            if (!graviteSwitched) //Si la gravité n'est pas switched, le changement de gravité suit ces règles : 
            {
                if (Input.GetKeyDown(KeyCode.Space) && boule.GetComponent<Rigidbody2D>().gravityScale >= 1) //Si la boule se déplace vers le bas, en appuyant sur espace on :
                {
                    boule.GetComponent<Rigidbody2D>().gravityScale = -10; //Change la gravité 
                    GetComponent<Animator>().SetBool("AxeY", true); //On active que le personnage se déplace sur l'Axe des Y
                    changementDirection(true, false);
                }
                else if (Input.GetKeyDown(KeyCode.Space) && boule.GetComponent<Rigidbody2D>().gravityScale <= -1)  //Si la boule se déplace vers le haut, en appuyant sur espace on :
                {
                    changementDirection(false, true);
                    boule.GetComponent<Rigidbody2D>().gravityScale = 10; // Change la gravité
                }
            }
            else //Sinon (le personnage va de gauche à droite), le changement de gravité suit ces règles : 
            {
                if (Input.GetKeyDown(KeyCode.Space) && boule.GetComponent<Rigidbody2D>().gravityScale <= -1) //Si le personnage se déplace vers la gauche
                {
                    boule.GetComponent<Rigidbody2D>().gravityScale = 2; //On change la gravité (la valeur est moins grande que sur l'Axe des Y)
                    changementDirection(true, false);

                }
                else if (Input.GetKeyDown(KeyCode.Space) && boule.GetComponent<Rigidbody2D>().gravityScale >= 1) //Si le personnage se déplace vers la droite
                {
                    changementDirection(false, true);
                    boule.GetComponent<Rigidbody2D>().gravityScale = -2;
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (!dash)
                {
                    flecheDash.gameObject.SetActive(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 posSouris = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector3 direction = posSouris - transform.position;
                        direction.z = 0;
                        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y);
                        flecheDash.gameObject.SetActive(false);
                        dash = true;
                    }
                }
            } else if (Input.GetKeyUp(KeyCode.D))
            {
                flecheDash.gameObject.SetActive(false);
            }
        }
         
    }
    private void FixedUpdate()
    {
        if(graviteAxeY) //Si on veut que la gravité agisse normalement
        {
            Physics2D.gravity = new Vector2(0, -9.8f); //Le vecteur de gravité va vers la bas.
        } else if (graviteAxeX) //Sinon
        {
            Physics2D.gravity = new Vector2(9.8f, 0); //Le vecteur de gravité va vers la droite.
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionBouncer("bouncerGauche", -40f, 0f);
        collisionBouncer("bouncerDroite", 40f, 0f);
        collisionBouncer("bouncerHaut", 0f, 50f);
        if (collision.gameObject.name == "Piques") //Si le personnage touche un pique
        {
            LumierePersonnage.GetComponent<Light2D>().color = Color.red;
            mort = true; //il meurt
            GetComponent<AudioSource>().PlayOneShot(mortSon);
            if (graviteSwitched) //Si le bonhomme est vert
            {
                GetComponent<SpriteRenderer>().sprite = BonhommeMortVert; //On met son sprite mort vert
            } else if (!graviteSwitched) //Si le bonhomme est rose
            {
                GetComponent<SpriteRenderer>().sprite = BonhommeMort; //On met son sprtie mort rose
            }
        }
        if (collision.gameObject.name == "PorteSortie")
        {
            gagner = true;
        }

        void collisionBouncer(string bouncerTag, float directionForceX, float directionForceY)
        {
            if (collision.gameObject.name == bouncerTag) //Si le personnage rentre en contact avec un tremplin qui est dirigé vers la droite
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(directionForceX, directionForceY); //Le personnage est propulsé vers la droite
                GetComponent<AudioSource>().PlayOneShot(tremplinSon);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == cleBleue) //Si le personnage ramasse la clé bleue
        {
            porteBleue.GetComponent<Animator>().SetTrigger("cleBleuePrise"); //La porte bleue s'ouvre
            GetComponent<AudioSource>().PlayOneShot(ouverturePorteSon); //On fait jouer le son d'ouverture
            Destroy(collision.gameObject); //La clé disparait
            LumiereAmbiante.GetComponent<Light2D>().intensity = 0f; //La lumière ambiante est éteinte
            //On chande de caméra
            cameraGenerale.SetActive(false); 
            cameraGenerale.GetComponent<AudioListener>().enabled = false;
            cameraRapprochee.SetActive(true);
            cameraRapprochee.GetComponent<AudioListener>().enabled = true;

        }
        if (collision.gameObject.name == "CleMauve") //Si le personnage ramasse la clé mauve
        {
            Destroy(collision.gameObject); // La clé mauve disparait
            Destroy(GameObject.Find("CadenasMauve")); //Le cadenas mauve disparait
            GetComponent<AudioSource>().PlayOneShot(ouverturePorteSon);
        }

        if (collision.gameObject.name == "Etoile") //Si le personnage prend une étoile
        {
            if (collision.gameObject.transform.childCount < 2) //Il peut seulement la prendre si l'étoile n'a pas de child object (si elle n'a pas de cadenas)
            {
                Destroy(collision.gameObject); //Si toutes les conditions sont remplis, l'étoile disparait
                nbreEtoileRecupere++;
                GetComponent<AudioSource>().PlayOneShot(etoileRamasseSon);
            }
        }
        if (collision.gameObject.name == "Switcher") //Si le personnage rentre dans la zone de switch
        {
            graviteSwitched = true; //La gravité est switched
            graviteAxeY = false; //Le vecteur de gravité de l'axe Y n'est plus vrai
            graviteAxeX = true; // On utilise le vecteur de gravité horizontal
            GetComponent<SpriteRenderer>().sprite = BonhommeVert; //On change le sprite de la boule pour qu'il soit vert
            LumierePersonnage.GetComponent<Light2D>().color = Color.green; //On change la couleur de la lumiere du personnage ambiante en vert.
            GetComponent<Animator>().SetBool("AxeY", false); //Le vecteur de gravité de l'axe Y n'est plus vrai
            GetComponent<Animator>().SetBool("AxeX", true); // On utilise le vecteur de gravité horizontal
            GetComponent<Animator>().SetBool("tournerHaut", false); //On désactive les animations
            GetComponent<Animator>().SetBool("retourner", false);
            GetComponent<AudioSource>().PlayOneShot(changementGraviteSon);
        }

    }

    void changementDirection(bool positionTourner, bool positionInitiale) //Fonction de changement de direction
    {
        //On décide la valeur des parameters
        GetComponent<Animator>().SetBool("tournerHaut", positionTourner);
        GetComponent<Animator>().SetBool("retourner", positionInitiale); 
        //On joue le son de changement
        GetComponent<AudioSource>().PlayOneShot(changementGraviteSon);
    }
}
