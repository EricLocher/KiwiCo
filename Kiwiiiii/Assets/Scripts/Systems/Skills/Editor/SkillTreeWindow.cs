using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillTreeWindow : EditorWindow
{
    StyleSheet styleSheet;
    SkillTree skillTree;
    Vector2 scrollInspectors, scrollTree;

    public void Init(SkillTree skillTree, StyleSheet styleSheet)
    {
        this.skillTree = skillTree;
        this.styleSheet = styleSheet;
    }

    public void CreateWindow()
    {
        #region Create Window
        var window = GetWindow<SkillTreeWindow>(utility: false, title: "Skill Tree", focus: true);
        window.minSize = new Vector2(1000f, 500f);
        window.maxSize = new Vector2(1000f, 500f);

        rootVisualElement.styleSheets.Add(styleSheet);
        #endregion

        Draw();
    }

    void Draw()
    {
        rootVisualElement.Clear();

        VisualElement root = rootVisualElement;
        LeftField(root);
        RightField(root);
    }

    void LeftField(VisualElement root)
    {
        ScrollView leftField = new ScrollView();
        leftField.AddToClassList("leftField");
        root.Add(leftField);

        
        foreach (Skill skill in skillTree.skills) {

            VisualElement skillElement = AddElement("skill", leftField);
            TextElement text = new TextElement();
            text.text = skill.skillName;
            text.AddToClassList("text");
            skillElement.Add(text);

            PopupField<Skill> DropDownMenu = new PopupField<Skill>();

            DropDownMenu.RegisterCallback<ChangeEvent<Skill>>(evt => {
                skill.SetPreviousSkill(DropDownMenu.value);
                Draw();
            });

            DropDownMenu.choices.Add((Skill)CreateInstance(typeof(NoSkill)));

            foreach (Skill _skill in skillTree.skills) {
                if (skill == _skill) { continue; }
                DropDownMenu.choices.Add(_skill);
            }

            DropDownMenu.value = skill.previousSkill;
            skillElement.Add(DropDownMenu);
        }
    }

    void RightField(VisualElement root)
    {
        ScrollView rightField = new ScrollView();
        rightField.AddToClassList("rightField");
        root.Add(rightField);

        foreach (Skill skill in skillTree.skills) {
            VisualElement _skill = AddElement("skill", rightField);

            Label skillName = new Label(skill.skillName);
            skillName.AddToClassList("text");
            _skill.Add(skillName);
        }

    }

    VisualElement AddElement(string className, VisualElement parent)
    {
        VisualElement element = new VisualElement();
        element.AddToClassList(className);
        parent.Add(element);

        return element;
    }

}


