using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public DialogueTrigger dialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogue.TriggerOpeningDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
