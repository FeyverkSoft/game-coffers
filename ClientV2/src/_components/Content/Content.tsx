import * as React from 'react';
import style from "./content.module.scss";

export const Content = ({ ...props }) => {
    return (
        <div className={style["content"]}>
            {props.children}
        </div>
    );
}