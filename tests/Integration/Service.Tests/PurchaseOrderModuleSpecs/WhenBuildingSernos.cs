﻿namespace Linn.Production.Service.Tests.PurchaseOrderModuleSpecs
{
    using FluentAssertions;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenBuildingSernos : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var resource = new BuildSernosRequestResource
                               {
                                   FromSerial = 1,
                                   OrderNumber = 1, 
                                   PartNumber = "PART",
                                   ToSerial = 2
                               };
            this.SernosPack.BuildSernos(
                Arg.Any<int>(), 
                Arg.Any<string>(), 
                Arg.Any<string>(), 
                Arg.Any<int>(), 
                Arg.Any<int>(), 
                Arg.Any<int>(), 
                Arg.Any<int>()).Returns(true);

            this.Response = this.Browser.Post(
                "/production/resources/purchase-orders/build-sernos",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(resource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallProxy()
        {
            this.SernosPack.Received().BuildSernos(
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<int>());
        }
    }
}