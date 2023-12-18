using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SVM.SimpleMachineLanguage;
using SVM.VirtualMachine;
using System.Collections.Generic;

namespace SVM.UnitTests
{
    [TestClass]
    public class IncrInstructionTests
    {
        private Mock<IVirtualMachine> mockVm;
        private Stack<object> stack;

        [TestInitialize]
        public void Initialize()
        {
            // Setup the Mock Virtual Machine and Stack before each test
            mockVm = new Mock<IVirtualMachine>();
            stack = new Stack<object>();
            mockVm.Setup(vm => vm.Stack).Returns(stack);
        }

        [TestMethod]
        public void Incr_ShouldIncrementTopValueOfStack_WhenStackHasInteger()
        {
            // Arrange
            stack.Push(2);
            var incr = new Incr();
            incr.VirtualMachine = mockVm.Object;

            // Act
            incr.Run();

            // Assert
            Assert.AreEqual(3, stack.Peek());
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException))]
        public void Incr_ShouldThrowException_WhenStackIsEmpty()
        {
            // Arrange
            var incr = new Incr();
            incr.VirtualMachine = mockVm.Object;

            // Act
            incr.Run();

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(SvmRuntimeException))]
        public void Incr_ShouldThrowException_WhenTopOfStackIsNotInteger()
        {
            // Arrange
            stack.Push("not an integer");
            var incr = new Incr();
            incr.VirtualMachine = mockVm.Object;

            // Act
            incr.Run();

            // Assert is handled by ExpectedException
        }
    }
}
