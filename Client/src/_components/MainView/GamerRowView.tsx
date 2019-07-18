import * as React from "react";
import style from "./gamerrowview.module.less"
import { Lang, DLang, LangF, GamersListView } from "../../_services";
import { BaseReactComp } from "../BaseReactComponent";
import { IHolded } from "../../core";

interface IGamerRowViewProps extends React.Props<any> {
    gamer: GamersListView & IHolded;
    [id: string]: any;
}
/// Плашка с информацией о пользователе
export class GamerRowView extends BaseReactComp<IGamerRowViewProps> {

    render() {
        const { gamer } = this.props;
        return (
            <div
                key={gamer.id}
                className={`${style['user-card']} ${style[gamer.status.toLowerCase()]}`}
            >
                <div className={style['user-status']}>
                    {DLang('USER_STATUS', gamer.status)}
                </div>
                <div>
                    {gamer.characters}
                </div>
                <div>
                    {DLang('USER_ROLE', gamer.rank)}
                </div>
                <div>
                    {gamer.penalties}
                </div>
                <div>
                    {gamer.balance}
                </div>
                <div>
                    {gamer.loans}
                </div>
            </div>
        );
    }
}