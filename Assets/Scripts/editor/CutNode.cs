using System;
using UnityEngine;
using UnityEditor;
public enum CutsceneType
{
    WAIT,
    SET,
    MOVE,
    ANIMATE,
    TALK
}
public class CutNode
{
    public Rect rect;
    public CutsceneType type;
    public int ID;
    public string title;
    //style for the node
    public GUIStyle style;
    public GUIStyle defStyle;
    public GUIStyle selectedStyle;
    public Action<CutNode> OnRemoveNode;

    public bool isDragging;
    public bool isSelected;
    public ConnectionPoint inPoint;
    public ConnectionPoint outPoint;
    //make the node
    public CutNode(Vector2 pos, float width, float height, GUIStyle nodeStyle, GUIStyle sStyle, GUIStyle inPointStyle, GUIStyle outPointStyle,
        Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<CutNode> OnClickRemoveNode)
    {
        type = CutsceneType.WAIT;
        rect = new Rect(pos.x, pos.y, width, height);
        style = nodeStyle;
        inPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        defStyle = nodeStyle;
        selectedStyle = sStyle;
        OnRemoveNode = OnClickRemoveNode;

    }
    public void setTitle()
    {
        switch (type)
        {
            case CutsceneType.ANIMATE:
                title = "Play Animation";
            break;
            case CutsceneType.MOVE:
                title = "Move Sprite";
            break;
            case CutsceneType.TALK:
                title = "Play Dialouge";
            break;
            case CutsceneType.SET:
                title = "Set Sprite";
                break;
            case CutsceneType.WAIT:
                title = "Wait";
            break;
        }
    }
    public void Drag( Vector2 pos)
    {
        rect.position += pos;
    }
    // Update is called once per frame
    public void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, title, style);
    }
    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if(e.button == 0)
                {
                    if (rect.Contains(e.mousePosition))
                    {
                        isDragging = true;
                        isSelected = true;
                        style = selectedStyle;
                    }
                    else
                    {
                        isSelected = false;
                        style = defStyle;
                    } 
                    GUI.changed = true;
                }
                if(e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;
            case EventType.MouseUp:
                isDragging = false;
                break;
            case EventType.MouseDrag:
                if (e.button == 0 && isDragging)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;

        }
        return false;
    }
    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }
    private void OnClickRemoveNode()
    {
        OnRemoveNode?.Invoke(this);
    }
}
