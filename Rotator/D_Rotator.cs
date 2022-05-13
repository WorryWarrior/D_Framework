using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework
{
    public class D_Rotator : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 1f;

        [SerializeField] private bool rotateBetweenValues = false;

        [SerializeField] [D_NaughtyAttributes.ShowIf("rotateBetweenValues")]
        private RandomVector3Data rotationVectorData = null;

        [SerializeField] [D_NaughtyAttributes.ShowIf("rotateBetweenValues", inverted: true)]
        private Vector3 rotationVector = default;

        private Vector3 rotationValue;

        private void Awake()
        {
            rotationValue = rotateBetweenValues ? rotationVectorData.GetRandomValue() : rotationVector;
        }

        private void Update()
        {
            transform.Rotate(rotationValue * rotationSpeed * Time.deltaTime);
        }
    }
}