using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace Shared
{
  

        public static class clsImageHelper
        {

        public static string GenerateGuid()
        {
            
        return Guid.NewGuid().ToString();
        }
        public static string ReplaceFileNameWithGuid(string SourceFile)
        {
            string FileName = SourceFile;
            FileInfo fi = new FileInfo(FileName);
            string extn = fi.Extension;
            return GenerateGuid() + extn;
        }
     public static bool CreateFolderIfDoesNotExist(string FolderPath)
    {
        try
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool CopyImageToProjectImagesFolder(ref string SourceFile)
            {

            string BasePath = AppDomain.CurrentDomain.BaseDirectory;

            string ProjectDirectory = Path.GetFullPath(Path.Combine(BasePath, @"..\..\"));

            
            string DestinationFolder = Path.Combine(ProjectDirectory, "Images\\");

            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }
            string destinationFile = DestinationFolder + ReplaceFileNameWithGuid(SourceFile);
            try
            {
                File.Copy(SourceFile, destinationFile, true);
                SourceFile = destinationFile;
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                
            }
            return true;
        }

        }
    }

