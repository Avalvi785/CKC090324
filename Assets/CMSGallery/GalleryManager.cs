using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.UI.Extensions;

public class GalleryManager : MonoBehaviour
{
    public GameObject categoryPrefab;
    public GameObject subCategoryPrefab;//my addition
    public Transform contentPanel;
    public Transform subCategoryContentPanel;
    public Button homeButton;
    public Sprite defaultThumbnail; // Assign your default thumbnail sprite in the Unity Editor

    private string rootFolderPath = "C:/Main Categories";

    void Start()
    {
        // Initialize by instantiating the root categories
        InstantiateRootCategories();
        homeButton.onClick.AddListener(() => OnHomeButtonClick());
    }

    void InstantiateRootCategories()
    {
        string[] rootDirectories = Directory.GetDirectories(rootFolderPath);
        foreach (string rootDir in rootDirectories)
        {
            GameObject category = Instantiate(categoryPrefab, contentPanel);
            category.name = new DirectoryInfo(rootDir).Name;

            // Set icon and label (Replace these lines with your actual icon and label setting logic)
            SetCategoryInfo(category, rootDir);

            // Attach a click event to the button component
            Button button = category.GetComponent<Button>();
            DragIcon icon = category.GetComponentInChildren<DragIcon>();
            if (icon != null)
            {
                icon.directory = rootDir;
            }
            if (button != null)
            {
                button.onClick.AddListener(() => OnCategoryButtonClick(rootDir));
            }
        }
    }

    public void OnCategoryButtonClick(string folderPath)
    {
        Debug.LogError("Category Open");
        // Clear existing children of the clicked category (if any)
        ClearContentPanel();
        // Instantiate Sub Categories
        string[] subDirectories = Directory.GetDirectories(folderPath);
        foreach (string subDir in subDirectories)
        {
            GameObject category = Instantiate(subCategoryPrefab, subCategoryContentPanel);
            category.name = new DirectoryInfo(subDir).Name;

            // Set icon and label (Replace these lines with your actual icon and label setting logic)
            SetCategoryInfo(category, subDir);

            // Attach a click event to the button component
            Button button = category.GetComponent<Button>();
            DragIcon icon = category.GetComponentInChildren<DragIcon>();
            if (icon != null)
            {
                icon.directory = subDir;
            }
            if (button != null)
            {
                button.onClick.AddListener(() => OnCategoryButtonClick(subDir));
            }
        }
    }

    void SetCategoryInfo(GameObject category, string folderPath)
    {
        // Attempt to find TMP_Text component with different methods
        TMP_Text textMeshPro = category.GetComponentInChildren<TMP_Text>(true); // Include inactive children
        if (textMeshPro == null)
        {
            textMeshPro = category.GetComponentInChildren<TMP_Text>();
        }

        if (textMeshPro != null)
        {
            textMeshPro.text = category.name;
        }
        else
        {
            Debug.LogError("TMP_Text component not found under the category GameObject: " + category.name);
            return;
        }

        // Set thumbnail to Icon and DraggableIcon
        Image iconImage = null;
        Image draggableIconImage = null;

        foreach (Transform child in category.transform)
        {
            if (child.name == "Icon")
            {
                iconImage = child.GetComponent<Image>();
            }
            else if (child.name == "DraggebleIcon")
            {
                draggableIconImage = child.GetComponent<Image>();
            }
        }

        if (iconImage != null)
        {
            Sprite thumbnail = LoadThumbnail(folderPath);
            iconImage.sprite = (thumbnail != null) ? thumbnail : defaultThumbnail;
        }
        else
        {
            Debug.LogError("Icon Image component not found under the category GameObject: " + category.name);
        }

        if (draggableIconImage != null)
        {
            Sprite thumbnail = LoadThumbnail(folderPath);
            draggableIconImage.sprite = (thumbnail != null) ? thumbnail : defaultThumbnail;
        }
        else
        {
            Debug.LogError("DraggableIcon Image component not found under the category GameObject: " + category.name);
        }
    }



    Sprite LoadThumbnail(string folderPath)
    {
        string thumbnailPath = Path.Combine(folderPath, "thumbnail.png");
        if (File.Exists(thumbnailPath))
        {
            byte[] fileData = File.ReadAllBytes(thumbnailPath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData); // Load the image data
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }

        return null;
    }

    void ClearContentPanel()
    {
        // Clear existing children of the content panel
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
        foreach(Transform child in subCategoryContentPanel)

        {
            Destroy(child.gameObject);
        }
    }

    void Update()
    {
        // Check for home button press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnHomeButtonClick();
        }
    }

    void OnHomeButtonClick()
    {
        Debug.Log("Home");
        // Clear existing children of the clicked category
        ClearContentPanel();

        // Instantiate the root categories after clearing
        InstantiateRootCategories();
    }
}