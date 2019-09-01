﻿/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

namespace LaraUI {

    type SimpleValueElement = HTMLButtonElement | HTMLSelectElement | HTMLTextAreaElement;

    export class ElementEventValue {
        ElementId: string;
        Value: string;
        Checked: boolean;
    }

    export class ClientEventMessage {
        Values: ElementEventValue[];
        ExtraData: string;
        isEmpty(): boolean {
            return this.Values.length == 0
                && !this.ExtraData;
        }
    }

    export function collectValues(): ClientEventMessage {
        var message = new ClientEventMessage();
        message.Values = [];
        collectType("input", message, collectInput);
        collectType("textarea", message, collectSimpleValue);
        collectType("button", message, collectSimpleValue);
        collectType("select", message, collectSimpleValue);
        collectType("option", message, collectOption);
        return message;
    }

    function collectType(tagName: string,
        message: ClientEventMessage,
        processor: (el: Element, m: ElementEventValue) => void) {
        let list = document.getElementsByTagName(tagName);
        for (let index = 0; index < list.length; index++) {
            let el = list[index];
            if (el.id) {
                let entry = new ElementEventValue();
                entry.ElementId = el.id;
                processor(el, entry);
                message.Values.push(entry);
            }
        }
    }

    function collectInput(el: Element, entry: ElementEventValue): void {
        let input = el as HTMLInputElement;
        entry.Value = input.value;
        entry.Checked = input.checked;
    }

    function collectSimpleValue(el: Element, entry: ElementEventValue): void {
        let input = el as SimpleValueElement;
        entry.Value = input.value;
    }

    function collectOption(el: Element, entry: ElementEventValue): void {
        let option = el as HTMLOptionElement;
        entry.Checked = option.selected;
    }
}