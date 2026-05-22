using UnityEngine;

public class FolderTemplateEditor : MonoBehaviour
{
#if UNITY_EDITOR
    private static readonly string[] DefaultFolderTemplate =
    {
        "_Core",
        "_Core/Art",
        "_Core/Audio",
        "_Core/Code",
        "_Core/Code/Scripts",
        "_Core/Code/Scripts/Editor",
        "_Core/Code/Scripts/Editor/Level Editor",
        "_Core/Code/Scripts/Runtime",
        "_Core/Code/Scripts/Runtime/Data",
        "_Core/Code/Scripts/Runtime/Elements",
        "_Core/Code/Scripts/Runtime/Elements/Domain",
        "_Core/Code/Scripts/Runtime/Elements/Visual",
        "_Core/Code/Scripts/Runtime/System",
        "_Core/Code/Scripts/Runtime/System/Management",
        "_Core/Code/Scripts/Runtime/System/Creation",
        "_Core/Code/Scripts/Runtime/UI",
        "_Core/Code/Scripts/Runtime/Helper",
        "_Core/Code/Scripts/Runtime/Common",
        "_Core/Code/Shaders",
        "_Core/Prefabs",
        "_Core/Prefabs/Entities",
        "_Core/Prefabs/UI",
        "_Core/Scenes",
        "Presets",
        "Resources",
        "Resources/Data",
        "TrashStuff"
    };

    [UnityEditor.MenuItem("Tools/Folder Template/Generate Default Folders")]
    public static void GenerateDefaultFolders()
    {
        var createdCount = 0;
        var skippedCount = 0;

        foreach (var relativeFolder in DefaultFolderTemplate)
        {
            var assetPath = $"Assets/{relativeFolder}";

            if (UnityEditor.AssetDatabase.IsValidFolder(assetPath))
            {
                skippedCount++;
                continue;
            }

            var absolutePath = System.IO.Path.Combine(Application.dataPath, relativeFolder);
            System.IO.Directory.CreateDirectory(absolutePath);
            createdCount++;
        }

        UnityEditor.AssetDatabase.Refresh();
        Debug.Log($"Folder template generated. Created: {createdCount}, already existed: {skippedCount}.");
    }

    [ContextMenu("Generate Default Folders")]
    private void GenerateDefaultFoldersFromComponent()
    {
        GenerateDefaultFolders();
    }
#endif
}
