﻿/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

/*
Lara is a server-side DOM rendering library for C#
This file is the client runtime for Lara.
https://laraui.com
*/

namespace LaraUI {

    let documentId: string;

    export function initialize(id: string): void {
        documentId = id;
        window.addEventListener("unload", terminate, false);
    }
    
    function terminate(): void {
        let url = "/_discard?doc=" + documentId;
        navigator.sendBeacon(url);
    }

    export function plug(el: Element, eventName: string): void {
        let url = getEventUrl(el, eventName);
        sendAjax(url);
    }

    function getEventUrl(el: Element, eventName: string): string {
        return "/_event?doc=" + documentId
            + "&el=" + el.id
            + "&ev=" + eventName;
    }

    function sendAjax(url: string): void {
        let ajax = new XMLHttpRequest();
        ajax.onreadystatechange = function () {
            if (this.readyState == 4) {
                if (this.status == 200) {
                    processAjaxResult(this);
                } else {
                    processAjaxError(this);
                }
            }
        };
        let message = collectValues();
        ajax.open("POST", url, true);
        if (message.isEmpty()) {
            ajax.send();
        } else {
            ajax.send(JSON.stringify(message));
        }
    }

    function processAjaxResult(ajax: XMLHttpRequest): void {
        let result = JSON.parse(ajax.responseText) as EventResult;
        if (result.List) {
            processResult(result.List);
        }
    }

    function processAjaxError(ajax: XMLHttpRequest): void {
        if (ajax.responseText) {
            document.write(ajax.responseText);
        } else {
            console.log("Internal Server Error on AJAX call. Detailed exception information on the client is turned off.")
        }
    }
}