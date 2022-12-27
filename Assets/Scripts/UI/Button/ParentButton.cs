using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentButton : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> children;

    [SerializeField]
    private ParentButton otherButton;

    bool isActive = false;

    public void PushParentButton()
    {
        this.SetActive(!this.isActive);
        otherButton.SetActive(false);
    }

    public void SetActive(bool active)
    {
        if (this.isActive == active)
            return;

        this.isActive = active;
        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetActive(active);
        }
    }
}
