import * as React from 'react';
import style from "./header.module.scss";
import { NavLink, NavLinkProps } from 'react-router-dom';

export const HeaderLink = ({ ...props }: NavLinkProps) => {
    return (
        <NavLink
            className={style['link']}
            activeClassName={style['active']}
            {...props}
        >
            {props.children}
        </NavLink>
    );
}