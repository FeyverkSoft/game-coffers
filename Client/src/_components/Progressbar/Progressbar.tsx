
import * as React from 'react';
import { BlendColor } from "../../_helpers";
import style from "./progressbar.module.less";

interface IProgressbarProps {
    className?: string;
    percentage?: number;
    startColor?: string;
    endColor?: string;
    textForPercentage(percentage?: number | string): string;
}
export const Progressbar = ({ ...props }: IProgressbarProps) => {
    return (
        <div className={`${style["progressbar"]} ${props.className ? ' ' + style["props.className"] : ''}`}>
            <div className={style['progressbar-bar']}
                style={{
                    width: props.percentage + '%',
                    backgroundColor: BlendColor(props.startColor || props.endColor, props.endColor || props.startColor, props.percentage)
                }}></div>
            <div className={style["progress-text"]}>{props.textForPercentage(props.percentage)}</div>
        </div>
    );
}