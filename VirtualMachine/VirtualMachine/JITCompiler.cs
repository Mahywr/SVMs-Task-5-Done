namespace SVM.VirtualMachine;

#region Using directives
using System.Reflection;
#endregion

/// <summary>
/// Utility class which generates compiles a textual representation
/// of an SML instruction into an executable instruction instance
/// </summary>
internal static class JITCompiler
{
    #region Constants
    #endregion

    #region Fields
    #endregion

    #region Constructors
    #endregion

    #region Properties
    #endregion

    #region Public methods
    #endregion

    #region Non-public methods
    internal static IInstruction CompileInstruction(string opcode)
    {
        IInstruction instruction = null;

        #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
        #endregion
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass && typeof(IInstruction).IsAssignableFrom(type) &&
                    string.Equals(type.Name, opcode, StringComparison.OrdinalIgnoreCase))
                {
                    return (IInstruction)Activator.CreateInstance(type);
                }
            }
        }

        throw new SvmCompilationException($"No matching instruction found for opcode: {opcode}");
        
    }

    internal static IInstruction CompileInstruction(string opcode, params string[] operands)
    {
        IInstructionWithOperand instruction = null;

        #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
        #endregion

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass && typeof(IInstructionWithOperand).IsAssignableFrom(type) &&
                    string.Equals(type.Name, opcode, StringComparison.OrdinalIgnoreCase))
                {
                    var instructionWithOperand = (IInstructionWithOperand)Activator.CreateInstance(type);

                    if (operands != null && operands.Length > 0)
                    {
                        instructionWithOperand.Operands = operands;
                    }

                    return instructionWithOperand;
                }
            }
        }

        throw new SvmCompilationException($"No matching instruction with operands found for opcode: {opcode}");
    }
}
    #endregion

