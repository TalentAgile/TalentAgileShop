"use strict";
// ReSharper disable InconsistentNaming
interface IPrice {
    DeliveryCost: number;
    ProductCost: number;
    InvalidDiscountCode: boolean;
}

interface ICart {

    Products: ICartProduct[];

}


interface ICartProduct {
    Id: string;
    Count: number;

}
// ReSharper restore InconsistentNaming

declare var cartApiUrl: string;

function formatMoney(n: number): string {
    return n.toLocaleString("en-US", { minimumFractionDigits: 2, useGrouping: false });
}

function updatePrice(price: IPrice) {
    if (price.InvalidDiscountCode) {
        $("#deliveryCost").text("######");
        $("#productCost").text("######");
        $("#total").text("######");
    } else {
        const total = price.ProductCost + price.DeliveryCost;
        $("#deliveryCost").text(formatMoney(price.DeliveryCost));
        $("#productCost").text(formatMoney(price.ProductCost));
        $("#total").text(formatMoney(total));
    }
}

function updateItem(cartLine: JQuery, productId: string, cart: ICart) {
    const productData = cart.Products.filter(p => p.Id === productId);

    //if (productData.length === 0) {        
    //    $(cartLine).remove();
    //} else {
    $(cartLine).children(".count").text(productData[0].Count);
    //}
}


function updateCartPrice() {
    var discountCode = $("#discountCode").val();
    $.ajax
        ({
            type: "Get",
            url: `${cartApiUrl}/price?discountCode=${discountCode}`,
            dataType: "json",
            async: false,
            success(priceData: IPrice) {
                if (priceData.InvalidDiscountCode) {
                    logDanger(`"${discountCode}" is not a valid discount code`);
                }
                updatePrice(priceData);

            }
        });
}

function changeCart(cartLine: JQuery, command: string): void {
    var productId = $(cartLine).data("productid");

    $.ajax
        ({
            type: "POST",
            url: cartApiUrl,
            dataType: "json",
            contentType: "application/json",
            async: false,
            //json object to sent to the authentication url
            data: `{"command": "${command}", "productId" : "${productId}"}`,
            success(cart: ICart) {
                updateItem(cartLine, productId, cart);
                updateCartPrice();

            }
        });
}

$(() => {
    $("a.addbtn").click(evt => {
        var cartLine = $(evt.currentTarget).parent().parent();
        changeCart(cartLine, "add");
    });
    $("a.removebtn").click(evt => {
        var cartLine = $(evt.currentTarget).parent().parent();
        changeCart(cartLine, "remove");
    });
    $("#changeCodeBtn").click(evt => {
        updateCartPrice();
    });
});