using UnityEngine;


public enum LevelType
{
    MagicEraserType,
    CutBackgroundType,
    CutClothsType,
    CutHairsType
}

public class LevelsControllerBase : MonoBehaviour
{
    [SerializeField] private bool hasHeadInstruction, isTextInstruction, isImageInstruction,showStepByStepInstructions;
    
    [Space(40)]
    [Multiline]
    [SerializeField] private string messageForSelectTool,messageForEraserTool,messageForCutTool,messageForBackgroundTool,messageForClothsTool,messageForHairsTool;

    [Space(40)] [Multiline] [SerializeField] private string textHeadInstruction;
    [Space(40)] [Multiline] [SerializeField] private string imageHeadInstruction;

    [Space(40)]
    [SerializeField] private LevelType levelType;
    
    public bool HasHeadInstruction => hasHeadInstruction;

    public bool IsTextInstruction => isTextInstruction;

    public bool IsImageInstruction => isImageInstruction;

    public bool ShowStepByStepInstructions => showStepByStepInstructions;

    public string MessageForSelectTool => messageForSelectTool;

    public string MessageForEraserTool => messageForEraserTool;

    public string MessageForCutTool => messageForCutTool;

    public string MessageForBackgroundTool => messageForBackgroundTool;

    public string MessageForClothsTool => messageForClothsTool;

    public string MessageForHairsTool => messageForHairsTool;

    public string TextHeadInstruction => textHeadInstruction;

    public string ImageHeadInstruction => imageHeadInstruction;
}
