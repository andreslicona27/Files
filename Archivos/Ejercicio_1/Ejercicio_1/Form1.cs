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
                MessageBox.Show("You dont have permision to access this path informaiton", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You have to add a valid path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("That directory does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("There has been a problem with the path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowInfo()
        {
            DirectoryInfo[] subDirectories = ongoingDir.GetDirectories();
            FileInfo[] files = ongoingDir.GetFiles();

            lbFiles.Items.Clear();
            lbDirectories.Items.Clear();
            directoriesDictionary.Clear();

            if (ongoingDir.Parent != null)
            {
                directoriesDictionary.Add("^", new DirectoryInfo(ongoingDir.Parent.FullName));
            }
            else if (subDirectories.Length > 0)
            {
                Array.ForEach(subDirectories, sub => directoriesDictionary.Add(sub.Name, sub));
            }

            foreach (string d in directoriesDictionary.Keys)
            {
                lbDirectories.Items.Add(d);
            }
            //lbDirectories.Items.AddRange(directoriesDictionary.Keys.ToArray());

            filesDictionary.Clear();
            Array.ForEach(files, f => filesDictionary.Add(f.Name, f));
            if (files.Length > 0)
            {
                foreach(string f in filesDictionary.Keys)
                {
                    lbFiles.Items.Add(f);
                }
                //lbFiles.Items.AddRange(filesDictionary.Keys.ToArray());
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
                MessageBox.Show("You dont have permision to access this path informaiton", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ongoingDir = previousPath;
                ShowInfo();
            }
            catch (Exception)
            {
                MessageBox.Show("There has been a problem with the path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ongoingDir = previousPath;
                ShowInfo();
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