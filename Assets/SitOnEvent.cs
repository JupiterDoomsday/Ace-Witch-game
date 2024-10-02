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

    DIRECTION GetSitPosition()
    {
        switch(reqDir)
        {
            case DIRECTION.UP:
                return DIRECTION.DOWN;
            case DIRECTION.LEFT:
                return DIRECTION.RIGHT;
            case DIRECTION.DOWN:
                return DIRECTION.UP;
            case DIRECTION.RIGHT:
                return DIRECTION.LEFT;
        }
        return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Player") && marji.dir == reqDir)
        {
            marji.transform.position = SetPosition;
            marji.player_animator.SetInteger("x", 0);
            marji.player_animator.SetInteger("y", 0);
            marji.SetSitting(GetSitPosition());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && marji.dir == reqDir)
        {
            marji.transform.position = SetPosition;
            marji.SetSitting(GetSitPosition());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          // marji.IsWalking();
        }
    }

}
