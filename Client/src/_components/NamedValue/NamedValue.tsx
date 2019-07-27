import * as React from 'react';
import style from "./namedvalue.module.less";

export interface NamedValueProps extends React.Props<any> {
    name: string;
    title?: string;
}

export const NamedValue = ({ ...props }: NamedValueProps) => {
    return <div className={style['named-value']}>
        <label className={style['label']}>
            {props.name}
        </label>
        <div
            className={style['value']}
            title={props.title}>
            {props.children}
        </div>
    </div>
}