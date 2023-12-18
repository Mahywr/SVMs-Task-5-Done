using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SVM.VirtualMachine.Debug;

namespace Debugger
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {

        }
        public void Updatecode(List<string> codeLines)
        {

            ListCode.Items.Clear();
            foreach (var line in codeLines)
            {
                ListCode.Items.Add(line);
            }
        }


        public void Updatestack(Stack<object> stack)
        {

            ListStack.Items.Clear();
            foreach (var item in stack)
            {
                ListStack.Items.Add(item.ToString());
            }
        }

        private void Continue_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
