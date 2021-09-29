﻿using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransit.Registration;
using System;

namespace DummyApiService.Extensions
{
    public static class MassTransitRoutingKey
    {
        public static void AddRequestHeaders(this SendContext context)
        {
            if (!context.TryGetPayload(out RabbitMqSendContext sendContext))
            {
                throw new ArgumentException("The RabbitMqSendContext was not available");
            }
            sendContext.BasicProperties.ReplyTo = context.ResponseAddress.GetLastPart();
            sendContext.Headers.Set("RequestId", sendContext.RequestId?.ToString());
        }
    }
}
