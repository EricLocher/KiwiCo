using System;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode : ScriptableObject, IComparable<TreeNode>
{
    public List<TreeNode> children;
    [HideInInspector] public List<TreeNode> parents;

    public float mod = 0;
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

        float _x = x;
        float lastChildMod = 0;

        for (int i = 0; i < children.Count; i++) {
            float lastX = children[i].calculateX(_x + i + lastChildMod);
            lastChildMod = children[i].mod;
            mod += lastChildMod;

            if (i == 0 || i == children.Count - 1) {
                _x += lastX;
            }
        }

        this.x = (_x/2f);
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


