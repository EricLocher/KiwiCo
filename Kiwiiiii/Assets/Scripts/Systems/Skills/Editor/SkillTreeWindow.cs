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
        window.minSize = new Vector2(300f, 500f);

        rootVisualElement.styleSheets.Add(styleSheet);
        #endregion

        Draw();
    }

    void Draw()
    {
        rootVisualElement.Clear();
        VisualElement root = rootVisualElement;

        Field(root);
    }

    void Field(VisualElement root)
    {
        ScrollView field = new ScrollView();
        field.AddToClassList("field");
        root.Add(field);

        foreach (Skill skill in skillTree.skills) {

            VisualElement skillElement = AddElement("skill", field);
            TextElement text = new TextElement();
            text.text = skill.name;
            text.AddToClassList("text");
            skillElement.Add(text);

            VisualElement flexBox = AddElement("flexBox", skillElement);


            TextElement header = new TextElement();
            header.text = "Set Previous Skill";
            header.AddToClassList("header");
            flexBox.Add(header);


            PopupField<Skill> DropDownMenu = new PopupField<Skill>();

            DropDownMenu.RegisterCallback<ChangeEvent<Skill>>(evt => {
                skill.SetPreviousSkill(DropDownMenu.value);
                Draw();
            });

            DropDownMenu.choices.Add((NoSkill)CreateInstance(typeof(NoSkill)));

            foreach (Skill _skill in skillTree.skills) {
                if (skill == _skill) { continue; }
                DropDownMenu.choices.Add(_skill);
            }

            DropDownMenu.value = skill.previousSkill;
            DropDownMenu.AddToClassList("menu");
            flexBox.Add(DropDownMenu);
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


