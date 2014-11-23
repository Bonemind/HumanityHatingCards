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

    public float TopMargin = 0f;
    public float SideMargins = 0f;

    /// <summary>
    /// Wrap the text on start
    /// </summary>
    void Start()
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

        //Calculate how wide a line can be
        float rowLimit = parent.renderer.bounds.extents.x - SideMargins; //find the sweet spot    

        //Wrap the text
        string builder = "";
        textMesh.text = "";
        string text = "This is some text we'll use to demonstrate word wrapping. It would be too easy if a proper wrapping was already implemented in Unity :)";
        string[] parts = text.Split(' ');
        for (int i = 0; i < parts.Length; i++)
        {
            textMesh.text += parts[i] + " ";
            if (textMesh.renderer.bounds.extents.x > rowLimit)
            {
                textMesh.text = builder.TrimEnd() + System.Environment.NewLine + parts[i] + " ";
            }
            builder = textMesh.text;
        }

    }
}
