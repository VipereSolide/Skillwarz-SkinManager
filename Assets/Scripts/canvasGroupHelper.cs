using System.Collections;
using UnityEngine;

namespace FeatherLight.Pro
{
    public static class CanvasGroupHelper
    {
        public static CanvasGroup InstanceCanvasGroup;

        /// <summary>
        /// Disables or Enables a canvasGroup by setting it's alpha to 0 and by setting the "interactable" & "blocksRaycasts" values to false or true.
        /// </summary>
        /// <param name="_group"> The affected CanvasGroup. </param>
        /// <param name="_value"> The active value. </param>
        public static void SetActive(CanvasGroup _group, bool value)
        {
            if (value)
            {
                _group.alpha = 1;
            }
            else
            {
                _group.alpha = 0;
            }

            _group.interactable = value;
            _group.blocksRaycasts = value;
        }

        /// <summary>
        /// Disables or Enables a canvasGroup by setting it's alpha to 0 and by setting the "interactable" & "blocksRaycasts" values to false or true.
        /// </summary>
        /// <param name="_value"> The active value. </param>
        public static void SetActive(bool value)
        {
            if (InstanceCanvasGroup == null)
            {
                Debug.LogError("Couldn't find the InstanceCanvasGroup. Please assign the InstanceCanvasGroup or provide a valid CanvasGroup.");
                return;
            }

            if (value)
            {
                InstanceCanvasGroup.alpha = 1;
            }
            else
            {
                InstanceCanvasGroup.alpha = 0;
            }

            InstanceCanvasGroup.interactable = value;
            InstanceCanvasGroup.blocksRaycasts = value;
        }

        /// <summary>
        /// Lerps the alpha of a canvasGroup to 0 or 1 upon a given time.
        /// </summary>
        /// <param name="_group"> The affected canvasGroup. </param>
        /// <param name="value"> Decides if it'll be lerped to 0 or 1. </param>
        /// <param name="overTime"> The time it takes to lerp it. </param>
        public static IEnumerator Fade(CanvasGroup _group, bool value, float overTime)
        {
            float startTime = Time.time;

            while (Time.time < startTime + overTime)
            {
                _group.alpha = Mathf.Lerp( (value) ? 0 : 1 , (value) ? 1 : 0, (Time.time - startTime)/overTime);
                yield return null;
            }
            
            CanvasGroupHelper.SetActive(_group, value);
        }


        /// <summary>
        /// Lerps the alpha of a canvasGroup to 0 or 1 upon a given time.
        /// </summary>
        /// <param name="value"> Decides if it'll be lerped to 0 or 1. </param>
        /// <param name="overTime"> The time it takes to lerp it. </param>
        public static IEnumerator Fade(bool value, float overTime)
        {
            CanvasGroup _group = InstanceCanvasGroup;

            if (_group == null)
            {
                Debug.LogError("Couldn't find the InstanceCanvasGroup. Please assign the InstanceCanvasGroup or provide a valid CanvasGroup.");
                yield return null;
            }

            float startTime = Time.time;
            
            while (Time.time < startTime + overTime)
            {
                _group.alpha = Mathf.Lerp( (value) ? 0 : 1 , (value) ? 1 : 0, (Time.time - startTime)/overTime);
                yield return null;
            }
            
            CanvasGroupHelper.SetActive(_group, value);
        }
    }
}