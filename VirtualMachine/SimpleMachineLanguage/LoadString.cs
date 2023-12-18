namespace SVM.SimpleMachineLanguage
{
    public class LoadString : BaseInstructionWithOperand
    {
        public override void Run()
        {
            // Check for null VirtualMachine or Operands
            if (VirtualMachine == null)
                throw new SvmRuntimeException("Virtual machine reference not set in LoadString instruction.");

            if (VirtualMachine.Stack == null)
                throw new SvmRuntimeException("Stack is null in LoadString instruction.");

            if (Operands == null || Operands.Length == 0)
                throw new SvmRuntimeException("Operands for LoadString instruction are not set.");

            // Check operand type
            if (Operands[0].GetType() != typeof(string))
            {
                throw new SvmRuntimeException(String.Format(BaseInstruction.OperandOfWrongTypeMessage,
                                                this.ToString(), VirtualMachine.ProgramCounter));
            }

            // Push the string onto the stack
            VirtualMachine.Stack.Push(Operands[0]);
        }
    }
}
