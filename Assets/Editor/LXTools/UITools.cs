using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


namespace LXTools
{
    public class LXUITools
    {
        [MenuItem("Tools/UI/图集引用数检测(先选中Prefab或GameObject)")]
        public static void CheckUISpriteRefCount()
        {
            HashSet<string> _spritePackingTags = new HashSet<string>();
            var go = Selection.activeGameObject;
            if (go != null)
            {
                var name = go.name;
                var components = go.GetComponentsInChildren(typeof(Image), true);
                if (components != null)
                {
                    foreach (var cmp in components)
                    {
                        var image = cmp as Image;
                        if (image && image.sprite)
                        {
                            var assetPath = AssetDatabase.GetAssetPath(image.sprite.texture);
                            if (!string.IsNullOrEmpty(assetPath))
                            {
                                var importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                                if (importer && !string.IsNullOrEmpty(importer.spritePackingTag))
                                {
                                    _spritePackingTags.Add(importer.spritePackingTag);
                                }
                            }
                        }
                    }
                }

                Debug.Log(string.Format("图集引用统计：<color=#ffff00>{0}</color> 包含 <color=#ffff00>{1}</color> 个图集引用", name, _spritePackingTags.Count));
                foreach (var tag in _spritePackingTags)
                {
                    Debug.Log(string.Format("{0}包含图集：<color=#ffff00>{1}</color>", name, tag));
                }
            }
            else
            {
                Debug.LogError("请选中Prefab或GameObject");
            }
        }


        [MenuItem("Tools/UI/检测未被引用的Sprite(不包含Dynamic)")]
        public static void Func()
        {
            Debug.Log("检测开始");

            HashSet<string> refGuids = new HashSet<string>();

            // string searchPath = "Assets/GameAssets/Prefabs/UI";
            // string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { searchPath, });
            // Debug.Log(string.Format("检测Prefab目录:Assets/GameAssets/Prefabs/UI, Prefab总数：<color=#ffff00>{0}</color>", guids.Length));
            // foreach (var guid in guids)
            // {
            //     string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            //     var prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject; //获取对应asset
            //     if (prefab)
            //     {
            //         var components = prefab.GetComponentsInChildren(typeof(Image), true);
            //         foreach (var cmp in components)
            //         {
            //             var image = cmp as Image;
            //             if (image && image.sprite)
            //             {
            //                 var spritePath = AssetDatabase.GetAssetPath(image.sprite);
            //                 var spriteGUID = AssetDatabase.AssetPathToGUID(spritePath);
            //                 refGuids.Add(spriteGUID);
            //             }
            //         }
            //     }
            // }

            //用prefab中的guid来判断是否引用更靠谱点，防止加载资源根据组件判断容易疏漏，且效率更快一些
            string searchPath = Application.dataPath + "/GameAssets/Prefabs/UI";
            string[] files = Directory.GetFiles(searchPath, "*.prefab", SearchOption.AllDirectories);
            Debug.Log(string.Format("检测Prefab目录:Assets/GameAssets/Prefabs/UI, Prefab总数：<color=#ffff00>{0}</color>", files.Length));

            foreach (var file in files)
            {
                string str = File.ReadAllText(file, System.Text.Encoding.Default);
                var matchs = Regex.Matches(str, "guid:.*?([a-z0-9]*),", RegexOptions.IgnoreCase);
                foreach (Match match in matchs)
                {
                    if (match != null && match.Groups != null && match.Groups.Count > 1)
                    {
                        var guid = match.Groups[1].Value;
                        refGuids.Add(guid);
                    }
                }
            }

            string[] guids = AssetDatabase.FindAssets("t:Sprite", new string[] { "Assets\\GameAssets\\Texture\\UIAtlas", });
            var exceptGuids = AssetDatabase.FindAssets("t:Sprite", new string[] { "Assets\\GameAssets\\Texture\\UIAtlas\\Dynamic", });

            var guidsHashSet = new HashSet<string>(guids);
            var totalCount = guidsHashSet.Count;
            guidsHashSet.ExceptWith(exceptGuids);

            guidsHashSet.ExceptWith(refGuids);

            Debug.Log(string.Format("统计: Sprite总数：<color=#ffff00>{0}</color> Dynamic剔除数：<color=#ffff00>{1}</color> 未被引用Sprite总数：<color=#ffff00>{2}</color>",
            totalCount, exceptGuids.Length, guidsHashSet.Count));


            foreach (var guid in guidsHashSet)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                Debug.Log(string.Format("未引用图片：<color=#ffff00>{0}</color>", path));
            }

            Debug.Log("检测完毕");
        }
    }
}