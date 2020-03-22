import * as React from "react";
import style from "./character.module.scss";
import { DeleteFilled } from '@ant-design/icons';
import { Private } from "../Private";
import { ICharacter } from "../../_services/gamer/GamersListView";
import { Tooltip, Button } from "antd";
import { Lang } from "../../_services";

export const Characters = ({ ...props }) => <div className={style['chars_wrapper']}>{props.children}</div>;

interface ICharacterProps {
    character: ICharacter;
    onDeleteChar(id: string, userId: string): void;
}

export const Character = React.memo(({ ...props }: ICharacterProps) => {
    return <div key={props.character.name} className={`${style['char']}`}>
        <div
            className={`${style['char_name']} ${props.character.isMain ? style['main'] : ''}`}
            title={props.character.className}
        >
            {props.character.name}
        </div>
        <Private roles={['admin', 'leader', 'officer']}>
            <Tooltip title={Lang('DELETE')}>
                <Button
                    type="link"
                    icon={<DeleteFilled />}
                    onClick={() => props.onDeleteChar(props.character.id, props.character.userId)}
                />
            </Tooltip>
        </Private>
    </div>
});