using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public struct SaveSlot
{
    public SaveName name;
    public TextMeshProUGUI textDescription;
    public Button button;
}

[System.Serializable]
public enum SLType
{
    Save,
    Load
}

public class SaveLoadManager : MonoBehaviour
{
    public struct OnPerformedEventParameter
    {
        public SaveName saveName;
        public SLType type;

        public OnPerformedEventParameter(SLType type, SaveName saveName) : this()
        {
            this.type = type;
            this.saveName = saveName;
        }
    }
    [SerializeField] private SaveSlot[] slots;
    [SerializeField] private GameController gameController;
    public UnityEvent<OnPerformedEventParameter> onPerformed;
    public SLType type;
    private GameDataManager dataManager;

    private void Awake()
    {
        dataManager = gameController.GetComponent<GameDataManager>();
        UpdateSaves();
    }

    public void UpdateSaves()
    {
        foreach (var slot in slots)
        {
            slot.button.onClick.AddListener(() =>
            {
                Perform(slot.name);

            });
            if (PlayerPrefs.HasKey(slot.name.ToString()))
            {
                var progress = dataManager.GetDocument(slot.name);
                var node = GetCurrentPlayingNode(progress);
                slot.textDescription.text = node.description;
            }
            else
            {
                slot.textDescription.text = "无记录";

            }
        }
    }

    private void Perform(SaveName slot)
    {
        if (type == SLType.Save)
        {
            var progress = gameController.SaveCurrentProgress();
            dataManager.SaveDocument(slot, progress);
        }
        else
        {
            if (!PlayerPrefs.HasKey(slot.ToString()))
            {
                return;
            }
            var progress = dataManager.GetDocument(slot);
            gameController.LoadData(progress);
        }
        onPerformed?.Invoke(new OnPerformedEventParameter(type, slot));
    }

    private GameNode GetCurrentPlayingNode(string save)
    {
        var stack = new Stack<GameNode>();
        stack.Push(gameController.startNode);
        int index = 0;
        while(stack.Count != 0)
        {
            var current = stack.Pop();
            var state = save[index];
            if(state == '2')
            {
                 return current;
            }
            foreach(var option in current.options)
            {
                stack.Push(option.node);
            }
            ++index;
        }
        return gameController.startNode;
    }
}
