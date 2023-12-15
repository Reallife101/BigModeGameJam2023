using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    public void TriggerOpeningDialogue()
    {
        if (dialogueRunner.IsDialogueRunning)
            {
                dialogueRunner.Stop();
            }
        dialogueRunner.StartDialogue("Opening");
        StartCoroutine(dialogueStopTime());
        
    }

    public void TriggerDialogue1()
    {
        if (dialogueRunner.IsDialogueRunning)
            {
                dialogueRunner.Stop();
            }
        dialogueRunner.StartDialogue("Dialogue1");
    }
    

    public void TriggerDialogue2()
    {
        if (dialogueRunner.IsDialogueRunning)
            {
                dialogueRunner.Stop();
            }
        dialogueRunner.StartDialogue("Dialogue2");
    }

    public void TriggerDialogue3()
    {
        if (dialogueRunner.IsDialogueRunning)
            {
                dialogueRunner.Stop();
            }
        dialogueRunner.StartDialogue("Dialogue3");
    }
    public void TriggerDialogue4()
    {
        if (dialogueRunner.IsDialogueRunning)
            {
                dialogueRunner.Stop();
            }
        dialogueRunner.StartDialogue("Dialogue4");
    }
    public void TriggerPlayerDeathDialogue()
    {
        if (dialogueRunner.IsDialogueRunning)
            {
                dialogueRunner.Stop();
            }
            dialogueRunner.StartDialogue("PlayerDeath");
    }

    IEnumerator dialogueStopTime()
    {
        yield return new WaitForSeconds(2f);
        dialogueRunner.Stop();
    }
}
