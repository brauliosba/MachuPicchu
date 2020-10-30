using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class Texts
{
    public List<string> titles;
    public List<string> phrases;
}

public class PanelManager : MonoBehaviour
{
    public GameObject warningPanel;

    public GameObject textPanel;
    public Text textTitle;
    public Text textDescription;

    public GameObject imagePanel;
    public Text imageTitle;
    public Text imageDescription;
    public Image image;
    public List<Sprite> sprites = new List<Sprite>();

    public GameObject questionPanel;
    public Button afirmacion;
    public Button negacion;
   
    private AudioSource audioSource;

    private bool previousResponse;
    private string path;
    private string jsonString;
    private Texts texts;
    private Texts imagesDescriptions;
    private int interactionNumber;

    public void Start()
    {
        {
            path = Path.Combine(Application.streamingAssetsPath, "Text.json");
            jsonString = File.ReadAllText(path);
            texts = JsonUtility.FromJson<Texts>(jsonString);

            path = Path.Combine(Application.streamingAssetsPath, "Images.json");
            jsonString = File.ReadAllText(path);
            imagesDescriptions = JsonUtility.FromJson<Texts>(jsonString);
        }
    }

    public void panelControl(string type, string number, Vector3 position)
    {
        if (number != "-") { interactionNumber = int.Parse(number) - 1; } 
        switch (type)
        {
            case "sound":
                audioSource = GetComponent<AudioSource>();
                audioSource.Play();
                break;
            case "text":
                textPanel.transform.position = position + new Vector3(0, 1.8f, 8.0f);
                textTitle.text = texts.titles[interactionNumber];
                textDescription.text = texts.phrases[interactionNumber];
                textPanel.SetActive(true);
                break;
            case "image":
                image.sprite = sprites[interactionNumber];
                imagePanel.transform.position = position + new Vector3(0, 1.8f, 8.0f);
                imageTitle.text = imagesDescriptions.titles[interactionNumber];
                imageDescription.text = imagesDescriptions.phrases[interactionNumber];
                imagePanel.SetActive(true);
                break;
            case "question":
                questionPanel.transform.position = position + new Vector3(0, 1.8f, 8.0f);
                questionPanel.SetActive(true);
                break;
            case "response":
                if (previousResponse)
                {
                    textDescription.text = "Nos alegramos que así sea.";
                }
                else
                {
                    textDescription.text = "Por favor comantanos como podemos mejorar.";
                }
                textPanel.transform.position = position + new Vector3(0, 1.8f, 15.0f);
                textPanel.SetActive(true);
                break;
            case "warning":
                warningPanel.transform.position = position + new Vector3(0, 1.5f, 3.0f);
                warningPanel.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void HandleAnswer(bool response)
    {
        previousResponse = response;
        questionPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
