using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Branch", menuName="CustomObject/Dialogue/Branch")]
public class DialogueBranch : ScriptableObject
{
    public string dialogueID = "";
    public List<string> DialogueLines;
    public List<ResponseBranch> ResponseOption;
}

// Serialized means that its variable can appear in the inspector
[System.Serializable]
public class ResponseBranch
{
    public string text;
    public DialogueBranch nextBranch; 
}