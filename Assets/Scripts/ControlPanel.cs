using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public static ControlPanel Instance;
    [SerializeField]
    private InputControl inputControl;
    [SerializeField]
    private GameObject viewPanel;
    [SerializeField]
    private GameObject testPanel;

    [Header("Block Info Screen")]
    [SerializeField]
    private GameObject blockInfoPanel;
    [SerializeField]
    private TMP_Text[] infoEntries;

    private StackHandler currentStack;
    private List<StackHandler> stacks;
    private int currentStackIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void Init(List<StackHandler> stacks)
    {
        this.stacks = stacks;
        currentStackIndex = 1;
        UpdateCurrentStack();
    }

    public void NextStack()
    {
        currentStackIndex++;
        UpdateCurrentStack();
    }

    public void PrevStack()
    {
        currentStackIndex--;
        UpdateCurrentStack();
    }

    public void TestStack()
    {
        viewPanel.SetActive(false);
        testPanel.SetActive(true);
        currentStack.StartTest();
    }

    public void StopTest()
    {
        viewPanel.SetActive(true);
        testPanel.SetActive(false);
        CloseInfoPanel();
        foreach (StackHandler stack in stacks)
            stack.Refresh();
    }

    private void UpdateCurrentStack()
    {
        currentStackIndex = Mathf.Clamp(currentStackIndex, 0, stacks.Count - 1);
        currentStack = stacks[currentStackIndex];
        inputControl.UpdateTarget(currentStack.transform);
        CloseInfoPanel();
    }

    public void DisplayBlockInfo(Block block)
    {
        blockInfoPanel.SetActive(true);
        infoEntries[0].text = block.grade + " : " + block.domain;
        infoEntries[1].text = block.cluster;
        infoEntries[2].text = block.standardid + " : " + block.standarddescription;
    }

    public void CloseInfoPanel()
    {
        blockInfoPanel.SetActive(false);
    }
}
