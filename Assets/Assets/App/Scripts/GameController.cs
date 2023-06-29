using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using Lingdar77.Expand;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private VideoPlayer player;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Slider slider;
    [SerializeField] private Transform buttonsArea;

    [Header("Prefabs")]
    [SerializeField] private GameObject buttonPrefab;

    [Header("Data")]
    public GameNode startNode;
    private GameNode currentNode;
    private Action playingEnded = null;
    private Stack<GameNode> previousNodes = new Stack<GameNode>();
    private SliderController sliderController;
    private bool sliderPointerState;

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
        player.time = player.clip.length - 1;
    }

    public void Back()
    {
        if (previousNodes.TryPop(out var previous))
        {
            currentNode = previous;
            InitPlayer();
        }

    }

    public void onSliderValueChange(float value)
    {
        if (sliderPointerState)
        {
            var time = player.clip.length * value;
            player.time = time;
        }
    }
}
