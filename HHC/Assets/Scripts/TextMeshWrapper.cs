using UnityEngine;
using System.Collections;

public class TextMeshWrapper : MonoBehaviour
{
    /// <summary>
    /// The textmesh to wrap the text of
    /// </summary>
    private TextMesh textMesh;

    /// <summary>
    /// The parent of this object
    /// </summary>
    GameObject parent;

    /// <summary>
    /// Get/set the text of the textmesh. This automatically updates the wrap.
    /// </summary>
    public string Text
    {
        get
        {
            return textMesh.text;
        }
        set
        {
            textMesh.text = value;
            UpdateText();
        }
    }

    /// <summary>
    /// The margin at the top.
    /// </summary>
    public float TopMargin = 0f;

    /// <summary>
    /// The margins on the sides.
    /// </summary>
    public float SideMargins = 0f;

    /// <summary>
    /// Wrap the text on start
    /// </summary>
    public void Start()
    {
        //Grab the parent and mesh
        parent = transform.parent.gameObject;
        textMesh = gameObject.GetComponent<TextMesh>();

        //Sets the position of this object in relation to the parent
        float xPos = (parent.renderer.bounds.center.x - parent.renderer.bounds.extents.x) + SideMargins;
        float yPos = (parent.renderer.bounds.center.y + parent.renderer.bounds.extents.y) - TopMargin;
        Vector3 newPos = new Vector3(xPos, yPos, transform.position.z);
        transform.position = newPos;

        //Scale the textmesh is relation to the parent, so the text stays normal
        transform.localScale = new Vector3(transform.lossyScale.x / parent.transform.lossyScale.x, transform.lossyScale.y / parent.transform.lossyScale.y, transform.lossyScale.z / parent.transform.lossyScale.z);

        UpdateText();
    }

    /// <summary>
    /// Update the text.
    /// </summary>
    private void UpdateText()
    {
        //Calculate how wide a line can be
        float rowLimit = parent.renderer.bounds.extents.x - SideMargins; //find the sweet spot    

        //Wrap the text
        string[] parts = textMesh.text.Replace(System.Environment.NewLine, "").Split(' ');
        textMesh.text = "";
        foreach (string part in parts)
        {
            textMesh.text += part + " ";
            if (textMesh.renderer.bounds.extents.x > rowLimit)
            {
                textMesh.text = textMesh.text.TrimEnd() + System.Environment.NewLine + part + " ";
            }
        }
    }
}
