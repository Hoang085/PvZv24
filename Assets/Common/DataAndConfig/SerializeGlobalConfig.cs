using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEditor;
#endif
using UnityEngine;

namespace  EazyEngine.Core
{
  public abstract class BaseGlobalConfig : ScriptableObject
  {
    
  }

    public class GlobalConfig<T> : BaseGlobalConfig,ISerializationCallbackReceiver where T : GlobalConfig<T>
    {
      
    public static T instance;
#if UNITY_EDITOR
      private static GlobalConfigAttribute configAttribute;
    static  string AssetPathWithAssetsPrefix(GlobalConfigAttribute configAtr)
    {
          
      string assetPath = configAtr.AssetPath;
      return assetPath.StartsWith("Assets/") ? assetPath : "Assets/" + assetPath;
            
    }

    static   string AssetPathWithoutAssetsPrefix(GlobalConfigAttribute configAtr)
    {
            
      string assetPath = configAtr.AssetPath;
      return assetPath.StartsWith("Assets/") ? assetPath.Substring("Assets/".Length) : assetPath;
            
    }
    private static GlobalConfigAttribute ConfigAttribute
    {
      get
      {
        if (GlobalConfig<T>.configAttribute == null)
        {
          GlobalConfig<T>.configAttribute = typeof (T).GetCustomAttribute<GlobalConfigAttribute>();
          if (GlobalConfig<T>.configAttribute == null)
            GlobalConfig<T>.configAttribute = new GlobalConfigAttribute(TypeExtensions.GetNiceName(typeof (T)));
        }
        return GlobalConfig<T>.configAttribute;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this instance has instance loaded.
    /// </summary>
    public static bool HasInstanceLoaded
    {
      get
      {
        return (UnityEngine.Object) GlobalConfig<T>.instance != (UnityEngine.Object) null;
      }
    }
#endif
  

      /// <summary>Gets the singleton instance.</summary>
    public static T Instance
    {
      get
      {
        #if UNITY_EDITOR
        if ((UnityEngine.Object) GlobalConfig<T>.instance == (UnityEngine.Object) null)
        {
          GlobalConfig<T>.LoadInstanceIfAssetExists();
          T instance = GlobalConfig<T>.instance;
          string str = Application.dataPath + "/" + GlobalConfig<T>.ConfigAttribute.AssetPath + TypeExtensions.GetNiceName(typeof (T)) + ".asset";
          if ((UnityEngine.Object) instance == (UnityEngine.Object) null && EditorPrefs.HasKey("PREVENT_SIRENIX_FILE_GENERATION"))
          {
            Debug.LogWarning((object) (GlobalConfig<T>.ConfigAttribute.AssetPath + TypeExtensions.GetNiceName(typeof (T)) + ".asset was prevented from being generated because the PREVENT_SIRENIX_FILE_GENERATION key was defined in Unity's EditorPrefs."));
            GlobalConfig<T>.instance = ScriptableObject.CreateInstance<T>();
            return GlobalConfig<T>.instance;
          }
          if ((UnityEngine.Object) instance == (UnityEngine.Object) null && File.Exists(str) && EditorSettings.serializationMode == SerializationMode.ForceText)
          {
            if (AssetScriptGuidUtility.TryUpdateAssetScriptGuid(str, typeof (T)))
            {
              Debug.Log((object) "Could not load config asset at first, but successfully detected forced text asset serialization, and corrected the config asset m_Script guid.");
              GlobalConfig<T>.LoadInstanceIfAssetExists();
              instance = GlobalConfig<T>.instance;
            }
            else
              Debug.LogWarning((object) "Could not load config asset, and failed to auto-correct config asset m_Script guid.");
          }
          if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
          {
            instance = ScriptableObject.CreateInstance<T>();
            if (!Directory.Exists(AssetPathWithAssetsPrefix(GlobalConfig<T>.ConfigAttribute)))
            {
              Directory.CreateDirectory(
                new DirectoryInfo(AssetPathWithAssetsPrefix(GlobalConfig<T>.ConfigAttribute))
                  .FullName);
              AssetDatabase.Refresh();
            }

            string niceName = TypeExtensions.GetNiceName(typeof (T));
            string path = !GlobalConfig<T>.ConfigAttribute.AssetPath.StartsWith("Assets/") ? "Assets/" + GlobalConfig<T>.ConfigAttribute.AssetPath + niceName + ".asset" : GlobalConfig<T>.ConfigAttribute.AssetPath + niceName + ".asset";
            if (File.Exists(str))
            {
              Debug.LogWarning((object) ("Could not load config asset of type " + niceName + " from project path '" + path + "', but an asset file already exists at the path, so could not create a new asset either. The config asset for '" + niceName + "' has been lost, probably due to an invalid m_Script guid. Set forced text serialization in Edit -> Project Settings -> Editor -> Asset Serialization -> Mode and trigger a script reload to allow Odin to auto-correct this."));
            }
            else
            {
              AssetDatabase.CreateAsset((UnityEngine.Object) instance, path);
              AssetDatabase.SaveAssets();
              GlobalConfig<T>.instance = instance;
              instance.OnConfigAutoCreated();
              EditorUtility.SetDirty((UnityEngine.Object) instance);
              AssetDatabase.SaveAssets();
              AssetDatabase.Refresh();
            }
          }
          GlobalConfig<T>.instance = instance;
        }
        #endif
        return GlobalConfig<T>.instance;
      }
    }
#if UNITY_EDITOR
    /// <summary>Tries to load the singleton instance.</summary>
    public static void LoadInstanceIfAssetExists()
    {
      if (GlobalConfig<T>.ConfigAttribute.IsInResourcesFolder)
      {
        GlobalConfig<T>.instance = Resources.Load<T>(GlobalConfig<T>.ConfigAttribute.ResourcesPath + TypeExtensions.GetNiceName(typeof (T)));
      }
      else
      {
        string niceName = TypeExtensions.GetNiceName(typeof (T));
        GlobalConfig<T>.instance = AssetDatabase.LoadAssetAtPath<T>(GlobalConfig<T>.ConfigAttribute.AssetPath + niceName + ".asset");
        if ((UnityEngine.Object) GlobalConfig<T>.instance == (UnityEngine.Object) null)
          GlobalConfig<T>.instance = AssetDatabase.LoadAssetAtPath<T>("Assets/" + GlobalConfig<T>.ConfigAttribute.AssetPath + niceName + ".asset");
      }
      if (!((UnityEngine.Object) GlobalConfig<T>.instance == (UnityEngine.Object) null))
        return;
      string[] assets = AssetDatabase.FindAssets("t:" + typeof (T).Name);
      if (assets.Length == 0)
        return;
      GlobalConfig<T>.instance = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(assets[0]));
    }

    /// <summary>
    /// Opens the config in a editor window. This is currently only used internally by the Sirenix.OdinInspector.Editor assembly.
    /// </summary>
    public void OpenInEditor()
    {
      System.Type type = (System.Type) null;
      try
      {
        Assembly assembly1 = (Assembly) null;
        foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
        {
          if (assembly2.GetName().Name == "Sirenix.OdinInspector.Editor")
          {
            assembly1 = assembly2;
            break;
          }
        }
        if (assembly1 != null)
          type = assembly1.GetType("Sirenix.OdinInspector.Editor.SirenixPreferencesWindow");
      }
      catch
      {
      }
      if (type != null)
        ((IEnumerable<MethodInfo>) type.GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>) (x => x.Name == "OpenWindow" && x.GetParameters().Length == 1)).First<MethodInfo>().Invoke((object) null, new object[1]
        {
          (object) this
        });
      else
        Debug.LogError((object) "Failed to open window, could not find Sirenix.OdinInspector.Editor.SirenixPreferencesWindow");
    }
#endif
    protected virtual void OnConfigAutoCreated()
    {
    }

    public void OnBeforeSerialize()
    {
    }

      public virtual void OnAfterDeserialize()
      {
         GlobalConfig<T>.instance = (T)this;
      }
    }

}
