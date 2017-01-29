"use strict";

declare var productId: string;
declare var cartApiUrl: string;

function addToCart(productId: string): void {
   
    $.ajax
    ({
        type: "POST",       
        url: cartApiUrl,
        dataType: "json",
        contentType: "application/json",
        async: false,
    
        data: `{"command": "add", "productId" : "${productId}"}`,
        success() {
            logSuccess(`Added to cart!`);
        }
    });
}




$(() => {
    $("#addToCartButton").click(() => {
        addToCart(productId);


    });
});