import * as React from "react";
import style from "./character.module.scss";
import { DeleteFilled, PlusCircleFilled } from '@ant-design/icons';
import { Private } from "../Private";
import { ICharacter } from "../../_services/gamer/GamersListView";
import { Tooltip, Button } from "antd";
import { Lang } from "../../_services";
import { IDictionary } from "../../core";

interface ICharacters {
    userId: string;
    characters: IDictionary<ICharacter>;
    toggleAddCharModal(userId: string): void;
    onDeleteChar(id: string, userId: string): void;
}
export const Characters = ({ ...props }: ICharacters) => <div className={style['chars_wrapper']}>
    {
        Object.keys(props.characters).filter(_ => _ !== 'holding').map(_ => <Character
            key={props.characters[_].id}
            character={props.characters[_]}
            onDeleteChar={props.onDeleteChar}
        />)
    }
    <Private roles={['admin', 'leader', 'officer']}>
        <Tooltip title={Lang('ADD')}>
            <Button
                type="link"
                className={style['add']}
                icon={<PlusCircleFilled />}
                onClick={() => props.toggleAddCharModal(props.userId)}
            />
        </Tooltip>
    </Private>
</div>;

interface ICharacterProps {
    character: ICharacter;
    onDeleteChar(id: string, userId: string): void;
}

export const Character = React.memo(({ ...props }: ICharacterProps) => {
    return <div key={props.character.name} className={`${style['char']}`}>
        <Tooltip
            className={`${style['char_name']} ${props.character.isMain ? style['main'] : ''}`}
            title={props.character.className}>
            {props.character.name}
        </Tooltip>
        <Private roles={['admin', 'leader', 'officer']}>
            <Tooltip title={Lang('DELETE_CHAR')}>
                <Button
                    type="link"
                    className={style['remove']}
                    icon={<DeleteFilled />}
                    onClick={() => props.onDeleteChar(props.character.id, props.character.userId)}
                />
            </Tooltip>
        </Private>
    </div>
});