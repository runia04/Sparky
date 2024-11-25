﻿using Bongo.Core.Services.IServices;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Bongo.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Web.Test
{
    [TestFixture]
    public class RoomBookingControllerTest
    {
        //private Mock<IStudyRoomBookingService> _studyRoomBookingService;
        //private RoomBookingController _bookingController;
        //[SetUp]
        //public void SetUp()
        //{
        //    _studyRoomBookingService = new Mock<IStudyRoomBookingService>();
        //    _bookingController = new RoomBookingController(_studyRoomBookingService.Object);
        //}
        //[Test]
        //public void IndexPage_CallRequest_VerifyGetAllInvoked()
        //{
        //    _bookingController.Index();
        //    _studyRoomBookingService.Verify(x=>x.GetAllBooking(),Times.Once);
        //}
        //[Test]
        //public void BookRoomCheck_ModelStateInvalid_ReturnView()
        //{
        //    _bookingController.ModelState.AddModelError("test","test");
        //    var result = _bookingController.Book(new Bongo.Models.Model.StudyRoomBooking());
        //    ViewResult viewResult = result as ViewResult;
        //    Assert.That(viewResult.ViewName, Is.EqualTo("Book"));
        //}
        //[Test]
        //public void BookRoomCheck_NotSuccessful_NoRoomCode()
        //{
        //    _studyRoomBookingService.Setup(x=>x.BookStudyRoom(It.IsAny<StudyRoomBooking>())).Returns(new StudyRoomBookingResult()
        //        {
        //            Code=StudyRoomBookingCode.NoRoomAvailable
        //        });
        //    var result = _bookingController.Book(new StudyRoomBooking());
        //    ClassicAssert.IsInstanceOf<ViewResult>(result);
        //    ViewResult viewResult = result as ViewResult;
        //    Assert.That(viewResult.ViewData["Error"],Is.EqualTo("No Study Room available for selected date"));
        //}
        //[Test]
        //public void BookRoomCheck_Successful_SuccessCodeAndRedirect()
        //{
        //    //arrange
        //    _studyRoomBookingService.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>())).Returns((StudyRoomBooking booking)=>new StudyRoomBookingResult()
        //    {
        //        Code = StudyRoomBookingCode.Success,
        //        FirstName=booking.FirstName,
        //        LastName=booking.LastName,
        //        Date=booking.Date,
        //        Email=booking.Email
        //    });
        //    //act
        //    var result = _bookingController.Book(new StudyRoomBooking()
        //    {
        //        Date=DateTime.Now,
        //        Email="hello@dotnetmastery.com",
        //        FirstName="Hello",
        //        LastName="DotNetMastery",
        //        StudyRoomId=1
        //    });
        //    //assert
        //    ClassicAssert.IsInstanceOf<RedirectToActionResult>(result);
        //    RedirectToActionResult actionResult = result as RedirectToActionResult;
        //    Assert.That(actionResult.RouteValues["FirstName"], Is.EqualTo("Hello"));
        //    Assert.That(actionResult.RouteValues["Code"], Is.EqualTo(StudyRoomBookingCode.Success));

        //}
    }
}
