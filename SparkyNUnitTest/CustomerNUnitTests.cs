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
    public class CustomerNUnitTests
    {
        private Customer customer;
        [SetUp]
        public void SetUp()
        {
            customer = new Customer();
        }
        [Test]
        public void CombineName_InputFirstAndLastName_ReturnFullName()
        {
            //Arrange
            //var customer = new Customer();
            ////Act
            //string fullName = customer.GreetAndCombineNames("Ben", "Spark");
            ////Assert
            //ClassicAssert.AreEqual(fullName, "Hello, Ben Spark");
            //Assert.That(fullName, Is.EqualTo("Hello, Ben Spark"));
            //Assert.That(fullName, Does.Contain("ben Spark").IgnoreCase);
            //Assert.That(fullName, Does.StartWith("Hello,"));
            //Assert.That(fullName, Does.EndWith("Spark"));
            //Assert.That(fullName, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));

            //Act
            customer.GreetAndCombineNames("Ben", "Spark");
            //Assert
            Assert.Multiple(() =>
            {
                ClassicAssert.AreEqual(customer.GreetMessage, "Hello, Ben Spark");
                Assert.That(customer.GreetMessage, Is.EqualTo("Hello, Ben Spark"));
                Assert.That(customer.GreetMessage, Does.Contain("ben Spark").IgnoreCase);
                Assert.That(customer.GreetMessage, Does.StartWith("Hello,"));
                Assert.That(customer.GreetMessage, Does.EndWith("Spark"));
                Assert.That(customer.GreetMessage, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
            });
        }

        [Test]
        public void GreetMessage_NotGreeted_ReturnNull()
        {
            //arrange
            // var customer = new Customer();
            //act
            //   customer.GreetAndCombineNames("Ben", "Spark");
            //assert
            ClassicAssert.IsNull(customer.GreetMessage);
        }
        [Test]
        public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
        {
            int result = customer.Discount;
            Assert.That(result, Is.InRange(10, 25));
        }
        [Test]
        public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
        {
            customer.GreetAndCombineNames("ben", "");
            ClassicAssert.IsNotNull(customer.GreetMessage);
            ClassicAssert.IsFalse(string.IsNullOrEmpty(customer.GreetMessage));

        }
        [Test]
        public void GreetChecker_EmptyFirstName_ThrowException()
        {
            string msg = "Empty First Name";

            var exceptionDetails = Assert.Throws<ArgumentNullException>(() => customer.GreetAndCombineNames("", "spark"));
            ClassicAssert.AreEqual(msg, exceptionDetails.ParamName);
            //   Assert.That(() => customer.GreetAndCombineNames("", "spark"), Throws.ArgumentNullException.With.Message.EquivalentTo("Empty First Name"));
            Assert.That(() => customer.GreetAndCombineNames("", "spark"),
                Throws.Exception
                  .TypeOf<ArgumentNullException>()
                  .With.Property("ParamName")
                  .EqualTo("Empty First Name"));

            Assert.Throws<ArgumentNullException>(() => customer.GreetAndCombineNames("", "spark"));
        }

        [Test]
        public void CustomerType_CreateCustomerWithLessThan100rder_ReturnBasicCustomer()
        {
            customer.OrderTotal = 10;
            var result = customer.GetCustomerDetails();
            Assert.That(result, Is.TypeOf<BasicCustomer>());
        }
        [Test]
        public void CustomerType_CreateCustomerWithMoreThan100rder_ReturnBasicCustomer()
        {
            customer.OrderTotal = 110;
            var result = customer.GetCustomerDetails();
            Assert.That(result, Is.TypeOf<PlatinumCustomer>());
        }
    }
}
