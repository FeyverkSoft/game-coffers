import * as React from 'react';
import style from "./header.module.scss";

export const Header = ({ ...props }) => {
    return (
        <header className={style["header"]}>
            {props.children}
        </header>
    );
}

export const Logo = ({ ...props }) => {
    return <div className={style["logo"]}>
        {<div className={style["title"]}><b>Games</b>Treasury<sup>β</sup></div>}
        <div className={style["small-title"]}><b>G</b><b>T</b><sup>β</sup></div>
    </div>
}
