// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: IAPService.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace CSharp.Protobuf.Service {
  /// <summary>
  /// IAP 서비스 정의
  /// </summary>
  public static partial class IAPService
  {
    static readonly string __ServiceName = "iap.purchase.IAPService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::CSharp.Protobuf.Purchase.PurchaseRequest> __Marshaller_iap_purchase_PurchaseRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::CSharp.Protobuf.Purchase.PurchaseRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::CSharp.Protobuf.Purchase.PurchaseResponse> __Marshaller_iap_purchase_PurchaseResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::CSharp.Protobuf.Purchase.PurchaseResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::CSharp.Protobuf.Purchase.PurchaseHistoryRequest> __Marshaller_iap_purchase_PurchaseHistoryRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::CSharp.Protobuf.Purchase.PurchaseHistoryRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::CSharp.Protobuf.Purchase.PurchaseHistoryResponse> __Marshaller_iap_purchase_PurchaseHistoryResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::CSharp.Protobuf.Purchase.PurchaseHistoryResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::CSharp.Protobuf.Purchase.PurchaseRequest, global::CSharp.Protobuf.Purchase.PurchaseResponse> __Method_MakePurchase = new grpc::Method<global::CSharp.Protobuf.Purchase.PurchaseRequest, global::CSharp.Protobuf.Purchase.PurchaseResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "MakePurchase",
        __Marshaller_iap_purchase_PurchaseRequest,
        __Marshaller_iap_purchase_PurchaseResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::CSharp.Protobuf.Purchase.PurchaseHistoryRequest, global::CSharp.Protobuf.Purchase.PurchaseHistoryResponse> __Method_GetPurchaseHistory = new grpc::Method<global::CSharp.Protobuf.Purchase.PurchaseHistoryRequest, global::CSharp.Protobuf.Purchase.PurchaseHistoryResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetPurchaseHistory",
        __Marshaller_iap_purchase_PurchaseHistoryRequest,
        __Marshaller_iap_purchase_PurchaseHistoryResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::CSharp.Protobuf.Service.IAPServiceReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of IAPService</summary>
    [grpc::BindServiceMethod(typeof(IAPService), "BindService")]
    public abstract partial class IAPServiceBase
    {
      /// <summary>
      /// 단일 상품 구매
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::CSharp.Protobuf.Purchase.PurchaseResponse> MakePurchase(global::CSharp.Protobuf.Purchase.PurchaseRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// 구매 이력 조회
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::CSharp.Protobuf.Purchase.PurchaseHistoryResponse> GetPurchaseHistory(global::CSharp.Protobuf.Purchase.PurchaseHistoryRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for IAPService</summary>
    public partial class IAPServiceClient : grpc::ClientBase<IAPServiceClient>
    {
      /// <summary>Creates a new client for IAPService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public IAPServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for IAPService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public IAPServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected IAPServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected IAPServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// 단일 상품 구매
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::CSharp.Protobuf.Purchase.PurchaseResponse MakePurchase(global::CSharp.Protobuf.Purchase.PurchaseRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return MakePurchase(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// 단일 상품 구매
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::CSharp.Protobuf.Purchase.PurchaseResponse MakePurchase(global::CSharp.Protobuf.Purchase.PurchaseRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_MakePurchase, null, options, request);
      }
      /// <summary>
      /// 단일 상품 구매
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::CSharp.Protobuf.Purchase.PurchaseResponse> MakePurchaseAsync(global::CSharp.Protobuf.Purchase.PurchaseRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return MakePurchaseAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// 단일 상품 구매
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::CSharp.Protobuf.Purchase.PurchaseResponse> MakePurchaseAsync(global::CSharp.Protobuf.Purchase.PurchaseRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_MakePurchase, null, options, request);
      }
      /// <summary>
      /// 구매 이력 조회
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::CSharp.Protobuf.Purchase.PurchaseHistoryResponse GetPurchaseHistory(global::CSharp.Protobuf.Purchase.PurchaseHistoryRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetPurchaseHistory(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// 구매 이력 조회
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::CSharp.Protobuf.Purchase.PurchaseHistoryResponse GetPurchaseHistory(global::CSharp.Protobuf.Purchase.PurchaseHistoryRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetPurchaseHistory, null, options, request);
      }
      /// <summary>
      /// 구매 이력 조회
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::CSharp.Protobuf.Purchase.PurchaseHistoryResponse> GetPurchaseHistoryAsync(global::CSharp.Protobuf.Purchase.PurchaseHistoryRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetPurchaseHistoryAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// 구매 이력 조회
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::CSharp.Protobuf.Purchase.PurchaseHistoryResponse> GetPurchaseHistoryAsync(global::CSharp.Protobuf.Purchase.PurchaseHistoryRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetPurchaseHistory, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override IAPServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new IAPServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(IAPServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_MakePurchase, serviceImpl.MakePurchase)
          .AddMethod(__Method_GetPurchaseHistory, serviceImpl.GetPurchaseHistory).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, IAPServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_MakePurchase, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::CSharp.Protobuf.Purchase.PurchaseRequest, global::CSharp.Protobuf.Purchase.PurchaseResponse>(serviceImpl.MakePurchase));
      serviceBinder.AddMethod(__Method_GetPurchaseHistory, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::CSharp.Protobuf.Purchase.PurchaseHistoryRequest, global::CSharp.Protobuf.Purchase.PurchaseHistoryResponse>(serviceImpl.GetPurchaseHistory));
    }

  }
}
#endregion