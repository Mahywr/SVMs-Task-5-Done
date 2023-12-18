using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVM.VirtualMachine.Debug; 

namespace Debugger 
{
    public class Debugger : IDebugger
    {
        private SvmVirtualMachine virtualMachine;
        private DebugForm debugForm;

        public Debugger()
        {
            debugForm = new DebugForm();
        }

        public void Break(IDebugFrame debugFrame)
        {
            List<string> codeLines = debugFrame.CodeFrame
                                     .Select(instruction => instruction.ToString())
                                     .ToList();

            debugForm.Updatecode(codeLines);
            debugForm.Updatestack(virtualMachine.Stack);
            debugForm.ShowDialog();
        }

        public SvmVirtualMachine VirtualMachine
        {
            set { virtualMachine = value; }
        }
    }
}
