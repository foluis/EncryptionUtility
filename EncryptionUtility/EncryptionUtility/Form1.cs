using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace EncryptionUtility
{
    public partial class Form1 : Form
    {
        private short tag = 0;
        private string originaFileName = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            tag = Convert.ToInt16(((Button)sender).Tag);

            try
            {
                openFileDialog1.Filter = "Configuration File (.config)|*.config";
                openFileDialog1.Multiselect = false;
                DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    string file = openFileDialog1.FileName;
                    originaFileName = Path.GetFileName(openFileDialog1.FileName);                    

                    CopyFile(file, Path.GetDirectoryName(file));
                    Delete(file);

                    GenerateFile(Path.GetDirectoryName(file));

                    RenameFile(Path.GetDirectoryName(file));
                }
            }
            catch (Exception ex)
            {
                label1.Text = ex.ToString();
            }
        }

        private void CopyFile(string file, string copyFile)
        {
            File.Copy(file, copyFile + @"\web.config", true);
        }

        private void Delete(string file)
        {
            File.Delete(file);
        }

        private void RenameFile(string file)
        {            
            File.Move(file + @"\web.config", file + @"\" + originaFileName);
        }

        private void RenameBackFile(string file)
        {
            throw new NotImplementedException();
        }

        private void EncryptFile(string file)
        {
            throw new NotImplementedException();
        }

        

        

        private void DeleteFile(string file)
        {
            
        }

        private void GenerateFile(string connectionStringPath)
        {

            string fileName = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\aspnet_regiis.exe";

            if (8 == IntPtr.Size
                || (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                fileName = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\aspnet_regiis.exe";
            }

            string arguments = string.Empty;

            if (tag == 1)
            {
                arguments = $"-pef \"connectionStrings\" \"{connectionStringPath}\"";
            }
            else
            {
                arguments = $"-pdf \"connectionStrings\" \"{connectionStringPath}\"";
            }

            using (Process process = new Process())
            {
                process.EnableRaisingEvents = true;
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                label1.Text = output;
                //MessageBox.Show(output);
                string err = process.StandardError.ReadToEnd();
                //MessageBox.Show(err);
                process.WaitForExit();
            }
        }
    }
}
