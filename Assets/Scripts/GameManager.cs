using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string dataURL;
    [SerializeField]
    private StackHandler stackPrefab;
    [SerializeField]
    private float stacksSpacing = 15;
    [SerializeField]
    private ControlPanel controlPanel;

    private List<StackHandler> stacks;

    private void Start()
    {
        StartCoroutine(GetData(InitializeStacks));
    }

    private IEnumerator GetData(Action<string> onComplete)
    {
        UnityWebRequest dataRequest = UnityWebRequest.Get(dataURL);
        yield return dataRequest.SendWebRequest();
        if (dataRequest.result == UnityWebRequest.Result.Success)
        {
            string data = dataRequest.downloadHandler.text;
            onComplete.Invoke(data);
        }
        else
        {
            Debug.Log(dataRequest.downloadHandler.error);
        }
    }

    private void InitializeStacks(string blocksData)
    {
        stacks = new List<StackHandler>();
        List<Block> blocks = JsonConvert.DeserializeObject<List<Block>>(blocksData);
        List<string> grades = blocks.Select(block => block.grade).Distinct().ToList();
        float stackPosition = -stacksSpacing;
        for (int grade = 0; grade < 3; grade++)
        {
            List<Block> stackBlocks = blocks.Where(block => block.grade == grades[grade]).ToList();
            StackHandler stack = Instantiate(stackPrefab, transform);
            stack.Init(grades[grade], stackBlocks);
            stack.transform.localPosition = new Vector3(stackPosition, 0, 0);
            stackPosition += stacksSpacing;
            stacks.Add(stack);
        }
        controlPanel.Init(stacks);
    }
}