import * as React from "react";
import { Link } from "react-router-dom";
import style from "./breadcrumbs.module.less";
import { IF } from "../../_helpers";

/// крошки
export class Crumbs {
    to: string | URL;
    name: string = "";
    constructor(to: string | URL, name: string | Function) {
        this.to = to;
        if (name) {
            if (typeof (name) === 'function')
                this.name = name();
            else
                this.name = name.toString();
        }
    }
    get Key() {
        return `${this.to}${this.name}`;
    }
}

export const Breadcrumbs = ({ ...props }) => {
    return (
        <div className={style["breadcrumbs"]}>
            <IF value={props.items}>
                <Link to='/' className={style['a']}>></Link>
            </IF>
            <IF value={props.items}>
                {
                    props.items.map((x: Crumbs) => {
                        return <Link className={style['a']} to={x.to} key={x.Key}>&nbsp;{x.name ? x.name : x.to}&nbsp;></Link>
                    })
                }
            </IF>
        </div>
    )
};