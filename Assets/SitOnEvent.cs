using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitOnEvent : MonoBehaviour
{
    [SerializeField]
    private DIRECTION reqDir;
    [SerializeField]
    private Vector2 SetPosition;
    [SerializeField]
    private GameObject player;
    private Player marji;
    // Start is called before the first frame update
    void Start()
    {
        marji = player.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Player") && marji.dir == reqDir)
        {
            marji.transform.position = SetPosition;
            marji.player_animator.SetInteger("x", 0);
            marji.SetSitting(reqDir);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           marji.IsWalking();
        }
    }

}
