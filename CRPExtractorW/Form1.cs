using System;
using System.IO;
using System.Windows.Forms;
using CRPTools;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click( object sender, EventArgs e )
        {
            var dlg = new OpenFileDialog ();
            var result = dlg.ShowDialog();
            if ( result == DialogResult.OK )
            {
                textBox1.Text = dlg.FileName;
                if ( textBox2.Text == "" )
                    textBox2.Text = Path.Combine( new System.IO.FileInfo( textBox1.Text ).Directory.FullName, Path.GetFileNameWithoutExtension( textBox1.Text ) );
                textBox2.SelectionStart = textBox2.Text.Length;
                textBox2.SelectionLength = 0;
            }
        }

        private void button2_Click( object sender, EventArgs e )
        {
            var dlg = new FolderBrowserDialog ();
            var result = dlg.ShowDialog();
            if ( result == DialogResult.OK )
                textBox2.Text = dlg.SelectedPath;
        }

        private void button4_Click( object sender, EventArgs e )
        {
            try
            {
                if ( !CrpExporter.Export( textBox1.Text, textBox2.Text ) )
                    MessageBox.Show( this, "Unexpected Error while extracting CRP-File.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                else
                    MessageBox.Show( this, "Done.", "You did it!", MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
            catch (Exception ex)
            {
                MessageBox.Show( this, "Error while extracting CRP-File.\r\n" + ex.Message , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void button3_Click( object sender, EventArgs e )
        {
            this.Close();
        }

    }
}
