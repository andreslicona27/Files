using System.Diagnostics;
using System.IO;

namespace Ejercicio_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DirectoryInfo ongoingDir;
        Dictionary<string, DirectoryInfo> directoriesDictionary = new Dictionary<string, DirectoryInfo> { };
        Dictionary<string, FileInfo> filesDictionary = new Dictionary<string, FileInfo> { };
        private void btnPath_Click(object sender, EventArgs e)
        {
            string path = txtPath.Text.Trim();
   
            try
            {
                if (path[0] == '%' && path[path.Length - 1] == '%')
                {
                    string variable = path.Substring(1, path.Length - 2);
                    path = Environment.GetEnvironmentVariable(variable);
                }

                ongoingDir = new DirectoryInfo(path);
                ShowInfo();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Error", "You dont have permision to access this path informaiton", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Error", "You have to add a valid path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error", "There has been a problem with the path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowInfo()
        {
            DirectoryInfo[] subDirectories = ongoingDir.GetDirectories();
            FileInfo[] files = ongoingDir.GetFiles();
            bool directoryIsParent = ongoingDir.Parent != null;
            bool hasSubdirectories = subDirectories.Length > 0;

            lbFiles.Items.Clear();
            lbDirectories.Items.Clear();

            if (directoryIsParent)
            {
                directoriesDictionary.Add("^", new DirectoryInfo(ongoingDir.Parent.FullName));
            }
            else if (hasSubdirectories)
            {
                Array.ForEach(subDirectories, sub => directoriesDictionary.Add(sub.Name, sub));
            }

            lbDirectories.Items.AddRange(directoriesDictionary.Keys.ToArray());

            filesDictionary.Clear();
            Array.ForEach(files, f => filesDictionary.Add(f.Name, f));
            if (files.Length > 0)
            {
                lbFiles.Items.AddRange(filesDictionary.Keys.ToArray());
            }

        }

        private void lbDirectories_SelectedIndexChanged(object sender, EventArgs e)
        {
            DirectoryInfo previousPath = ongoingDir;
            try
            {
                if (lbDirectories.SelectedItem != null)
                {
                    txtPath.Text = ongoingDir.FullName;
                    ShowInfo();
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Error", "You dont have permision to access this path informaiton", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error", "There has been a problem with the path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbFiles_SelectedValueChanged(object sender, EventArgs e)
        {

            if (filesDictionary[lbFiles.SelectedItem.ToString()].Extension == ".txt")
            {
                ShowingFile fileContent = new ShowingFile(filesDictionary[lbFiles.SelectedItem.ToString()].FullName);
                fileContent.Text = lbFiles.SelectedItem.ToString();
                if (fileContent.ShowDialog() == DialogResult.Yes)
                {
                    string newContent = fileContent.content;
                    if (MessageBox.Show(this, "The file has some changes\nDo you want to saved them?", "Modifications", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        File.WriteAllText(filesDictionary[lbFiles.SelectedItem.ToString()].FullName, newContent);
                    }
                }
            }
        }
    }
}