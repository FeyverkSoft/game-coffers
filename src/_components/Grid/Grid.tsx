import * as React from 'react';
import style from "./grid.module.less";

export interface IGridProps extends React.Props<any> {
    align?: "center" | "left" | "right";
    [id: string]: any;
}

export const Grid = ({ ...props }: IGridProps) => {
    return (<div className={`${style['grid']} ${style[props.align || "left"]}`}>
        {props.children}
    </div>);
}

export const Col1 = ({ ...props }) => {
    return (<div className={style['col-1']}>
        {props.children}
    </div>);
}

export const Col2 = ({ ...props }) => {
    return (<div className={style['col-2']}>
        {props.children}
    </div>);
}
export const Col3 = ({ ...props }) => {
    return (<div className={style['col-3']}>
        {props.children}
    </div>);
}

export const Col2_3 = ({ ...props }) => {
    return (<div className={style['col-2_3']}>
        {props.children}
    </div>);
}

export const Col4 = ({ ...props }) => {
    return (<div className={style['col-4']}>
        {props.children}
    </div>);
}