using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillTree
{
    public List<Skill> startNodes;
    public List<Skill> nodes;

    [HideInInspector] public int depth = 0;

    //https://rachel53461.wordpress.com/2014/04/20/algorithm-for-drawing-trees/

    public void CalculateTree()
    {
        float x = 0f;

        foreach (TreeNode node in nodes) {
            node.x = 0;
            node.mod = 0;
            if(node.children.Count != 0)
            node.mod = (node.children.Count - 1) / 2f;
        }

        foreach (TreeNode node in startNodes) {
            int _index = node.calculateNodeIndex(0);
            if (_index >= depth) { depth = _index; }
            x += node.calculateX(x);
            x++;
        }
        nodes.Sort();

    }

}
