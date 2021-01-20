using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeSorting : MonoBehaviour
{
    public TilemapRenderer layer;
    private string prevLayer;
    public bool isBehindPlayer = true;
    public SpriteRenderer[] props; //number of sprite in the scene
    private int[] prevSprites;
    int len;
    // called when the player touched this tile
    private void Start()
    {
        prevLayer = layer.sortingLayerName;
        len = props.Length;
        prevSprites = new int[len];
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            string layerDest = "_bkg";
            if (isBehindPlayer){
                layer.sortingOrder = 1;
            }
            else
            {
                layerDest = "foreground";
                layer.sortingOrder = 0;
            }
            layer.sortingLayerID = SortingLayer.NameToID(layerDest);
            for (int i = 0; i < len; i++)
            {
                prevSprites[i] = props[i].sortingOrder;
                props[i].sortingLayerID = SortingLayer.NameToID(layerDest);
            }
        }


    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            layer.sortingLayerID = SortingLayer.NameToID(prevLayer);
            for (int i = 0; i < len; i++)
            {
                props[i].sortingOrder= prevSprites[i];
                props[i].sortingLayerID = SortingLayer.NameToID(prevLayer);
            }
        }
    }
}
