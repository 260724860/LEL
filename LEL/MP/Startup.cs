﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.RegisterServices;
using Swashbuckle.AspNetCore.Swagger;
using Senparc.CO2NET.Utilities;
using Senparc.CO2NET;
using Senparc.CO2NET.Cache;
using Senparc.Weixin.RegisterServices;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Entities;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.WxOpen;

namespace MP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("allow_all", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
   

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Swagger文档配置
            //Swagger所需要的配置项
            services.AddSwaggerGen(c =>
            {
                //添加Swagger.
                c.SwaggerDoc("LELDoc", new Info
                {
                    Version = "LELDoc",
                    Title = "乐尔乐独立公众号平台",
                    Description = "乐尔乐独立公众号平台,和其他业务进行拆分",
                    //服务条款
                    TermsOfService = "None",
                    //作者信息
                    Contact = new Contact
                    {
                        Name = "谢超远",
                        Email = "260724860@qq.com",
                        Url = "http://www.baidu.com/"
                    },
                    //许可证
                    License = new License
                    {
                        Name = "谢超远著",
                        Url = "http://www.baidu.com/"
                    }
                });
                // 下面三个方法为 Swagger JSON and UI设置xml文档注释路径
                var basePath = System.IO.Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = System.IO.Path.Combine(basePath, "MP.xml");
                c.IncludeXmlComments(xmlPath);
            });
            #endregion

            #region 公众号和小程序配置
            //services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMemoryCache();//使用本地缓存必须添加
            services.AddSession();//使用Session
            /*
             * CO2NET 是从 Senparc.Weixin 分离的底层公共基础模块，经过了长达 6 年的迭代优化，稳定可靠。
             * 关于 CO2NET 在所有项目中的通用设置可参考 CO2NET 的 Sample：
             * https://github.com/Senparc/Senparc.CO2NET/blob/master/Sample/Senparc.CO2NET.Sample.netcore/Startup.cs
             */
            services.AddSenparcGlobalServices(Configuration)//Senparc.CO2NET 全局注册
                    .AddSenparcWeixinServices(Configuration);//Senparc.Weixin 注册


            #endregion

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env ,IOptions<SenparcSetting> senparcSetting, IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
            app.UseCors("allow_all");
            // 启用静态文件
            app.UseStaticFiles();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            #region 配置Swagger
            // 配置Swagger  必须加在app.UseMvc前面
            app.UseSwagger();
           // app.UseSwagger(c => { c.RouteTemplate = "swagger/{LELDoc}/swagger.json"; });
            //Swagger Core需要配置的  必须加在app.UseMvc前面
            app.UseSwaggerUI(c =>
            {
                //c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                //c.EnableDeepLinking();
                c.SwaggerEndpoint("/swagger/LELDoc/swagger.json", "LELDoc");
            });
            #endregion

            app.UseHttpsRedirection();
            app.UseMvc();

            // 启动 CO2NET 全局注册，必须！
            IRegisterService register = RegisterService.Start(env, senparcSetting.Value)
                                                        //关于 UseSenparcGlobal() 的更多用法见 CO2NET Demo：https://github.com/Senparc/Senparc.CO2NET/blob/master/Sample/Senparc.CO2NET.Sample.netcore/Startup.cs
                                                        .UseSenparcGlobal();

            //如果需要自动扫描自定义扩展缓存，可以这样使用：
            //register.UseSenparcGlobal(true);
            //如果需要指定自定义扩展缓存，可以这样用：
            //register.UseSenparcGlobal(false, GetExCacheStrategies);

            #region CO2NET 全局配置

            #region 全局缓存配置（按需）

            //当同一个分布式缓存同时服务于多个网站（应用程序池）时，可以使用命名空间将其隔离（非必须）
            register.ChangeDefaultCacheNamespace("DefaultCO2NETCache");


            #endregion

            #region 注册日志（按需，建议）

            register.RegisterTraceLog(ConfigTraceLog);//配置TraceLog

            #endregion

            #endregion

            #region 微信相关配置


            /* 微信配置开始
             * 
             * 建议按照以下顺序进行注册，尤其须将缓存放在第一位！
             */

            //注册开始

            //开始注册微信信息，必须！
            register.UseSenparcWeixin(senparcWeixinSetting.Value, senparcSetting.Value)
                //注意：上一行没有 ; 下面可接着写 .RegisterXX()

            #region 注册公众号或小程序（按需）

                //注册公众号（可注册多个）                                                -- DPBMARK MP
                .RegisterMpAccount(senparcWeixinSetting.Value, "【盛派网络小助手】公众号")// DPBMARK_END

                //注册多个公众号或小程序（可注册多个）                                        -- DPBMARK MiniProgram
                .RegisterWxOpenAccount(senparcWeixinSetting.Value, "【盛派网络小助手】小程序")// DPBMARK_END

                //除此以外，仍然可以在程序任意地方注册公众号或小程序：
                //AccessTokenContainer.Register(appId, appSecret, name);//命名空间：Senparc.Weixin.MP.Containers
            #endregion

            #region 注册微信支付（按需）        -- DPBMARK TenPay

                //注册旧微信支付版本（V2）（可注册多个）
                //.RegisterTenpayOld(senparcWeixinSetting.Value, "【盛派网络小助手】公众号")//这里的 name 和第一个 RegisterMpAccount() 中的一致，会被记录到同一个 SenparcWeixinSettingItem 对象中

                //注册最新微信支付版本（V3）（可注册多个）
               // .RegisterTenpayV3(senparcWeixinSetting.Value, "【盛派网络小助手】公众号")//记录到同一个 SenparcWeixinSettingItem 对象中

            #endregion                          // DPBMARK_END

            #region 注册微信第三方平台（按需）  -- DPBMARK Open

                //注册第三方平台（可注册多个）
                //.RegisterOpenComponent(senparcWeixinSetting.Value,
                //    //getComponentVerifyTicketFunc
                //    componentAppId =>
                //    {
                //        var dir = Path.Combine(ServerUtility.ContentRootMapPath("~/App_Data/OpenTicket"));
                //        if (!Directory.Exists(dir))
                //        {
                //            Directory.CreateDirectory(dir);
                //        }

                //        var file = Path.Combine(dir, string.Format("{0}.txt", componentAppId));
                //        using (var fs = new FileStream(file, FileMode.Open))
                //        {
                //            using (var sr = new StreamReader(fs))
                //            {
                //                var ticket = sr.ReadToEnd();
                //                return ticket;
                //            }
                //        }
                //    },

                //     //getAuthorizerRefreshTokenFunc
                //     (componentAppId, auhtorizerId) =>
                //     {
                //         var dir = Path.Combine(ServerUtility.ContentRootMapPath("~/App_Data/AuthorizerInfo/" + componentAppId));
                //         if (!Directory.Exists(dir))
                //         {
                //             Directory.CreateDirectory(dir);
                //         }

                //         var file = Path.Combine(dir, string.Format("{0}.bin", auhtorizerId));
                //         if (!File.Exists(file))
                //         {
                //             return null;
                //         }

                //         using (Stream fs = new FileStream(file, FileMode.Open))
                //         {
                //             var binFormat = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //             var result = (RefreshAuthorizerTokenResult)binFormat.Deserialize(fs);
                //             return result.authorizer_refresh_token;
                //         }
                //     },

                //     //authorizerTokenRefreshedFunc
                //     (componentAppId, auhtorizerId, refreshResult) =>
                //     {
                //         var dir = Path.Combine(ServerUtility.ContentRootMapPath("~/App_Data/AuthorizerInfo/" + componentAppId));
                //         if (!Directory.Exists(dir))
                //         {
                //             Directory.CreateDirectory(dir);
                //         }

                //         var file = Path.Combine(dir, string.Format("{0}.bin", auhtorizerId));
                //         using (Stream fs = new FileStream(file, FileMode.Create))
                //         {
                //             //这里存了整个对象，实际上只存RefreshToken也可以，有了RefreshToken就能刷新到最新的AccessToken
                //             var binFormat = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //             binFormat.Serialize(fs, refreshResult);
                //             fs.Flush();
                //         }
                //     }, "【盛派网络】开放平台")

            //除此以外，仍然可以在程序任意地方注册开放平台：
            //ComponentContainer.Register();//命名空间：Senparc.Weixin.Open.Containers
            #endregion                          // DPBMARK_END

            ;

            /* 微信配置结束 */

            #endregion
        }

        /// <summary>
        /// 配置微信跟踪日志
        /// </summary>
        private void ConfigTraceLog()
        {
            //这里设为Debug状态时，/App_Data/WeixinTraceLog/目录下会生成日志文件记录所有的API请求日志，正式发布版本建议关闭

            //如果全局的IsDebug（Senparc.CO2NET.Config.IsDebug）为false，此处可以单独设置true，否则自动为true
            Senparc.CO2NET.Trace.SenparcTrace.SendCustomLog("系统日志", "系统启动");//只在Senparc.Weixin.Config.IsDebug = true的情况下生效

            //全局自定义日志记录回调
            Senparc.CO2NET.Trace.SenparcTrace.OnLogFunc = () =>
            {
                //加入每次触发Log后需要执行的代码
            };

            //当发生基于WeixinException的异常时触发
            WeixinTrace.OnWeixinExceptionFunc = ex =>
            {
                //加入每次触发WeixinExceptionLog后需要执行的代码

                //发送模板消息给管理员                             -- DPBMARK Redis
                var eventService = new Senparc.Weixin.MP.Sample.CommonService.EventService();
                eventService.ConfigOnWeixinExceptionFunc(ex);      // DPBMARK_END
            };
        }
    }
}
