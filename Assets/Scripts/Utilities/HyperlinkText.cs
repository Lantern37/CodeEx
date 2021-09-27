using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Utilities
{
    /// <summary>
    /// Text control, support hyperlink
    /// </summary>
    public class HyperlinkText : Text, IPointerClickHandler
    {
        /// <summary>
        /// Hyperlink information
        /// </summary>
        private class HyperlinkInfo
        {
            public int startIndex;

            public int endIndex;

            public string name;

            public readonly List<Rect> boxes = new List<Rect>();
        }

        /// <summary>
        /// After parsing the final text
        /// </summary>
        private string m_OutputText;

        /// <summary>
        /// Hyperlink information list
        /// </summary>
        private readonly List<HyperlinkInfo> m_HrefInfos = new List<HyperlinkInfo>();

        /// <summary>
        /// Text constructor
        /// </summary>
        protected static readonly StringBuilder s_TextBuilder = new StringBuilder();

        [Serializable]
        public class HrefClickEvent : UnityEvent<string>
        {
        }

        [SerializeField] private HrefClickEvent m_OnHrefClick = new HrefClickEvent();

        /// <summary>
        /// Hyperlink click event
        /// </summary>
        public HrefClickEvent onHrefClick
        {
            get { return m_OnHrefClick; }
            set { m_OnHrefClick = value; }
        }


        /// <summary>
        /// Hyperlink regular
        /// </summary>
        private static readonly Regex s_HrefRegex =
            new Regex(@"<a href=([^>\n\s]+)>(.*?)(</a>)", RegexOptions.Singleline);

        private HyperlinkText mHyperlinkText;


        public string GetHyperlinkInfo
        {
            get { return text; }
        }

        protected override void Awake()
        {
            base.Awake();
            mHyperlinkText = GetComponent<HyperlinkText>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            mHyperlinkText.onHrefClick.AddListener(OnHyperlinkTextInfo);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            mHyperlinkText.onHrefClick.RemoveListener(OnHyperlinkTextInfo);
        }


        public override void SetVerticesDirty()
        {
            base.SetVerticesDirty();
#if UNITY_EDITOR
            if (UnityEditor.PrefabUtility.GetPrefabType(this) == UnityEditor.PrefabType.Prefab)
            {
                return;
            }
#endif
            //  m_OutputText = GetOutputText(text);
            text = GetHyperlinkInfo;
            m_OutputText = GetOutputText(text);

        }

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            var orignText = m_Text;
            m_Text = m_OutputText;
            base.OnPopulateMesh(toFill);
            m_Text = orignText;
            UIVertex vert = new UIVertex();

            // Handle hyperlink bounding box
            foreach (var hrefInfo in m_HrefInfos)
            {
                hrefInfo.boxes.Clear();
                if (hrefInfo.startIndex >= toFill.currentVertCount)
                {
                    continue;
                }

                // Add the index coordinates of the text vertex in the hyperlink to the bounding box
                toFill.PopulateUIVertex(ref vert, hrefInfo.startIndex);
                var pos = vert.position;
                var bounds = new Bounds(pos, Vector3.zero);
                for (int i = hrefInfo.startIndex, m = hrefInfo.endIndex; i < m; i++)
                {
                    if (i >= toFill.currentVertCount)
                    {
                        break;
                    }

                    toFill.PopulateUIVertex(ref vert, i);
                    pos = vert.position;
                    if (pos.x < bounds.min.x) // wrap the line and re-add the bounding box
                    {
                        hrefInfo.boxes.Add(new Rect(bounds.min, bounds.size));
                        bounds = new Bounds(pos, Vector3.zero);
                    }
                    else
                    {
                        bounds.Encapsulate(pos); // Expand the bounding box
                    }
                }

                hrefInfo.boxes.Add(new Rect(bounds.min, bounds.size));
            }
        }

        /// <summary>
        /// Get the final output text after the hyperlink is parsed
        /// </summary>
        /// <returns></returns>
        protected virtual string GetOutputText(string outputText)
        {
            s_TextBuilder.Length = 0;
            m_HrefInfos.Clear();
            var indexText = 0;
            foreach (Match match in s_HrefRegex.Matches(outputText))
            {
                s_TextBuilder.Append(outputText.Substring(indexText, match.Index - indexText));
                s_TextBuilder.Append("<color=blue>"); // Hyperlink color

                var group = match.Groups[1];
                var hrefInfo = new HyperlinkInfo
                {
                    startIndex = s_TextBuilder.Length * 4, // The starting vertex index of the text in the hyperlink
                    endIndex = (s_TextBuilder.Length + match.Groups[2].Length - 1) * 4 + 3,
                    name = group.Value
                };
                m_HrefInfos.Add(hrefInfo);

                s_TextBuilder.Append(match.Groups[2].Value);
                s_TextBuilder.Append("</color>");
                indexText = match.Index + match.Length;
            }

            s_TextBuilder.Append(outputText.Substring(indexText, outputText.Length - indexText));
            return s_TextBuilder.ToString();
        }

        /// <summary>
        /// Click event to detect whether the hyperlink text is clicked
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            Vector2 lp = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position,
                eventData.pressEventCamera, out lp);

            foreach (var hrefInfo in m_HrefInfos)
            {
                var boxes = hrefInfo.boxes;
                for (var i = 0; i < boxes.Count; ++i)
                {
                    if (boxes[i].Contains(lp))
                    {
                        m_OnHrefClick.Invoke(hrefInfo.name);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// The current click on the hyperlink callback
        /// </summary>
        /// <param name="info">callback information</param>
        private void OnHyperlinkTextInfo(string info)
        {
            Debug.Log("Hyperlink Information:" + info);
        }
    }
}