import * as React from "react";
import style from "./userbirthdayview.module.less"
import { BlendColor } from "../../_helpers";

interface IUserBirthdayViewProps extends React.Props<any> {
    id: string;
    name: string;
    birthday: string;
    dayCount: number;
}

/// Плашка с информацией о др пользователя 
export const UserBirthdayView = ({ ...props }: IUserBirthdayViewProps) => {
    return <div
        key={props.id}
        className={`${style['user-bd-row']} ${props.dayCount == 0 ? style['current'] : ''}`}
        style={{ background: BlendColor('#36c13955', '#fb6d2955', (100 / 365) * props.dayCount) }}
    >
        <div
            className={style['name']}>
            {props.name}
        </div>
        <div
            className={style['bd']}
        >{props.birthday}
        </div>
        <div
            className={style['count']}>
            {props.dayCount}
        </div>
    </div>
}