using System;
using System.Collections.Generic;
using System.Text;
using Kufar.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace UnitTestKufar.Tests
{
    
    public class UnitTest
    {

        [Fact]
        public void Test1()
        {
            // Arrange
            var helper = new PageLinkTagHelper(new UrlHelperFactory());

            //var x = helper.CreateTag(1, helper);

        }
    }
}
