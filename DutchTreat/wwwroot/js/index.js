//$(document).ready(function () {
    console.log("Hello pluralsight");

    var theForm = $("#theForm");
    theForm.hide();

    var button = $("#buyButton");
    button.on("click", function () {
        console.log("Buying Item");
    });

    //button.addEventListener("click", function () {
    //    alert("Buying Item");
    //});

    var productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("You clicked on " + $(this).text());
    });

    var loginToggle = $("#loginToggle");
    var popupForm = $(".popup-form");

    loginToggle.on("click", function () {
        popupForm.fadeToggle(300);        //If hidden the show the login from, if shown then hide it.
    });

//});
