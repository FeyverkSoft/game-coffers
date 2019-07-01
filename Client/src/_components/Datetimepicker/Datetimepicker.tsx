import * as React from 'react';
import Datetime, { DatetimepickerProps } from 'react-datetime';
import { Moment } from "moment";
import { CurrentLang } from '../../_services';
import { BaseInput } from '../Input/BaseInput';



const TypedDatetime = Datetime as React.ComponentType<DatetimepickerProps & {
    renderInput?: (props: any, openCalendar: () => void, closeCalendar: () => void) => void;
}>;

interface IDateTimeInputProps extends React.Props<any> {
    timeout?: number;
    path?: string;
    ignoreInvalidValue?: boolean;
    onChange?: Function;
    value?: string | Date;
    max?: string | Date;
    min?: string | Date;
    isRequired?: boolean;
    regExp?: string;
    [id: string]: any;
}
export class DateTimeInput extends React.Component<IDateTimeInputProps> {
    state: any;
    constructor(props: IDateTimeInputProps) {
        super(props);
        this.state = {
            regExpValid: true,
            isRequedValid: true,
            valid: true,
            value: this.props.value || null,
            typingTimeOut: 0,
            timeout: this.props.timeout || 150,
            path: props.path
        }
    }
    onChangeValue = (val: Moment | string) => {
        let $this = this;
        if ($this.state.typingTimeout) {
            clearTimeout($this.state.typingTimeout);
        }

        $this.setState({
            value: val,
            typingTimeout: setTimeout(function () {
                if ($this.props.onChange) {
                    $this.props.onChange(val, true, $this.state.path);
                }
            }, $this.state.timeout || 100),
            valid: true
        });
    }
    render() {
        const { state } = this;
        return (
            <TypedDatetime
                className="date-picker"
                locale={CurrentLang()}
                onChange={this.onChangeValue}
                data-path={state.path}
                timeFormat="HH:mm:ss"
                dateFormat="DD.MM.YYYY"
                closeOnSelect={true}
                renderInput={(props) =>
                    <BaseInput {...props}
                    />}
            />
        )
    }
}