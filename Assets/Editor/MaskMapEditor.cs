using UnityEngine;
using UnityEditor;
namespace Dplds.Editor
{
    public class MaskMapEditor : EditorWindow
    {
        private Texture2D metallicTexture;
        private Texture2D occlusionTexture;
        private Texture2D detailTexture;
        private Texture2D smoothnessTexture;
        private bool metallicTemp;
        private bool detailTemp;
        private bool occlusionTemp;
        [MenuItem("Dplds/Textures Settings/Mask Map Generator")]
        public static void ShowWindow()
        {
            GetWindow<MaskMapEditor>("Mask Map Generator");
        }
        private void OnGUI()
        {
            GUILayout.Label("Mask Map Generator", EditorStyles.boldLabel);
            metallicTexture = (Texture2D)EditorGUILayout.ObjectField("Metallic Texture", metallicTexture, typeof(Texture2D), false);
            occlusionTexture = (Texture2D)EditorGUILayout.ObjectField("Occlusion Texture", occlusionTexture, typeof(Texture2D), false);
            detailTexture = (Texture2D)EditorGUILayout.ObjectField("Detail Texture", detailTexture, typeof(Texture2D), false);
            smoothnessTexture = (Texture2D)EditorGUILayout.ObjectField("Smoothness Texture", smoothnessTexture, typeof(Texture2D), false);
            if (GUILayout.Button("Generate Mask Map"))
            {
                if (metallicTexture == null)
                {
                    metallicTemp = true;
                    metallicTexture = CreateBlackTexture(smoothnessTexture.width, smoothnessTexture.height);
                }
                if (detailTexture == null)
                {
                    detailTemp = true;
                    detailTexture = CreateBlackTexture(smoothnessTexture.width, smoothnessTexture.height);
                }
                if (occlusionTexture == null)
                {
                    occlusionTemp = true;
                    occlusionTexture = CreateBlackTexture(smoothnessTexture.width, smoothnessTexture.height);
                }

                Texture2D maskMap = CreateMaskMap(metallicTexture, occlusionTexture, detailTexture, smoothnessTexture);
                SaveMaskMap(maskMap);
            }
        }
        private Texture2D CreateBlackTexture(int width, int height)
        {
            Texture2D blackTexture = new Texture2D(width, height);
            Color32[] pixels = new Color32[width * height];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.black;
            }
            
            blackTexture.SetPixels32(pixels);
            blackTexture.Apply();
            return blackTexture;
        }
        private Texture2D CreateMaskMap(Texture2D metallic, Texture2D occlusion, Texture2D detail, Texture2D smoothness)
        {
            int width = smoothness.width;
            int height = smoothness.height;
            Texture2D maskMap = new Texture2D(width, height, TextureFormat.RGBA32, false);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color metallicPixel = metallic.GetPixel(x, y);
                    Color occlusionPixel = occlusion.GetPixel(x, y);
                    Color detailPixel = detail.GetPixel(x, y);
                    Color smoothnessPixel = smoothness.GetPixel(x, y);
                    Color maskPixel = new Color(metallicPixel.r, occlusionPixel.g, detailPixel.b, smoothnessPixel.r);
                    maskMap.SetPixel(x, y, maskPixel);
                }
            }
            maskMap.Apply();
            return maskMap;
        }
        private void SaveMaskMap(Texture2D maskMap)
        {
            string path = EditorUtility.SaveFilePanel("Save Mask Map", "", "MaskMap", "png");
            if (path.Length > 0)
            {
                byte[] pngData = maskMap.EncodeToPNG();
                System.IO.File.WriteAllBytes(path, pngData);
                AssetDatabase.Refresh();
            }
            //Destroy temporary textures after saving the Mask Map
            if (metallicTemp)
                DestroyImmediate(metallicTexture);
            if (detailTemp)
                DestroyImmediate(detailTexture);
            if (occlusionTemp)
                DestroyImmediate(occlusionTexture);
        }
    }
}
