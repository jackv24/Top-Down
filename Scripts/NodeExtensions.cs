using Godot;
using System;
using System.Collections.Generic;

public static class NodeExtensions
{
    public static T FindParentOfType<T>(this Node node, bool recursive = false)
        where T : class
    {
        Node parent = node.GetParent();

        if(parent != null)
        {
            T t = parent as T;
            if(t != null)
                return t;
            
            if(recursive)
                return parent.FindParentOfType<T>(true);
        }

        return null;
    }

    public static List<T> FindChildrenOfType<T>(this Node node, bool recursive = false)
        where T : class
    {
        List<T> children = new List<T>();

        int childCount = node.GetChildCount();
        for (int i = 0; i < childCount; i++)
        {
            T child = node.GetChild(i) as T;
            if(child != null)
                children.Add(child);

            if(recursive)
                children.AddRange(node.GetChild(i).FindChildrenOfType<T>(true));
        }

        return children;
    }

    public static T FindChildOfType<T>(this Node node, bool recursive = false)
        where T : class
    {
        int childCount = node.GetChildCount();
        for (int i = 0; i < childCount; i++)
        {
            T child = node.GetChild(i) as T;

            if(child == null && recursive)
                child = node.GetChild(i).FindChildOfType<T>(true);
            
            if(child != null)
                return child;
        }

        return null;
    }
}