using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Michsky.UI.Dark
{
    public class VirtualCursor : PointerInputModule
    {
        [Header("OBJECTS")]
        public RectTransform border;
        public GameObject standardCursor;
        public GameObject circleCursor;
        public GameObject frameCursor;

        [Header("INPUT")]
        public EventSystem vEventSystem;
        public string horizontalAxis = "Horizontal";
        public string verticalAxis = "Vertical";

        [Header("SETTINGS")]
        [Tooltip("1000 equals 1.0 sensivity")]
        [Range(100, 10000)] public float speed = 1000f;
        public CursorType cursorType;

        private PointerEventData pointer;
        private GameObject selectedCursor;
        private Animator cursorAnim;

        Vector2 cursorPos;
        RectTransform cursorObj;

        public enum CursorType
        {
            STANDARD,
            CIRCLE,
            FRAME
        }

        public new void Start()
        {
            cursorObj = this.GetComponent<RectTransform>();
            pointer = new PointerEventData(vEventSystem);

            if (cursorType == CursorType.STANDARD)
            {
                standardCursor.SetActive(true);
                circleCursor.SetActive(false);
                frameCursor.SetActive(false);

                selectedCursor = standardCursor;
            }

            else if (cursorType == CursorType.CIRCLE)
            {
                standardCursor.SetActive(false);
                circleCursor.SetActive(true);
                frameCursor.SetActive(false);

                selectedCursor = circleCursor;
            }

            else if (cursorType == CursorType.FRAME)
            {
                standardCursor.SetActive(false);
                circleCursor.SetActive(false);
                frameCursor.SetActive(true);

                selectedCursor = frameCursor;
            }

            cursorAnim = selectedCursor.GetComponent<Animator>();
        }

        public void AnimateCursorIn()
        {
            if (gameObject.activeSelf == true)
            {
                cursorAnim.Play("In");
            }
        }

        public void AnimateCursorOut()
        {
            if (gameObject.activeSelf == true)
            {
                cursorAnim.Play("Out");
            }
        }

        void Update()
        {
            cursorPos.x += Input.GetAxis(horizontalAxis) * speed * Time.deltaTime;
            cursorPos.x = Mathf.Clamp(cursorPos.x, -+border.rect.width / 2, border.rect.width / 2);

            cursorPos.y += Input.GetAxis(verticalAxis) * speed * Time.deltaTime;
            cursorPos.y = Mathf.Clamp(cursorPos.y, -+border.rect.height / 2, border.rect.height / 2);

            cursorObj.anchoredPosition = cursorPos;
        }

        public override void Process()
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(cursorObj.transform.position);

            pointer.position = screenPos;
            eventSystem.RaycastAll(pointer, this.m_RaycastResultCache);
            RaycastResult raycastResult = FindFirstRaycast(this.m_RaycastResultCache);
            pointer.pointerCurrentRaycast = raycastResult;
            this.ProcessMove(pointer);

            if (Input.GetButtonDown("Submit"))
            {
                pointer.pressPosition = cursorPos;
                pointer.clickTime = Time.unscaledTime;
                pointer.pointerPressRaycast = raycastResult;

                if (this.m_RaycastResultCache.Count > 0)
                {
                    pointer.selectedObject = raycastResult.gameObject;
                    pointer.pointerPress = ExecuteEvents.ExecuteHierarchy(raycastResult.gameObject, pointer, ExecuteEvents.submitHandler);
                    pointer.rawPointerPress = raycastResult.gameObject;
                }

                else
                {
                    pointer.rawPointerPress = null;
                }
            }

            else
            {
                pointer.pointerPress = null;
                pointer.rawPointerPress = null;
            }
        }
    }
}