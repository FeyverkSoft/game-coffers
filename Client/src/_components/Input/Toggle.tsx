import * as React from 'react';
import style from "./toggle.module.less";
import { BaseInput, IBaseInputProps, IBaseInputState } from '..';
import { getGuid } from '../../_helpers';

interface ToggleProps extends IBaseInputProps {
}
export class Toggle extends BaseInput<ToggleProps, IBaseInputState> {
    constructor(props: ToggleProps) {
        super(props, {});
    }
    render() {
        let id = this.props.id || getGuid();
        let value = this.state.value != 'false';
        return (
            <div className={style['toggle-wrapper']}>
                <input
                    className={`${style['input']} ${this.state.valid ? style['default'] : style['error']}`}
                    placeholder={this.props.placeholder}
                    onChange={this.onChange}
                    checked={value}
                    value={value.toString()}
                    onBlur={this.onBlur}
                    id={id}
                    type="checkbox"
                >
                </input>
                <label htmlFor={id}></label>
            </div>
        );
    }
}