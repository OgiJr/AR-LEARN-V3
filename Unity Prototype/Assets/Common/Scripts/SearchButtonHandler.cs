using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchButtonHandler : MonoBehaviour
{

    public Button b;
    public GameObject t;
    public ServerDownloader s;

    // Start is called before the first frame update
    void Start()
    {
        b.onClick.AddListener(() => onClickListener());
    }

    void onClickListener() {
        TextMeshPro text = t.GetComponent<TextMeshPro>() as TextMeshPro;
        text.text = "fg5znv8p";
        s.getInfo(text.text);
        s.downloadModels();
    }
}
