using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Yun.Share.Voice.BaseInterface;
using Yun.Share.Voice.DataBase;
namespace Yun.Share.Voice
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "Voice.Cors";

        public static string MatchAssemblies = "^Yun.Share.Voice";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddTransient(services);
            AddDbContext(services);
            AddSwaggerGen(services);
            AddMvcJson(services);
            services.AddControllers().AddNewtonsoftJson(); ;
            AddAuthentication(services);
            AddCors(services);
            InitBaseData(services);
            services.AddOptions();
            services.AddMvcCore(
                   options =>
                   {
                       options.Filters.Add<HttpGlobalExceptionFilter>();//全局注册
                   });
        }
        /// <summary>
        /// 初始化基础数据
        /// </summary>
        /// <param name="services"></param>
        private void InitBaseData(IServiceCollection services)
        {
            services.AddHttpContextAccessor();// 添加HttpContext 
            //注册http请求服务
            services.AddHttpClient();
        }
        private void AddMvcJson(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                //全局配置Json序列化处理
                .AddJsonOptions(options =>
                {
                    ////忽略循环引用
                    //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    ////不使用驼峰样式的key
                    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    ////设置时间格式
                    //options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                }
                );
        }
        /// <summary>
        /// 跨域设置
        /// </summary>
        /// <param name="services"></param>
        private void AddCors(IServiceCollection services)
        {
            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                                             //builder.WithOrigins(  // 如果需要读取配置的跨域 打开这个即可
                                             //   Configuration["Authentication:CorsOrigins"]
                                             //       .Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray()   
                                             //   )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });

            //services.AddCors(options => options.AddPolicy(_defaultCorsPolicyName, 
            //   builder => builder.WithOrigins(
            //           Configuration["Authentication:CorsOrigins"]
            //           .Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray()
            //       )
            //   .AllowAnyHeader()
            //   .AllowAnyMethod()
            //   .AllowCredentials()));

        }

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="services"></param>
        private void AddTransient(IServiceCollection services)
        {
            //Assembly assembly = Assembly.Load("Yun.Share.Voice.IApplication"); // 批量注入IApplication包下面的所有抽象类
            //foreach (var implement in assembly.GetTypes())
            //{
            //    Type[] interfaceType = implement.GetInterfaces();
            //    foreach (var service in interfaceType)
            //    {
            //        services.AddTransient(service, implement);
            //    }
            //}

            var baseType = typeof(IDependency);
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var getFiles = Directory.GetFiles(path, "*.dll").Where(Match);  //.Where(o=>o.Match())
            var referencedAssemblies = getFiles.Select(Assembly.LoadFrom).ToList();  //.Select(o=> Assembly.LoadFrom(o))         

            var ss = referencedAssemblies.SelectMany(o => o.GetTypes());

            var types = referencedAssemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToList();
            var implementTypes = types.Where(x => x.IsClass && !x.Name.EndsWith("Controller")).ToList();
            var interfaceTypes = types.Where(x => x.IsInterface).ToList();

            foreach (var implementType in implementTypes)
            {
                var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                if (interfaceType != null)
                    services.AddTransient(interfaceType, implementType);

                /// <summary>
                /// 实现该接口将自动注册到Ioc容器，生命周期为每次请求创建一个实例
                //        /// </summary>
                //public interface IScopeDependency : IDependency
                //{
                //}
                ///// <summary>
                ///// 实现该接口将自动注册到Ioc容器，生命周期为单例
                ///// </summary>
                //public interface ISingletonDependency : IDependency
                //{
                //}
                ///// <summary>
                ///// 实现该接口将自动注册到Ioc容器，生命周期为每次创建一个新实例
                ///// </summary>
                //public interface ITransientDependency : IDependency
                //{
                //}

                //if (typeof(IScopeDependency).IsAssignableFrom(implementType))
                //{
                //    var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                //    if (interfaceType != null)
                //        services.AddScoped(interfaceType, implementType);
                //}
                //else if (typeof(ISingletonDependency).IsAssignableFrom(implementType))
                //{
                //    var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                //    if (interfaceType != null)
                //        services.AddSingleton(interfaceType, implementType);
                //}
                //else
                //{
                //    var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                //    if (interfaceType != null)
                //        services.AddTransient(interfaceType, implementType);
                //}
            }


            //services.AddSingleton<IAuthorizationHandler, PolicyHandler>();
            //services.AddTransient<IJwtTokenServer, JwtTokenServer>(); // 自定义注入
            //services.AddTransient<IUserLoginServer, UserLoginServer>(); // 自定义注入
            //services.AddTransient<IFileServer, FileServer>(); // 自定义注入
            //services.AddTransient<IUserServer, UserServer>(); // 自定义注入
            //services.AddTransient<IAliyunOssStorageServer, AliyunOssStorageServer>(); // 自定义注入
            //services.AddTransient<IFileGroupServer, FileGroupServer>(); // 自定义注入
            //services.AddTransient<IMessgeServer, MessgeServer>(); // 自定义注入
            //services.AddTransient<IUserCardServer, UserCardServer>(); // 自定义注入
            //services.AddTransient<IPayConfigeServer, PayConfigeServer>(); // 自定义注入
            //services.AddTransient<IWeCharCodeServer, WeCharCodeServer>(); // 自定义注入
            //services.AddTransient<IModelConfigeServer, ModelConfigeServer>(); // 自定义注入
            //services.AddTransient<IScreenServer, ScreenServer>(); // 自定义注入
            //services.AddTransient<IUserApplyConfigsServer, UserApplyConfigsServer>(); // 自定义注入
            //services.AddTransient<IOrderServer, OrderServer>(); // 自定义注入

        }

        /// <summary>
        /// 程序集是否匹配
        /// </summary>
        private static bool Match(string assemblyName)
        {

            assemblyName = Path.GetFileName(assemblyName);
            if (assemblyName.StartsWith($"{AppDomain.CurrentDomain.FriendlyName}.Views"))
                return false;
            if (assemblyName.StartsWith($"{AppDomain.CurrentDomain.FriendlyName}.PrecompiledViews"))
                return false;
            return Regex.IsMatch(assemblyName, MatchAssemblies, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
        /// 数据库连接
        /// </summary>
        /// <param name="services"></param>
        private void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<CoreDbContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("VoiceDB"), new MySqlServerVersion(new Version("5.0.2"))));
        }
        private void AddSwaggerGen(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Support APP API", Version = "v1" });
            });
            //    services.AddSwaggerGen(s =>
            //    {
            //        //Add Jwt Authorize to http header
            //        s.AddSecurityDefinition("Bearer", new ApiKeyScheme
            //        {
            //            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            //            Name = "Authorization",//Jwt default param name
            //            In = "header",//Jwt store address
            //            Type = "apiKey"//Security scheme type
            //        });
            //        //Add authentication type
            //        s.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
            //{
            //    { "Bearer", Array.Empty<string>() }
            //});
            //    });
        }
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="services"></param>
        private void AddAuthentication(IServiceCollection services)
        {
            #region JWT配置
            services.AddAuthentication(options =>
            {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                //主要是jwt  token参数设置
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //颁发者
                    ValidateIssuer = true,//是否验证Issuer
                    ValidIssuer = Configuration["Authentication:Jwt:Issuer"],
                    //被授权者
                    ValidateAudience = false,//是否验证Audience
                    ValidAudience = Configuration["Authentication:Jwt:Audience"],
                    //秘钥
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:Jwt:SecurityKey"])),
                    //是否验证失效时间【使用当前时间与Token的Claims中的NotBefore和Expires对比】
                    ValidateLifetime = true,//是否验证失效时间
                    ClockSkew = TimeSpan.FromMinutes(5)//允许的服务器时间偏移量【5分钟】
                };
            });

            #endregion;
            //services.AddMvc();
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //   .AddJwtBearer(options => {
            //       options.TokenValidationParameters = new TokenValidationParameters
            //       {
            //           ValidateIssuer = true,//是否验证Issuer
            //           ValidateAudience = true,//是否验证Audience
            //           ValidateLifetime = true,//是否验证失效时间
            //           ValidateIssuerSigningKey = true,//是否验证SecurityKey
            //           ValidAudience = Configuration["Authentication:Jwt:Audience"] ,
            //           //山下这两项和签发token时的issuer,Audience一致
            //           ValidIssuer = Configuration["Authentication:Jwt:Issuer"],
            //           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:Jwt:SecurityKey"]))//拿到token加密密钥.必须是16个字符
            //       };

            //   });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API"));
            }
            //添加认证中间件【必须在授权前面添加】
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            //添加授权中间件
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
