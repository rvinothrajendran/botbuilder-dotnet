﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Teams.Actions;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Teams.Conditions;
using Microsoft.Bot.Builder.Dialogs.Debugging;
using Microsoft.Bot.Builder.Dialogs.Declarative;
using Microsoft.Bot.Builder.Dialogs.Declarative.Resources;
using Newtonsoft.Json;

namespace Microsoft.Bot.Builder.Dialogs.Adaptive.Teams
{
    public class TeamsComponentRegistration : ComponentRegistration, IComponentDeclarativeTypes
    {
        public virtual IEnumerable<DeclarativeType> GetDeclarativeTypes(ResourceExplorer resourceExplorer)
        {
            // Conditionals
            yield return new DeclarativeType<OnTeamsAppBasedLinkQuery>(OnTeamsAppBasedLinkQuery.Kind);
            yield return new DeclarativeType<OnTeamsCardAction>(OnTeamsCardAction.Kind);
            yield return new DeclarativeType<OnTeamsChannelCreated>(OnTeamsChannelCreated.Kind);
            yield return new DeclarativeType<OnTeamsChannelDeleted>(OnTeamsChannelDeleted.Kind);
            yield return new DeclarativeType<OnTeamsChannelRenamed>(OnTeamsChannelRenamed.Kind);
            yield return new DeclarativeType<OnTeamsChannelRestored>(OnTeamsChannelRestored.Kind);
            yield return new DeclarativeType<OnTeamsFileConsent>(OnTeamsFileConsent.Kind);
            yield return new DeclarativeType<OnTeamsMessagingExtensionCardButtonClicked>(OnTeamsMessagingExtensionCardButtonClicked.Kind);
            yield return new DeclarativeType<OnTeamsMessagingExtensionConfigurationQuerySettingUrl>(OnTeamsMessagingExtensionConfigurationQuerySettingUrl.Kind);
            yield return new DeclarativeType<OnTeamsMessagingExtensionConfigurationSetting>(OnTeamsMessagingExtensionConfigurationSetting.Kind);
            yield return new DeclarativeType<OnTeamsMessagingExtensionFetchTask>(OnTeamsMessagingExtensionFetchTask.Kind);
            yield return new DeclarativeType<OnTeamsMessagingExtensionQuery>(OnTeamsMessagingExtensionQuery.Kind);
            yield return new DeclarativeType<OnTeamsMessagingExtensionSelectItem>(OnTeamsMessagingExtensionSelectItem.Kind);
            yield return new DeclarativeType<OnTeamsMessagingExtensionSubmitAction>(OnTeamsMessagingExtensionSubmitAction.Kind);
            yield return new DeclarativeType<OnTeamsO365ConnectorCardAction>(OnTeamsO365ConnectorCardAction.Kind);
            yield return new DeclarativeType<OnTeamsTaskModuleFetch>(OnTeamsTaskModuleFetch.Kind);
            yield return new DeclarativeType<OnTeamsTaskModuleSubmit>(OnTeamsTaskModuleSubmit.Kind);
            yield return new DeclarativeType<OnTeamsTeamArchived>(OnTeamsTeamArchived.Kind);
            yield return new DeclarativeType<OnTeamsTeamDeleted>(OnTeamsTeamDeleted.Kind);
            yield return new DeclarativeType<OnTeamsTeamHardDeleted>(OnTeamsTeamHardDeleted.Kind);
            yield return new DeclarativeType<OnTeamsTeamRenamed>(OnTeamsTeamRenamed.Kind);
            yield return new DeclarativeType<OnTeamsTeamRestored>(OnTeamsTeamRestored.Kind);
            yield return new DeclarativeType<OnTeamsTeamUnarchived>(OnTeamsTeamUnarchived.Kind);
            yield return new DeclarativeType<OnTeamsMessagingExtensionBotMessagePreviewEdit>(OnTeamsMessagingExtensionBotMessagePreviewEdit.Kind);
            yield return new DeclarativeType<OnTeamsMessagingExtensionBotMessagePreviewSend>(OnTeamsMessagingExtensionBotMessagePreviewSend.Kind);
            yield return new DeclarativeType<OnTeamsTabFetch>(OnTeamsTabFetch.Kind);
            yield return new DeclarativeType<OnTeamsTabSubmit>(OnTeamsTabSubmit.Kind);

            // Actions
            yield return new DeclarativeType<GetMeetingParticipant>(GetMeetingParticipant.Kind);
            yield return new DeclarativeType<GetMember>(GetMember.Kind);
            yield return new DeclarativeType<GetPagedMembers>(GetPagedMembers.Kind);
            yield return new DeclarativeType<GetPagedTeamMembers>(GetPagedTeamMembers.Kind);
            yield return new DeclarativeType<GetTeamChannels>(GetTeamChannels.Kind);
            yield return new DeclarativeType<GetTeamDetails>(GetTeamDetails.Kind);
            yield return new DeclarativeType<GetTeamMember>(GetTeamMember.Kind);
            yield return new DeclarativeType<SendAppBasedLinkQueryResponse>(SendAppBasedLinkQueryResponse.Kind);
            yield return new DeclarativeType<SendMessageToTeamsChannel>(SendMessageToTeamsChannel.Kind);
            yield return new DeclarativeType<SendMessagingExtensionActionResponse>(SendMessagingExtensionActionResponse.Kind);
            yield return new DeclarativeType<SendMessagingExtensionAttachmentsResponse>(SendMessagingExtensionAttachmentsResponse.Kind);
            yield return new DeclarativeType<SendMessagingExtensionAuthResponse>(SendMessagingExtensionAuthResponse.Kind);
            yield return new DeclarativeType<SendMessagingExtensionBotMessagePreviewResponse>(SendMessagingExtensionBotMessagePreviewResponse.Kind);
            yield return new DeclarativeType<SendMessagingExtensionConfigQuerySettingUrlResponse>(SendMessagingExtensionConfigQuerySettingUrlResponse.Kind);
            yield return new DeclarativeType<SendMessagingExtensionMessageResponse>(SendMessagingExtensionMessageResponse.Kind);
            yield return new DeclarativeType<SendMessagingExtensionSelectItemResponse>(SendMessagingExtensionSelectItemResponse.Kind);
            yield return new DeclarativeType<SendTaskModuleCardResponse>(SendTaskModuleCardResponse.Kind);
            yield return new DeclarativeType<SendTaskModuleMessageResponse>(SendTaskModuleMessageResponse.Kind);
            yield return new DeclarativeType<SendTaskModuleUrlResponse>(SendTaskModuleUrlResponse.Kind);
        }

        public virtual IEnumerable<JsonConverter> GetConverters(ResourceExplorer resourceExplorer, SourceContext sourceContext)
        {
            yield break;
        }
    }
}
