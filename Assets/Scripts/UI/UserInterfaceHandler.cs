using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class UserInterfaceHandler : MonoBehaviour
{
    [SerializeField] private GameObject inventory;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(InputManager.Instance.InventoryPanel)) 
        {
            inventory.SetActive(!inventory.activeSelf);
        }
    }
}
