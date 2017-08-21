using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BMenuManager : MonoBehaviour {

    #region Parametres

    public string saveName = "SaveRoom_Furnitures.txt";
    public string loadName = "SaveRoom_Furnitures.txt";

    public GameObject menuPanel;
    public GameObject loadPanel;
    public GameObject buildPanel;
    public GameObject roomPanel;
    public GameObject furniturePanel;
    public GameObject sensorPanel;
    public GameObject accuatorPanel;
    public GameObject binarySensorPanel;
    public GameObject furnitureScrollView;
    public GameObject saveScrollView;

    public GameObject furnituresFolder;
    public GameObject sensorsFolder;
    public GameObject wallsFolder;

    public RectTransform contentWindow;
    public RectTransform saveWindow;

    public InputField pathField;

    #endregion

    #region MonoBehavior

    void Start()
    {
        //LoadSaveFolder();

        Object[] furnitures = Resources.LoadAll("Furniture");
        int n = furnitures.Length;
        float bottom = n / 7;
        contentWindow.offsetMax = new Vector2(contentWindow.offsetMax.x, bottom);
        GameObject furnitureButton;
        BAddFurniture bAddFurniture;
        BBuildManager bBuildManager = GetComponent<BBuildManager>();
        //float x = contentWindow.anchoredPosition.x;
        //float y = contentWindow.anchoredPosition.y;

        int x = -1000; // -920
        int y = 640; // 640
        foreach (GameObject g in furnitures)
        {
            furnitureButton = Instantiate(Resources.Load("UI/FurnitureButton")) as GameObject;
            bAddFurniture = furnitureButton.GetComponent<BAddFurniture>();
            bAddFurniture.Init(bBuildManager, g.name, new Vector3(x, y, 0), furnitureScrollView.transform);
            x += 496;
            if (x >= 1300)
            { // 840
                x = -1000;
                y -= 726;
            }
        }

        Debug.Log(Application.dataPath);

        string path = Application.dataPath + "/Extra/Saves";
        int pathLegth = path.Length;

        pathField.text = path;

        string[] saves = Directory.GetFiles(path, "*shima");
        n = saves.Length;
   
        bottom = n / 7;
        saveWindow.offsetMax = new Vector2(saveWindow.offsetMax.x, bottom);
        GameObject saveButton;

        x = -974;
        y = 2219;

        //Trouver pourquoi on doit faire un tel ajustement??
        x += 1240;
        y -= 2659;

        foreach (string input in saves)
        {
            saveButton = Instantiate(Resources.Load("UI/FurnitureButton")) as GameObject;
            bAddFurniture = saveButton.GetComponent<BAddFurniture>();

            Debug.Log(input);
            string output = input.Substring(input.IndexOf('\\') + 1);

            int index = output.IndexOf('.');
            string sub;
            if (index >= 0)
            {
                sub = output.Substring(0, index);
            }

            else
            {
                sub = "An Error Occur";
            }

            Debug.Log(sub);

            bAddFurniture.Init(bBuildManager, sub, new Vector3(x, y, 0), saveScrollView.transform);

            x += 600;
            if (x >= 800)
            {
                x = -974;
                y -= 900;
            }
        }

        ShowBuildMenu(false);
        ShowRoomMenu(false);
        ShowSensorMenu(false);
        ShowAccuatorMenu(false);
        ShowBinaryMenu(false);
        ShowFurniturePanelMenu(false);
        ShowLoadMenu(false);


        //		Transform furnitureButton = furnitureScrollView.transform.Find ("FurnitureButton");
        //		furnitureButton.transform.name = furnitures [0].name+"Button";
        //		Transform buttonText = furnitureButton.Find ("Text");
        //		Text text = buttonText.GetComponent<Text> () as Text;
        //		text.text = furnitures [0].name;

        //		foreach(GameObject g in Resources.LoadAll("Furniture")){
        //			Debug.Log (g.name);
        //
        //		}
    }
    void Update()
    {

    }

    #endregion

    #region MenuButton

    public void Menu() {
        if (menuPanel != null)
            menuPanel.SetActive(!menuPanel.activeInHierarchy);
        ShowBuildMenu(false);
        ShowRoomMenu(false);
        ShowSensorMenu(false);
        ShowAccuatorMenu(false);
        ShowBinaryMenu(false);
        ShowFurniturePanelMenu(false);
        ShowLoadMenu(false);
        
    }
    public void Load()
    {
        if (loadPanel != null)
            loadPanel.SetActive(!loadPanel.activeInHierarchy);
        ShowBuildMenu(false);
        ShowRoomMenu(false);
        ShowSensorMenu(false);
        ShowAccuatorMenu(false);
        ShowBinaryMenu(false);
        ShowFurniturePanelMenu(false);
    }
    public void Build() {
        if (buildPanel != null) {
            buildPanel.SetActive(!buildPanel.activeInHierarchy);
        }
    }
    public void Accuator() {
        if (roomPanel != null) {
            ShowRoomMenu(false);
            ShowSensorMenu(false);
            ShowAccuatorMenu(!accuatorPanel.activeInHierarchy);
        }
    }
    public void Room() {
        if (roomPanel != null) {
            ShowRoomMenu(!roomPanel.activeInHierarchy);
            ShowSensorMenu(false);
            ShowAccuatorMenu(false);
        }
    }
    public void Sensor() {
        if (sensorPanel != null) {
            ShowRoomMenu(false);
            ShowSensorMenu(!sensorPanel.activeInHierarchy);
            ShowAccuatorMenu(false);
        }
    }
    public void Furniture() {
        if (furniturePanel != null) {
            furniturePanel.SetActive(!furniturePanel.activeInHierarchy);
        }
    }
    public void BinarySensor() {
        if (binarySensorPanel != null) {
            binarySensorPanel.SetActive(!binarySensorPanel.activeInHierarchy);
        }
    }

    private void ShowBuildMenu(bool value)
    {
        if (buildPanel != null)
            buildPanel.SetActive(value);
    }
    private void ShowLoadMenu(bool value)
    {
        if (loadPanel != null)
            loadPanel.SetActive(value);
    }
    private void ShowRoomMenu(bool value)
    {
        if (roomPanel != null)
            roomPanel.SetActive(value);
    }
    private void ShowSensorMenu(bool value)
    {
        if (sensorPanel != null)
            sensorPanel.SetActive(value);
    }
    private void ShowAccuatorMenu(bool value)
    {
        if (accuatorPanel != null)
            accuatorPanel.SetActive(value);
    }
    private void ShowBinaryMenu(bool value)
    {
        if (binarySensorPanel != null)
            binarySensorPanel.SetActive(value);
    }
    private void ShowFurniturePanelMenu(bool value)
    {
        if (furniturePanel != null)
            furniturePanel.SetActive(value);
    }

    #endregion

    #region Save and Load

    public void SaveFurniture()
    {
        StreamWriter writer = new StreamWriter(saveName);

        BBuildManager a = this.transform.GetComponent<BBuildManager>();
        Debug.Log(a);

        List<Furniture_Recepteur> b = a.getFurnitureList();
        Debug.Log(b.Count);

        foreach (Furniture_Recepteur currentFurniture in b)
        {
            writer.WriteLine(JsonUtility.ToJson(currentFurniture.getFurniture()));
            Debug.Log(currentFurniture.getFurniture());
        }

        writer.Close();
    }
    public void LoadFurniture()
    {
        // 1) Effacer tous les objets actuellement present dans la scene
        // 2) Lire 1 ligne, convertir en GameObject
        // 3) Creer un Furniture_Recepteur pour chaque gameObject avec la fonction AddFurniture de BBuildManager

        this.GetComponentInParent<BBuildManager>().RemoveAllFurniture();

        StreamReader reader = new StreamReader(loadName);
        string line = reader.ReadLine();

        while (line != null)
        {
            GetComponentInParent<BBuildManager>().LoadFurniture(line);
            line = reader.ReadLine();
        }

        reader.Close();
        return;
    }

    private void LoadSaveFolder()
    {
        // string[] filePaths = Directory.GetFiles(Application.dataPath + "/", ".shima");
        // Debug.Log(filePaths.Length);

        DirectoryInfo directory = new DirectoryInfo(@"Assets\Resources\Saves");
        FileInfo[] info = directory.GetFiles("*.shima");
        Debug.Log(info.Length);
        Debug.Log(info[0].Name);

        float bottom = info.Length / 7;
        saveWindow.offsetMax = new Vector2(saveWindow.offsetMax.x, bottom);
        GameObject furnitureButton;
        BAddFurniture bAddFurniture;
        BBuildManager bBuildManager = GetComponent<BBuildManager>();

        int x = -1000; // -920
        int y = 640; // 640

        for (int i=0;i<info.Length;i++)
        {
            furnitureButton = Instantiate(Resources.Load("UI/FurnitureButton")) as GameObject;
            bAddFurniture = furnitureButton.GetComponent<BAddFurniture>();
            bAddFurniture.Init(bBuildManager, info[i].Name, new Vector3(x, y, 0), saveScrollView.transform);
            x += 496;
           if (x >= 1300)
            { // 840
                x = -1000;
                y -= 726;
            }
        }
    }

    #endregion
}
