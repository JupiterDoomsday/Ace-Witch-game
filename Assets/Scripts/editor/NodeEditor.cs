using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class NodeEditor : EditorWindow
{
    private List<CutNode> nodes;
    private List<Connection> connections;

    private Vector2 drag;
    private Vector2 offset;
    private GUIStyle nodeStyle;
    private GUIStyle inStyle;
    private GUIStyle outStyle;
    private GUIStyle selectedStyle;

    private ConnectionPoint selectedInPoint;
    private ConnectionPoint selectedOutPoint;

    [MenuItem("Window/Node Based Editor")]
    private static void OpenWindow() {
        NodeEditor window = GetWindow<NodeEditor>();
        window.titleContent = new GUIContent("Node Based Editor");
    }

    private void OnGUI()
    {
        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);
        DrawNodes();
        DrawConnections();
        DrawConnectionLine(Event.current);
        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);
        if (GUI.changed) Repaint();
    }
    private void DrawGrid(float gridSpace, float gridOpacity, Color color)
    {
        int widthGrid = Mathf.CeilToInt(position.width / gridSpace);
        int heightGrid = Mathf.CeilToInt(position.height / gridSpace);
        Handles.BeginGUI();
        Handles.color = new Color(color.r, color.g, color.b, gridOpacity);
        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % gridSpace, offset.y % gridSpace, 0);
        for(int i = 0; i < widthGrid; i++)
        {
            Handles.DrawLine(new Vector3(gridSpace * i, -gridSpace, 0) + newOffset,
                new Vector3(gridSpace * i, position.height, 0f) + newOffset);
        }
        for (int i = 0; i < widthGrid; i++)
        {
            Handles.DrawLine(new Vector3(-gridSpace, gridSpace * i, 0) + newOffset,
                new Vector3(position.width, gridSpace * i,  0f) + newOffset);
        }
        Handles.color = Color.white;
        Handles.EndGUI();
    }
    private void OnEnable()
    {
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        selectedStyle = new GUIStyle();
        selectedStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        selectedStyle.border = new RectOffset(12, 12, 12, 12);

        inStyle = new GUIStyle();
        inStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inStyle.border = new RectOffset(4, 4, 12, 12);

        outStyle = new GUIStyle();
        outStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outStyle.border = new RectOffset(4, 4, 12, 12);
    }

    // draw all the nodes in the editor
    private void DrawNodes()
    {
        if (nodes != null)
        {
            foreach (CutNode c in nodes)
            {
                c.Draw();
            }
        }
    }
    private void DrawConnections()
    {
        if(connections != null){
            foreach( Connection con in connections)
            {
                con.Draw();
            }
        }

    }
    private void ProcessEvents(Event e)
    {
        drag = Vector2.zero;
        switch (e.type)
        {
            case EventType.MouseDown:
                if(e.button == 1)
                {
                    ProcessContextMenu(e.mousePosition);
                }
            break;
            case EventType.MouseDrag:
                if(e.button == 0)
                {
                    OnDrag(e.delta);
                }
                break;
        }

    }
    private void ProcessContextMenu(Vector2 mousePos)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add node"), false, () => OnClickAddNode(mousePos));
        genericMenu.ShowAsContext();
    }
    private void OnClickAddNode(Vector2 mousePos)
    {
        if(nodes == null)
        {
            nodes = new List<CutNode>();
        }
        nodes.Add(new CutNode(mousePos, 200, 50, nodeStyle, selectedStyle, inStyle, outStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode));
    }

    private void OnClickRemoveNode(CutNode node)
    {
        if(connections != null)
        {
            List<Connection> conToRemove = new List<Connection>();
            foreach(Connection con in connections)
            {
                if (con.inPoint == node.inPoint || con.outPoint == node.outPoint)
                    conToRemove.Add(con);
            }
            foreach (Connection con in conToRemove)
                connections.Remove(con);

            conToRemove = null;
        }
        nodes.Remove(node);
    }

    private void OnClickInPoint(ConnectionPoint inPoint)
    {
        selectedInPoint = inPoint;
        if(selectedOutPoint != null)
        {
            if(selectedOutPoint.node != selectedInPoint.node)
                CreateConection();
            ClearConnectionSelection();
        }
    }
    private void OnClickOutPoint(ConnectionPoint outPoint)
    {
        selectedInPoint = outPoint;
        if (selectedInPoint != null)
        {
            if (selectedOutPoint.node != selectedInPoint.node)
                CreateConection();
            ClearConnectionSelection();
        }
    }

    private void OnDrag(Vector2 pos)
    {
        drag = pos;
        if(nodes != null)
        {
            foreach (CutNode node in nodes)
            {
                node.Drag(pos);
            }
        }
        GUI.changed = true;
    }
    private void CreateConection()
    {
        if(connections == null)
        {
            connections = new List<Connection>();
        }
        connections.Add(new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
    }

    private void OnClickRemoveConnection(Connection con)
    {
        connections.Remove(con);
    }

    private void ClearConnectionSelection()
    {
        selectedInPoint = null;
        selectedOutPoint = null;
    }

    private void ProcessNodeEvents(Event e)
    {
        if(nodes != null)
        {
            foreach(CutNode c in nodes)
            {
                bool guiChanged = c.ProcessEvents(e);
                if (guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }
    }
    private void DrawConnectionLine(Event e)
    {
        if (selectedInPoint != null && selectedOutPoint == null)
        {
            Handles.DrawBezier(
                selectedInPoint.rect.center,
                e.mousePosition,
                selectedInPoint.rect.center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }

        if (selectedOutPoint != null && selectedInPoint == null)
        {
            Handles.DrawBezier(
                selectedOutPoint.rect.center,
                e.mousePosition,
                selectedOutPoint.rect.center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }
    }

}
