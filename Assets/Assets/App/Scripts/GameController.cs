using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using Lingdar77.Expand;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using Lingdar77;

public class GameController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private VideoPlayer player;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Slider slider;
    [SerializeField] private Transform buttonsArea;
    [SerializeField] private SkipButton skipBox;
    [SerializeField] private GameDataManager dataManager;
    [SerializeField] private ToggleHide startPageToggleHide;

    [Header("Data")]
    public GameNode startNode;
    private ObjectPool pool;
    private GameNode currentNode;
    private Stack<GameNode> previousNodes = new Stack<GameNode>();
    private SliderController sliderController;
    private bool sliderPointerState;

    [Header("Events")]
    public UnityEvent onPlayingEnded;
    public static string newStart = "00000000000000000000000000";

    private void PlayingEnded()
    {

        previousNodes.Push(currentNode);
        currentNode.isReached = true;
        if (currentNode.isEnded)
        {
            Debug.Log("Ended: " + currentNode.description);
            startPageToggleHide?.PerformToggleHide();
            return;
        }
        buttonsArea.DestroyAllChildren(obj =>
        {
            var comp = (obj as GameObject).GetComponent<ObjectPoolChild>();
            comp.CacheObject2Pool();
        });
        onPlayingEnded?.Invoke();
        foreach (var option in currentNode.options)
        {
            var obj = pool.GetObject(option.buttonPrefab, buttonsArea);
            if (obj.TryGetComponent<Button>(out var button))
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    currentNode = option.node;

                    InitPlayer();
                });
                var text = button.GetComponentInChildren<TextMeshProUGUI>();
            }
        }
    }
    private void InitPlayer()
    {
        player.clip = currentNode.clip;
        text.text = currentNode.description;
        buttonsArea.DestroyAllChildren(obj =>
        {
            var comp = (obj as GameObject).GetComponent<ObjectPoolChild>();
            comp.CacheObject2Pool();
        });

        player.Play();

    }
    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
        currentNode = startNode;
        if (skipBox != null)
        {
            skipBox.gameObject.SetActive(false);
        }
        if (player != null)
        {
            player.loopPointReached += _ => PlayingEnded();
        }
        if (slider != null)
        {
            sliderController = slider.GetComponent<SliderController>();
            sliderController.onPointerUpdate += state =>
            {
                if (state)
                {
                    player.Pause();
                }
                else
                {
                    player.Play();
                }
                sliderPointerState = state;
            };
        }
    }
    private void Update()
    {
        if (!sliderPointerState && player != null && slider != null)
        {
            var progress = player.time / player.clip.length;
            slider.value = (float)progress;
        }
    }
    public void StartPlay()
    {
        LoadData(newStart);
    }
    public void Skip()
    {
        skipBox.gameObject.SetActive(true);
        player.Pause();
        skipBox.doSkip = () =>
        {
            skipBox.doSkip = null;
            skipBox.cancelSkip = null;
            player.time = player.clip.length - .5f;
            skipBox.gameObject.SetActive(false);
            player.Play();
            Debug.Log("Do skip");
        };
        skipBox.cancelSkip = () =>
        {
            skipBox.doSkip = null;
            skipBox.cancelSkip = null;
            skipBox.gameObject.SetActive(false);
            Debug.Log("Cancel skip");

            player.Play();
        };
    }
    public void Back()
    {
        if (previousNodes.TryPop(out var previous))
        {
            currentNode = previous;
            InitPlayer();
        }

    }
    public void SwitchPause()
    {
        if (player.isPaused)
        {
            player.Play();
        }
        else
        {
            player.Pause();
        }
    }
    public void OnSliderValueChange(float value)
    {
        if (sliderPointerState)
        {
            var time = player.clip.length * value;
            player.time = time;
        }
    }
    public string SaveCurrentProgress()
    {
        Stack<GameNode> stack = new Stack<GameNode>();
        stack.Push(startNode);
        string progress = "";
        while (stack.Count != 0)
        {
            var current = stack.Pop();
            if (current != currentNode)
                progress += (current.isReached ? '1' : '0');
            else
                progress += '2';
            foreach (var option in current.options)
            {
                stack.Push(option.node);
            }
        }
        Debug.Log(progress);
        return progress;
    }
    public void LoadData(string save)
    {
		Debug.Log(save);
		Stack<GameNode> stack = new Stack<GameNode>();
        stack.Push(startNode);
        int index = 0;
        GameNode lastNode = startNode;
        while (stack.Count != 0)
        {
            var current = stack.Pop();
            char state = save[index];
            current.isReached = state != '0';
            if (save[index] == '2')
            {
                lastNode = current;
            }
            foreach (var option in current.options)
            {
                stack.Push(option.node);
                
            }
            ++index;
        }
		currentNode = lastNode;
		InitPlayer();
		onPlayingEnded?.Invoke();
    }
    public void Load()
    {
        var data = dataManager.GetDocument(SaveName.SAVE1);
        LoadData(data);
    }
    public void Save()
    {
        var progress = SaveCurrentProgress();
        dataManager.SaveDocument(SaveName.SAVE1, progress);
        Debug.Log(progress);
    }
    public void PlayAt(GameNode node)
    {
        currentNode = node;
        InitPlayer();
        onPlayingEnded?.Invoke();
    }

    public void ContinuePlay()
    {
        var lastSave = dataManager.GetDocument(dataManager.GetLastSaveName());
        LoadData(lastSave);

    }
}
