using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;
    public AudioSource npc_audio;
    public AudioSource player_audio;
    

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        Debug.Log("Started conversation: Loaded messages: " + messages.Length);
        DisplayMessage();
        backgroundBox.LeanScale(new Vector3(1,3,0), 0.5f);
        //Debug.Log("Opening dialogue with " + messages.Length + " messages and " + actors.Length + " actors.");
    }

    void DisplayMessage() {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        AnimateTextColor();
        int actorId = messageToDisplay.actorId;
        AudioClip(actorId);
    }

    public void NextMessage() {
        activeMessage++;
        if (activeMessage < currentMessages.Length) {
            DisplayMessage();
        } else {
            Debug.Log("End of conversation.");
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            isActive = false;
            return;
        }
        DisplayMessage();
    }

    void AnimateTextColor() {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }
    void AudioClip(int actorId) {
        //audioSource.clip = currentMessages[activeMessage].audioClip;
        if(actorId == 0) {
            //npc_audio.clip = currentMessages[activeMessage].audioClip;
            npc_audio.Play();
        } else {
            //player_audio.clip = currentMessages[activeMessage].audioClip;
            player_audio.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isActive == true) {
            NextMessage();
        }
    }
}
