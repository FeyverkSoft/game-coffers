import * as React from 'react';
import style from "./header.module.scss";
import { NavLinkProps } from 'react-router-dom';
import { PrivateLink } from '../PrivateNavLink';

export const HeaderLink = ({ ...props }: NavLinkProps) => {
    return (
        <PrivateLink
            className={style['link']}
            {...props}
        >
            {props.children}
        </PrivateLink>
    );
}