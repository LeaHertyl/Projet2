using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject[] ObjectToCollect; //on cree une liste de gameObjects
    private int random; //on cree une variable de type nombre entier
    private GameObject obj; //on cree une variable de type GameObject

    private float timer; //on cree une variable de type float

    private PickUpItems pick; //on cree une variable pick qui se refere au script PickUpItems -> on peut appeler n'importe quel script du projet comme ca

    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0, ObjectToCollect.Length); //on donne à la variable random une varibale aleatoire entre 0 et la longueur de la liste ObjectToCollect
        obj = ObjectToCollect[random]; //le GameObject prend pour composant l'objet qui se trouve dans la liste ObjectToCollect au numero defini aleatoirement a la ligne precedente
        Debug.Log(obj); //ca fonctionne

        //on instance l'objet de la liste definir a la position de l'objet auquel ce script est associe
        pick = Instantiate(obj, transform.position, Quaternion.identity).GetComponent<PickUpItems>(); //on vient attribuer a la variable pick le script PickUpItems associe a l'objet qu'on vient d'instancie

        timer = 10; //on attribue la valeur 10 a la variable timer
    }

    // Update is called once per frame
    void Update()
    {
        //si pick n'est pas null -> si un objet avec le script PickUpItems a ete instancie
        if (pick != null)
        {
            //si le booleen isPicked du script PickUpItem est True
            if(pick.isPicked)
            {
                //ca signifie que l'objet isntancie a ete ramasse
                pick = null; //on redonne a pick la valeur null
            }

            if(pick.isPicked2)
            {
                pick = null;
            }

        }
        else //si aucun objet n'a ete instancie
        {
            timer -= Time.deltaTime; //on lance le timer
        }

        //si le timer arrive a 0 ou en dessous
        if(timer <= 0)
        {
            timer = 10; //on reboot le timer

            pick = Instantiate(obj, transform.position, Quaternion.identity).GetComponent<PickUpItems>(); //on instancie un nouvel objet et on associe son script a la variable Pick
            //le timer ne se lancera pas car a la prochaine frame on rentrera dans la boucle if(pick != null)
        }

    }
}
