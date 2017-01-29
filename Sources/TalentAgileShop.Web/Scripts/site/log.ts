"use strict";

function addLogPanel(message: string, panelType: string):void {

    $("#logContent").append(`<div class='alert alert-${panelType} alert-dismissible' role='alert'><button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>${message}</div>`);
}


function logInfo(message: string):void  {
    addLogPanel(message,"info");
}

function logWarning(message: string):void  {
    addLogPanel(message, "warning");
}

function logSuccess(message: string): void {
    addLogPanel(message, "success");
}

function logDanger(message: string):void {
    addLogPanel(message, "danger");
}
