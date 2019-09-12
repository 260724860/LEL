﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace MPApiService
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// 乐尔乐独立公众号平台,和其他业务进行拆分
    /// </summary>
    public partial interface IMPApiServiceClient : IDisposable
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        JsonSerializerSettings SerializationSettings { get; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        JsonSerializerSettings DeserializationSettings { get; }

        /// <summary>
        /// Subscription credentials which uniquely identify client
        /// subscription.
        /// </summary>
        ServiceClientCredentials Credentials { get; }


            /// <summary>
        /// 获取Url授权链接
        /// </summary>
        /// <param name='appid'>
        /// </param>
        /// <param name='url'>
        /// </param>
        /// <param name='state'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> GetAuthorizeUrlWithHttpMessagesAsync(string appid = default(string), string url = default(string), string state = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 获取用户信息授权回调获取
        /// </summary>
        /// <param name='appid'>
        /// </param>
        /// <param name='code'>
        /// </param>
        /// <param name='state'>
        /// </param>
        /// <param name='returnUrl'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> UserInfoCallbackWithHttpMessagesAsync(string appid = default(string), string code = default(string), string state = default(string), string returnUrl = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name='openid'>
        /// </param>
        /// <param name='orderNO'>
        /// </param>
        /// <param name='unionid'>
        /// </param>
        /// <param name='pickupTime'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> SendSuppliersTemplateMsgWithHttpMessagesAsync(string openid = default(string), string orderNO = default(string), string unionid = default(string), string pickupTime = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// GET请求用于处理微信小程序后台的URL验证
        /// </summary>
        /// <param name='postModel'>
        /// </param>
        /// <param name='echostr'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> GetWithHttpMessagesAsync(PostModel postModel = default(PostModel), string echostr = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
        /// </summary>
        /// <param name='postModel'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> PostWithHttpMessagesAsync(PostModel postModel = default(PostModel), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 消息测试
        /// </summary>
        /// <param name='nickName'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> RequestDataWithHttpMessagesAsync(string nickName = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// wx.login登陆成功之后发送的请求
        /// </summary>
        /// <param name='code'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> OnLoginWithHttpMessagesAsync(string code = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 签名校验
        /// </summary>
        /// <param name='sessionId'>
        /// </param>
        /// <param name='rawData'>
        /// </param>
        /// <param name='signature'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> CheckWxOpenSignatureWithHttpMessagesAsync(string sessionId = default(string), string rawData = default(string), string signature = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name='type'>
        /// </param>
        /// <param name='sessionId'>
        /// </param>
        /// <param name='encryptedData'>
        /// </param>
        /// <param name='iv'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> DecodeEncryptedDataWithHttpMessagesAsync(string type = default(string), string sessionId = default(string), string encryptedData = default(string), string iv = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 模板消息测试
        /// </summary>
        /// <param name='sessionId'>
        /// </param>
        /// <param name='formId'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> TemplateTestWithHttpMessagesAsync(string sessionId = default(string), string formId = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 解密电话号码
        /// </summary>
        /// <param name='sessionId'>
        /// </param>
        /// <param name='encryptedData'>
        /// </param>
        /// <param name='iv'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> DecryptPhoneNumberWithHttpMessagesAsync(string sessionId = default(string), string encryptedData = default(string), string iv = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 解密运动步数
        /// </summary>
        /// <param name='sessionId'>
        /// </param>
        /// <param name='encryptedData'>
        /// </param>
        /// <param name='iv'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> DecryptRunDataWithHttpMessagesAsync(string sessionId = default(string), string encryptedData = default(string), string iv = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 小程序支付
        /// </summary>
        /// <param name='sessionId'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> GetPrepayidWithHttpMessagesAsync(string sessionId = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

    }
}
