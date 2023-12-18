namespace SVM.SimpleMachineLanguage
{
    public class Decr : BaseInstruction
    {
        public  IVirtualMachine VirtualMachine { get; set; }
        public override void Run()
        {
            // Ensure the stack is not empty
            if (this.VirtualMachine.Stack.Count == 0)
            {
                throw new SvmRuntimeException(String.Format(StackUnderflowMessage, this.VirtualMachine.ProgramCounter, this.GetType().Name));
            }

            // Pop the top value from the stack
            object topValue = this.VirtualMachine.Stack.Pop();

            // Ensure the top value is an integer
            if (!(topValue is int))
            {
                throw new SvmRuntimeException(String.Format(OperandOfWrongTypeMessage, this.VirtualMachine.ProgramCounter, this.GetType().Name));
            }

            int decrementedValue = (int)topValue - 1;

            // Push the decremented value back onto the stack
            this.VirtualMachine.Stack.Push(decrementedValue);
        }
    }
}
