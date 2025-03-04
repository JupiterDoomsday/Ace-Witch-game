using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOpening : MonoBehaviour, IDataPersistence
{
    // Start is called before the first frame update
    public StateMachine mach;
    public Player player;
    public CutsceneManager timelineManager;
    private bool opening = true;

    public void StartCutscene()
    {
        if(opening)
        {
            player.invo.AddItem(0, 1);
            mach.PlayYarnScript("openingStart");
            //timelineManager.PlayAndWaitCutscene(2, true);
        }
        else
        {
            Destroy(this);
        }
    }
    public void LoadData(GameData data)
    {
        opening = data.opening;
    }
    public void SaveData(ref GameData data)
    {
        data.opening = false;
    }
}
