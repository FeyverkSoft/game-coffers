import * as React from "react";
import style from "./gamerrowview.module.less"
import { Lang, DLang, LangF, GamersListView, GamerRankList, GamerRank, GamerStatusList, GamerStatus } from "../../_services";
import { BaseReactComp } from "../BaseReactComponent";
import { IHolded } from "../../core";
import { Spinner } from "../Spinner/Spinner";
import { IF } from "../../_helpers";
import { Private, EditableList, Item } from "..";

interface IGamerRowViewProps extends React.Props<any> {
    gamer: GamersListView & IHolded;
    onAddChar(userId: string): void;
    onAddLoan(userId: string): void;
    onAddPenalty(userId: string): void;
    onDeleteChar(userId: string, char: string): void;
    onRankChange(userId: string, rank: GamerRank): void;
    onStatusChange(userId: string, status: GamerStatus): void;
    showPenaltyInfo(penaltyId: string, gamerId: string): void;
    showLoanInfo(loanId: string, gamerId: string): void;
    showBalanceInfo(gamerId: string): void;
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
                            <Private roles={['admin', 'leader', 'officer']}>
                                <div
                                    className={style['delete']}
                                    onClick={() => this.props.onDeleteChar(gamer.id, c)}
                                />
                            </Private>
                        </div>)}
                        <Private roles={['admin', 'leader', 'officer']}>
                            <span className={style['add']}
                                onClick={() => this.props.onAddChar(gamer.id)}
                            />
                        </Private>
                    </div>
                </div>
                <div className={style['user-status']}>
                    <div className={style['title']}>
                        {Lang('USER_ROW_STATUS')}
                    </div>
                    <div className={style['content']}>
                        <EditableList
                            roles={['admin', 'leader', 'officer']}
                            items={GamerStatusList.map(t => new Item(t, DLang('USER_STATUS', t)))}
                            value={gamer.status}
                            onSave={(value) => this.props.onStatusChange(gamer.id, value as GamerStatus)}
                        />
                    </div>
                </div>
                <div className={style['rank']}>
                    <div className={style['title']}>
                        {Lang('USER_ROW_RANK')}
                    </div>
                    <div className={style['content']}>
                        <EditableList
                            roles={['admin', 'leader', 'officer']}
                            items={GamerRankList.map(t => new Item(t, DLang('USER_ROLE', t)))}
                            value={gamer.rank}
                            onSave={(value) => this.props.onRankChange(gamer.id, value as GamerRank)}
                        />
                    </div>
                </div>

                <div className={style['penalties']}>
                    <div className={style['title']}>
                        {Lang('USER_ROW_PENALTIES')}
                    </div>
                    <div className={style['content']}>
                        {Object.keys(gamer.penalties).map(_ => {
                            const p = gamer.penalties[_];
                            return (
                                <div key={p.id}
                                    title={p.description}
                                    className={`${style['penalty']} ${style[p.penaltyStatus.toLowerCase()]}`}
                                    onClick={() => this.props.showPenaltyInfo(p.id, gamer.id)}
                                >
                                    {p.amount}
                                </div>
                            )
                        })}
                        <Private roles={['admin', 'leader', 'officer']}>
                            <span className={style['add']}
                                onClick={() => this.props.onAddPenalty(gamer.id)}
                            />
                        </Private>
                    </div>
                </div>

                <div className={style['balance']}>
                    <div className={style['title']}>
                        {Lang('USER_ROW_BALANCE')}
                    </div>
                    <div
                        className={`${style['content']} ${gamer.balance < 0 ? style['red'] : ''}`}
                        onClick={() => this.props.showBalanceInfo(gamer.id)}
                    >
                        {gamer.balance}
                    </div>
                </div>

                <div className={style['loans']}>
                    <div className={style['title']}>
                        {Lang('USER_ROW_LOANS')}
                    </div>
                    <div className={style['content']}>
                        {Object.keys(gamer.loans).map(_ => {
                            const l = gamer.loans[_];
                            return (
                                <div key={l.id}
                                    className={`${style['loan']} ${style[l.loanStatus.toLowerCase()]}`}
                                    title={`${l.expiredDate.toString()} ${l.description}`}
                                    onClick={() => this.props.showLoanInfo(l.id, gamer.id)}
                                >
                                    {l.amount}
                                </div>
                            )
                        })}
                        <Private roles={['admin', 'leader', 'officer']}>
                            <span className={style['add']}
                                onClick={() => this.props.onAddLoan(gamer.id)}
                            />
                        </Private>
                    </div>
                </div>
                <div>
                </div>
            </div>
        );
    }
}