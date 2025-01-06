using System;
using System.Security.Cryptography;
using Moq;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class HousekeeperServiceTests
	{
		private Mock<IUnitOfWork> _unitOfWork;
		private Mock<IStatementGenerator> _statementGenerator;
		private Mock<IEmailSender> _emailSender;
		private Mock<IXtraMessageBox> _xtraMessageBox;
		private HousekeeperService _housekeeperService;
        private Housekeeper _housekeeper;
        private DateTime _statementDate = new DateTime(2025, 1, 1);
		private string _statementFileName;

        [SetUp]
		public void SetUp()
		{
			_housekeeper = new Housekeeper()
			{
				Email = "a",
				FullName = "b",
				Oid = 1,
				StatementEmailBody = "c"
			};

            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>()
            {
                _housekeeper
            }.AsQueryable);

			_statementFileName = "fileName";
            _statementGenerator = new Mock<IStatementGenerator>();
			_statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);

            _emailSender = new Mock<IEmailSender>();
			_xtraMessageBox = new Mock<IXtraMessageBox>();
			_housekeeperService = new HousekeeperService(_unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _xtraMessageBox.Object);
        }

		[Test]
		public void SendStatementEmails_WhenCalled_GenerateStatements()
		{
			_housekeeperService.SendStatementEmails(_statementDate);

			_statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
		}

        [Test]
		[TestCase(null)]
		[TestCase(" ")]
		[TestCase("")]
        public void SendStatementEmails_WhenCalled_ShouldNotGenerateStatement(string housekeeperEmail)
        {
			_housekeeper.Email = housekeeperEmail;

            _housekeeperService.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
        }

		[Test]
		public void SendStatementEmails_WhenCalled_EmailTheStatement()
		{
			_housekeeperService.SendStatementEmails(_statementDate);

			_emailSender.Verify(es => es.EmailFile(
				_housekeeper.Email,
				_housekeeper.StatementEmailBody,
				_statementFileName,
				It.IsAny<string>())
			);
		}

        [Test]
		[TestCase(null)]
		[TestCase(" ")]
        public void SendStatementEmails_WhenCalled_ShouldNotEmailTheStatement(string statementFileName)
        {
			_statementFileName = statementFileName;

            _housekeeperService.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never
            );
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())
            ).Throws<Exception>();

            _housekeeperService.SendStatementEmails(_statementDate);

			_xtraMessageBox.Verify(xmb => xmb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }
    }
}