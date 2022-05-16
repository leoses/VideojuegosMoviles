using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlowFreeGame.Menu
{
    public class ContentScrollScript : MonoBehaviour
    {
        [SerializeField] private Text dimensionsText;

        public Text GetDimensionsText()
        {
            return dimensionsText;
        }
    }
}