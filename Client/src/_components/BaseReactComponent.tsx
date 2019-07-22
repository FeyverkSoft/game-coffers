import * as React from "react";
import { getObject, Dictionary } from "../core";

export interface BaseReactCompState {
    invalid?: Dictionary<boolean>;
}

export interface IStatedField<T extends {} | undefined = any> {
    invalid?: boolean;
    value: T;
}

export class BaseReactComp<TProps = {}, TState = {}> extends React.Component<TProps, TState> {
    onInputVal = <T extends {} = any>(val: T, valid: boolean, path: string): void => {
        if (path) {
            let obj = getObject(this.state || {}, path + '.value', val);
            obj = getObject(this.state || {}, path + '.isValid', valid);
            this.setState(obj);
        }
    }

    onInput = <T extends {} = any>(val: T, valid: boolean, path: string): void => {
        if (path) {
            let obj = getObject(this.state || {}, path, val);
            this.setState(obj);
        }
    }

    isValidForm = <T extends any = any>(form: T): boolean => {
        for (let propertyName in form) {
            if (!form[propertyName].isValid && (form[propertyName].isValid != undefined && form[propertyName].value != undefined))
                return false;
        };
        return true;
    }
}
export class BaseReactStateComp<TProps = {}, TState = {}> extends React.Component<TProps, TState & BaseReactCompState> {
    constructor(props: any) {
        super(props);
    }
    onInputVal = <T extends {} = any>(val: T, valid: boolean, path: string): void => {
        if (typeof (valid) === typeof ('') && !path)
            path = valid.toString();
        if (path) {
            let obj = getObject(this.state || {}, path, val);
            if (!obj.invalid)
                obj.invalid = {};
            if (!valid && valid != undefined) {
                obj.invalid[path] = true;
            } else {
                if (obj.invalid[path] != undefined)
                    delete obj.invalid[path];
            }
            this.setState(obj);
        }
    }

    isInValid = (): boolean => {
        return (!!this.state.invalid) && Object.keys(this.state.invalid).length > 0;
    }
}