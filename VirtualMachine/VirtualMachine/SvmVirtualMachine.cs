using Microsoft.VisualBasic.ApplicationServices;
using SVM.VirtualMachine.Debug;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

/// <summary>
/// Implements the Simple Virtual Machine (SVM) virtual machine 
/// </summary>
public sealed class SvmVirtualMachine : IVirtualMachine
{
    #region Constants
    private const string CompilationErrorMessage = "An SVM compilation error has occurred at line {0}.\r\n\r\n{1}\r\n";
    private const string RuntimeErrorMessage = "An SVM runtime error has occurred.\r\n\r\n{0}\r\n";
    private const string InvalidOperandsMessage = "The instruction \r\n\r\n\t{0}\r\n\r\nis invalid because there are too many operands. An instruction may have no more than one operand.";
    private const string InvalidLabelMessage = "Invalid label: the label {0} at line {1} is not associated with an instruction.";
    private const string ProgramCounterMessage = "Program counter violation; the program counter value is out of range";
    #endregion

    #region Fields
    private IDebugger debugger = null;
    private List<IInstruction> program = new List<IInstruction>();
    private Stack<object> stack = new Stack<object>();
    private int programCounter = 0;
    #endregion

    #region Constructors
    private HashSet<int> breakpoints = new HashSet<int>();
    private bool IsBreakpoint(int programCounter)
    {
        
        return breakpoints.Contains(programCounter);
    }
    private IDebugFrame CreateDebugFrame(int currentInstructionIndex)
    {
       
        var frame = new List<IInstruction>();
        int start = Math.Max(0, currentInstructionIndex - 4);
        int end = Math.Min(program.Count - 1, currentInstructionIndex + 4);

        for (int i = start; i <= end; i++)
        {
            frame.Add(program[i]);
        }

        return new DebugFrame(program[currentInstructionIndex], frame);
    }

    public SvmVirtualMachine(string filepath)
    {
        try
        {
            Compile(filepath);
        }
        catch (SvmCompilationException ex)
        {
            Console.WriteLine($"Compilation error: {ex.Message}. SVM is exiting.");
            return;
        }

        
        try
        {
            string debuggerPath = @"C:\Users\Asus\Desktop\CW\SVM\bin\Debug\Debugger.dll"; // Modify this with the correct path
            Assembly debuggerAssembly = Assembly.LoadFrom(debuggerPath);
            Type debuggerType = debuggerAssembly.GetType("Debugger.Debugger");
            if (debuggerType == null)
            {
                throw new InvalidOperationException("Debugger type not found in assembly.");
            }

            object debuggerInstance = Activator.CreateInstance(debuggerType);
            PropertyInfo vmProperty = debuggerType.GetProperty("VirtualMachine");
            if (vmProperty == null || !vmProperty.CanWrite)
            {
                throw new InvalidOperationException("VirtualMachine property not found or not writable.");
            }

            vmProperty.SetValue(debuggerInstance, this, null);
            this.debugger = debuggerInstance as IDebugger;
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Debugger DLL not found: {ex.Message}");
        }
        catch (TypeLoadException ex)
        {
            Console.WriteLine($"Error loading debugger type: {ex.Message}");
        }
        catch (TargetInvocationException ex)
        {
            Console.WriteLine($"Error creating debugger instance: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred while loading the debugger: {ex.Message}");
        }

        // Running the SML program
        try
        {
            Run();
        }
        catch (SvmRuntimeException ex)
        {
            Console.WriteLine($"Runtime error: {ex.Message}. SVM is exiting.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred during program execution: {ex.Message}. SVM is exiting.");
        }


        #endregion
    }
    
    


    #region Properties
    /// <summary>
    ///  Gets a reference to the virtual machine stack.
    ///  This is used by executing instructions to retrieve
    ///  operands and store results
    /// </summary>
    public Stack<object> Stack
    {
        get
        {
            return stack;
        }
    }

    /// <summary>
    /// Accesses the virtual machine 
    /// program counter (see programCounter in the Fields region).
    /// This can be used by executing instructions to 
    /// determine their order (ie. line number) in the 
    /// sequence of executing SML instructions
    /// </summary>
    public int ProgramCounter
    {

        #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
        #endregion
        get { return programCounter; }
        set { programCounter = value; }
    }
    #endregion

    #region Public Methods

    #endregion

    #region Non-public Methods

    /// <summary>
    /// Reads the specified file and tries to 
    /// compile any SML instructions it contains
    /// into an executable SVM program
    /// </summary>
    /// <param name="filepath">The path to the 
    /// .sml file containing the SML program to
    /// be compiled</param>
    /// <exception cref="SvmCompilationException">
    /// If file is not a valid SML program file or 
    /// the SML instructions cannot be compiled to an
    /// executable program</exception>
    private void Compile(string filepath)
    {
        if (!File.Exists(filepath))
        {
            throw new SvmCompilationException("The file " + filepath + " does not exist");
        }

        int lineNumber = 0;
        try
        {
            using (StreamReader sourceFile = new StreamReader(filepath))
            {
                while (!sourceFile.EndOfStream)
                {
                    string instruction = sourceFile.ReadLine();
                    if (!String.IsNullOrEmpty(instruction) &&
                        !String.IsNullOrWhiteSpace(instruction))
                    {
                        ParseInstruction(instruction, lineNumber);
                        lineNumber++;
                    }
                }
            }
        }
        catch (SvmCompilationException err)
        {
            Console.WriteLine(CompilationErrorMessage, lineNumber, err.Message);
            throw;
        }
    }

    /// <summary>
    /// Executes a compiled SML program 
    /// </summary>
    /// <exception cref="SvmRuntimeException">
    /// If an unexpected error occurs during
    /// program execution
    /// </exception>
    /// 


    private void Run()   
    {


        DateTime start = DateTime.Now;

        
        for (int i = 0; i < program.Count; i++)
        {
            programCounter = i;

           
            program[i].VirtualMachine = this;

            
            if (IsBreakpoint(i))
            {
               
                IDebugFrame debugFrame = CreateDebugFrame(i);

                
                if (debugger != null)
                {
                    debugger.Break(debugFrame);
                }
               
            }

            
            program[i].Run();
        }

        
        long memUsed = System.Environment.WorkingSet;
        TimeSpan elapsed = DateTime.Now - start;
        Console.WriteLine(String.Format(
            "\r\n\r\nExecution finished in {0} milliseconds. Memory used = {1} bytes. Press any key to exit.",
            elapsed.TotalMilliseconds, 
            memUsed));

      
        Console.ReadKey();
    }

    /// <summary>
    /// Parses a string from a .sml file containing a single
    /// SML instruction
    /// </summary>
    /// <param name="instruction">The string representation
    /// of an instruction</param>
    private void ParseInstruction(string instruction, int lineNumber)
    {
        #region TASK 5 & 7 - MAY REQUIRE MODIFICATION BY THE STUDENT
        #endregion

        bool isBreakpoint = false;

        
        if (instruction.StartsWith("* "))
        {
            isBreakpoint = true;
            instruction = instruction.Substring(2); 
            breakpoints.Add(lineNumber); 
        }

        string[] tokens = null;
        if (instruction.Contains("\""))
        {
            tokens = instruction.Split(new char[] { '\"' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim();
            }
        }
        else
        {
            tokens = instruction.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        if (tokens.Length > 3)
        {
            throw new SvmCompilationException(String.Format(InvalidOperandsMessage, instruction));
        }

        IInstruction compiledInstruction = null;
        switch (tokens.Length)
        {
            case 1:
                compiledInstruction = JITCompiler.CompileInstruction(tokens[0]);
                break;
            case 2:
                compiledInstruction = JITCompiler.CompileInstruction(tokens[0], tokens[1].Trim('\"'));
                break;
            case 3:
                compiledInstruction = JITCompiler.CompileInstruction(tokens[0], tokens[1].Trim('\"'), tokens[2].Trim('\"'));
                break;
        }

        if (compiledInstruction != null)
        {
            
            if (isBreakpoint)
            {
              
                compiledInstruction.IsBreakpoint = true;
            }
            program.Add(compiledInstruction);
        }
    }
    #endregion

    #region System.Object overrides
    /// <summary>
    /// Determines whether the specified <see cref="System.Object">Object</see> is equal to the current <see cref="System.Object">Object</see>.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object">Object</see> to compare with the current <see cref="System.Object">Object</see>.</param>
    /// <returns><b>true</b> if the specified <see cref="System.Object">Object</see> is equal to the current <see cref="System.Object">Object</see>; otherwise, <b>false</b>.</returns>
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    /// <summary>
    /// Serves as a hash function for this type.
    /// </summary>
    /// <returns>A hash code for the current <see cref="System.Object">Object</see>.</returns>
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    /// <summary>
    /// Returns a <see cref="System.String">String</see> that represents the current <see cref="System.Object">Object</see>.
    /// </summary>
    /// <returns>A <see cref="System.String">String</see> that represents the current <see cref="System.Object">Object</see>.</returns>
    public override string ToString()
    {
        return base.ToString();
    }
    #endregion
}