using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject building;
    public GameObject Furniture;
    public GameObject Boxes;
    public GameObject deco;
    public GameObject Crates;
    public ButtonManager buttonManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        DontDestroyOnLoad(building);

        if (buttonManager.MapChoose == true)
        {
            building.SetActive(true);
            Furniture.SetActive(true);
            Boxes.SetActive(true);
            deco.SetActive(true);
            Crates.SetActive(true);
        }
        
    }
}
