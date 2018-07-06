﻿using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Jasper.Http.Testing
{
    public class now_parameter_bindings
    {
        [Fact]
        public async Task use_datetime_argument()
        {
            var result = await HttpTesting.Scenario(_ =>
            {
                _.Get.Url("/current/time");
            });

            var time = DateTime.Parse(result.ResponseBody.ReadAsText());

            var seconds = DateTime.UtcNow.Subtract(time).Seconds;
            Math.Abs(seconds).ShouldBeLessThan(30);
        }

        [Fact]
        public async Task use_datetimeoffset_argument()
        {
            var dateTimeOffset = DateTimeOffset.UtcNow;
            var result = await HttpTesting.Scenario(_ =>
            {
                _.Get.Url("/current/offset/time");
            });

            var time = DateTimeOffset.Parse(result.ResponseBody.ReadAsText());


            var seconds = dateTimeOffset.Subtract(time).Seconds;
            Math.Abs(seconds).ShouldBeLessThan(30);
        }
    }

    public class TimeEndpoint
    {
        public string get_current_time(DateTime now)
        {
            return now.ToString("R");
        }

        public string get_current_offset_time(DateTimeOffset now)
        {

            return now.ToString("R");
        }
    }
}
