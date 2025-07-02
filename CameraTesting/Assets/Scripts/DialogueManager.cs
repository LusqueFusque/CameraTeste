using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    public string character;
    public string sprite;
    public string text;
}

[System.Serializable]
public class DialogueData
{
    public List<DialogueLine> dialogue;
}

public class DialogueManager : MonoBehaviour
{
    public Image characterImage;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI dialogueText;
    public Sprite defaultSprite;

    private DialogueData dialogueData;
    private int currentIndex = 0;

    void Start()
    {
        LoadDialogue();
        ShowLine();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextLine();
        }
    }

    void LoadDialogue()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "dialogue.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            dialogueData = JsonUtility.FromJson<DialogueData>(json);
        }
        else
        {
            Debug.LogError("Arquivo JSON não encontrado em: " + path);
        }
    }

    void ShowLine()
    {
        if (dialogueData == null || currentIndex >= dialogueData.dialogue.Count)
        {
            dialogueText.text = "Fim do diálogo.";
            return;
        }

        DialogueLine line = dialogueData.dialogue[currentIndex];
        characterNameText.text = line.character;
        dialogueText.text = line.text;

        Sprite newSprite = Resources.Load<Sprite>(line.sprite);
        characterImage.sprite = newSprite != null ? newSprite : defaultSprite;
    }

    void ShowNextLine()
    {
        currentIndex++;
        ShowLine();
    }
}
