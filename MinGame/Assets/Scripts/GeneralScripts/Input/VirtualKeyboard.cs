// fuck file headers copy my code idc
// this class deals with keyboard inputs btw

using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyboard : MonoBehaviour
{
    public Text TextObject;
    public string frontText;

    private TouchScreenKeyboard keyboard;
    private string text;
    bool active = false;

    // Use this for initialization
    void Start()
    {
        if (keyboard == null)
        {
            keyboard = TouchScreenKeyboard.Open(null);
        }
    }

    // check for when a key is pressed
    public string UpdateKeyboard()
    {
        string sentText = null;

        if (!active)
        {
            if (keyboard != null)
            {
                keyboard.active = true;
            }
            active = true;
            TextObject.enabled = true;
        }

        // for each character inserted this frame
        foreach (char c in Input.inputString)
        {
            if (c == '\b' && text.Length > 0)
            {
                // backspace: remove last char
                text = text.Substring(0, text.Length - 1);
            }
            else if (c == '\n' || c == '\r')
            {
                sentText = text;
            }
            else
            {
                // add any other char to the string
                if (TextObject.text.Length < 10 && c != '\b')
                {
                    text += c;
                }
            }
        } 

        // add any front text that is needed
        if(frontText != "")
        {
            TextObject.text = frontText + text;    
        }

        // set the text to the final value
        TextObject.text = text;

        return sentText;
    }

    // blank out the text
    public void BlankText()
    {
        text = "";
        TextObject.text = text;
    }
}
