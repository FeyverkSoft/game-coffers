import * as React from "react";
import style from "./gamerrowview.module.less"
import { Lang, DLang, LangF, GamersListView } from "../../_services";
import { BaseReactComp } from "../BaseReactComponent";
import { IHolded } from "../../core";
import { Spinner } from "../Spinner/Spinner";
import { IF } from "../../_helpers";

interface IGamerRowViewProps extends React.Props<any> {
    gamer: GamersListView & IHolded;
    onAddChar(userId: string): void;
    onDeleteChar(userId: string, char: string): void;
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
                <IF value={gamer.holding}>
                    <Spinner />
                </IF>
                <div className={style['main']}>
                    <div className={style['title']}>
                        {Lang('USER_CHAR_LIST')}
                    </div>
                    <div className={style['char-list']}>
                        {gamer.characters.map(c => <div key={c} className={style['char_name']}>
                            {c}
                            <div
                                className={style['delete']}
                                onClick={() => this.props.onDeleteChar(gamer.id, c)}
                            ></div>
                        </div>)}
                        <span className={style['add']}
                            onClick={() => this.props.onAddChar(gamer.id)}
                        />
                    </div>
                </div>
                <div className={style['user-status']}>
                    <div className={style['title']}>
                        {Lang('USER_ROW_STATUS')}
                    </div>
                    <div className={style['content']}>
                        {DLang('USER_STATUS', gamer.status)}
                    </div>
                </div>
                <div className={style['rank']}>
                    <div className={style['title']}>
                        {Lang('USER_ROW_RANK')}
                    </div>
                    <div className={style['content']}>
                        {DLang('USER_ROLE', gamer.rank)}
                    </div>
                </div>

                <div className={style['penalties']}>
                    <div className={style['title']}>
                        {Lang('USER_ROW_PENALTIES')}
                    </div>
                    <div className={style['content']}>
                        {gamer.penalties.map(p => (
                            <div key={p.id} title={p.description}>
                                {p.amount}
                            </div>
                        ))}
                    </div>
                </div>

                <div className={style['balance']}>
                    <div className={style['title']}>
                        {Lang('USER_ROW_BALANCE')}
                    </div>
                    <div className={`${style['content']} ${gamer.balance < 0 ? style['red'] : ''}`}>
                        {gamer.balance}
                    </div>
                </div>

                <div className={style['loans']}>
                    <div className={style['title']}>
                        {Lang('USER_ROW_LOANS')}
                    </div>
                    <div className={style['content']}>
                        {gamer.loans.map(l => (
                            <div key={l.id}
                                className={`${style['loan']} ${style[l.loanStatus.toLowerCase()]}`}
                                title={l.expiredDate.toString()}
                            >
                                {l.amount}
                            </div>
                        ))}
                    </div>
                </div>
                <div>
                    
                </div>
            </div>
        );
    }
}