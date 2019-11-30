import * as React from 'react';
import style from "./content.module.less";

export const Content = ({ ...props }) => {
    return (
        <div className={style["content"]}>
            {props.children}
        </div>
    );
}