using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BAddSave : MonoBehaviour
{

    private BBuildManager bbuildManager = null;
    private string path;

    public void Init(BBuildManager buildManager, string path, string saveName, Vector3 position, Transform parent)
    {
        this.path = path;
        bbuildManager = buildManager;
        transform.name = saveName + "Button";

        Transform buttonText = transform.Find("Text");
        Text text = buttonText.GetComponent<Text>() as Text;
        text.text = saveName;

        transform.SetParent(parent);

        transform.localPosition = position;
        transform.localScale = new Vector3(5, 22.22f, 1);
    }

    public void AddSave()
    {
        Transform buttonText = transform.Find("Text");
        Text text = buttonText.GetComponent<Text>() as Text;
        Debug.Log(text.text);

        if (bbuildManager == null)
            bbuildManager = GameObject.Find("LoadButton").GetComponent<BBuildManager>();

        bbuildManager.menuManager.LoadSaveFile(path +"\\" + text.text + ".shima");
    }

}
