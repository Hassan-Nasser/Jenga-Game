using UnityEngine;
using System;

public class BlockHandler : MonoBehaviour
{
    public int BlockType { get { return block.mastery; } }

    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private Material[] materials;

    private Block block;

    public void Init(Block block)
    {
        this.block = block;
        meshRenderer.material = materials[block.mastery];
    }

    public void EnablePhysics()
    {
        rigidbody.isKinematic = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
            ControlPanel.Instance.DisplayBlockInfo(block);
    }
}

public enum BlockType { Glass = 0, Wood = 1, Stone = 2 }

[Serializable]
public class Block
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;
}