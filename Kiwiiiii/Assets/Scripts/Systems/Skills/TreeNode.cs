using System;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode : ScriptableObject, IComparable<TreeNode>
{
    public List<TreeNode> children;
    [HideInInspector] public List<TreeNode> parents;

    public int index = 0;
    public int x = 0;

    #region Calculate Tree

    public int calculateNodeIndex(int currentIndex)
    {
        index = currentIndex;
        if(children.Count == 0) { return currentIndex; }

        foreach (TreeNode childNode in children) {
            int _index = childNode.calculateNodeIndex(index + 1);
            if (_index >= currentIndex) { currentIndex = _index; }
        }

        return currentIndex;
    }

    public void AssignParent()
    {
        foreach (TreeNode childNode in children) {
            childNode.parents.Add(this);
            childNode.AssignParent();
        }
    }

    public int CompareTo(TreeNode obj)
    {
        //TreeNode other = obj as TreeNode;
        return index.CompareTo(obj.index);
    }

    #endregion
}


