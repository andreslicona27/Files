namespace Ejercicio_2
{
    public partial class Form1 : Form
    {
        DirectoryInfo searchingDir;
        string searchingPhrase;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string path = txtPath.Text.Trim();
            searchingPhrase = txtPhrase.Text;

            try
            {
                if (path[0] == '%' && path[path.Length - 1] == '%')
                {
                    string variable = path.Substring(1, path.Length - 2);
                    path = Environment.GetEnvironmentVariable(variable);
                }

                searchingDir = new DirectoryInfo(path);
                StartSearch(searchingPhrase);
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

        private void StartSearch(string phrase)
        {
            
        }
    }
}