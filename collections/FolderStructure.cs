using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;

namespace _0xNotepad.collections
{
    public class Folder
    {
        public string Name { get; set; }
        public List<Folder> SubFolders { get; set; }
        public List<string> Files { get; set; }
    }

    public static class FolderStructureManager
    {
   
        public static TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeViewItem { Header = directoryInfo.Name };

            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryNode.Items.Add(CreateDirectoryNode(directory));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                TreeViewItem fileNode = new TreeViewItem();
                fileNode.Header = file.Name;
                fileNode.Tag = file;

                directoryNode.Items.Add(fileNode);
            }

            return directoryNode;
        }
    }
}
