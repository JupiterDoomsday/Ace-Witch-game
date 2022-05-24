using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_STATE
{
    WALK,
    ALERT,
    ALARM,
    SEE,
    STALK,
    ATTACK
};
public class Enemy : MonoBehaviour
{
    public E_STATE state;
    public Rigidbody2D rb2d;
    public float speed;
    public float lineOfSightDepth;
    public float lineOfSightWidth;
}
