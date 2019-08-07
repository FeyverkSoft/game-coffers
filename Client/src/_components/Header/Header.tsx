import * as React from 'react';
import style from "./header.module.less";

export class Header extends React.Component {
    render() {
        let $this = this;
        return (
            <header className={style["header"]}>
                {$this.props.children}
            </header>
        );
    }
}

export class Nav extends React.Component {
    render() {
        let $this = this;
        return (
            <nav >
                <ul>
                    {$this.props.children}
                </ul>
            </nav>
        );
    }
}

export const Logo = ({...props}) => {
    return <div className={style["logo"]}>
        {<div className={style["title"]}><b>Games</b>Treasury<sup>α</sup></div>}
        <div className={style["small-title"]}><b>G</b><b>T</b><sup>α</sup></div>
    </div>
}
