﻿using System.Xml.Linq;
using Microsoft.Bot.Builder.Adapters.WeChat.Schema.Request;
using Microsoft.Bot.Builder.Adapters.WeChat.Schema.Request.Event;
using Microsoft.Bot.Builder.Adapters.WeChat.Schema.Request.Event.Common;
using Microsoft.Bot.Builder.Adapters.WeChat.Test.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Bot.Builder.Adapters.WeChat.Test
{
    [TestClass]
    public class RequestMessageFactoryTest
    {
        [TestMethod]
        public void GetRequestEntityTest()
        {
            {
                // Text
                var doc = XDocument.Parse(MockDataUtility.XmlText);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is TextRequest);
                MessageBaseTest(result as TextRequest);
                var textRequest = result as TextRequest;
                Assert.AreEqual("this is a test", textRequest.Content);
            }

            {
                // Image
                var doc = XDocument.Parse(MockDataUtility.XmlImage);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is ImageRequest);
                MessageBaseTest(result as RequestMessage);
                var imageRequest = result as ImageRequest;
                Assert.AreEqual("this is a url", imageRequest.PicUrl);
                Assert.AreEqual("media_id", imageRequest.MediaId);
            }

            {
                // Voice
                var doc = XDocument.Parse(MockDataUtility.XmlVoice);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is VoiceRequest);
                MessageBaseTest(result as RequestMessage);
                var voiceRequest = result as VoiceRequest;
                Assert.AreEqual("media_id", voiceRequest.MediaId);
                Assert.AreEqual("Format", voiceRequest.Format);
            }

            {
                // Video
                var doc = XDocument.Parse(MockDataUtility.XmlVideo);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is VideoRequest);
                MessageBaseTest(result as RequestMessage);
                var videoRequest = result as VideoRequest;
                Assert.AreEqual("media_id", videoRequest.MediaId);
                Assert.AreEqual("thumb_media_id", videoRequest.ThumbMediaId);
            }

            {
                // Short Video
                var doc = XDocument.Parse(MockDataUtility.XmlShortVideo);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is ShortVideoRequest);
                MessageBaseTest(result as RequestMessage);
                var shortvideoRequest = result as ShortVideoRequest;
                Assert.AreEqual("media_id", shortvideoRequest.MediaId);
                Assert.AreEqual("thumb_media_id", shortvideoRequest.ThumbMediaId);
            }

            {
                // Location
                var doc = XDocument.Parse(MockDataUtility.XmlLocation);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is LocationRequest);
                MessageBaseTest(result as RequestMessage);
                var locationRequest = result as LocationRequest;
                Assert.AreEqual(23.134521, locationRequest.Location_X);
                Assert.AreEqual(113.358803, locationRequest.Location_Y);
                Assert.AreEqual(20, locationRequest.Scale);
                Assert.AreEqual("LocationInfo", locationRequest.Label);
            }

            {
                // Link
                var doc = XDocument.Parse(MockDataUtility.XmlLink);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is LinkRequest);
                MessageBaseTest(result as RequestMessage);
                var linkRequest = result as LinkRequest;
                Assert.AreEqual("This is a link", linkRequest.Title);
                Assert.AreEqual("This is a link", linkRequest.Description);
                Assert.AreEqual("url", linkRequest.Url);
            }

            {
                // Click Event
                var doc = XDocument.Parse(MockDataUtility.XmlEventClick);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is ClickEvent);
                EventBaseTest(result as RequestEvent);
                var clickEvent = result as ClickEvent;
                Assert.AreEqual(clickEvent.Event, EventType.Click);
                Assert.AreEqual("EVENTKEY", clickEvent.EventKey);
            }

            {
                // Location Event
                var doc = XDocument.Parse(MockDataUtility.XmlEventLocation);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is LocationEvent);
                EventBaseTest(result as RequestEvent);
                var locationEvent = result as LocationEvent;
                Assert.AreEqual(23.104105, locationEvent.Latitude);
                Assert.AreEqual(113.320107, locationEvent.Longitude);
                Assert.AreEqual(65.000000, locationEvent.Precision);
            }

            {
                // View Event
                var doc = XDocument.Parse(MockDataUtility.XmlEventView);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is ViewEvent);
                EventBaseTest(result as RequestEvent);
                var viewEvent = result as ViewEvent;
                Assert.AreEqual("www.qq.com", viewEvent.EventKey);
            }

            {
                // Subscribe Event
                var doc = XDocument.Parse(MockDataUtility.XmlEventSubscribe);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is SubscribeEvent);
                EventBaseTest(result as RequestEvent);
                var subscribeEvent = result as SubscribeEvent;
                Assert.AreEqual("qrscene_123123", subscribeEvent.EventKey);
                Assert.AreEqual("TICKET", subscribeEvent.Ticket);
            }

            {
                // Scan Event
                var doc = XDocument.Parse(MockDataUtility.XmlEventScan);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is ScanEvent);
                EventBaseTest(result as RequestEvent);
                var scanEvent = result as ScanEvent;
                Assert.AreEqual("SCENE_VALUE", scanEvent.EventKey);
                Assert.AreEqual("TICKET", scanEvent.Ticket);
            }

            {
                // ScanPush Event
                var doc = XDocument.Parse(MockDataUtility.XmlEventScanPush);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is ScanPushEvent);
                EventBaseTest(result as ScanPushEvent);
                var scanPushEvent = result as ScanPushEvent;
                Assert.AreEqual("6", scanPushEvent.EventKey);
                Assert.AreEqual("qrcode", scanPushEvent.ScanCodeInfo.ScanType);
                Assert.AreEqual("1", scanPushEvent.ScanCodeInfo.ScanResult);
            }

            {
                // WaitScanPush Event
                var doc = XDocument.Parse(MockDataUtility.XmlEventWaitScanPush);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is WaitScanPushEvent);
                EventBaseTest(result as WaitScanPushEvent);
                var waitScanPushEvent = result as WaitScanPushEvent;
                Assert.AreEqual("6", waitScanPushEvent.EventKey);
                Assert.AreEqual("qrcode", waitScanPushEvent.ScanCodeInfo.ScanType);
                Assert.AreEqual("2", waitScanPushEvent.ScanCodeInfo.ScanResult);
            }

            {
                // Camera Event
                var doc = XDocument.Parse(MockDataUtility.XmlEventCamera);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is CameraEvent);
                EventBaseTest(result as CameraEvent);
                var cameraEvent = result as CameraEvent;
                Assert.AreEqual("6", cameraEvent.EventKey);
                Assert.AreEqual(1, cameraEvent.SendPicsInfo.Count);
                Assert.AreEqual("1b5f7c23b5bf75682a53e7b6d163e185", cameraEvent.SendPicsInfo.PicList[0].Item.PicMD5Sum);
            }

            {
                // CameraOrAlbum Event
                var doc = XDocument.Parse(MockDataUtility.XmlEventCameraOrAlbum);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is CameraOrAlbumEvent);
                EventBaseTest(result as CameraOrAlbumEvent);
                var cameraOrAlbumEvent = result as CameraOrAlbumEvent;
                Assert.AreEqual("6", cameraOrAlbumEvent.EventKey);
                Assert.AreEqual(1, cameraOrAlbumEvent.SendPicsInfo.Count);
                Assert.AreEqual("5a75aaca956d97be686719218f275c6b", cameraOrAlbumEvent.SendPicsInfo.PicList[0].Item.PicMD5Sum);
            }

            {
                // WeChatAlbum Event
                var doc = XDocument.Parse(MockDataUtility.XmlEventWeChatAlbum);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is WeChatAlbumEvent);
                EventBaseTest(result as WeChatAlbumEvent);
                var wechatAlbumEvent = result as WeChatAlbumEvent;
                Assert.AreEqual("6", wechatAlbumEvent.EventKey);
                Assert.AreEqual(1, wechatAlbumEvent.SendPicsInfo.Count);
                Assert.AreEqual("5a75aaca956d97be686719218f275c6b", wechatAlbumEvent.SendPicsInfo.PicList[0].Item.PicMD5Sum);
            }

            {
                // SelectLocation Event
                var doc = XDocument.Parse(MockDataUtility.XmlEventSelectLocation);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is SelectLocationEvent);
                EventBaseTest(result as SelectLocationEvent);
                var selectLocationEvent = result as SelectLocationEvent;
                Assert.AreEqual("6", selectLocationEvent.EventKey);
                Assert.AreEqual("23", selectLocationEvent.SendLocationInfo.Location_X);
                Assert.AreEqual("113", selectLocationEvent.SendLocationInfo.Location_Y);
                Assert.AreEqual("15", selectLocationEvent.SendLocationInfo.Scale);
                Assert.AreEqual("No.328 Xinghu Street", selectLocationEvent.SendLocationInfo.Label);
                Assert.AreEqual("test", selectLocationEvent.SendLocationInfo.Poiname);
            }

            {
                // ViewMiniProgram Event
                var doc = XDocument.Parse(MockDataUtility.XmlEventViewMiniProgram);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is ViewMiniProgramEvent);
                EventBaseTest(result as ViewMiniProgramEvent);
                var viewMiniProgramEvent = result as ViewMiniProgramEvent;
                Assert.AreEqual("pages/index/index", viewMiniProgramEvent.EventKey);
                Assert.AreEqual("MENUID", viewMiniProgramEvent.MenuId);
            }

            {
                // MassSendJobFinished Event
                var doc = XDocument.Parse(MockDataUtility.XmlEventMassSendJobFinished);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is MassSendJobFinishedEvent);
                EventBaseTest(result as MassSendJobFinishedEvent);
                var massSendJobFinishedEvent = result as MassSendJobFinishedEvent;
                Assert.AreEqual(1000001625, massSendJobFinishedEvent.MsgID);
                Assert.AreEqual("err(30003)", massSendJobFinishedEvent.Status);
                Assert.AreEqual(0, massSendJobFinishedEvent.TotalCount);
                Assert.AreEqual(0, massSendJobFinishedEvent.FilterCount);
                Assert.AreEqual(0, massSendJobFinishedEvent.SentCount);
                Assert.AreEqual(0, massSendJobFinishedEvent.ErrorCount);
                Assert.AreEqual(2, massSendJobFinishedEvent.CopyrightCheckResult.Count);
                Assert.AreEqual(1, massSendJobFinishedEvent.CopyrightCheckResult.ResultList.Items[0].ArticleIdx);
                Assert.AreEqual(0, massSendJobFinishedEvent.CopyrightCheckResult.ResultList.Items[0].UserDeclareState);
                Assert.AreEqual(2, massSendJobFinishedEvent.CopyrightCheckResult.ResultList.Items[0].AuditState);
                Assert.AreEqual("Url_1", massSendJobFinishedEvent.CopyrightCheckResult.ResultList.Items[0].OriginalArticleUrl);
                Assert.AreEqual(1, massSendJobFinishedEvent.CopyrightCheckResult.ResultList.Items[0].OriginalArticleType);
                Assert.AreEqual(1, massSendJobFinishedEvent.CopyrightCheckResult.ResultList.Items[0].CanReprint);
                Assert.AreEqual(1, massSendJobFinishedEvent.CopyrightCheckResult.ResultList.Items[0].NeedReplaceContent);
                Assert.AreEqual(1, massSendJobFinishedEvent.CopyrightCheckResult.ResultList.Items[0].NeedShowReprintSource);
                Assert.AreEqual("Url_2", massSendJobFinishedEvent.CopyrightCheckResult.ResultList.Items[1].OriginalArticleUrl);
                Assert.AreEqual(2, massSendJobFinishedEvent.CopyrightCheckResult.CheckState);
            }

            {
                // TemplateSendFinished
                var doc = XDocument.Parse(MockDataUtility.XmlEventTemplateSendFinished);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsTrue(result is TemplateSendFinishedEvent);
                EventBaseTest(result as TemplateSendFinishedEvent);
                var templateSendFinishedEvent = result as TemplateSendFinishedEvent;
                Assert.AreEqual(200163840, templateSendFinishedEvent.MsgID);
                Assert.AreEqual("failed:user block", templateSendFinishedEvent.Status);
            }
        }

        public void MessageBaseTest(RequestMessage result)
        {
            Assert.AreEqual("toUser", result.ToUserName);
            Assert.AreEqual("fromUser", result.FromUserName);
            Assert.AreEqual(1234567890123456, result.MsgId);
            Assert.AreEqual(1348831860, result.CreateTime);
        }

        public void EventBaseTest(RequestEvent result)
        {
            Assert.AreEqual("toUser", result.ToUserName);
            Assert.AreEqual("FromUser", result.FromUserName);
            Assert.AreEqual(123456789, result.CreateTime);
        }
    }
}
