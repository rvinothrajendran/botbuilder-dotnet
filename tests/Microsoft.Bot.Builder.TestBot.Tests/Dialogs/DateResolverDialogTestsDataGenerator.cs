﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Bot.Builder.Testing.XUnit;

namespace Microsoft.BotBuilderSamples.Tests.Dialogs
{
    [SuppressMessage("Microsoft.StyleCop.CSharp.OrderingRules", "SA1118:ParameterMustNotSpanMultipleLines", Justification = "Ignoring to make code more readable")]

    public class DateResolverDialogTestsDataGenerator
    {
        public static IEnumerable<object[]> DateResolverCases()
        {
            yield return BuildTestCaseObject(
                "tomorrow",
                null,
                $"{DateTime.UtcNow.AddDays(1):yyyy-MM-dd}",
                new[,]
                {
                    { "hi", "When would you like to travel?" },
                    { "tomorrow", null },
                });

            yield return BuildTestCaseObject(
                "the day after tomorrow",
                null,
                $"{DateTime.UtcNow.AddDays(2):yyyy-MM-dd}",
                new[,]
                {
                    { "hi", "When would you like to travel?" },
                    { "the day after tomorrow", null },
                });

            yield return BuildTestCaseObject(
                "two days from now",
                null,
                $"{DateTime.UtcNow.AddDays(2):yyyy-MM-dd}",
                new[,]
                {
                    { "hi", "When would you like to travel?" },
                    { "two days from now", null },
                });

            yield return BuildTestCaseObject(
                "valid input given (tomorrow)",
                $"{DateTime.UtcNow.AddDays(1):yyyy-MM-dd}",
                $"{DateTime.UtcNow.AddDays(1):yyyy-MM-dd}",
                new[,]
                {
                    { "hi", null },
                });

            yield return BuildTestCaseObject(
                "retry prompt",
                null,
                $"{DateTime.UtcNow.AddDays(1):yyyy-MM-dd}",
                new[,]
                {
                    { "hi", "When would you like to travel?" },
                    { "bananas", "I'm sorry, to make your booking please enter a full travel date including Day Month and Year." },
                    { "tomorrow", null },
                });
        }

        private static object[] BuildTestCaseObject(string testCaseName, string input, string result, string[,] utterancesAndReplies)
        {
            var testData = new DateResolverDialogTestCase
            {
                Name = testCaseName,
                InitialData = input,
                ExpectedResult = result,
                UtterancesAndReplies = utterancesAndReplies,
            };
            return new object[] { new TestDataObject(testData) };
        }
    }
}