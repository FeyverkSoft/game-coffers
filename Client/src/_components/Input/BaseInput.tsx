import React from "react";

export interface IBaseInputProps extends React.Props<any> {
    timeout?: number;
    path?: string;
    type?: 'checkbox' | 'hidden' | 'password' | 'text' | 'number' | 'email' | 'datetime' | 'date' | 'url';
    ignoreInvalidValue?: boolean;
    onChange?: Function;
    value?: string | number;
    isRequired?: boolean;
    regExp?: string;
    [id: string]: any;
}
export interface IBaseInputState {
    regExpValid?: boolean;
    isRequedValid?: boolean;
    valid?: boolean;
    value?: string | number | Date | undefined;
    typingTimeOut?: number;
    timeout?: number;
    path?: string | undefined;
}

export class BaseInput<TProps extends IBaseInputProps = {}, TState extends IBaseInputState = {}> extends React.Component<TProps, TState> {
    constructor(props: IBaseInputProps & TProps, state: TState) {
        super(props);
        this.state = {
            ...state,
            regExpValid: true,
            isRequedValid: true,
            valid: true,
            value: this.props.value || '',
            typingTimeOut: 0,
            timeout: this.props.timeout || 150,
            path: props.path,
        };
    }

    onChange = (event?: React.FormEvent<HTMLInputElement>) => {
        if (!event)
            return;
        let $this = this;
        let val = event.currentTarget.value;
        let valid = $this.validate(val);
        if (this.props.ignoreInvalidValue && !valid && val != '')
            return;
        if ($this.state.typingTimeOut) {
            clearTimeout($this.state.typingTimeOut);
        }

        $this.setState({
            value: val,
            typingTimeOut: setTimeout(function () {
                if ($this.props.onChange) {
                    $this.props.onChange(val, valid, $this.state.path);
                }
            }, $this.state.timeout || 100),
            valid: valid
        });
    }

    validate = (value?: string | number) => {
        let valid = true;
        let val: string | number | undefined | Date = value || "";
        val = (val || (this.state || {}).value);

        if (this.props.isRequired && (!val || val == "")) {
            valid = false;
            this.setState({ isRequedValid: false });
        } else {
            this.setState({ isRequedValid: true });
        }
        if (this.props.regExp && val && valid) {
            let reg = new RegExp(String(this.props.regExp || ""), 'gi');

            if (reg.test(val.toString())) {
                this.setState({ regExpValid: true });
                valid = valid && true;
            }
            else {
                valid = false;
                this.setState({ regExpValid: false });
            }
        }
        return valid;
    }

    onBlur = () => {
        this.setState({ valid: this.validate() }, () => {
            if (this.props.onChange && !this.state.valid)
                this.props.onChange(this.state.value, this.state.valid, this.state.path)
        });
    }

    componentWillReceiveProps(props: IBaseInputProps) {
        if (this.state.value != (props.value || '') || this.state.value == '') {
            this.setState({
                value: props.value || '',
                valid: this.validate(props.value)
            });
        }
    }

    render() {
        return <input {...this.props} />
    }
}
