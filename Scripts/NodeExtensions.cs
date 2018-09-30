using Godot;
using System;
using System.Collections.Generic;

public static class NodeExtensions
{
    public static T FindParentOfTypeRecursive<T>(this Node node)
        where T : class
    {
        Node parent = node.GetParent();

        if(parent != null)
        {
            T t = parent as T;
            if(t != null)
                return t;
            
            return parent.FindParentOfTypeRecursive<T>();
        }

        return null;
    }

    public static List<T> FindChildrenOfType<T>(this Node node)
        where T : class
    {
        List<T> children = new List<T>();

        int childCount = node.GetChildCount();
        for (int i = 0; i < childCount; i++)
        {
            T child = node.GetChild(i) as T;
            if(child != null)
                children.Add(child);
        }

        return children;
    }
}