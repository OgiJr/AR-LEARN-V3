using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageManager : MonoBehaviour
{
    private bool bg = false;

    public Sprite bgimg;
    public Sprite engimg;
    public Image icon;

    public Text placeHolderA;
    public Text placeHolderB;
    public Text placeHolderC;
    public Text titleA;
    public Text titleB;
    public Text titleC;
    public Text button;
    public Text buttonB;

    public Text placeHolderD;
    public Text placeHolderE;

    public Text titleD;
    public Text placeHolderF;
    public Text buttonC;

    public ServerDownloader serverDownloader;

    public void ChangeLanguage()
    {
        if (PlayerPrefs.GetInt("Language") == 1)
        {
            PlayerPrefs.SetInt("Language", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Language", 1);
        }
    }

    private void Update()
    {
        if (this.gameObject.name == "UISystem")
        {
            if (PlayerPrefs.GetInt("Language") == 1)
            {
                icon.sprite = bgimg;
                placeHolderA.text = "Потърси пакет...";
                if (serverDownloader.p.text == null)
                {
                    placeHolderB.text = "Моля изберете 3D модел, за да изпишем неговата информация.";
                }
                placeHolderC.text = "Името на обекта...";
                titleA.text = "Описание на обекта";
                titleB.text = "Изберете обект";
                button.text = "Потърси...";
                titleC.text = "Речник";
                buttonB.text = "Влезте в речника";
                placeHolderC.text = "Потърсете обект";
            }
            else
            {
                icon.sprite = engimg;
                placeHolderA.text = "Search Package...";
                if (serverDownloader.p.text == null)
                {
                    placeHolderB.text = "Please select a 3D model so that we can desplay its information.";
                }
                placeHolderC.text = "Object Name...";
                button.text = "Search";
                titleA.text = "Object Description";
                titleB.text = "Select An Object";
                titleC.text = "Text Dictionary";
                buttonB.text = "Enter Text Dictionary";
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {

            if (PlayerPrefs.GetInt("Language") == 1)
            {
                placeHolderD.text = "Имаше грешка при намирането на обекта, който търсехте.";
                placeHolderE.text = "Моля изберете изображение с по-високо качество.";
            }
            else
            {
                placeHolderD.text = "There was an error while trying to find the object you are looking for.";
                placeHolderE.text = "Please select an image with a higher qualityy";
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Language") == 1)
            {
                placeHolderF.text = "Текст...";
                buttonC.text = "Потърси";
                titleD.text = "Речник";
            }
            else
            {
                placeHolderF.text = "Enter Text...";
                buttonC.text = "Search";
                titleD.text = "Dictionary";
            }
        }
    }
}
