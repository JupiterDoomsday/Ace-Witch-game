using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // Start is called before the first frame update
    void Start()
    {
        frame = GetComponent<Image>();
        frame.enabled = false;
        sprite.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        int curItem = player.GetSelectedItem();
        if ( curItem != itemSelected)
        {
            itemSelected = curItem;
            if( curItem != -1)
            {
                sprite.sprite = data.GetIcon(curItem);
                frame.enabled = true;
                sprite.enabled = true;
            }
            else
            {
                frame.enabled = false;
                sprite.enabled = false;
            }
        }
    }
}
