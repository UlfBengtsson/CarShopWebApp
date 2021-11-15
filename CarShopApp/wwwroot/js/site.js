﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getWithAjax(actionUrl) {

    $.get(actionUrl, function (response) {
        console.log("Ajax response:", response);
        document.getElementById("result").innerHTML = response;
    });

}

function postWithAjax(actionUrl, inputId) {
    let inputElement = $("#" + inputId);
    var data = {
        [inputElement.attr("name")] : inputElement.val()
    }
    console.log("inputElement:", inputElement);
    console.log("data:", data);
    // post : url , data to post , what to do when we get the response back
    $.post(actionUrl, data , function (response) {
        console.log("Ajax response:", response);
        document.getElementById("result").innerHTML = response;
    });

}

function getLastCarJSON(actionUrl) {

    $.get(actionUrl, function (response) {
        console.log("JSON response:", response);
        document.getElementById("result").innerHTML = response;
    });

}
/*
function getLastCar(actionUrl) {

    $.get( actionUrl , function (response) {
        console.log("response:", response);
        document.getElementById("result").innerHTML = response;
    });

}

function getCarList(actionUrl) {

    $.get(actionUrl, function (response) {
        console.log("response:", response);
        document.getElementById("result").innerHTML = response;
    });

}
*/
