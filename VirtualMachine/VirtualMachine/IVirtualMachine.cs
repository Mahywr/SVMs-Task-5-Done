using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVM.VirtualMachine
{
    public interface IVirtualMachine
    {
        // Stack for storing data - assuming it stores objects for flexibility
        Stack<object> Stack { get; }

        // Program counter to know the current instruction's position
        int ProgramCounter { get; }

        // You can add other methods or properties that your instructions might require
        // For example, methods to handle program flow, error reporting, etc.
    }
}
