﻿using Azure.Core;
using Bongo.Core.Services;
using Bongo.Core.Services.IServices;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Core.Tests
{
    [TestFixture]
    public class StudyRoomBookingServiceTest
    {
        private StudyRoomBooking _request;
        private List<StudyRoom> _availableStudyRoom;
        private Mock<IStudyRoomBookingRepository> _studyRoomBookingRepoMock;
        private Mock<IStudyRoomRepository> _studyRoomRepoMock;
        private StudyRoomBookingService _bookingService;
        [SetUp]
        public void SetUp()
        {
            _request = new StudyRoomBooking
            {
                FirstName = "Ben",
                LastName = "Spark",
                Email = "ben@gmail.com",
                Date = new DateTime(2024, 1, 1)
            };
            _availableStudyRoom = new List<StudyRoom>
            {
                new StudyRoom
                {
                    Id=10,RoomName="Michigan",RoomNumber="A202"
                }
            };

            _studyRoomBookingRepoMock = new Mock<IStudyRoomBookingRepository>();
            _studyRoomRepoMock = new Mock<IStudyRoomRepository>();
            _studyRoomRepoMock.Setup(x => x.GetAll()).Returns(_availableStudyRoom);
            _bookingService = new StudyRoomBookingService(_studyRoomBookingRepoMock.Object, _studyRoomRepoMock.Object);
        }
        [TestCase]
        public void GetAllBooking_InvokedMethod_CheckIfRepoIsCalled()
        {
            _bookingService.GetAllBooking();
            _studyRoomBookingRepoMock.Verify(x => x.GetAll(null), Times.Once);
        }
        [TestCase]
        public void BookingException_NullRequest_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _bookingService.BookStudyRoom(null));
            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'request')"));
            Assert.That(exception.ParamName, Is.EqualTo("request"));
            //  ClassicAssert.Equals("request",exception.ParamName);
        }
        [Test]
        public void StudyRoomBooking_SaveBookingWithAvailableRoom_ReturnsResultWithAllValues()
        {
            StudyRoomBooking savedStudyRoomBooking = null;
            _studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>())).Callback<StudyRoomBooking>(booking => { savedStudyRoomBooking = booking; });
            //act
            _bookingService.BookStudyRoom(_request);
            //assert
            _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Once);
            ClassicAssert.NotNull(savedStudyRoomBooking);
            Assert.That(savedStudyRoomBooking.FirstName, Is.EqualTo(_request.FirstName));
            Assert.That(savedStudyRoomBooking.LastName, Is.EqualTo(_request.LastName));
            Assert.That(savedStudyRoomBooking.Email, Is.EqualTo(_request.Email));
            Assert.That(savedStudyRoomBooking.Date, Is.EqualTo(_request.Date));
            Assert.That(savedStudyRoomBooking.StudyRoomId, Is.EqualTo(_availableStudyRoom.First().Id));
        }
        [Test]
        public void StudyRoomBookingResultCheck_InputRequest_ValueMatchInResult()
        {
            StudyRoomBookingResult result = _bookingService.BookStudyRoom(_request);
            ClassicAssert.NotNull(result);
            Assert.That(result.FirstName, Is.EqualTo(_request.FirstName));
            Assert.That(result.LastName, Is.EqualTo(_request.LastName));
            Assert.That(result.Email, Is.EqualTo(_request.Email));
            Assert.That(result.Date, Is.EqualTo(_request.Date));

        }
        [TestCase(true, ExpectedResult = StudyRoomBookingCode.Success)]
        [TestCase(false, ExpectedResult = StudyRoomBookingCode.NoRoomAvailable)]
        public StudyRoomBookingCode ResultCodeSuccess_RoomAvability_ReturnSuccessResultCode(bool roomAvailability)
        {
            if (!roomAvailability)
            {
                _availableStudyRoom.Clear();
            }
            return _bookingService.BookStudyRoom(_request).Code;
            // Assert.That(result.Code,Is.EqualTo(StudyRoomBookingCode.Success));
        }

        [TestCase(0, false)]
        [TestCase(55, true)]
        public void StudyRoomBooking_BookRoomWithAvailability_ReturnsBookingId(int expectedBookingId, bool roomAvailability)
        {
            if (!roomAvailability)
            {
                _availableStudyRoom.Clear();
            }
            StudyRoomBooking savedStudyRoomBooking = null;
            _studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>())).Callback<StudyRoomBooking>(booking => {  booking.BookingId=55; });
            var result = _bookingService.BookStudyRoom(_request);
            Assert.That(result.BookingId,Is.EqualTo(expectedBookingId));
            //if (!roomAvailability)
            //{

            //    _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
            //}
        }
        [Test]
        public void BookNotInvoked_SaveBookingWithoutAvailableRoom_BookMethodNotInvoked()
        {  
            _availableStudyRoom.Clear();
            var result = _bookingService.BookStudyRoom(_request);
            _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
        }
    }
}

 