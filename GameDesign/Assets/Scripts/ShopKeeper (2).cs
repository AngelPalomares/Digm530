using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{

    private bool CanOpen;

    public string[] itemsforsale = new string[40]; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CanOpen && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove && !Shop.Instance.ShopMenu.activeInHierarchy)
        {
            Shop.Instance.itemsforsale = itemsforsale;
            Shop.Instance.OpenShop();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CanOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CanOpen=false;
        }
    }
}
