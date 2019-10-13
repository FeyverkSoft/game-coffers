import React from "react";
import style from "./canvas.module.less";
import { Spinner } from "../Spinner/Spinner";
import { IF } from "../../_helpers";
import { SearchInput } from "../Input/SearchInput";

interface IСanvasProps extends React.Props<any> {
    isLoading?: boolean;
    title?: string;
    subChildren?: React.ReactNode;
    type: 'error' | 'default' | 'important' | 'success';
    subType?: 'error' | 'default' | 'important' | 'success' | 'none';
    onSearch?(text: string): void;
}

///Холст
export const CanvasBlock = React.memo(({ ...props }: IСanvasProps) => {
    return <div
        className={`${style['canvas']} ${props.title ? style['titled'] : ''}`}
    >
        <IF value={props.title}>
            <div className={`${style['title']} ${style[props.type]}`}>
                {props.title}
            </div>
        </IF>
        <IF value={props.onSearch}>
            <div className={style['searh']}>
                {<SearchInput
                    onChange={(value: string) => props.onSearch != undefined ? props.onSearch(value) : undefined}
                ></SearchInput>}
            </div>
        </IF>
        <IF value={props.subChildren}>
            <div className={`${style['sub-children']} ${style[props.subType || 'default']}`}>
                {props.subChildren}
            </div>
        </IF>
        {props.children}
        <IF value={props.isLoading}>
            <Spinner />
        </IF>
    </div>;
});