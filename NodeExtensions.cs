using Godot;
using System;

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
}