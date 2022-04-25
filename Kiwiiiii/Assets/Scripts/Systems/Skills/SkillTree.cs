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
        foreach (TreeNode node in startNodes) {
            int _index = node.calculateNodeIndex(0);
            if(_index >= depth) { depth = _index; }
        }

        nodes.Sort();

        int index = depth;
        int x = 0;
        //Post-order traversal.
        for (int i = nodes.Count - 1; i >= 0; i--) {
            if(nodes[i].index != index) { index = nodes[i].index; x = 0; }
            nodes[i].x = x;
            x++;
        }
    }

}
