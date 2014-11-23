using UnityEngine;
using System.Collections;

public class TextMeshWrapper : MonoBehaviour
{
    /// <summary>
    /// The textmesh to wrap the text of
    /// </summary>
    private TextMesh TextMesh
    {
        get
        {
            return _textMesh;
        }
        set
        {
            _textMesh = value;
            if (value != null)
            {
                UpdateText();
            }
        }
    }
    private TextMesh _textMesh;

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
            return _text;
        }
        set
        {
            if (value == _text)
            {
                return;
            }
            _text = value;
            if (TextMesh != null)
            {
                UpdateText();
            }
        }
    }
    private string _text;

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
        TextMesh mesh = gameObject.GetComponent<TextMesh>();
        string startText = mesh.text;
        TextMesh = mesh;

        //Sets the position of this object in relation to the parent
        float xPos = (parent.renderer.bounds.center.x - parent.renderer.bounds.extents.x) + SideMargins;
        float yPos = (parent.renderer.bounds.center.y + parent.renderer.bounds.extents.y) - TopMargin;
        Vector3 newPos = new Vector3(xPos, yPos, transform.position.z);
        transform.position = newPos;

        //Scale the textmesh is relation to the parent, so the text stays normal
        transform.localScale = new Vector3(transform.lossyScale.x / parent.transform.lossyScale.x, transform.lossyScale.y / parent.transform.lossyScale.y, transform.lossyScale.z / parent.transform.lossyScale.z);

        //If the text is not set, set the start text.
        if (string.IsNullOrEmpty(Text))
        {
            Text = startText;
        }
    }

    /// <summary>
    /// Update the text.
    /// </summary>
    private void UpdateText()
    {
        //Calculate how wide a line can be
        float rowLimit = parent.renderer.bounds.extents.x - SideMargins; //find the sweet spot    

        //Wrap the text
        string[] parts = Text.Replace(System.Environment.NewLine, "").Split(' ');
        TextMesh.text = "";
        foreach (string part in parts)
        {
            TextMesh.text += part + " ";
            if (TextMesh.renderer.bounds.extents.x > rowLimit)
            {
                TextMesh.text = TextMesh.text.Substring(0, TextMesh.text.Length - part.Length - 1) + System.Environment.NewLine + part + " ";
            }
        }
    }
}
