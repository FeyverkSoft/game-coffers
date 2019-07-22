import * as React from 'react';
import style from "./input.module.less";
import { BaseInput, IBaseInputProps, IBaseInputState } from '..';
import { IF } from '../../_helpers';

interface InputProps extends IBaseInputProps {
    placeholder?: string;
    label?: string;
}
export class Input extends BaseInput<InputProps, IBaseInputState> {
    constructor(props: InputProps) {
        super(props, {});
    }
    render() {
        return (
            <div className={style['input-wrapper']}>
                <input
                    className={`${style['input']} ${this.state.valid ? style['default'] : style['error']} ${this.state.value ? style['full'] : ''}
                    `}
                    placeholder={this.props.placeholder}
                    onChange={this.onChange}
                    onBlur={this.onBlur}
                    id={this.props.path}
                    type={this.props.type}
                    value={(this.state.value || '').toString()}
                >
                </input>
                <IF value={this.props.label}>
                    <label
                        className={`${style['label']} ${this.state.valid ? style['default'] : style['error']}`}
                        htmlFor={this.props.path}
                    >
                        {this.props.label}
                    </label>
                </IF>
                <span className={style['bar']} />
            </div>
        );
    }
}