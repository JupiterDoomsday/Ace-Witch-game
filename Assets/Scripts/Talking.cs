using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class Talking : GameState
{
    public DialogueRunner dialogueRunner;
    public Image left;
    public Image right;
    public Player player;
    public AudioSource audioSource;
    //[SerializeField]
    public List<Npc> actors;
    public void Awake()
    {
        actors = new List<Npc>();
    }

    private Npc findActor(string str)
    {
        foreach (Npc key in actors)
        {
            if (str.Equals(key.Name))
                return key;
        }
        return null;
    }
    //this is a custome action in unity that create the talking settings for the
    [YarnCommand("profile")]
    public void ShowProfile(string profile)
    {
        Sprite witch = findActor(profile).Profile;
        if (witch == null)
        {
            Debug.Log("ERROR actor doesn't exsist");
            return;
        }

        //if (pos.Equals("left"))
        left.sprite = witch;
        left.SetNativeSize();
        right.sprite = player.profile;
        right.SetNativeSize();
    }
    [YarnCommand("sound")]
    public void PlayAudio(string str)
    {
        Npc npc = findActor(str);
        if (npc == null)
            return;
        if (npc.audio == null)
            return;

        audioSource.PlayOneShot(npc.audio, .9f);
    }
    /*public void ShowProfile(string person, string personNew)
    {
        Sprite witch = findActor(person);
        Sprite witchnd = findActor(personNew);
    }
    */


    // Start is called before the first frame update
    public void handleInput(Player player)
    {
        Debug.Log("Is talking");
    }
    public void UpdateState(Player player)
    {
        Debug.Log("still talking");
    }

}
