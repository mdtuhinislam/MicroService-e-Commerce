using AutoMapper;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Discount.Grpc.Repository;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICouponRepository _couponRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;
        public DiscountService(ICouponRepository couponRepository,
            ILogger<DiscountService> logger,
            IMapper mapper)
        {
               _couponRepository = couponRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetDiscountAsync(request.ProductId);

            if(coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "No discount found"));
            }
            _logger.LogInformation($"Discount retrive for Product Name : {coupon.ProductName}, Amount: {coupon.Amount}");

            return _mapper.Map<CouponRequest>(coupon); 
        }

        public override async Task<CouponRequest> CreateDiscount(CouponRequest request, ServerCallContext context)
        {
            if(request is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Request is empty"));
            var coupon = _mapper.Map<Coupon>(request);
            bool isSuccess = await _couponRepository.CreateDiscountAsync(coupon);
            if (isSuccess)
            {
                _logger.LogInformation($"Discount created successfully Name : {coupon.ProductName}, Amount: {coupon.Amount}");
                return _mapper.Map<CouponRequest>(await _couponRepository.GetDiscountAsync(request.ProductId));
                
            }
            return new CouponRequest();
        }

        public override async Task<CouponRequest> UpdateDiscount(CouponRequest request, ServerCallContext context)
        {
            if (request is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Request is empty"));
            var coupon = _mapper.Map<Coupon>(request);
            bool isSuccess = await _couponRepository.UpdateDiscountAsync(coupon);
            if (isSuccess)
            {
                _logger.LogInformation($"Discount updated successfully Name : {coupon.ProductName}, Amount: {coupon.Amount}");
                return _mapper.Map<CouponRequest>(await _couponRepository.GetDiscountAsync(request.ProductId));

            }
            return new CouponRequest();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var isDeleted = await _couponRepository.DeleteDiscountAsync(request.ProductId);

            if (!isDeleted)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "No discount found"));
            }
            _logger.LogInformation($"Discount deleted");

            return new DeleteDiscountResponse { Success = isDeleted};
        }
    }
}
