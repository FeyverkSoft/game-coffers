import * as React from 'react';
import style from "./card.module.scss";
import { Card as AntdCard } from 'antd';
import { CardProps } from 'antd/lib/card';

export const Card = ({ ...props }: CardProps) => <AntdCard
    {...props}
    className={`${style['card']} ${props.className}`}>
    {props.children}
</AntdCard>;