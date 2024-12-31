

namespace TestNinja.UnitTests
{
    [TestFixture]
	public class StackTests
	{
		private TestNinja.Fundamentals.Stack<string> _stack;

		[SetUp]
		public void SetUp()
		{
			_stack = new TestNinja.Fundamentals.Stack<string>();
		}

		[Test]
		public void Push_ArgumentIsNull_ThrowArgumentNullException()
		{
			Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
		}

		[Test]
		public void Push_ValidArgument_AddTheObjectToTheStack()
		{
            _stack.Push("a");

			Assert.That(_stack.Count, Is.EqualTo(1));
        }

		[Test]
		public void Count_EmptyStack_ReturnZero()
		{
			Assert.That(_stack.Count, Is.EqualTo(0));
		}

		[Test]
		public void Pop_EmptyStack_ThrowInvalidOperationException()
		{
			Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
		}

		[Test]
		public void Pop_StackWithObjects_ReturnRemovedObjectFromTheTopOfTheStack()
		{
			// Arrange
			_stack.Push("a");
			_stack.Push("b");
			_stack.Push("c");

			// Act
			var result = _stack.Pop();

			// Assert
			Assert.That(result, Is.EqualTo("c"));
		}

        [Test]
        public void Pop_StackWithObjects_RemoveObjectFromTheTopOfTheStack()
        {
            // Arrange
            _stack.Push("a");
            _stack.Push("b");

            // Act
            _stack.Pop();

            // Assert
            Assert.That(_stack.Count, Is.EqualTo(1));
        }

		[Test]
		public void Peek_EmptyStack_ThrowInvalidOperationException()
		{
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackWithObjects_ReturnObjectOnTopOfTheStack()
        {
            // Arrange
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            // Act
            var result = _stack.Peek();

            // Assert
            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Peek_StackWithObjects_DoesNotRemoveObjectFromTheStack()
        {
            // Arrange
            _stack.Push("a");

            // Act
            var result = _stack.Peek();

            // Assert
            Assert.That(_stack.Count, Is.EqualTo(1));
        }
    }
}

