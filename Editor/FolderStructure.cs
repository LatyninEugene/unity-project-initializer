using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectInitializer
{
    [CreateAssetMenu(fileName = "Folder Structure", menuName = "Project Initializer/Folder Structure")]
    [Serializable]
    public class FolderStructure : ScriptableObject
    {

        public List<Folder> folders;

    }

    [Serializable]
    public class Folder
    {
        public string name;
        public List<string> subFolders;

    }
}