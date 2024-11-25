
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace SparkyNUnitTest
{

    public class CustomerXUnitTests
    {
        private Customer customer;
      
        public  CustomerXUnitTests()
        {
            customer = new Customer();
        }
        [Fact]
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

            Assert.Equal("Hello, Ben Spark", customer.GreetMessage);
                Assert.Contains("ben Spark".ToLower(), customer.GreetMessage.ToLower());
                Assert.StartsWith("Hello,", customer.GreetMessage);
                Assert.EndsWith("Spark", customer.GreetMessage);
                Assert.Matches("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", customer.GreetMessage);
            
        }

        [Fact]
        public void GreetMessage_NotGreeted_ReturnNull()
        {
            //arrange
            // var customer = new Customer();
            //act
            //   customer.GreetAndCombineNames("Ben", "Spark");
            //assert
            Assert.Null(customer.GreetMessage);
        }
        [Fact]
        public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
        {
            int result = customer.Discount;
            Assert.InRange(result,10, 25);
        }
        [Fact]
        public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
        {
            customer.GreetAndCombineNames("ben", "");
            Assert.NotNull(customer.GreetMessage);
            Assert.False(string.IsNullOrEmpty(customer.GreetMessage));

        }
        [Fact]
        public void GreetChecker_EmptyFirstName_ThrowException()
        {
         

            var exceptionDetails = Assert.Throws<ArgumentNullException>(() => customer.GreetAndCombineNames("", "spark"));
            Assert.Equal("Empty First Name", exceptionDetails.ParamName);
          
            Assert.Throws<ArgumentNullException>(() => customer.GreetAndCombineNames("", "spark"));
        }

        [Fact]
        public void CustomerType_CreateCustomerWithLessThan100rder_ReturnBasicCustomer()
        {
            customer.OrderTotal = 10;
            var result = customer.GetCustomerDetails();
            Assert.IsType<BasicCustomer>(result);
        }
        [Fact]
        public void CustomerType_CreateCustomerWithMoreThan100rder_ReturnBasicCustomer()
        {
            customer.OrderTotal = 110;
            var result = customer.GetCustomerDetails();
            Assert.IsType<PlatinumCustomer>(result);
        }
        //https://xunit.net/docs/comparisons
    }
}
