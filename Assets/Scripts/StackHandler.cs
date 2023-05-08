using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class StackHandler : MonoBehaviour
{
    [SerializeField]
    private BlockHandler blockPrefab;
    [SerializeField]
    private TMP_Text gradeText;

    private List<BlockHandler> blocks;
    private List<Block> sortedData;

    public void Init(string grade, List<Block> blocksData)
    {
        gradeText.text = grade;
        blocks = new List<BlockHandler>();
        sortedData = blocksData.OrderBy(block => block.domain)
                            .OrderBy(block => block.cluster).
                            OrderBy(block => block.standardid).ToList();
        Setup();
    }

    public void Refresh()
    {
        foreach (BlockHandler block in blocks)
            if (block != null)
                DestroyImmediate(block?.gameObject);
        blocks.Clear();
        Setup();
    }

    private void Setup()
    {
        int row = -1, column = 0;
        int currentCount = 0;
        for (int blockIndex = 0; blockIndex < sortedData.Count; blockIndex++)
        {
            BlockHandler blockHandler = Instantiate(blockPrefab, transform);
            blockHandler.Init(sortedData[blockIndex]);
            blocks.Add(blockHandler);

            blockHandler.transform.localPosition = GetBlockPosition(row, column);
            blockHandler.transform.localEulerAngles = GetBlockAngle(column);
            currentCount++;
            row++;
            if (currentCount == 3)
            {
                currentCount = 0;
                row = -1;
                column++;
            }
        }
    }

    public void StartTest()
    {
        foreach (BlockHandler block in blocks)
        {
            if (block.BlockType == (int)BlockType.Glass)
                DestroyImmediate(block.gameObject);
            else
                block.EnablePhysics();
        }
    }

    private Vector3 GetBlockPosition(int row, int column)
    {
        float offset = 1.5f * row;
        Vector3 position = new Vector3(0, column, 0);
        if (column % 2 == 0)
            position.x += offset;
        else
            position.z += offset;
        return position;
    }

    private Vector3 GetBlockAngle(int column)
    {
        return new Vector3(0, column % 2 == 0 ? 0 : 90, 0);
    }
}