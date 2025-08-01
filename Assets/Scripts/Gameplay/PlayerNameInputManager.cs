using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerNameInputManager : MonoBehaviour
{
    public TMP_InputField playerName;
    private string enteredName;

    public string GetInput()
    {
        SetDefaultedInput();
        return enteredName;
    }

    public void SetDefaultedInput()
    {
        enteredName = playerName.text.Trim();
        if (string.IsNullOrEmpty(enteredName))
        {
            enteredName = "Anonymous";
        }
    }
}
