﻿// Licensed under the MIT License.
// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AdaptiveExpressions.Properties;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Teams.Actions;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using Newtonsoft.Json;

namespace Microsoft.Bot.Builder.Dialogs.Adaptive.Teams.Actions
{
    /// <summary>
    /// Send a messaging extension 'config' response. This is the type of response used for a 'composeExtension/querySettingUrl' request.
    /// </summary>
    public class SendMessagingExtensionConfigQuerySettingUrlResponse : BaseTeamsCacheInfoResponseDialog
    {
        /// <summary>
        /// Class identifier.
        /// </summary>
        [JsonProperty("$kind")]
        public const string Kind = "Teams.SendMessagingExtensionConfigQuerySettingUrlResponse";

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessagingExtensionConfigQuerySettingUrlResponse"/> class.
        /// </summary>
        /// <param name="callerPath">Optional, source file full path.</param>
        /// <param name="callerLine">Optional, line number in source file.</param>
        [JsonConstructor]
        public SendMessagingExtensionConfigQuerySettingUrlResponse([CallerFilePath] string callerPath = "", [CallerLineNumber] int callerLine = 0)
            : base()
        {
            RegisterSourceLocation(callerPath, callerLine);
        }

        /// <summary>
        /// Gets or sets config url response to send. i.e $"{config.siteUrl}/searchSettings.html?settings={escapedSettings}".
        /// </summary>
        /// <value>
        /// Message to send.
        /// </value>
        [JsonProperty("configUrl")]
        public StringExpression ConfigUrl { get; set; }

        /// <summary>
        /// Called when the dialog is started and pushed onto the dialog stack.
        /// </summary>
        /// <param name="dc">The <see cref="DialogContext"/> for the current turn of conversation.</param>
        /// <param name="options">Optional, initial information to pass to the dialog.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (options is CancellationToken)
            {
                throw new ArgumentException($"{nameof(options)} cannot be a cancellation token");
            }

            if (Disabled != null && Disabled.GetValue(dc.State) == true)
            {
                return await dc.EndDialogAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            }

            string configUrl = ConfigUrl.GetValueOrNull(dc.State);
            if (string.IsNullOrEmpty(configUrl)) 
            { 
                throw new InvalidOperationException($"{nameof(ConfigUrl)} is required for {Kind}.");
            }

            var response = new MessagingExtensionResponse
            {
                ComposeExtension = new MessagingExtensionResult
                {
                    Type = MessagingExtensionResultResponseType.config.ToString(),
                    SuggestedActions = new MessagingExtensionSuggestedAction
                    {
                        Actions = new List<CardAction>
                        {
                            new CardAction
                            {
                                Type = ActionTypes.OpenUrl,
                                Value = configUrl,
                            },
                        },
                    },
                },
                CacheInfo = GetCacheInfo(dc)
            };

            var invokeResponse = CreateInvokeResponseActivity(response);
            ResourceResponse sendResponse = await dc.Context.SendActivityAsync(invokeResponse, cancellationToken: cancellationToken).ConfigureAwait(false);

            return await dc.EndDialogAsync(sendResponse, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Builds the compute Id for the dialog.
        /// </summary>
        /// <returns>A string representing the compute Id.</returns>
        protected override string OnComputeId()
        {
            return $"{GetType().Name}[{ConfigUrl?.ToString() ?? string.Empty}]";
        }
    }
}
