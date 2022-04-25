using _0_Framework.Application;
using _0_Framework.Application.SmsService;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application;

public class OrderApplication:IOrderApplication
{
    private readonly IAuthHelper  _authHelper;
    private readonly IConfiguration  _configuration;
    private readonly IOrderRepository _orderRepository;
    private readonly IShopInventoryAcl _shopInventoryAcl;
    private readonly ISmsServiceSender _serviceSender;
    private readonly IShopAccountAcl _shopAccountAcl;
    public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IConfiguration configuration, IShopInventoryAcl shopInventoryAcl, ISmsServiceSender serviceSender, IShopAccountAcl shopAccountAcl)
    {
        _orderRepository = orderRepository;
        _authHelper = authHelper;
        _configuration = configuration;
        _shopInventoryAcl = shopInventoryAcl;
        _serviceSender = serviceSender;
        _shopAccountAcl = shopAccountAcl;
    }

    public long PlaceOrder(Cart cart)
    {
        var currentUser = _authHelper.GetAccountId();
        var order = new Order(currentUser, cart.DiscountAmount, cart.PaymentMethod, cart.TotalItemsAmount,
            cart.CartPayAmount);
        if (cart.Items != null)
            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem(item.Id, item.DiscountRate, item.UnitPrice, item.Count);
                order.AddItem(orderItem);
            }
        _orderRepository.Create(order);
        _orderRepository.SaveChanges();
        
        return order.Id;
    }

    public List<OrderItemViewModel> GetItems(long id)
    {
        return _orderRepository.GetItems(id);
    }

    public string PaymentSucceeded(long orderId,long refId)
    {
        var order = _orderRepository.Get(orderId);
        order.PaymentSucceeded(refId);

        var symbol = _configuration.GetSection("Symbol").Value;
        
        var issueTrackingNumber = CodeGenerator.Generate(symbol);
        order.SetIssueTrackingNumber(issueTrackingNumber);

        _shopInventoryAcl.ReduceFromInventory(order.Items);
        _orderRepository.SaveChanges();

        var (number, name) = _shopAccountAcl.GetUserInformation(order.AccountId);
        _serviceSender.Send(number,$"{name}گرامی،سفارش شما با شماره پیگیری{issueTrackingNumber}ثبت شد و به زودی برای شما ارسال میگردد..ممنون از اعتماد شما");

        return issueTrackingNumber;
    }

    public double GetPayAmountBy(long orderId)
    {
        return _orderRepository.GetPayAmountBy(orderId);
    }

    public void Cancel(long id)
    {
        var order = _orderRepository.Get(id);
        order.Cancel();
        _orderRepository.SaveChanges();
    }

    public void Confirm(long id)
    {
        var order=_orderRepository.Get(id);
        order.PaymentSucceeded(0);
        var symbol = _configuration.GetSection("Symbol").Value;
        var issueTrackingNumber = CodeGenerator.Generate(symbol);
        order?.SetIssueTrackingNumber(issueTrackingNumber);
        _orderRepository.SaveChanges();
    }

    public List<OrderViewModel> Search(OrderSearchModel searchModel)
    {
        return _orderRepository.Search(searchModel);
    }
}