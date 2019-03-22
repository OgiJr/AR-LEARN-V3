using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManagerMoreInformation : MonoBehaviour
{
    public CloudHandler cloudHandler;
    public Text title;
    public Image image;

    private void Update()
    {
        if (cloudHandler.selectedObject != null)
        {
            TextAsset asset = Resources.Load(@"Text/" + cloudHandler.selectedObject.name) as TextAsset;
            this.gameObject.GetComponent<Text>().text = asset.text;

            string titleText = cloudHandler.selectedObject.name;
            title.text = titleText;

            Sprite texture;
            texture = Resources.Load<Sprite>(@"Images/" + cloudHandler.selectedObject.name);
            image.color = Color.white;
            image.sprite = texture;
        }
    }
}