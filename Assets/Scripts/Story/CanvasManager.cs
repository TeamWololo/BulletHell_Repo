using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    enum StoryScenes
    {
        FIRST_STORY,
        SECOND_STORY,
        THIRD_STORY,
        FINISHED
    }
    
    struct Scene
    {
        private TextMeshProUGUI text;
        private Image image;
        private float alphaValue;
        private float speed;
        private bool isFadeInCompleted;
        private float sceneTime;
        private float timer;
        public bool isCompleted;
    
        public Scene(float _sceneTime, float _speed, TextMeshProUGUI _text, Image _image)
        {
            alphaValue = 0.0f;
            speed = _speed;
            image = _image;
            text = _text;
            isFadeInCompleted = false;
            sceneTime = _sceneTime;
            timer = 0.0f;
            isCompleted = false;
            
            SetAlphaValues();
        }
        
        void SetAlphaValues()
        {
            text.alpha = alphaValue;
            image.color = new Color(1.0f, 1.0f, 1.0f, alphaValue);
        }
        
        void FadeIn()
        {
            if (alphaValue < 1.0f)
            {
                alphaValue += speed * Time.deltaTime;
            }
            else
            {
                alphaValue = 1.0f;
            }
        
            SetAlphaValues();
    
            isFadeInCompleted = Math.Abs(alphaValue - 1.0f) < float.Epsilon ? true : false;
        }
    
        void FadeOut()
        {
            if (alphaValue > 0.0f)
            {
                alphaValue -= speed * Time.deltaTime;
            }
            else
            {
                alphaValue = 0.0f;
            }
        
            SetAlphaValues();
    
            isCompleted = Math.Abs(alphaValue - 0.0f) < float.Epsilon ? true : false;
        }
    
        public void Start()
        {
            if (isCompleted)
            {
                alphaValue = 0.0f;
                SetAlphaValues();
                return;
            }
            
            if (!isFadeInCompleted)
            {
                FadeIn();
                return;
            }
            
            timer += Time.deltaTime;
            
            if (timer < sceneTime)
            {
                return;
            }
            
            FadeOut();
        }
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
    [SerializeField] private float sceneTime = 3.0f;
    [SerializeField] [Range(0.1f, 2.0f)] private float speed = 1.0f;
    
    private StoryScenes currentStory = StoryScenes.FIRST_STORY;
    private Scene firstScene, secondScene, thirdScene;

    // Start is called before the first frame update
    void Start()
    {
        firstScene = new Scene(sceneTime, speed, storyText, storyImage);
        secondScene = new Scene(sceneTime, speed, secondStoryText, secondStoryImage);
        thirdScene = new Scene(sceneTime, speed, thirdStoryText, thirdStoryImage);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentStory)
            {
                case StoryScenes.FIRST_STORY:
                    firstScene.isCompleted = true;
                    break;
                case StoryScenes.SECOND_STORY:
                    secondScene.isCompleted = true;
                    break;
                case StoryScenes.THIRD_STORY:
                    thirdScene.isCompleted = true;
                    break;
            }
        }
        
        switch (currentStory)
        {
            case StoryScenes.FIRST_STORY:
                firstScene.Start();
                if (firstScene.isCompleted)
                {
                    currentStory = StoryScenes.SECOND_STORY;
                }
                break;
            case StoryScenes.SECOND_STORY:
                secondScene.Start();
                if (secondScene.isCompleted)
                {
                    currentStory = StoryScenes.THIRD_STORY;
                }
                break;
            case StoryScenes.THIRD_STORY:
                thirdScene.Start();
                if (thirdScene.isCompleted)
                {
                    currentStory = StoryScenes.FINISHED;
                }
                break;
            case StoryScenes.FINISHED:
                SceneManager.LoadScene("Level_01");
                break;
        }
    }
}
