using System;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode : ScriptableObject, IComparable<TreeNode>
{
    public List<TreeNode> children;
    [HideInInspector] public List<TreeNode> parents;

    public float mod { get { return (children.Count - 1) / 2f; } }
    public int index = 0;
    public float x = 0;

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

    public float calculateX(float x)
    {
        if(children.Count == 0) { this.x = x; return x; }

        float _x = 0;
        float lastX = x - (children.Count/2f);

        for (int i = 0; i < children.Count; i++) {
            lastX = children[i].calculateX(lastX + 1);

            if(i == 0 || i == children.Count-1)
            _x += lastX;
        }

        this.x = (_x/2f) + x;
        return this.x;
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
        return index.CompareTo(obj.index);
    }

    #endregion
}


