using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyNUnitTest
{
    [TestFixture]
    public class BankAccountNUnitTests
    {
     
        [SetUp]
        public void Setup()
        {
           
        }
        //[Test]
        //public void BankDepositLogFkker_Add100_ReturnTrue()
        //{
        //    BankAccount bankAccount = new(new LogFakker());
        //    var result = bankAccount.Deposit(100);
        //    Assert.That(result);
        //    Assert.That(bankAccount.GetBalance, Is.EqualTo(100));
        //}

        [Test]
        public void BankDeposit_Add100_ReturnTrue()
        {
            var logMoc = new Mock<ILogBook>();
            logMoc.Setup(u => u.Message(""));
            BankAccount bankAccount = new(logMoc.Object);
            var result = bankAccount.Deposit(100);
            Assert.That(result);
            Assert.That(bankAccount.GetBalance, Is.EqualTo(100));
        }
        [Test]
        [TestCase(200,100)]
        [TestCase(200, 150)]
        public void BankWithdraw_Withdraw100With2000Balance_ReturnTrue(int balance,int withdrawal)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u=>u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdraw(It.Is<int>(x=>x>0))).Returns(true);
            BankAccount bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdrawal);
            ClassicAssert.IsTrue(result);
        }
        [Test]
        [TestCase(200, 300)]
        public void BankWithdraw_Withdraw300With200Balance_ReturnsFalse(int balance, int withdrawal)
        {

            var logMock = new Mock<ILogBook>();
            logMock.Setup(u => u.LogBalanceAfterWithdraw(It.Is<int>(x => x > 0))).Returns(true);
            //logMock.Setup(u => u.LogBalanceAfterWithdraw(It.Is<int>(x => x < 0))).Returns(false);
            logMock.Setup(u => u.LogBalanceAfterWithdraw(It.IsInRange<int>(int.MinValue,-1,Moq.Range.Inclusive))).Returns(false);
            BankAccount bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdrawal);
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void BankLogDummy_LogMockString_ReturnTrue()
        {

            var logMock = new Mock<ILogBook>();
            string desiredOutput="hello";
            logMock.Setup(u => u.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());
            Assert.That(logMock.Object.MessageWithReturnStr("HELLo"), Is.EqualTo(desiredOutput));
        }

        [Test]
        public void BankLogDummy_LogMockStringOutputStr_ReturnTrue()
        {

            var logMock = new Mock<ILogBook>();
             string desiredOutput="hello" ;

            logMock.Setup(u => u.LogWithOutputresult(It.IsAny<string>(),out desiredOutput)).Returns(true);
             string result = "";
            
            ClassicAssert.IsTrue(logMock.Object.LogWithOutputresult("Ben",out result));

           // logMock.Setup(u => u.LogWithOutputresult(It.IsAny<string>())).Returns((string str) => str.ToLower());
            Assert.That(result, Is.EqualTo(desiredOutput));
        }

        [Test]
        public void BankLogDummy_LogRefChecker_ReturnTrue()
        {

            var logMock = new Mock<ILogBook>();
            Customer customer = new();
            Customer customerNotUsed = new();
            logMock.Setup(u => u.LogWithRefObject(ref customer)).Returns(true);

            ClassicAssert.IsTrue(logMock.Object.LogWithRefObject(ref customer));
            ClassicAssert.IsFalse(logMock.Object.LogWithRefObject(ref customerNotUsed));
        }
        [Test]
        public void BankLogDummy_SetAndGetLogTypeAndSeverityMock_MockTest()
        {

            var logMock = new Mock<ILogBook>();
            logMock.SetupAllProperties();
           // logMock.Setup(u => u.LogSeverity).Returns(10);
            logMock.Setup(u => u.LogType).Returns("Warning");
            
            logMock.Object.LogSeverity = 100;
            //     Assert.That(logMock.Object.LogSeverity, Is.EqualTo(10));
            Assert.That(logMock.Object.LogSeverity, Is.EqualTo(100));
            Assert.That(logMock.Object.LogType, Is.EqualTo("Warning"));

            //callbacks
            string logTemp = "Hello, ";
            logMock.Setup(u=>u.LogToDb(It.IsAny<string>()))
                .Returns(true).Callback((string str)=>logTemp+=str);
            logMock.Object.LogToDb("Ben");
            Assert.That(logTemp, Is.EqualTo("Hello, Ben"));

            //callbacks
            int counter = 5 ;
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                .Returns(true).Callback(() => counter++);
            logMock.Object.LogToDb("Ben");
            logMock.Object.LogToDb("Ben");
            Assert.That(counter, Is.EqualTo(7));
        }
        [Test]
        public void BankLogDummy_VerifyExample()
        {
            var logMock = new Mock<ILogBook>();
            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(100);
            Assert.That(bankAccount.GetBalance, Is.EqualTo(100));

            //verification
            logMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(2));
            logMock.Verify(u => u.Message("Test"), Times.AtLeastOnce);
            // logMock.VerifySet(u => u.LogSeverity=101, Times.Once);
            logMock.VerifyGet(u => u.LogSeverity, Times.Once);
        }
    }
}
