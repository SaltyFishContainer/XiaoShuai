using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    GameObject mine;
    // Start is called before the first frame update
    void Start()
    {
        mine = this.gameObject;
    }

    public void ScaleUp() {
        this.transform.localPosition = this.transform.localPosition + new Vector3(0, 2, 0);
        this.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
    }
    public void ScaleDown() {
        this.transform.localPosition = this.transform.localPosition - new Vector3(0, 2, 0);
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
