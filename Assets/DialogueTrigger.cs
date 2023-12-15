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
        
    }

    public void TriggerBeforeAttackDialogue()
    {
        if (dialogueRunner.IsDialogueRunning)
            {
                dialogueRunner.Stop();
            }
        dialogueRunner.StartDialogue("BeforeAttack");
    }
    

    public void TriggerPlayerHurtDialogue()
    {
        if (dialogueRunner.IsDialogueRunning)
            {
                dialogueRunner.Stop();
            }
        dialogueRunner.StartDialogue("PlayerHurt");
    }

    public void TriggerBossHurtDialogue()
    {
        if (dialogueRunner.IsDialogueRunning)
            {
                dialogueRunner.Stop();
            }
        dialogueRunner.StartDialogue("BossHurt");
    }
    public void TriggerPlayerDeathDialogue()
    {
        if (dialogueRunner.IsDialogueRunning)
            {
                dialogueRunner.Stop();
            }
            dialogueRunner.StartDialogue("PlayerDeath");
    }
}
