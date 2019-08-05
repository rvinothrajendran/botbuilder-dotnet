﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Bot.Builder.Adapters.WeChat.Helpers;
using Microsoft.Bot.Builder.Adapters.WeChat.Schema.Helpers;
using Microsoft.Bot.Builder.Adapters.WeChat.Schema.Requests;
using Microsoft.Bot.Builder.Adapters.WeChat.Schema.Requests.Events;
using Microsoft.Bot.Builder.Adapters.WeChat.Schema.Requests.Events.Common;
using Microsoft.Bot.Builder.Adapters.WeChat.Schema.Responses;
using Microsoft.Extensions.Logging;

namespace Microsoft.Bot.Builder.Adapters.WeChat.Schema
{
    public static class WeChatMessageFactory
    {
        /// <summary>
        /// Parse XDcoument to WeChat request message.
        /// </summary>
        /// <param name="doc">The XDocument object need to be parsed.</param>
        /// <param name="logger">The logger for WeChat adapter.</param>
        /// <returns>WeChat request message.</returns>
        public static IRequestMessageBase GetRequestEntity(XDocument doc, ILogger logger)
        {
            IRequestMessageBase requestMessage = null;
            RequestMessageType msgType;

            try
            {
                msgType = MsgTypeHelper.GetRequestMsgType(doc);
                switch (msgType)
                {
                    case RequestMessageType.Location:
                        requestMessage = EntityHelper.FillEntityWithXml<LocationRequest>(doc);
                        break;
                    case RequestMessageType.Image:
                        requestMessage = EntityHelper.FillEntityWithXml<ImageRequest>(doc);
                        break;
                    case RequestMessageType.Link:
                        requestMessage = EntityHelper.FillEntityWithXml<LinkRequest>(doc);
                        break;
                    case RequestMessageType.Text:
                        requestMessage = EntityHelper.FillEntityWithXml<TextRequest>(doc);
                        break;
                    case RequestMessageType.Video:
                        requestMessage = EntityHelper.FillEntityWithXml<VideoRequest>(doc);
                        break;
                    case RequestMessageType.Voice:
                        requestMessage = EntityHelper.FillEntityWithXml<VoiceRequest>(doc);
                        break;
                    case RequestMessageType.ShortVideo:
                        requestMessage = EntityHelper.FillEntityWithXml<ShortVideoRequest>(doc);
                        break;
                    case RequestMessageType.Event:
                        switch (doc.Root.Element("Event").Value)
                        {
                            case EventType.Enter:
                                requestMessage = EntityHelper.FillEntityWithXml<EnterEvent>(doc);
                                break;
                            case EventType.Location:
                                requestMessage = EntityHelper.FillEntityWithXml<LocationEvent>(doc);
                                break;
                            case EventType.Subscribe:
                                requestMessage = EntityHelper.FillEntityWithXml<SubscribeEvent>(doc);
                                break;
                            case EventType.Unsubscribe:
                                requestMessage = EntityHelper.FillEntityWithXml<UnsunscribeEvent>(doc);
                                break;
                            case EventType.Click:
                                requestMessage = EntityHelper.FillEntityWithXml<ClickEvent>(doc);
                                break;
                            case EventType.Scan:
                                requestMessage = EntityHelper.FillEntityWithXml<ScanEvent>(doc);
                                break;
                            case EventType.View:
                                requestMessage = EntityHelper.FillEntityWithXml<ViewEvent>(doc);
                                break;
                            case EventType.ScanPush:
                                requestMessage = EntityHelper.FillEntityWithXml<ScanPushEvent>(doc);
                                break;
                            case EventType.WaitScanPush:
                                requestMessage = EntityHelper.FillEntityWithXml<WaitScanPushEvent>(doc);
                                break;
                            case EventType.Camera:
                                requestMessage = EntityHelper.FillEntityWithXml<CameraEvent>(doc);
                                break;
                            case EventType.CameraOrAlbum:
                                requestMessage = EntityHelper.FillEntityWithXml<CameraOrAlbumEvent>(doc);
                                break;
                            case EventType.WeChatAlbum:
                                requestMessage = EntityHelper.FillEntityWithXml<WeChatAlbumEvent>(doc);
                                break;
                            case EventType.SelectLocation:
                                requestMessage = EntityHelper.FillEntityWithXml<SelectLocationEvent>(doc);
                                break;
                            case EventType.MassSendJobFinished:
                                requestMessage = EntityHelper.FillEntityWithXml<MassSendJobFinishedEvent>(doc);
                                break;
                            case EventType.TemplateSendFinished:
                                requestMessage = EntityHelper.FillEntityWithXml<TemplateSendFinishedEvent>(doc);
                                break;
                            case EventType.ViewMiniProgram:
                                requestMessage = EntityHelper.FillEntityWithXml<ViewMiniProgramEvent>(doc);
                                break;

                                /* Not support right now
                                case "CARD_PASS_CHECK":
                                    requestMessage = EntityHelper.FillEntityWithXml<CardReviewSuccessfulEvent>(doc);
                                    break;
                                case "CARD_NOT_PASS_CHECK":
                                    requestMessage = EntityHelper.FillEntityWithXml<CardReviewFailedEvent>(doc);
                                    break;
                                case "USER_GET_CARD":
                                    requestMessage = EntityHelper.FillEntityWithXml<CardCollectedEvent>(doc);
                                    break;
                                case "USER_DEL_CARD":
                                    requestMessage = EntityHelper.FillEntityWithXml<CardDeletedEvent>(doc);
                                    break;
                                case "USER_GIFTING_CARD":
                                    requestMessage = EntityHelper.FillEntityWithXml<CardGiftingEvent>(doc);
                                    break;
                                case "USER_CONSUME_CARD":
                                    requestMessage = EntityHelper.FillEntityWithXml<RemoveAfterUseEvent>(doc);
                                    break;
                                case "USER_VIEW_CARD":
                                    requestMessage = EntityHelper.FillEntityWithXml<ViewCardEvent>(doc);
                                    break;
                                case "UPDATE_MEMBER_CARD":
                                    requestMessage = EntityHelper.FillEntityWithXml<MemberCardUpdatedEvent>(doc);
                                    break;
                                case "CARD_SKU_REMIND":
                                    requestMessage = EntityHelper.FillEntityWithXml<CardLowInStockEvent>(doc);
                                    break;
                                case "CARD_PAY_ORDER":
                                    requestMessage = EntityHelper.FillEntityWithXml<CardPointChangeEvent>(doc);
                                    break;
                                case "SUBMIT_MEMBERCARD_USER_INFO":
                                    requestMessage = EntityHelper.FillEntityWithXml<MemberShipActivatedEvent>(doc);
                                    break;
                                */

                                // TODO: kf, verify, WeApp, Wxa
                        }

                        break;

                    default:
                        {
                            requestMessage = new UnknowRequest()
                            {
                                Content = doc,
                            };
                            break;
                        }
                }
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex, string.Format("RequestMessage Error, MsgType may not exist, XML：{0}", doc, CultureInfo.InvariantCulture));
                throw;
            }

            return requestMessage;
        }

        public static string ConvertResponseToXml(object entity)
        {
            var responseXmlString = string.Empty;

            // Just let it throw when convert entity failed.
            if (entity is IResponseMessageBase responseMessage)
            {
                switch (responseMessage.MsgType)
                {
                    case ResponseMessageType.Text:
                        responseXmlString = EntityHelper.ConvertEntityToXmlString<TextResponse>(responseMessage);
                        break;
                    case ResponseMessageType.Image:
                        responseXmlString = EntityHelper.ConvertEntityToXmlString<ImageResponse>(responseMessage);
                        break;
                    case ResponseMessageType.Voice:
                        responseXmlString = EntityHelper.ConvertEntityToXmlString<VoiceResponse>(responseMessage);
                        break;
                    case ResponseMessageType.Video:
                        responseXmlString = EntityHelper.ConvertEntityToXmlString<VideoResponse>(responseMessage);
                        break;
                    case ResponseMessageType.Music:
                        responseXmlString = EntityHelper.ConvertEntityToXmlString<MusicResponse>(responseMessage);
                        break;
                    case ResponseMessageType.News:
                        responseXmlString = EntityHelper.ConvertEntityToXmlString<NewsResponse>(responseMessage);
                        break;
                }
            }
            else
            {
                responseXmlString = entity.ToString();
            }

            return responseXmlString;
        }
    }
}
