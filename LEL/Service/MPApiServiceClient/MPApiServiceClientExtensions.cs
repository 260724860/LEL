﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace MPApiService
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// Extension methods for MPApiServiceClient.
    /// </summary>
    public static partial class MPApiServiceClientExtensions
    {
            /// <summary>
            /// 获取Url授权链接
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='appid'>
            /// </param>
            /// <param name='url'>
            /// </param>
            /// <param name='state'>
            /// </param>
            public static void GetAuthorizeUrl(this IMPApiServiceClient operations, string appid = default(string), string url = default(string), string state = default(string))
            {
                Task.Factory.StartNew(s => ((IMPApiServiceClient)s).GetAuthorizeUrlAsync(appid, url, state), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// 获取Url授权链接
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='appid'>
            /// </param>
            /// <param name='url'>
            /// </param>
            /// <param name='state'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task GetAuthorizeUrlAsync(this IMPApiServiceClient operations, string appid = default(string), string url = default(string), string state = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.GetAuthorizeUrlWithHttpMessagesAsync(appid, url, state, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// 获取用户信息授权回调获取
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='appid'>
            /// </param>
            /// <param name='code'>
            /// </param>
            /// <param name='state'>
            /// </param>
            /// <param name='returnUrl'>
            /// </param>
            public static void UserInfoCallback(this IMPApiServiceClient operations, string appid = default(string), string code = default(string), string state = default(string), string returnUrl = default(string))
            {
                Task.Factory.StartNew(s => ((IMPApiServiceClient)s).UserInfoCallbackAsync(appid, code, state, returnUrl), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// 获取用户信息授权回调获取
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='appid'>
            /// </param>
            /// <param name='code'>
            /// </param>
            /// <param name='state'>
            /// </param>
            /// <param name='returnUrl'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task UserInfoCallbackAsync(this IMPApiServiceClient operations, string appid = default(string), string code = default(string), string state = default(string), string returnUrl = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.UserInfoCallbackWithHttpMessagesAsync(appid, code, state, returnUrl, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// 发送模板消息
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='openid'>
            /// </param>
            /// <param name='orderNO'>
            /// </param>
            /// <param name='unionid'>
            /// </param>
            /// <param name='pickupTime'>
            /// </param>
            public static void SendSuppliersTemplateMsg(this IMPApiServiceClient operations, string openid = default(string), string orderNO = default(string), string unionid = default(string), string pickupTime = default(string))
            {
                Task.Factory.StartNew(s => ((IMPApiServiceClient)s).SendSuppliersTemplateMsgAsync(openid, orderNO, unionid, pickupTime), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// 发送模板消息
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='openid'>
            /// </param>
            /// <param name='orderNO'>
            /// </param>
            /// <param name='unionid'>
            /// </param>
            /// <param name='pickupTime'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task SendSuppliersTemplateMsgAsync(this IMPApiServiceClient operations, string openid = default(string), string orderNO = default(string), string unionid = default(string), string pickupTime = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.SendSuppliersTemplateMsgWithHttpMessagesAsync(openid, orderNO, unionid, pickupTime, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// GET请求用于处理微信小程序后台的URL验证
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='postModel'>
            /// </param>
            /// <param name='echostr'>
            /// </param>
            public static void Get(this IMPApiServiceClient operations, PostModel postModel = default(PostModel), string echostr = default(string))
            {
                Task.Factory.StartNew(s => ((IMPApiServiceClient)s).GetAsync(postModel, echostr), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// GET请求用于处理微信小程序后台的URL验证
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='postModel'>
            /// </param>
            /// <param name='echostr'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task GetAsync(this IMPApiServiceClient operations, PostModel postModel = default(PostModel), string echostr = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.GetWithHttpMessagesAsync(postModel, echostr, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='postModel'>
            /// </param>
            public static void Post(this IMPApiServiceClient operations, PostModel postModel = default(PostModel))
            {
                Task.Factory.StartNew(s => ((IMPApiServiceClient)s).PostAsync(postModel), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='postModel'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task PostAsync(this IMPApiServiceClient operations, PostModel postModel = default(PostModel), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.PostWithHttpMessagesAsync(postModel, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// 消息测试
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nickName'>
            /// </param>
            public static void RequestData(this IMPApiServiceClient operations, string nickName = default(string))
            {
                Task.Factory.StartNew(s => ((IMPApiServiceClient)s).RequestDataAsync(nickName), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// 消息测试
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nickName'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task RequestDataAsync(this IMPApiServiceClient operations, string nickName = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.RequestDataWithHttpMessagesAsync(nickName, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// wx.login登陆成功之后发送的请求
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='code'>
            /// </param>
            public static void OnLogin(this IMPApiServiceClient operations, string code = default(string))
            {
                Task.Factory.StartNew(s => ((IMPApiServiceClient)s).OnLoginAsync(code), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// wx.login登陆成功之后发送的请求
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='code'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task OnLoginAsync(this IMPApiServiceClient operations, string code = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.OnLoginWithHttpMessagesAsync(code, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// 签名校验
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='sessionId'>
            /// </param>
            /// <param name='rawData'>
            /// </param>
            /// <param name='signature'>
            /// </param>
            public static void CheckWxOpenSignature(this IMPApiServiceClient operations, string sessionId = default(string), string rawData = default(string), string signature = default(string))
            {
                Task.Factory.StartNew(s => ((IMPApiServiceClient)s).CheckWxOpenSignatureAsync(sessionId, rawData, signature), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// 签名校验
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='sessionId'>
            /// </param>
            /// <param name='rawData'>
            /// </param>
            /// <param name='signature'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task CheckWxOpenSignatureAsync(this IMPApiServiceClient operations, string sessionId = default(string), string rawData = default(string), string signature = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.CheckWxOpenSignatureWithHttpMessagesAsync(sessionId, rawData, signature, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// 解密
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='type'>
            /// </param>
            /// <param name='sessionId'>
            /// </param>
            /// <param name='encryptedData'>
            /// </param>
            /// <param name='iv'>
            /// </param>
            public static void DecodeEncryptedData(this IMPApiServiceClient operations, string type = default(string), string sessionId = default(string), string encryptedData = default(string), string iv = default(string))
            {
                Task.Factory.StartNew(s => ((IMPApiServiceClient)s).DecodeEncryptedDataAsync(type, sessionId, encryptedData, iv), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// 解密
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='type'>
            /// </param>
            /// <param name='sessionId'>
            /// </param>
            /// <param name='encryptedData'>
            /// </param>
            /// <param name='iv'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DecodeEncryptedDataAsync(this IMPApiServiceClient operations, string type = default(string), string sessionId = default(string), string encryptedData = default(string), string iv = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.DecodeEncryptedDataWithHttpMessagesAsync(type, sessionId, encryptedData, iv, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// 模板消息测试
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='sessionId'>
            /// </param>
            /// <param name='formId'>
            /// </param>
            public static void TemplateTest(this IMPApiServiceClient operations, string sessionId = default(string), string formId = default(string))
            {
                Task.Factory.StartNew(s => ((IMPApiServiceClient)s).TemplateTestAsync(sessionId, formId), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// 模板消息测试
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='sessionId'>
            /// </param>
            /// <param name='formId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task TemplateTestAsync(this IMPApiServiceClient operations, string sessionId = default(string), string formId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.TemplateTestWithHttpMessagesAsync(sessionId, formId, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// 解密电话号码
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='sessionId'>
            /// </param>
            /// <param name='encryptedData'>
            /// </param>
            /// <param name='iv'>
            /// </param>
            public static void DecryptPhoneNumber(this IMPApiServiceClient operations, string sessionId = default(string), string encryptedData = default(string), string iv = default(string))
            {
                Task.Factory.StartNew(s => ((IMPApiServiceClient)s).DecryptPhoneNumberAsync(sessionId, encryptedData, iv), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// 解密电话号码
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='sessionId'>
            /// </param>
            /// <param name='encryptedData'>
            /// </param>
            /// <param name='iv'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DecryptPhoneNumberAsync(this IMPApiServiceClient operations, string sessionId = default(string), string encryptedData = default(string), string iv = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.DecryptPhoneNumberWithHttpMessagesAsync(sessionId, encryptedData, iv, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// 解密运动步数
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='sessionId'>
            /// </param>
            /// <param name='encryptedData'>
            /// </param>
            /// <param name='iv'>
            /// </param>
            public static void DecryptRunData(this IMPApiServiceClient operations, string sessionId = default(string), string encryptedData = default(string), string iv = default(string))
            {
                Task.Factory.StartNew(s => ((IMPApiServiceClient)s).DecryptRunDataAsync(sessionId, encryptedData, iv), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// 解密运动步数
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='sessionId'>
            /// </param>
            /// <param name='encryptedData'>
            /// </param>
            /// <param name='iv'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DecryptRunDataAsync(this IMPApiServiceClient operations, string sessionId = default(string), string encryptedData = default(string), string iv = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.DecryptRunDataWithHttpMessagesAsync(sessionId, encryptedData, iv, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// 小程序支付
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='sessionId'>
            /// </param>
            public static void GetPrepayid(this IMPApiServiceClient operations, string sessionId = default(string))
            {
                Task.Factory.StartNew(s => ((IMPApiServiceClient)s).GetPrepayidAsync(sessionId), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// 小程序支付
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='sessionId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task GetPrepayidAsync(this IMPApiServiceClient operations, string sessionId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.GetPrepayidWithHttpMessagesAsync(sessionId, null, cancellationToken).ConfigureAwait(false);
            }

    }
}
