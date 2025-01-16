using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class SelectedItem : MonoBehaviour
{
    Image frame;
    [SerializeField]
    Image sprite;
    [SerializeField]
    private ItemDataBase data;
    [SerializeField]
    private Player player;
    private int itemSelected = -1;
    private InMemoryVariableStorage varStorage;
    // Start is called before the first frame update
    void Start()
    {
        frame = GetComponent<Image>();
        frame.enabled = false;
        sprite.enabled = false;
        varStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.act != ACT.MENU && itemSelected != -1)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                player.UnselectItem();
            }
        }

        int curItem = player.GetSelectedItem();
        if ( curItem != itemSelected)
        {
            itemSelected = curItem;
            if( curItem != -1)
            {
                varStorage.SetValue("$selectItem", curItem);
                sprite.sprite = data.GetIcon(curItem);
                frame.enabled = true;
                sprite.enabled = true;
            }
            else
            {
                frame.enabled = false;
                sprite.enabled = false;
                varStorage.SetValue("$selectItem", -1);
            }
        }
    }
}
