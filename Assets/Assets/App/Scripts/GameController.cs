using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using Lingdar77.Expand;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private VideoPlayer player;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Slider slider;
    [SerializeField] private Transform buttonsArea;
    [SerializeField] private SkipButton skipBox;

    [Header("Prefabs")]
    [SerializeField] private GameObject buttonPrefab;

    [Header("Data")]
    public GameNode startNode;
    private GameNode currentNode;
    private Action playingEnded = null;
    private Stack<GameNode> previousNodes = new Stack<GameNode>();
    private SliderController sliderController;
    private bool sliderPointerState;
    [Header("Events")]
    public UnityEvent onPlayingEnded;

    public static string newStart = "00000000000000000000000000";

    private void InitPlayer()
    {
        player.clip = currentNode.clip;
        text.text = currentNode.description;
        buttonsArea.DestroyAllChildren();

        player.Play();
        playingEnded = () =>
        {
            if (currentNode.isEnded)
            {
                Debug.Log("Ended: " + currentNode.description);
                return;
            }
            previousNodes.Push(currentNode);
            currentNode.isReached = true;
            buttonsArea.DestroyAllChildren();
            onPlayingEnded?.Invoke();
            foreach (var option in currentNode.options)
            {
                if (Instantiate(buttonPrefab, buttonsArea).TryGetComponent<Button>(out var button))
                {
                    button.onClick.AddListener(() =>
                    {
                        currentNode = option.node;

                        InitPlayer();
                    });
                    var text = button.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = option.buttonTittle;
                }
            }
        };
    }
    private void Awake()
    {
        currentNode = startNode;
        if (skipBox != null)
        {
            skipBox.gameObject.SetActive(false);
        }
        if (player != null)
        {
            player.loopPointReached += _ => playingEnded?.Invoke();
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
        InitPlayer();
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
    public void Save()
    {
        Stack<GameNode> stack = new Stack<GameNode>();
        stack.Push(startNode);
        string progress = "";
        while (stack.Count != 0)
        {
            var current = stack.Pop();
            if (current.clip != player.clip)
                progress += (current.isReached ? 1 : 0);
            else
                progress += 2;
            foreach (var option in current.options)
            {
                stack.Push(option.node);
            }
        }
        Debug.Log(progress);
        // return progress;
    }
    public void Load(string save)
    {
        Stack<GameNode> stack = new Stack<GameNode>();
        stack.Push(startNode);
        int index = 0;
        GameNode lastNode = startNode;
        while (stack.Count != 0)
        {
            var current = stack.Pop();
            char state = save[index];
            current.isReached = state != '0';
            if (state == '2')
            {
                lastNode = current;
            }
            foreach (var option in current.options)
            {
                stack.Push(option.node);
            }
            ++index;
        }
        startNode = lastNode;
        onPlayingEnded?.Invoke();
        // return lastNode;
    }
}
