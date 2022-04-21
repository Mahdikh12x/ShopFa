namespace ShopManagement.Application.Contracts.Order;

public class PaymentMethod
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }


    protected PaymentMethod(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public static List<PaymentMethod?> GetMethods()
    {
        var methods = new List<PaymentMethod?>
        {
            new(TypePayMethod.OnlinePay, "پرداخت آنلاین"
                , "در این روش،میتوانید با استفاده از درگاه پرداخت امن ،به صورت آنلاین خرید خود را انجام دهید و سفارش خود را ثبت کنید(ارسال بین سه تا پنج روز کاری)."),
            new(TypePayMethod.CashPay, "خرید نقدی",
                "در این روش میتوانید سفارش خود را ثبت کنید و پس از ثبت سفارش،کارشناس ما پس از بررسی با شما تماس خواهد گرفت و در صورت صحت خرید شما، سفارش شما ارسال خواهد شد"),
            new(TypePayMethod.InstallmentPay, "خرید اقساط",
                "در این روش شم میتوانید پس از اهراز هویت و ثبت شرایط اقساط خود،سفارش خود را ثبت کرده و از خرید به صورت اقساط بهره مند شوید")
        };
        return methods;
    }

    public static PaymentMethod? GetMethodBy(int paymentMethod)
    {
        return GetMethods().FirstOrDefault(x => x != null && x.Id == paymentMethod);
    }
}