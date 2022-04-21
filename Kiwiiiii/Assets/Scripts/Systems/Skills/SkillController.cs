using UnityEngine;
using UnityEngine.UIElements;

public class SkillController : MonoBehaviour
{
    public StyleSheet windowStyleSheet;
    public SkillTree skillTree;

    void Start()
    {
        skillTree.CalculateTree();
    }
}
