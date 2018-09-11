using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Screenshot : MonoBehaviour
{
    public string screenshotRelativeFilepath;
    public RectTransform cardTransform;
    public DisplayCard cardTemplate;
    public CardTextParser allCards;
    bool isScreenshotting = false;

    public void Start()
    {
        if(!cardTransform)
        {
            Debug.LogError("The card transform was not set, unable to find the image to screenshot!");
        }

        if (!cardTemplate)
        {
            Debug.LogError("The card template was not set, unable to change images!");
        }

        if (!allCards)
        {
            Debug.LogError("The card transform was not set, unable to get the card list!");
        }

        if(screenshotRelativeFilepath == null || screenshotRelativeFilepath == "")
        {
            Debug.LogWarning("The filepath for the output of the images wasn't set. The images will be dumped at the top of the file tree!");
        }
    }

    public void ScreenshotAllCards()
    {
        StartCoroutine(ScreenshotAll());
        Debug.Log("Screenshot all images");
    }

    IEnumerator SaveCardImage(string relativeFilepath, string fileName)
    {
        //Wait for end of screen buffer
        yield return new WaitForEndOfFrame();
        Debug.Log("Screenshotting " + relativeFilepath);

        //Create new texture of the specified size
        //Debug.Log("The width is " + cardTransform.rect.width + " and the height is " + cardTransform.rect.height);
        Texture2D tex = new Texture2D((int)cardTransform.rect.width, (int)cardTransform.rect.height, TextureFormat.RGB24, false);

        //float textX = FindObjectOfType<DisplayCard>().GetComponent<RectTransform>().offsetMin.x;

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(cardTransform.position.x, cardTransform.position.y, cardTransform.rect.width, cardTransform.rect.height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Object.Destroy(tex);
        string path = Path.Combine(Application.dataPath, relativeFilepath);

        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        catch (IOException ex)
        {
            Debug.LogError("Error message: " + ex.Message);
        }

        fileName = SanitizeFileName(fileName, @"/\:*?""<>|");

        //Write to a file in the project folder
        File.WriteAllBytes(Path.Combine(path, fileName), bytes);
    }

    IEnumerator ScreenshotAll()
    {
        isScreenshotting = true;

        for(int i = 0;i<allCards.GetNumOfCards();i++)
        {
            SpringsCard card = allCards.GetCard(i);
            cardTemplate.ActivateCardImage(card);
            StartCoroutine(SaveCardImage(screenshotRelativeFilepath, card.name + ".png"));
            yield return new WaitForEndOfFrame();
        }

        isScreenshotting = false;
    }

    string SanitizeFileName(string fileName, string badChars)
    {
        string returnString = "";
        bool appendChar;

        for (int i = 0; i < fileName.Length; i++)
        {
            appendChar = true;

            for (int j = 0; j < badChars.Length; j++)
            {
                if(fileName[i] == badChars[j])
                {
                    appendChar = false;
                }
            }

            if (appendChar)
            {
                returnString += fileName[i];
            }
        }

        return returnString;
    }
}
