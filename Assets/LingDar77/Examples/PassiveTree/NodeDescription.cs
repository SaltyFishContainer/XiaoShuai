using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Lingdar77
{
    public class NodeDescription : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titile;
        [SerializeField] private TextMeshProUGUI description;
        public PassiveTreeVisualNode visualNode;

        public void Refresh()
        {
            if (visualNode == null)
            {
                return;
            }
            titile.text = visualNode.titile;
            description.text = visualNode.description;
        }
    }
}
