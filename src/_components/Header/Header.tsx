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

export class Logo extends React.Component {
    render() {
        return <div className={style["logo"]}>
            {/*<div className={style["title"]}><b>Daddy</b>And<b>Co</b><sup>α</sup></div>*/}
            <div className={style["small"]}><b>D</b>&<b>Co</b><sup>α</sup></div>
            <div className={style["small-title"]}><b>D</b>&<b>Co</b><sup>α</sup></div>
        </div>
    }
}