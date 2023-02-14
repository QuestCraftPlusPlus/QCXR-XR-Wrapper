using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace HurricaneVR.Editor
{
    public static class LayerMaskDrawer
    {

        public static int LayerMaskField(string label, int layermask)
        {
            return FieldToLayerMask(EditorGUILayout.MaskField(label, LayerMaskToField(layermask),
                InternalEditorUtility.layers));
        }

        public static int LayerMaskField(Rect position, string label, int layermask)
        {
            return FieldToLayerMask(EditorGUI.MaskField(position, label, LayerMaskToField(layermask),
                InternalEditorUtility.layers));
        }

        /// <summary>
        /// Converts field LayerMask values to in game LayerMask values
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private static int FieldToLayerMask(int field)
        {
            if (field == -1) return -1;
            int mask = 0;
            var layers = InternalEditorUtility.layers;
            for (int c = 0; c < layers.Length; c++)
            {
                if ((field & (1 << c)) != 0)
                {
                    mask |= 1 << LayerMask.NameToLayer(layers[c]);
                }
                else
                {
                    mask &= ~(1 << LayerMask.NameToLayer(layers[c]));
                }
            }

            return mask;
        }

        /// <summary>
        /// Converts in game LayerMask values to field LayerMask values
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        private static int LayerMaskToField(int mask)
        {
            if (mask == -1) return -1;
            int field = 0;
            var layers = InternalEditorUtility.layers;
            for (int c = 0; c < layers.Length; c++)
            {
                if ((mask & (1 << LayerMask.NameToLayer(layers[c]))) != 0)
                {
                    field |= 1 << c;
                }
            }

            return field;
        }
    }
}