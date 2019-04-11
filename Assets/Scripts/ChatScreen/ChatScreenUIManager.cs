using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatScreenUIManager : MonoBehaviour
{
	private string nick;

	public GameObject messageDisplayPanel;
	public GameObject messageNode;
	public InputField messageField;
	public InputField nickField;
	public PopUpPanel popUpPanel;

	// Constant definitions
	const string LANGUAGE_LABEL_INVALID_CHAT_NICK = "invalid-chat-nick";
	const string LANGUAGE_LABEL_CHAT_NICK_UPDATED = "chat-nick-updated";



	// Start is called before the first frame update
	void Start()
    {
		nick = string.Empty;
		DatabaseManager.instance.SetChatMessageReceiveHandler(AddNewMessage);
        // TODO
		// Get older messages and construct the message display
    }

    
	/* Button action method to send the new message to database */
	public void SendButtonAction()
	{
		// If nick is invalid show error dialog
		if (nick.Equals(string.Empty))
		{
			popUpPanel.Deactivate();
			popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_INVALID_CHAT_NICK)).SetBackgroundColor(Color.red).Activate();
		}

		// Else save message to database and clear input field
		else
		{
			//AddNewMessage();
			DatabaseManager.instance.SendChatMessage(new ChatMessage(nick, messageField.text));
			messageField.text = string.Empty;
		}
	}


	/* Button action method to change nick */
	public void ChangeButtonAction()
	{
		nick = nickField.text;
		popUpPanel.Deactivate();
		popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_CHAT_NICK_UPDATED)).SetBackgroundColor(Color.green).Activate();
	}


	/* Delegate method to be activated upon state change on messages section of database */
	private void AddNewMessage(ChatMessage newMessage)
	{
		// Create new message node and get components
		GameObject newNode = Instantiate(messageNode);
		Text sender = (Text)newNode.transform.Find("Sender").GetComponent("Text");
		Text messageContainer = (Text)newNode.transform.Find("Message").GetComponent("Text");

		// Set component values and re-parent node to display panel
		sender.text = newMessage.nick;
		messageContainer.text = newMessage.message;
		newNode.transform.parent = messageDisplayPanel.transform;

		// Resize display panel (if necessary) and display new message
		ResizeMessageDisplay();
		newNode.SetActive(true);
	}


	/* Helper method to resize the  */
	private void ResizeMessageDisplay()
	{
		// Get components to make calculations
		RectTransform parentTrans = messageDisplayPanel.transform.parent.GetComponent<RectTransform>();
		RectTransform rectTrans = messageDisplayPanel.GetComponent<RectTransform>();
		VerticalLayoutGroup vlg = messageDisplayPanel.GetComponent<VerticalLayoutGroup>();

		// Calculate total size of display panel
		int childCount = messageDisplayPanel.transform.childCount;
		float totalHeight = childCount * messageNode.GetComponent<RectTransform>().rect.height + vlg.spacing * (childCount-1);
		
		// Set size and location if size is bigger than parent size
		if (parentTrans.rect.height < totalHeight)
		{
			rectTrans.sizeDelta = new Vector2(rectTrans.sizeDelta.x, totalHeight);
			rectTrans.position = new Vector3(rectTrans.position.x, rectTrans.position.y, 0);
		}
	}
}
