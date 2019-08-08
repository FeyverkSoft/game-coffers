import * as React from "react";
import style from "./gamerrowview.module.less"
import { Lang, DLang, GamersListView, GamerRankList, GamerRank, GamerStatusList, GamerStatus, LangF, CurrentLang } from "../../_services";
import { IHolded } from "../../core";
import { Spinner } from "../Spinner/Spinner";
import { IF } from "../../_helpers";
import { Private, EditableList, Item } from "..";
import { Link } from "react-router-dom";

const UserStatuses = GamerStatusList.map(t => new Item(t, DLang('USER_STATUS', t)));
const GamerRanks = GamerRankList.map(t => new Item(t, DLang('USER_ROLE', t)));

interface IGamerRowViewProps extends React.Props<any> {
    gamer: GamersListView & IHolded;
    isCurrentUser: boolean;
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

interface ICharacterProps {
    name: string;
    className: string;
    onDeleteChar(): void;
}

const Character = React.memo(({ ...props }: ICharacterProps) => {
    return <div key={props.name} className={style['char_name']}>
        <div
            title={props.className}
        >
            {props.name}
        </div>
        <Private roles={['admin', 'leader', 'officer']}>
            <div
                className={style['delete']}
                onClick={() => props.onDeleteChar()}
            />
        </Private>
    </div>
});

/// Плашка с информацией о пользователе
export const GamerRowView = React.memo(({ ...props }: IGamerRowViewProps) => {
    const { gamer, isCurrentUser } = props;
    return (
        <div
            key={gamer.id}
            id={gamer.id}
            className={`${style['user-card']} ${style[gamer.status.toLowerCase()]} ${isCurrentUser ? style['selected'] : ''}`}
        >
            <IF value={gamer.holding}>
                <Spinner />
            </IF>
            <div className={style['main']}>
                <Link
                    className={style['title']}
                    to={`./birthday#${gamer.id}`}>
                    {LangF('USER_CHAR_LIST', gamer.name)}
                </Link>
                <div className={style['char-list']}>
                    {gamer.characters.map(c => <Character
                        onDeleteChar={() => props.onDeleteChar(props.gamer.id, c.name)}
                        {...c}
                    />)}
                    <Private roles={['admin', 'leader', 'officer']}>
                        <span className={style['add']}
                            onClick={() => props.onAddChar(gamer.id)}
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
                        items={UserStatuses}
                        value={gamer.status}
                        onSave={(value) => props.onStatusChange(gamer.id, value as GamerStatus)}
                    />
                </div>
            </div>
            <div className={style['rank']}>
                <div className={style['title']}>
                    {Lang('USER_ROW_RANK')}
                </div>
                <div className={`${style['content']} ${style[gamer.rank.toLowerCase()]}`}>
                    <EditableList
                        roles={['admin', 'leader', 'officer']}
                        items={GamerRanks}
                        value={gamer.rank}
                        onSave={(value) => props.onRankChange(gamer.id, value as GamerRank)}
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
                                onClick={() => props.showPenaltyInfo(p.id, gamer.id)}
                            >
                                {p.amount}
                            </div>
                        )
                    })}
                    <Private roles={['admin', 'leader', 'officer']}>
                        <span className={style['add']}
                            onClick={() => props.onAddPenalty(gamer.id)}
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
                    onClick={() => props.showBalanceInfo(gamer.id)}
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
                                onClick={() => props.showLoanInfo(l.id, gamer.id)}
                            >
                                {l.amount}
                            </div>
                        )
                    })}
                    <Private roles={['admin', 'leader', 'officer']}>
                        <span className={style['add']}
                            onClick={() => props.onAddLoan(gamer.id)}
                        />
                    </Private>
                </div>
            </div>
        </div>
    );
});