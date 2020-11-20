using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

[System.Serializable]
public class Texts
{
    public List<string> titles;
    public List<string> phrases;
}

public class PanelManager : MonoBehaviour
{
    public GameObject player;

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

    public GameObject cursorMano;
   
    private AudioSource audioSource;

    private bool previousResponse;
    private string path;
    private string jsonString;
    private Texts texts;
    private Texts imagesDescriptions;
    private int interactionNumber;

    public void Start()
    {
        path = Path.Combine(Application.streamingAssetsPath, "Text.json");
        jsonString = File.ReadAllText(path);
        texts = JsonUtility.FromJson<Texts>(jsonString);

        path = Path.Combine(Application.streamingAssetsPath, "Images.json");
        jsonString = File.ReadAllText(path);
        imagesDescriptions = JsonUtility.FromJson<Texts>(jsonString);

        Cursor.visible = false;
        cursorMano.SetActive(false);
    }

    public void Update()
    {
        if (IsMouseOverUI())
        {
            cursorMano.transform.position = Input.mousePosition;
            cursorMano.SetActive(true);
        } else {
            Cursor.visible = false;
            cursorMano.SetActive(false);
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void panelControl(string type, string number)
    {
        if (number != "-") { interactionNumber = int.Parse(number) - 1; } 
        switch (type)
        {
            case "sound":
                audioSource = GetComponent<AudioSource>();
                audioSource.Play();
                break;
            case "text":
                textTitle.text = texts.titles[interactionNumber];
                textDescription.text = texts.phrases[interactionNumber];
                textPanel.SetActive(true);
                break;
            case "image":
                image.sprite = sprites[interactionNumber];
                imageTitle.text = imagesDescriptions.titles[interactionNumber];
                imageDescription.text = imagesDescriptions.phrases[interactionNumber];
                imagePanel.SetActive(true);
                break;
            case "question":
                player.GetComponent<PlayerController>().speed = 0;
                questionPanel.SetActive(true);
                break;
            case "release":
                player.GetComponent<PlayerController>().disabelPanels();
                break;
            /*
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
            */
            default:
                break;
        }
    }

    public void HandleAnswer(bool response)
    {
        previousResponse = response;
        questionPanel.SetActive(false);
        player.GetComponent<PlayerController>().speed = player.GetComponent<PlayerController>().originalSpeed;
        player.GetComponent<PlayerController>().started = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
