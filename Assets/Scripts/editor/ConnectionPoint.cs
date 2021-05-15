using System;
using UnityEngine;

public enum ConnectionPointType { In, Out }

public class ConnectionPoint
{
    public Rect rect;
    public ConnectionPointType type;
    public CutNode node;
    public GUIStyle style;
    public Action<ConnectionPoint> OnClickConectionPoint;
    // Start is called before the first frame update
    public ConnectionPoint(CutNode node, ConnectionPointType type, GUIStyle s, Action<ConnectionPoint> OnClickConectionPoint)
    {
        this.node = node;
        this.type = type;
        this.style = s;
        this.OnClickConectionPoint = OnClickConectionPoint;
        rect = new Rect(0, 0, 10f, 20f);
    }
    public void Draw()
    {
        rect.y = node.rect.y + (node.rect.height * .5f) - rect.height * 0.5f;
        switch (type)
        {
            case ConnectionPointType.In:
                rect.x = node.rect.x - rect.width + 8f;
                break;
            case ConnectionPointType.Out:
                rect.x = node.rect.x + node.rect.width - 8f;
                break;
        }
        //set up the connection point to attach the nodes GUI
        if (GUI.Button(rect, "", style))
        {
            if(OnClickConectionPoint != null)
            {
                OnClickConectionPoint(this);
            }
        }
    }
}
