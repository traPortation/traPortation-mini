using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentButton : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> children;

    public void PushParentButton()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetActive(!children[i].activeSelf);
        }
    }
}
