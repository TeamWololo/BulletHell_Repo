using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    struct Story
    {
        private TextMeshProUGUI text;
        private Image image;
    }
    
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private TextMeshProUGUI secondStoryText;
    [SerializeField] private TextMeshProUGUI thirdStoryText;
    
    [Header("Image")]
    [SerializeField] private Image storyImage;
    [SerializeField] private Image secondStoryImage;
    [SerializeField] private Image thirdStoryImage;

    [Header("Variables")] 
    [SerializeField] [Range(0.1f, 2.0f)] private float speed = 1.0f;
    
    private float alphaValue = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SetAlphaValues(TextMeshProUGUI text, Image image)
    {
        text.alpha = alphaValue;
        image.color = new Color(1.0f, 1.0f, 1.0f, alphaValue);
    }

    void FadeIn(TextMeshProUGUI text, Image image)
    {
        if (alphaValue < 1.0f)
        {
            alphaValue += speed * Time.deltaTime;
        }
        else
        {
            alphaValue = 1.0f;
        }
        
        SetAlphaValues(text, image);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
